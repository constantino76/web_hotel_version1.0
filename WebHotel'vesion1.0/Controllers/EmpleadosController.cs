using AppLogin.Data;
using AppLogin.Logica;
using DocumentFormat.OpenXml.Office2019.Drawing.Model3D;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebHotel_vesion1._0.Models;
using WebHotel_vesion1._0.Models.ViewModel;
using WebHotel_vesion1._0.Repositories.Interfaces;

namespace WebHotel_vesion1._0.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class EmpleadosController : Controller
    {
        private readonly IUsuario _iusuario;
        private readonly IRol _irol;
        private readonly IUsuarioRol _usuarioRol;    
        public EmpleadosController(IUsuario isuario,IRol irol, IUsuarioRol usuarioRol) {


            _iusuario = isuario;
            _irol = irol;
            _usuarioRol = usuarioRol;
        }


        // GET: EmpleadosController
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
        public async Task<IActionResult> Create(UsuarioViewModel user)
        {

           
            // initialize properties


            Usuario usuario = new Usuario {

                IdUsuario = user.IdUsuario,
                NombreCompleto = user.NombreCompleto,
                Correo = user.Correo,
                Clave = user.Clave,

            };

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
                Roles = await _irol.GetRols()

            };
            return View(usuariorol);
        }

        // POST: EmpleadosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit( UsuarioViewModel userviewmodel)
        {
             
            if (userviewmodel == null) {


                return Content("El modelo no puede ser nulo");

            }

            try
            { Usuario user= new Usuario {
            
            IdUsuario=userviewmodel.IdUsuario,
            NombreCompleto=userviewmodel.NombreCompleto,
            Correo=userviewmodel.Correo,   
            Clave=userviewmodel.Clave,  
            
            
            };
                //llamada para el metodo de actualizar usuario
             _iusuario.Update(user);

                // llamada al metodo para almacenar el IdUsuario y el IdRol 
                UsuarioRol usuariorol = new UsuarioRol {

                    IdUsuario = userviewmodel.IdUsuario,
               IdRol=userviewmodel.IdRol,   
                };

                _usuarioRol.InsertUserRol(usuariorol);
            }
            catch
            {
                throw new Exception("Ha ocurrido un error ");
                return View();
            }
            return View();
        }

        // GET: EmpleadosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmpleadosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
