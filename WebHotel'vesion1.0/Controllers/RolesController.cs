using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebHotel_vesion1._0.Repositories.Interfaces;
using WebHotel_vesion1._0.Models;

namespace WebHotel_vesion1._0.Controllers
{

    [Authorize]
    public class RolesController : Controller
    { 
         private readonly IRol _irol;
    

        public RolesController(IRol irol) {
        _irol=irol; 
        
        }
        // GET: RolesController1
        [Authorize(Roles ="Administrador,Empleado")]
        public  async Task< ActionResult> ListarRoles()
        { List<Rol>listRoles= await _irol.GetRols();   
            return View(listRoles);
        }

       

        
        public ActionResult Create()
        {
            return View();
        }

        // POST: RolesController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Rol rol)
        {
            _irol.CreateRol(rol);
            return RedirectToAction("ListarRoles");
        }

        // GET: RolesController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RolesController1/Edit/5
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

        // GET: RolesController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RolesController1/Delete/5
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
