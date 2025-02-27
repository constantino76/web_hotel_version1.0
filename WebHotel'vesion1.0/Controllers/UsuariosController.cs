﻿using AppLogin.Data;
using AppLogin.Logica;
using DocumentFormat.OpenXml.Office2019.Drawing.Model3D;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using WebHotel_vesion1._0.Models;
using WebHotel_vesion1._0.Models.ViewModel;
using WebHotel_vesion1._0.Repositories.Interfaces;

namespace WebHotel_vesion1._0.Controllers
{
    [Authorize]
   
    public class EmpleadosController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IUsuario _iusuario;
        private readonly IRol _irol;
        private readonly IUsuarioRol _usuarioRol;    
        public EmpleadosController(IWebHostEnvironment hostingEnvironment, IUsuario isuario,IRol irol, IUsuarioRol usuarioRol) {

            _hostingEnvironment = hostingEnvironment;
            _iusuario = isuario;
            _irol = irol;
            _usuarioRol = usuarioRol;
        }


        // GET: EmpleadosController
        [Authorize(Roles ="Administrador ,Empleado")]
        public async Task<ActionResult> listar_Empleados() {
            List<Usuario> listempleados = new List<Usuario>();


            listempleados = await _iusuario .getAll();


            return View(listempleados);
        }

        // GET: EmpleadosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EmpleadosController/Create
        [Authorize(Roles = "Administrador,Empleado")]
     
        public async Task<IActionResult> Create()
        {
            var usuariorol = new UsuarioViewModel {


                Roles = await  _irol.GetRols() 

        };


            return View(usuariorol);
        }





        // POST: EmpleadosController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsuarioViewModel user,IFormFile Imagen)
        {
            Usuario usuario =null;
            IFormFile file = null;
            //     Success = 1,
            // DuplicateEmailOrPassword = 2,
            //ErrorConexionString = 3,
            // UpdateError = 4,
            // DeleteSuccess = 5,
            // DeleteError = 6

            // initialize properties

            if (user!=null && Imagen != null) {
                file = Imagen;

                var profile = Path.Combine(_hostingEnvironment.WebRootPath, "profile");

                if (!Directory.Exists(profile)) {
                
                Directory.CreateDirectory(profile); 
                
                }
                //obtenemos el numero de archivos en el directorio 
                int cont=Directory.GetFiles(profile).Length;
                string filename = $"{cont:D2}.jpeg";// asignamos el nombre
                var filepath = Path.Combine(profile, filename);//ruta donde se guardara la foto

                using (var  filestream= new FileStream(filepath,FileMode.Create)) {
                await Imagen.CopyToAsync(filestream);   
                
                }



               usuario  = new Usuario
                {

                    IdUsuario = user.IdUsuario,
                    NombreCompleto = user.NombreCompleto,
                    Correo = user.Correo,
                    Clave = user.Clave,
                    ImageUrl = Path.Combine("profile", filename).Replace("\\", "/")

               };
            }

            var status= _iusuario.Create(usuario);
            int valor = (int) await status;

            TempData["Status"] = (int)await status;

            //initialization object usuariorol



            if (valor == 1)
            {

                var userrol = new UsuarioRol
                {
                    IdUsuario = user.IdUsuario,
                    IdRol = user.IdRol,  
                };
                var status_ = await _usuarioRol.InsertUserRol(userrol);

            }

            

            return RedirectToAction("Create");


        }

        // GET: EmpleadosController/Edit/5
        // metodo edit para seleccionar mediante el id usuario 
        public async  Task< ActionResult> Edit(string  id)

        {
            var id_ = id;
            var user = await _iusuario.getUser(id);
            var usuariorol = new UsuarioViewModel
            {
                IdUsuario = user.IdUsuario,
                NombreCompleto = user.NombreCompleto, 
                Correo = user.Correo, 
                Clave=user.Clave,
                imageUrl=user.ImageUrl,
                Roles = await _irol.GetRols()

            };
            return View(usuariorol);
        }

        // POST: EmpleadosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<IActionResult> Edit( UsuarioViewModel userviewmodel, IFormFile Imagen)
        {

            if (Imagen == null) {
                return Content("Se debe    seleccionar una imagen ");
            }
             
            if (userviewmodel == null) {


                return Content("El modelo no puede ser nulo");

            }

           Usuario olduser = await _iusuario.getUser(userviewmodel.IdUsuario);
            if (olduser == null) {

                return Content("No se encontro el registro en el sistema");
            
            }

            try
            {  // obtenemos la ruta de la carpeta profile
                var profile = Path.Combine(_hostingEnvironment.WebRootPath, "profile");
                // obtenemos la ruta de la vieja imagen 
                var oldImagePath = Path.Combine(_hostingEnvironment.WebRootPath, olduser.ImageUrl);

                if (System.IO.File.Exists(oldImagePath)) { 
                
                System.IO.File.Delete(oldImagePath); 
                }

                oldImagePath = oldImagePath.Replace("\\", "/");

                int cont =Directory.GetFiles(profile).Length; 
                
                if (cont >=1) {
                    cont = cont += 1; 
                
                
                }
                //asignamos el nombre al nuevo archivo
                string filename = $"{cont:D2}.jpeg";

                var filePath = Path.Combine(profile, filename);

                // Guardar la nueva imagen
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await Imagen.CopyToAsync(fileStream);
                }


              olduser.ImageUrl=  Path.Combine("profile", filename).Replace("\\", "/");
                //  carga de las propiedades con los datos del modelo 
                Usuario user = new Usuario
                {

                    IdUsuario = userviewmodel.IdUsuario,
                    NombreCompleto = userviewmodel.NombreCompleto,
                    Correo = userviewmodel.Correo,
                    Clave = userviewmodel.Clave,
                    ImageUrl = olduser.ImageUrl

                };
                //llamada para el metodo de actualizar usuario
                bool result = await _iusuario.Update(user);
                if (result) { 

                    // llamada al metodo para almacenar el IdUsuario y el IdRol 
                    UsuarioRol usuariorol = new UsuarioRol
                    {

                        IdUsuario = userviewmodel.IdUsuario,
                        IdRol = userviewmodel.IdRol,
                    };

                
                    _usuarioRol.UpdateUserRol(usuariorol);
            }
            }
            catch
            {
                throw new Exception("Ha ocurrido un error ");
                return View();
            }
            
            var roles = new UsuarioViewModel { // almacenamos los roles en la vista para que en caso de retorno no de null a momento de listar los roles 
                Roles = await  _irol.GetRols() 
            
            
            };
            return RedirectToAction("listar_Empleados");
        }

        // GET: EmpleadosController/Delete/5
        [Authorize(Roles="Administrador")]
        public async Task<ActionResult> Delete(string  id)
        {
            var user = await _iusuario.getUser(id);
            return View(user);
        }

        // POST: EmpleadosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteUser(string IdUsuario)
        {
           

              bool result =await   _iusuario.Delete(IdUsuario);

                return RedirectToAction("listar_Empleados");
            
         
           
            return View();
        }
        [Authorize(Roles ="Administrador,Empleado")]
        public async Task<IActionResult> MostrarDatosUsuario(string id) {

            if (String.IsNullOrEmpty(id)) {

                return Content("El id no puede estar vacio");
            
            }
            Usuario usuario = await _iusuario.getUser(id);

            //UsuarioViewModel userviewmodel = new UsuarioViewModel
            //{


            //    IdUsuario = usuario.IdUsuario,
            //    NombreCompleto = usuario.NombreCompleto,
            //    Correo = usuario.Correo,
            //    Roles = await _irol.GetRols()

            //};
            return View(usuario);
        
        }
    }
}
