using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebHotel_vesion1._0.Models;

using System.IO;
using WebHotel_vesion1._0.Models.ViewModel;
using WebHotel_vesion1._0.Repositories.Interfaces;

namespace WebHotel_vesion1._0.Controllers
{
    
    [Authorize]
    public class HabitacionesController : Controller
    {
        private readonly IHabitacion _ihabitacion;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public HabitacionesController(IWebHostEnvironment hostingEnvironment, IHabitacion ihabitacion) {



            _hostingEnvironment = hostingEnvironment;
            _ihabitacion= ihabitacion;  



        }
        // GET: HabitacionesController
        [Authorize(Roles = "Administrador,Empleado")]
        public async Task<IActionResult> listarHabitaciones() {
            
            var habitaciones=_ihabitacion.ListarHabitaciones();  
            return View(await habitaciones);
        
        }

        // GET: HabitacionesController/Details/5
        [Authorize(Roles ="Administrador,Empleado")]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HabitacionesController/Create
        [Authorize(Roles ="Administrador")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: HabitacionesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<ActionResult> Create(HabitacionViewModel habitacion,IFormFile Imagen)
        {
            IFormFile file =null;

            try 
            {
                if (habitacion != null && Imagen != null) {






                    file = Imagen;

                    var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");


                    if (!Directory.Exists(uploads)) {


                        Directory.CreateDirectory(uploads);
                    
                    }
                    int cont =Directory.GetFiles(uploads).Length;
                    // cambiamos el nombre de la imagen 
                    String filename= $"{ cont:D2}.jpeg";


                    //var filePath = Path.Combine(uploads, file.FileName);
                    //combinamos la ruta con el nuevo nombre
                    var filePath=Path.Combine(uploads, filename);


                    //guardamos el archivo
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    Habitacion _habitacion = new Habitacion()
                    {


                        Id = habitacion.Id,

                        Numero = habitacion.Numero,
                        Descripcion = habitacion.Descripcion,
                        Tipo = habitacion.Tipo,
                        PrecioPorNoche = habitacion.PrecioPorNoche,
                        imageUrl = Path.Combine("uploads", filename).Replace("\\", "/")

                };
                   _ihabitacion.CrearHabitacion(_habitacion);   

                }
            }
            catch
            {
                return View();
            }


            return View();
        }

        // GET: HabitacionesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HabitacionesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: HabitacionesController/Delete/5
        [Authorize(Roles ="Administrador")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HabitacionesController/Delete/5
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
