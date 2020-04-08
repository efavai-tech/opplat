using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ContratacionWebApi.Data;
using ContratacionWebApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// using RhWebApi.Data;

namespace ContratacionWebApi.Controllers {
    [Route ("contratacion/[controller]")]
    [ApiController]
    public class ContratosController : Controller {
        private IHostingEnvironment _hostingEnvironment;
        private readonly ContratacionDbContext context;
        // private readonly RhWebApiDbContext context_rh;
        public ContratosController (ContratacionDbContext context, IHostingEnvironment environment) {
            this.context = context;
            _hostingEnvironment = environment;
            // this.context_rh = context_rh;
        }

        // GET contratos/Contratos/tipoTramite(Oferta o contrato)
        [HttpGet]
        public ActionResult GetAll (string tipoTramite) {
            // var trabajadores = context_rh.Trabajador.ToList ();

            var contratos = context.Contratos.Select (c => new {
                Id = c.Id,
                    Nombre = c.Nombre,
                    TipoId = c.Tipo,
                    Tipo = c.Tipo.ToString (),
                    TrabajadorId = c.TrabajadorId,
                    // AdminContrato = trabajadores.FirstOrDefault (t => t.Id == c.AdminContratoId),
                    EntidadId = c.EntidadId,
                    Entidad = c.Entidad.Nombre,
                    ObjetoDeContrato = c.ObjetoDeContrato,
                    Numero = c.Numero,
                    MontoCup = c.MontoCup,
                    MontoCuc = c.MontoCuc,
                    FechaDeRecepcion = c.FechaDeRecepcion.ToString ("dd/MM/yyyy"),
                    FechaDeVencimiento = c.FechaDeVencimiento.ToString ("dd/MM/yyyy"),
                    FechaDeFirmado = c.FechaDeFirmado,
                    TerminoDePago = c.TerminoDePago / 30 + " Meses y " + c.TerminoDePago % 30 + " Días",
                    EstadoId = c.Estado,
                    Estado = c.Estado.ToString (),
                    AprobJuridico = c.AprobJuridico,
                    AprobEconomico = c.AprobEconomico,
                    AprobComitContratacion = c.AprobComitContratacion
            });
            if (tipoTramite == "Oferta") {
                contratos = contratos.Where (c => c.FechaDeFirmado == null && c.AprobComitContratacion == false && c.EstadoId != Estado.Aprobado);
            }
            if (tipoTramite == "contrato") {
                contratos = contratos.Where (c => c.FechaDeFirmado != null && c.AprobComitContratacion == true && c.EstadoId == Estado.Aprobado);
            }
            return Ok (contratos);
        }

        // GET: contratos/Contratos/Id
        [HttpGet ("{id}", Name = "GetContrato")]
        public IActionResult GetbyId (int id) {
            var contrato = context.Contratos.FirstOrDefault (s => s.Id == id);

            if (contrato == null) {
                return NotFound ();
            }
            return Ok (contrato);

        }

        // POST contratos/Contratos
        [HttpPost]
        public IActionResult POST ([FromForm] ContratoDto contratoDto, IFormFile file) {
            if (ModelState.IsValid) {
                var contrato = new Contrato {
                    Id = contratoDto.Id,
                    Nombre = contratoDto.Nombre,
                    Tipo = contratoDto.Tipo,
                    TrabajadorId = contratoDto.TrabajadorId,
                    EntidadId = contratoDto.EntidadId,
                    ObjetoDeContrato = contratoDto.ObjetoDeContrato,
                    Numero = contratoDto.Numero,
                    MontoCup = contratoDto.MontoCup,
                    MontoCuc = contratoDto.MontoCuc,
                    TerminoDePago = contratoDto.TerminoDePago,
                };
                if (contratoDto.FechaDeRecepcion != null) {
                    contrato.FechaDeRecepcion = contratoDto.FechaDeRecepcion;
                } else {
                    contrato.FechaDeRecepcion = DateTime.Now;
                }
                if (contratoDto.FechaDeVencimiento != null) {
                    contrato.FechaDeVencimiento = contratoDto.FechaDeVencimiento;
                } else {
                    contrato.FechaDeVencimiento = DateTime.Now.AddDays (contratoDto.TerminoDePago);
                }

                context.Contratos.Add (contrato);
                context.SaveChanges ();

                foreach (var item in contratoDto.FormasDePago) {
                    var contratoId_FormaPagoId = new ContratoId_FormaPagoId {
                        ContratoId = contrato.Id,
                        FormaDePagoId = item
                    };
                    context.ContratoId_FormaPagoId.Add (contratoId_FormaPagoId);
                    context.SaveChanges ();
                }

                // Subir el Fichero del Contrato
                var uploads = Path.Combine (_hostingEnvironment.WebRootPath, "uploadContratos");
                if (file != null) {
                    var filePath = Path.Combine (uploads, file.FileName);
                    using (var fileStream = new FileStream (filePath, FileMode.Create)) {
                        file.CopyToAsync (fileStream);
                    }
                    contrato.FilePath = filePath;
                    context.SaveChanges ();
                }

                // Agregar Juridico y Económico como Dictaminador del contrato 
                if (contratoDto.DictaminadoresId != null) {
                    foreach (var item in contratoDto.DictaminadoresId) {
                    var contratoId_DictaminadorId = new ContratoId_DictaminadorId {
                    ContratoId = contrato.Id,
                    DictaminadorContratoId = item
                        };
                        context.ContratoId_DictaminadorId.Add (contratoId_DictaminadorId);
                        context.SaveChanges ();
                    }
                } else {
                    return BadRequest ($"Tienen que dictaminar el contrato el económico y el jurídico");
                }

                //Agregar Especialistas externos como Dictaminador/es del contrato 
                if (contratoDto.EspExternoId != null) {
                    foreach (var item in contratoDto.EspExternoId) {
                    var espExternoId_ContratoId = new EspExternoId_ContratoId {
                    ContratoId = contrato.Id,
                    EspecialistaExternoId = item
                        };
                        context.EspExternoId_ContratoId.Add (espExternoId_ContratoId);
                        context.SaveChanges ();
                    }
                }
                var HistoricoEstadoContrato = new HistoricoEstadoContrato {
                    ContratoId = contrato.Id,
                    Estado = Estado.Circulando,
                    Fecha = DateTime.Now,
                    Usuario = contratoDto.Usuario,
                };
                context.Add (HistoricoEstadoContrato);
                context.SaveChanges ();
                return new CreatedAtRouteResult ("GetContrato", new { id = contrato.Id });
            }
            return BadRequest (ModelState);
        }

