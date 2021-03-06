﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace op_costos_api.VersatModels2
{
    public class ConCuenta
    {
        [Key]
        public int Idcuenta { get; set; }
        public string Clave { get; set; }
        public int Idapertura { get; set; }
        public bool? Activa { get; set; }

        public virtual ICollection<OptCuentaCentroSubPeriodo> OptCuentaCentroSubPeriodo { get; set; }
        public virtual ICollection<OptCuentaPeriodo> OptCuentaPeriodo { get; set; }
        public virtual ICollection<FinDcAnticipo> FinDcAnticipo { get; set; }

    }
}
