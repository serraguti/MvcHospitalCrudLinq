﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcHospitalCrudLinq.Models
{
    public class Plantilla
    {
        public int IdPlantilla { get; set; }
        public String Apellido { get; set; }
        public String Funcion { get; set; }
        public String Turno { get; set; }
        public int Salario { get; set; }
        public int IdHospital { get; set; }
    }
}
