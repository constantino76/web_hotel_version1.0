using AppLogin.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebHotel_vesion1._0.Models;
using WebHotel_vesion1._0.Repositories.Interfaces;

namespace WebHotel_vesion1._0.Controllers
{
    public class AccesoController : Controller
    {
        private readonly IUsuario _iusuario;
     

        public AccesoController(IUsuario isuario)
        {
            _iusuario = isuario;    
            
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {

            return View();


        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string Correo,string Clave)
        {
            string Rol = "";

            var u = new Usuario
            {
                Correo = Correo,
                Clave = Clave
            };
            var user = new Usuario();
            user = await _iusuario.getUserInitSesion(Clave, Correo);
            if (user != null)
            {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name,user.NombreCompleto),
                       new Claim("Correo",user.Correo)
                     };

                foreach (var rol in user.UsuarioRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, rol.Rol.Nombre));

                    Rol = rol.Rol.Nombre;


                }
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                //if rol is Administrador rediect to  action ModuloMaestro
               if (Rol.Equals("Administrador")||Rol.Equals("Empleado")) {

                 return RedirectToAction("Dasboard", "Home");
                
                
                }
                else
                return RedirectToAction("privacy", "Home");

            }
            ViewBag.Mensaje = "Usuario no encontrado";

            return View("Login");


        }


        public async Task<IActionResult> Logout()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);


            return RedirectToAction("Login", "Acceso");
        }
    }
}
