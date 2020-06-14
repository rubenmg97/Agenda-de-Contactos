using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiDev.Bussines.Agenda;
using TiDev.Entity.Agenda;

namespace AgendaU.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        BusUsuario comandoU = new BusUsuario();
        BusContacto comandoC = new BusContacto();
        BusTipo comandoT = new BusTipo();
        BusReferencia comandoR = new BusReferencia();
        BusDatosContacto comandoDc = new BusDatosContacto();

        [HttpGet]
        public ActionResult Login()
        {
            try
            {
                return View();
            }
            catch (Exception error)
            {
                TempData["error"] = error.Message;
                return View();
            }
        }
        [HttpPost]
        public ActionResult Login(EntUsuario usuario)
        {
            try
            {
                if (comandoU.Entrar(usuario) == true)
                {
                    int id = comandoU.Obtener(usuario.NomUsuario);
                    EntUsuario u = new EntUsuario();
                    u = comandoU.Obtener(id);
                    Session["usuario"] = u;
                    return RedirectToAction("Datos");
                }
                else
                {
                    TempData["error"] = "Usuario o Contraseña Invalido";
                    return View();
                }
            }
            catch (Exception error)
            {
                TempData["error"] = error.Message;
                return View();
            }
        }

        public ActionResult Datos()
        {
            try
            {
                EntUsuario u = (EntUsuario)Session["usuario"];
                return View(comandoDc.Obtener(u.Id));
            }
            catch (Exception error)
            {
                TempData["error"] = error.Message;
                List<EntDatosContacto> list = new List<EntDatosContacto>();
                return View(list);
            }
        }


        [HttpGet]
        public ActionResult Buscar(String valor)
        {
            try
            {
                EntUsuario u = (EntUsuario)Session["usuario"];
                List<EntDatosContacto> list = comandoDc.Obtener(valor, u.Id);
                return View("Datos", list);
            }
            catch (Exception error)
            {
                EntUsuario u = (EntUsuario)Session["usuario"];
                TempData["error"] = error.Message;
                List<EntContacto> lista = new List<EntContacto>();
                return View("Datos", comandoC.ObtenerPorUsuario(u.Id));
            }
        }
        public ActionResult Cerrar()
        {//diferencia entre remove, clear, abandone
            Session["usuario"] = null;

            return RedirectToAction("Login", "Home");

        }
    
        [HttpGet]
        public ActionResult CreateU()
        {
            try
            {
                return View();
            }
            catch (Exception error)
            {
                TempData["error"] = error.Message;
                return View("Login");
            }
        }

        [HttpPost]
        public ActionResult CreateU(EntUsuario usuario, HttpPostedFileBase NomFoto)
        {
            try
            {
                var allowedExtensions = new[] {".Jpg", ".png", ".jpg", "jpeg"};
                var fileName = Path.GetFileName(NomFoto.FileName);
                string ext = Path.GetExtension(NomFoto.FileName); //getting the extension(ex-.jpg) 
                if (allowedExtensions.Contains(ext)) //check what type of extension  
                {
                    string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                    string myfile = usuario.NomUsuario + ext; //appending the name with id  
                                                              // store the file inside ~/project folder(Img)  
                    var path = Path.Combine(Server.MapPath("~/Imagenes"), myfile);

                    NomFoto.SaveAs(path);
                    usuario.NomFoto = myfile;
                    comandoU.Create(usuario);
                    TempData["resultado"] = $"Se Agrego Correctamente a {usuario.NombreCompleto}";
                    return RedirectToAction("Login");
                }
                else
                {
                    throw new ApplicationException("Solo archivos de imagen");
                }
            }
            catch (Exception error)
            {
                TempData["error"] = error.Message;
                return View("Create");
            }
        }

        [HttpGet]
        public ActionResult CreateC()
        {
            try
            {
                return View();
            }
            catch (Exception error)
            {
                TempData["error"] = error.Message;
                return View("Index");
            }
        }

        [HttpPost]
        public ActionResult CreateC(EntContacto contacto, HttpPostedFileBase NomFoto)
        {
            try
            {
                var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", "jpeg" };
                var fileName = Path.GetFileName(NomFoto.FileName);
                string ext = Path.GetExtension(NomFoto.FileName); //getting the extension(ex-.jpg) 
                if (allowedExtensions.Contains(ext)) //check what type of extension  
                {
                    string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                    string myfile = contacto.Nombre + ext; //appending the name with id  
                                                              // store the file inside ~/project folder(Img)  
                    var path = Path.Combine(Server.MapPath("~/Imagenes"), myfile);

                    NomFoto.SaveAs(path);
                    contacto.NomFoto = myfile;

                    EntUsuario u = (EntUsuario)Session["usuario"];
                    contacto.UserId = u.Id;
                    comandoC.Create(contacto);
                    TempData["resultado"] = $"Se Agrego Correctamente a {contacto.NombreCompleto}";
                    
                    List<EntContacto> ls =comandoC.Obtener(contacto.Nombre,contacto.UserId);
                    EntContacto contactoNuevaReferencia = ls[0];
                    Session["contacto"] = contactoNuevaReferencia;

                    return RedirectToAction("CreateR");
                }
                else
                {
                    throw new ApplicationException("Solo archivos de imagen");
                }
            }
            catch (Exception error)
            {
                TempData["error"] = error.Message;
                List<EntTipo> Tipo = comandoT.Obtener();
                ViewBag.TipoId = new SelectList(Tipo, "Id", "Nombre");
                return View("CreateC", contacto);
            }
        }

        [HttpGet]
        public ActionResult DeleteC(int id)
        {
            EntContacto contacto = new EntContacto();
            try
            {
                contacto = comandoC.Obtener(id);
                return View(contacto);
            }
            catch (Exception error)
            {
                TempData["error"] = error.Message;
                return RedirectToAction("Datos");
            }
        }

        [HttpPost]
        public ActionResult DeleteC(EntContacto contacto)
        {
            try
            {
                comandoC.DeleteReferecias(contacto);
                comandoC.Delete(contacto);
                TempData["resultado"] = $"Contacto Eliminido";
                return RedirectToAction("Datos");
            }
            catch (Exception error)
            {
                TempData["error"] = error.Message;
                EntContacto c = comandoC.Obtener(contacto.Id);
                return View("DeleteC", c);
            }
        }

        [HttpGet]
        public ActionResult EditC(int id)
        {
            EntContacto contacto = new EntContacto();
            try
            {
                contacto = comandoC.Obtener(id);
                Session["contacto"] = contacto;
                return View(contacto);
            }
            catch (Exception error)
            {
                TempData["error"] = error.Message;
                return RedirectToAction("Datos");
            }
        }

        [HttpPost]
        public ActionResult EditC(EntContacto contacto)
        {
            try
            {
                comandoC.Edit(contacto);
                TempData["resultado"] = $"Se edito correctamente a {contacto.NombreCompleto}";
                return RedirectToAction("Datos");
            }
            catch (Exception error)
            {
                TempData["error"] = error.Message;
                List<EntTipo> ls = comandoT.Obtener();
                ViewBag.TipoId = new SelectList(ls, "Id", "Nombre");
                return View("EditC", contacto);
            }
        }

        [HttpGet]
        public ActionResult CreateR()
        {
            try
            {
                List<EntTipo> ls = comandoT.Obtener();
                ViewBag.TipoId = new SelectList(ls, "Id", "Nombre");
                return View();
            }
            catch (Exception error)
            {
                TempData["error"] = error.Message;
                return RedirectToAction("Datos");
            }
        }

        [HttpPost]
        public ActionResult CreateR(EntReferencia referencia)
        {
            try
            {
                EntContacto c = (EntContacto)Session["contacto"];
                comandoR.Create(referencia, c.Id);
                TempData["resultado"] = $"Se Agrego Correctamente La referencia";
                Session["contacto"] = null;
                return RedirectToAction("Datos");
            }
            catch (Exception error)
            {
                TempData["error"] = error.Message;
                List<EntTipo> Tipo = comandoT.Obtener();
                ViewBag.TipoId = new SelectList(Tipo, "Id", "Nombre");
                return View("CreateR", referencia);
            }
        }

        [HttpGet]
        public ActionResult DeleteR(int id)
        {
            EntReferencia referencia = new EntReferencia();
            try
            {
                referencia = comandoR.Obtener(id);
                return View(referencia);
            }
            catch (Exception error)
            {
                TempData["error"] = error.Message;
                return RedirectToAction("Datos");
            }
        }

        [HttpPost]
        public ActionResult DeleteR(EntReferencia referencia)
        {
            try
            {
                comandoR.Delete(referencia);
                TempData["resultado"] = $"Registro Eliminidao";
                return RedirectToAction("Datos");
            }
            catch (Exception error)
            {
                TempData["error"] = error.Message;
                EntReferencia r = comandoR.Obtener(referencia.Id);
                return View("DeleteR", r);
            }
        }

        [HttpGet]
        public ActionResult EditR(int id)
        {
            EntReferencia referencia = new EntReferencia();
            try
            {
                referencia = comandoR.Obtener(id);
                List<EntTipo> ls = comandoT.Obtener();
                ViewBag.TipoId = new SelectList(ls, "Id", "Nombre", referencia.TipoId);
                return View(referencia);
            }
            catch (Exception error)
            {
                TempData["error"] = error.Message;
                return RedirectToAction("Datos");
            }
        }

        [HttpPost]
        public ActionResult EditR(EntReferencia referencia)
        {
            try
            {
                comandoR.Edit(referencia);
                TempData["resultado"] = $"Se edito correctamente la Referencia";
                return RedirectToAction("Datos");
            }
            catch (Exception error)
            {
                TempData["error"] = error.Message;
                List<EntTipo> ls = comandoT.Obtener();
                ViewBag.TipoId = new SelectList(ls, "Id", "Nombre", referencia.TipoId);
                return View("EditR", referencia);
            }
        }

    }
}