        // PUT contratos/contrato/id
        [HttpPut ("{id}")]
        public IActionResult PUT ([FromBody] Contrato contrato, int id) {
            if (contrato.Id != id) {
                return BadRequest (ModelState);

            }
            context.Entry (contrato).State = EntityState.Modified;
            context.SaveChanges ();
            return Ok ();
        }

        // DELETE contratos/contrato/id
        [HttpDelete ("{id}")]
        public IActionResult Delete (int id) {
            var contrato = context.Contratos.FirstOrDefault (s => s.Id == id);

            if (contrato.Id != id) {
                return NotFound ();
            }
            context.Contratos.Remove (contrato);
            context.SaveChanges ();
            return Ok (contrato);
        }
        // PUT contratos/contrato/AprobContrato/id
        [HttpPut ("/contratos/contrato/AprobContrato/id")]
        public IActionResult AprobContrato ([FromBody] AproContratoDto aproContratoDto, int id) {
            var c = context.Contratos.Find (id);

            if (c != null) {
                if (aproContratoDto.AprobEconomico == true) {
                    c.AprobEconomico = true;
                }
                if (aproContratoDto.AprobJuridico == true) {
                    c.AprobJuridico = true;
                }
                if (aproContratoDto.AprobComitContratacion == true) {
                    c.AprobComitContratacion = true;
                }

                context.Update (c);
                context.SaveChanges ();
                return Ok ();
            }
            return NotFound ();
        }

        // GET: contratacion/contratos/Tipos
        [HttpGet ("/contratacion/contratos/Tipos")]
        public IActionResult GetAllTiposContratos () {
            var tipo = new List<dynamic> () {
                new { Id = Tipo.Marco, Nombre = Tipo.Marco.ToString () },
                new { Id = Tipo.Compra, Nombre = Tipo.Compra.ToString () },
                new { Id = Tipo.Venta, Nombre = Tipo.Venta.ToString () },
                new { Id = Tipo.Servicio, Nombre = Tipo.Servicio.ToString () },
            };
            return Ok (tipo);
        }
        // GET: contratacion/contratos/Tipos
        [HttpGet ("/contratacion/contratos/Estados")]
        public IActionResult GetAllEstadosContratos () {
            var estadosContratos = new List<dynamic> () {
                new { Id = Estado.Nuevo, Nombre = Estado.Nuevo.ToString () },
                new { Id = Estado.Circulando, Nombre = Estado.Circulando.ToString () },
                new { Id = Estado.Aprobado, Nombre = Estado.Aprobado.ToString () },
                new { Id = Estado.NoAprobado, Nombre = "No Aprobado" },
                new { Id = Estado.Vigente, Nombre = Estado.Vigente.ToString () },
                new { Id = Estado.Cancelado, Nombre = Estado.Cancelado.ToString () },
                new { Id = Estado.Vencido, Nombre = Estado.Vencido.ToString () },
                new { Id = Estado.Revision, Nombre = Estado.Revision.ToString () },
                new { Id = Estado.SinEstado, Nombre = "Sin Estado" },
            };
            return Ok (estadosContratos);
        }

        //Post :contratacion/contratos/UploadFile
        [HttpPost ("/contratacion/contratos/UploadFile")]
        public async Task<IActionResult> Upload (IFormFile file) {
            var uploads = Path.Combine (_hostingEnvironment.WebRootPath, "uploadContratos");
            if (file != null) {
                var filePath =  Path.Combine (uploads, file.FileName);
                // if (File.Exists(filePath))
                // {
                //     File.Delete(filePath);
                // }
                using (var fileStream = new FileStream (filePath, FileMode.Create)) {
                    await file.CopyToAsync (fileStream);
                }
            }
            return Ok ();
        }
    }
}