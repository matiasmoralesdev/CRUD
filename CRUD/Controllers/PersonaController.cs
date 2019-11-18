using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUD.Models;
using CRUD.Models.ViewModels;


namespace CRUD.Controllers
{
    public class PersonaController : Controller
    {
        // GET: Persona
        public ActionResult Index()
        {

            List<ListPersonaViewModel> listaPersonas;

            using (dbEntities db = new dbEntities())
            {
                listaPersonas = (from pers in db.Persona
                                 select new ListPersonaViewModel
                                 {
                                     Dni = pers.dni,
                                     Nombre = pers.nombre,
                                     Email = pers.email,
                                     FechaNacimiento = pers.fecha_nacimiento
                                 }).ToList();
            }


            return View(listaPersonas);
        }


        public ActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Nuevo(PersonaViewModel persModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    using (dbEntities db = new dbEntities())
                    {
                        var personaTabla = new Persona();
                        personaTabla.dni = persModel.Dni;
                        personaTabla.nombre = persModel.Nombre;
                        personaTabla.email = persModel.Email;
                        personaTabla.fecha_nacimiento = persModel.FechaNacimiento;

                        db.Persona.Add(personaTabla);
                        db.SaveChanges();

                    }

                    return Redirect("~/Persona/");
                }
                return View(persModel);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        public ActionResult Borrar(int dni)
        {
            using(dbEntities db = new dbEntities())
            {
                var personaTabla = db.Persona.Find(dni);
                db.Persona.Remove(personaTabla);
                db.SaveChanges();
            }

            return Redirect("~/Persona/");
        }

    }
}