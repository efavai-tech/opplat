﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using RhWebApi.Models;

namespace RhWebApi.Models {
    [Table ("caracteristicas_del_trabjador")]
    public class CaracteristicasTrab {
        public int Id { get; set; }
        public int? TrabajadorId { get; set; }

        public virtual Trabajador Trabajador { get; set; }

        // public byte[] Foto { get; set; }

        // [Display(Name = "Color de piel")]
        // public ColorDePiel ColorDePiel { get; set; }

        public string ColorDeOjos { get; set; }

        public string TallaPantalon { get; set; }

        public TallaDeCamisa TallaDeCamisa { get; set; }

        public double TallaCalzado { get; set; }

        public string OtrasCaracteristicas { get; set; }

        [NotMapped]
        private string resumen;

        [NotMapped]
        public string Resumen {
            get { return String.Format ("TallaPantalon {0} , TallaCalzado {1} , OtrasCaracteristicas{2}", TallaPantalon, TallaCalzado, OtrasCaracteristicas); }
            set { resumen = value; }
        }
    }
}