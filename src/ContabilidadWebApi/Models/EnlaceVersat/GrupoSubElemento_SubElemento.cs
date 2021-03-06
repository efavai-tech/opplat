﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ContabilidadWebApi.Models
{
    public class GrupoSubElemento_SubElemento
    {
        public int Id { get; set; }

        public int GrupoSubelementoId { get; set; }

        public virtual GrupoSubelemento GrupoSubelemento { get; set; }

        public string SubElementoGastoId { get; set; }
        public string Descripcion { get; set; }
    }
}
