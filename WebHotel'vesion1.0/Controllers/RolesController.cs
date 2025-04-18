using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WebHotel_vesion1._0.Controllers
{

    [Authorize]
    public class RolesController : Controller
    {
        // GET: RolesController1
        [Authorize(Roles ="Administrador,Empleado")]
        public ActionResult ListarRoles()
        {
            return View();
        }

       

        
        public ActionResult Create()
        {
            return View();
        }

        // POST: RolesController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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
