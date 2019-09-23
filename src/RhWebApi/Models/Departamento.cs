using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using RhWebApi.Models;

namespace RhWebApi.Models
{
    [Table("departamento")]
    public class Departamento
    {
       public int Id { get; set; }
       public string Nombre { get; set; }
    }
}