﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ContabilidadWebApi.Models
{
    public class CentroCostoArea
    {
        public int Id { get; set; }

        public int AreaId { get; set; }

        public virtual Area Area { get; set; }

        public string CentroCostoId { get; set; }

        public string Detalles { get; set; }
    }
}
