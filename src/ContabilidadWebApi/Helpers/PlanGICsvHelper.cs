﻿using System;
using System.Collections.Generic;
using System.Linq;
using CsvHelper;
using System.Text.RegularExpressions;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using ContabilidadWebApi.Models;
using ContabilidadWebApi.Data;
using ContabilidadWebApi.Services;

namespace ContabilidadWebApi.Helper
{

    /// <summary>
    /// Helper para importar el plan de costo en formato csv.
    /// </summary>
    public class PlanGICsvHelper
    {

        private readonly DbContext _context;

        public PlanGICsvHelper(ContabilidadDbContext _context)
        {
            this._context = _context;

        }
        public PlanGICsvHelper(DbContext _context)
        {
            this._context = _context;

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader">Instancia del nuget CsvReader que contine el puntero al fichero csv.</param>
        /// <param name="year">Año del plan</param>
        public void readCsv(CsvReader reader, string year)
        {

            // string año;
            // //Obtain the year
            // ReadingYear(reader, out año);

            //Skip the um
            reader.Read();
            List<string> headers = readValues(reader);

            //Skip empty line after the headers.
            reader.Read();

            //Start reading the elements from the table.
            readingElements(reader, year);


        }

        /// <summary>
        /// Divide el registro obtendido.
        /// </summary>
        /// <param name="record">Linea obtenida por el puntero al fichero.</param>
        /// <returns>Arreglo de string</returns>
        private string[] SplitRecord(string record)
        {
            var line = record.Replace(',', '.');
            return line.Split(new char[] { ';' });
        }


        /// <summary>
        /// Obtiene el año del plan
        /// </summary>
        /// <param name="año">Año del plan</param>
        /// <param name="reader">Puntero al fichero en csv.</param>
        private void ReadingYear(CsvReader reader, out string año)
        {
            reader.Read();
            var record = SplitRecord(reader[0]);
            string year = record[1].Trim();
            año = year;
        }

        /// <summary>
        /// lee solmante los valores mensuales
        /// </summary>
        /// <param name="reader">Puntero al fichero en csv.</param>
        /// <returns></returns>
        private List<string> readValues(CsvReader reader)
        {
            reader.Read();
            var record = SplitRecord(reader[0]);
            return readValues(record);

        }

        /// <summary>
        /// Lee solamente los valores mensuales.
        /// </summary>
        /// <param name="record">registro de un subElemento.</param>
        /// <returns></returns>
        private List<string> readValues(string[] record)
        {
            var result = record.Skip(3).ToList<string>();
            result.RemoveAt(result.Count - 1);
            return result;
        }

        /// <summary>
        /// Determina si un registro solo posee valores vacios o nulos.
        /// </summary>
        /// <param name="record">Registro</param>
        /// <returns></returns>
        private bool isEmpty(string[] record)
        {
            int count = 0;
            for (int i = 1; i < record.Length; i++)
            {
                if (record[i] == "" || record[i] == "0") count++;
            }

            return count == record.Length;
        }

        /// <summary>
        /// Metodo principal que itera sobre los elementos y va almacenado en la bd.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="año"></param>
        private void readingElements(CsvReader reader, string año)
        {
            //costCenterId = costCenterId;
            while (reader.Read())
            {
                var line = SplitRecord(reader[0]);

                //Si la linea es vacia entoces continua a la proxima.
                if (isEmpty(line)) continue;

                string code;
                string top;


                //Si la linea no es un elemento entoces continua a la proxima.
                if (!isAtomicElement(line[0], out code, out top)) continue;

                var months = readValues(line);
                //Si no existen valores mensuales continua.
                if (isEmtpyPlan(months)) continue;

                PlanGI plan = new PlanGI();
                // DetallePlanGI planElement = new DetallePlanGI();
                DetallePlanGI valoresMensuales = new DetallePlanGI();
                plan.Year = año;

                string subcode = code;

                //Llena el plan
                // if (!ReadElement(subcode, planElement)) continue;
                ReadMonthValues(months, valoresMensuales);
                ConceptoPlan elemento = new ConceptoPlan();

                if (!_context.Set<ConceptoPlan>().Any(s => s.Concepto.Trim().ToUpper().Equals(code.Trim().ToUpper())))
                {
                    elemento = new ConceptoPlan { Concepto = code };
                    string valor = DatosPlanGI.Datos().SingleOrDefault(s => s.Dato.Equals(code)).Valor.ToString();
                    if (_context.Set<Cuenta>().Any(s => s.Nombre.Equals(valor)))
                    {
                        var cta = _context.Set<Cuenta>().SingleOrDefault(s => s.Nombre.Equals(valor));
                        elemento.Cuentas.Add(new ConceptoCuentas { Cuenta = cta });
                    }
                    _context.Add(elemento);
                    try
                    {
                        _context.SaveChanges();
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                }
                else
                {
                    elemento = _context.Set<ConceptoPlan>().SingleOrDefault(s => s.Concepto.Trim().ToUpper().Equals(code.Trim().ToUpper()));
                }

                plan.Fecha = DateTime.Now;
                plan.Titulo = "Plan de Igresos y Gastos";
                plan.Detalles.Add(new DetallePlanGI
                {
                    Concepto = elemento,
                    Enero = valoresMensuales.Enero,
                    Febrero = valoresMensuales.Febrero,
                    Marzo = valoresMensuales.Marzo,
                    Abril = valoresMensuales.Abril,
                    Mayo = valoresMensuales.Mayo,
                    Junio = valoresMensuales.Junio,
                    Julio = valoresMensuales.Julio,
                    Agosto = valoresMensuales.Agosto,
                    Septiembre = valoresMensuales.Septiembre,
                    Octubre = valoresMensuales.Octubre,
                    Noviembre = valoresMensuales.Noviembre,
                    Diciembre = valoresMensuales.Diciembre,
                });
                if (!_context.Set<PlanGI>().Any(s => s.Titulo.Equals("Plan de Igresos y Gastos") && s.Year == año))
                {
                    _context.Add(plan);
                    try
                    {
                        _context.SaveChanges();
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                }
                else
                {
                    if (_context.Set<DetallePlanGI>().Include(s => s.Concepto).Any(s => s.Concepto.Concepto == code))
                    {
                        var concepto = _context.Set<ConceptoPlan>().SingleOrDefault(s => s.Concepto.Equals(code));
                        var planS = _context.Set<PlanGI>().SingleOrDefault(s => s.Titulo.Equals("Plan de Igresos y Gastos") && s.Year == año);
                        var detalle = _context.Set<DetallePlanGI>().SingleOrDefault(s => s.ConceptoId == concepto.Id && s.PlanId == planS.Id);
                        detalle.Enero = valoresMensuales.Enero;
                        detalle.Febrero = valoresMensuales.Febrero;
                        detalle.Marzo = valoresMensuales.Marzo;
                        detalle.Abril = valoresMensuales.Abril;
                        detalle.Mayo = valoresMensuales.Mayo;
                        detalle.Junio = valoresMensuales.Junio;
                        detalle.Julio = valoresMensuales.Julio;
                        detalle.Agosto = valoresMensuales.Agosto;
                        detalle.Septiembre = valoresMensuales.Septiembre;
                        detalle.Octubre = valoresMensuales.Octubre;
                        detalle.Noviembre = valoresMensuales.Noviembre;
                        detalle.Diciembre = valoresMensuales.Diciembre;
                        _context.Update(detalle);
                        _context.SaveChanges();
                    }
                    else
                    {
                        //Si existe algun plan en ese año actualizarlo
                        var planSelect = _context.Set<PlanGI>().SingleOrDefault(s => s.Year == año);
                        _context.Set<DetallePlanGI>().Add(new DetallePlanGI
                        {
                            Concepto = elemento,
                            Plan = planSelect,
                            Enero = valoresMensuales.Enero,
                            Febrero = valoresMensuales.Febrero,
                            Marzo = valoresMensuales.Marzo,
                            Abril = valoresMensuales.Abril,
                            Mayo = valoresMensuales.Mayo,
                            Junio = valoresMensuales.Junio,
                            Julio = valoresMensuales.Julio,
                            Agosto = valoresMensuales.Agosto,
                            Septiembre = valoresMensuales.Septiembre,
                            Octubre = valoresMensuales.Octubre,
                            Noviembre = valoresMensuales.Noviembre,
                            Diciembre = valoresMensuales.Diciembre,
                        });
                        _context.SaveChanges();

                    }

                }
                //Saves the elemnts at range with value 0 for the plan.
                //SaveElementsAtRange(code, top);


            }
        }

        /// <summary>
        /// Determina si es un subElemento
        /// </summary>
        /// <param name="record">Regsitro</param>
        /// <param name="code">Codigo del subElemento.</param>
        /// <param name="top">Tope maximo del rango si este existe (Ej: 220340-27).</param>
        /// <returns></returns>
        private bool isAtomicElement(string record, out string code, out string top)
        {
            Regex re = new Regex(@"[a-z A-Z,ñ,Ñ,í,ó,á,é,ú,Í,Ó,Á,É,Ú,ü,Ü,0-9,%. ]*");
            Regex reTop = new Regex(@"-[A-Z]{2}");

            var result = re.Match(record);
            //var money = recoin.Match(record);

            top = reTop.Match(record).Value;
            code = result.Value;



            return (result.Value != "");

        }

        /// <summary>
        /// Determina si no existe una cuenta para el subElemento.
        /// </summary>
        /// <param name="values">Valores Mensuales</param>
        /// <returns></returns>
        private bool isEmtpyPlan(List<string> values)
        {
            foreach (var i in values)
            {
                if (i != "" && i != "0") return false;
            }
            return true;
        }

        /// <summary>
        /// Asigna a una instancia de plan los valores del elemento.
        /// </summary>
        /// <param name="subcode">Elemento</param>
        /// <param name="plan">Instancia del plan</param>
        /// <returns></returns>
        private bool ReadElement(string subcode, DetallePlanGI plan)
        {
            var data = DatosPlanGI.Datos().SingleOrDefault(s => s.Dato.Trim().ToUpper().Equals(subcode.Trim().ToUpper()));

            // if (_context.Set<ConceptoPlan>().Any(s=>s.Concepto.Trim().ToUpper().Equals(data.Dato.ToString())))
            // {

            // }
            // plan.Concepto = subcode;
            // plan.ElementoValor = Convert.ToString(data.Valor);

            return true;

        }


        /// <summary>
        /// Mapea los valores mensuales de un registro al plan en cuestion.
        /// </summary>
        /// <param name="values">Valores mensuales</param>
        /// <param name="plan">Instancia del plan que se esta importando.</param>
        private void ReadMonthValues(List<string> values, DetallePlanGI plan)
        {

            plan.Enero = ((values[0] == "" || values[0] == "0") ? 0 : Decimal.Parse(FixImporte(values[0]), CultureInfo.InvariantCulture));
            plan.Febrero = ((values[1] == "" || values[1] == "0") ? 0 : Decimal.Parse(FixImporte(values[1]), CultureInfo.InvariantCulture));
            plan.Marzo = ((values[2] == "" || values[2] == "0") ? 0 : Decimal.Parse(FixImporte(values[2]), CultureInfo.InvariantCulture));
            plan.Abril = ((values[3] == "" || values[3] == "0") ? 0 : Decimal.Parse(FixImporte(values[3]), CultureInfo.InvariantCulture));
            plan.Mayo = ((values[4] == "" || values[4] == "0") ? 0 : Decimal.Parse(FixImporte(values[4]), CultureInfo.InvariantCulture));
            plan.Junio = ((values[5] == "" || values[5] == "0") ? 0 : Decimal.Parse(FixImporte(values[5]), CultureInfo.InvariantCulture));
            plan.Julio = ((values[6] == "" || values[6] == "0") ? 0 : Decimal.Parse(FixImporte(values[6]), CultureInfo.InvariantCulture));
            plan.Agosto = ((values[7] == "" || values[7] == "0") ? 0 : Decimal.Parse(FixImporte(values[7]), CultureInfo.InvariantCulture));
            plan.Septiembre = ((values[8] == "" || values[8] == "0") ? 0 : Decimal.Parse(FixImporte(values[8]), CultureInfo.InvariantCulture));
            plan.Octubre = ((values[9] == "" || values[9] == "0") ? 0 : Decimal.Parse(FixImporte(values[9]), CultureInfo.InvariantCulture));
            plan.Noviembre = ((values[10] == "" || values[10] == "0") ? 0 : Decimal.Parse(FixImporte(values[10]), CultureInfo.InvariantCulture));
            plan.Diciembre = ((values[11] == "" || values[11] == "0") ? 0 : Decimal.Parse(FixImporte(values[11]), CultureInfo.InvariantCulture));
        }
        /// <summary>
        /// Arreglar Importes.
        /// </summary>
        /// <param name="money">Linea obtenida por el puntero al fichero.</param>
        /// <returns>Arreglo de string</returns>
        private string FixImporte(string money)
        {
            string geld = "";
            if (money.Contains('.'))
            {
                var replace = money.Split('.');
                if (replace.Length == 2)
                {
                    geld = replace[0] + "." + replace[1];

                }
                if (replace.Length == 3)
                {
                    geld = replace[0] + replace[1] + "." + replace[2];

                }
                if (replace.Length == 4)
                {
                    geld = replace[0] + replace[1] + replace[2] + "." + replace[3];

                }
            }
            else
            {
                geld = money;
            }
            return geld;
        }


        /// <summary>
        /// Obtiene los codigos de los subelementos contenidos en un rango.
        /// </summary>
        /// <param name="code">Elemento</param>
        /// <param name="top">Tope maximo del rango de subelementos</param>
        /// <returns>Un ienumerable que contine los codigos de los elementos contenidos en el rango.</returns>
        private IEnumerable<string> GetElementsAtRange(string code, string top)
        {

            List<string> result = new List<string>();


            if (top == "") return result;

            top = top.Substring(1);

            string subCode = code.Substring(0, 4);

            int first = int.Parse(code) % 100;
            int last = int.Parse(top);

            for (int i = first + 1; i <= last; i++)
            {
                result.Add(subCode + i);
            }

            return result;


        }


        // private void SaveElementsAtRange(string code, string top)
        // {
        //     foreach (var c in GetElementsAtRange(code, top))
        //     {
        //         PlanGI emptyPlan = new PlanGI();

        //         string subcode = c;

        //         //Llena el plan
        //         if (!ReadElement(subcode, emptyPlan)) continue;

        //         _context.Add(emptyPlan);
        //         _context.SaveChanges();
        //     }
        // }
    }
}
