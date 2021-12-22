using Microsoft.AspNetCore.Mvc;
using MvcHospitalCrudLinq.Data;
using MvcHospitalCrudLinq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcHospitalCrudLinq.Controllers
{
    public class HospitalesController : Controller
    {
        HospitalesContext context;

        public HospitalesController()
        {
            this.context = new HospitalesContext();
        }

        public IActionResult Index()
        {
            List<Hospital> lista = this.context.GetHospitales();
            return View(lista);
        }

        public IActionResult Details(int idhospital)
        {
            Hospital hospital = this.context.FindHospital(idhospital);
            return View(hospital);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Hospital hospital) {
            this.context.InsertHospital(hospital.IdHospital
                , hospital.Nombre, hospital.Direccion, hospital.Telefono
                , hospital.Camas);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int idhospital)
        {
            Hospital hospital = this.context.FindHospital(idhospital);
            return View(hospital);
        }

        [HttpPost]
        public IActionResult Edit(Hospital hospital)
        {
            this.context.UpdateHospital(hospital.IdHospital, hospital.Nombre
                , hospital.Direccion, hospital.Telefono, hospital.Camas);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int idhospital)
        {
            this.context.DeleteHospital(idhospital);
            return RedirectToAction("Index");
        }
    }
}
