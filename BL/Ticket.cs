using DL;
using Microsoft.EntityFrameworkCore;
using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Ticket
    {

        public static Result GetAllTicket()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.GrupoFh2Context context = new DL.GrupoFh2Context())
                {
                    var query = context.Tickets.FromSqlRaw("GetTicket").ToList();

                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Ticket ticket = new ML.Ticket();

                            ticket.IdTicket = obj.IdTicket;
                            ticket.AsignadoA = (int)obj.AsignadoA;
                            ticket.CerradoPor = (int)obj.CerradoPor;
                            ticket.Comentarios = obj.Comentarios;
                            ticket.FechaAsignacion = obj.FechaAsignacion.ToString();
                            ticket.Status = (bool)obj.Status;
                            ticket.Area = new ML.Area();
                            ticket.Area.IdArea = (int)obj.IdArea;
                            ticket.Area.NombreArea = obj.NombreArea;
                            ticket.Error = new ML.Error();
                            ticket.Error.IdError = (int)obj.IdError;
                            ticket.Error.DescripcionE = obj.DescripcionE;

                            ticket.Empleado = new ML.Empleado();
                            ticket.Empleado.IdEmpleado = obj.IdEmpleado;
                            ticket.Empleado.NombreEmpleado = obj.NombreEmpleado;

                            result.Objects.Add(ticket);


                            result.Correct = true;

                        }
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }


        public static ML.Result GetAllLINQ()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.GrupoFh2Context context = new DL.GrupoFh2Context())
                {
                    var query = (from ticket in context.Tickets
                                 join Area in context.Areas on ticket.IdArea equals Area.IdArea
                                 join Error in context.Errors on ticket.IdError equals Error.IdError
                               

                                 select new { IdArea = ticket.IdArea, IdTicket = ticket.IdTicket, AsignadoA = ticket.AsignadoA, Cerradopor = ticket.CerradoPor, Comentarios = ticket.Comentarios, FechaAsignacion = ticket.FechaAsignacion, Status=ticket.Status, NombreArea = ticket.NombreArea , Area=ticket.IdArea, DescripcionE=ticket.DescripcionE, Error=ticket.IdError });

                    result.Objects = new List<object>();

                    if (query != null && query.ToList().Count > 0)
                    {
                        foreach (var obj in query)
                        {
                            ML.Ticket ticket = new ML.Ticket();
                            ticket.IdTicket = obj.IdTicket;
                            ticket.AsignadoA = (int)obj.AsignadoA;
                            ticket.CerradoPor = (int)obj.Cerradopor;
                            ticket.Comentarios = obj.Comentarios;
                            ticket.FechaAsignacion = obj.FechaAsignacion.Value.ToString();
                            ticket.Status = (bool)obj.Status;


                            ticket.Area = new ML.Area();
                            ticket.Area.IdArea = (int)obj.IdArea;
                            ticket.Area.NombreArea = obj.NombreArea;
                            
                            
                            ticket.Error=new ML.Error();
                            ticket.Error.IdError = (int)obj.Error;
                            ticket.Error.DescripcionE = obj.DescripcionE;





                            result.Objects.Add(ticket);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Ex = ex;
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static Result Add(ML.Ticket ticekt)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.GrupoFh2Context context = new DL.GrupoFh2Context())
                {

                    var query = context.Database.ExecuteSqlRaw($"AddTicket  {ticekt.AsignadoA},'{ticekt.Comentarios}','{ticekt.FechaAsignacion}',{ticekt.Area.IdArea}, {ticekt.Error.IdError}");

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se insertó el registro";
                    }


                    result.Correct = true;
                }

            }
            catch (Exception ex)

            {

                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetAllByIdTicket(int IdTicket)
        {
            ML.Result resultById = new ML.Result();
            try
            {
                using (DL.GrupoFh2Context context = new DL.GrupoFh2Context())
                {
                    var query = context.Tickets.FromSqlRaw($"GetByIdTicket {IdTicket}").AsEnumerable().FirstOrDefault();



                    if (query != null)
                    {
                        ML.Ticket ticket = new ML.Ticket();
                        ticket.IdTicket = query.IdTicket;
                        ticket.AsignadoA = (int)query.AsignadoA;
                        ticket.CerradoPor = (int)query.CerradoPor;
                        ticket.Comentarios = query.Comentarios;
                        ticket.FechaAsignacion = query.FechaAsignacion.Value.ToString();
                        ticket.Status = (bool)query.Status;


                        ticket.Area = new ML.Area();
                        ticket.Area.IdArea = (int)query.IdArea;
                        ticket.Area.NombreArea = query.NombreArea;


                        ticket.Error = new ML.Error();
                        ticket.Error.IdError = (int)query.IdError;
                        ticket.Error.DescripcionE = query.DescripcionE;

                        resultById.Object = ticket;
                        resultById.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                resultById.Ex = ex;
                resultById.Correct = false;
                resultById.ErrorMessage = ex.Message;
            }
            return resultById;

        }

        public static ML.Result Update(ML.Ticket ticket)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.GrupoFh2Context context = new DL.GrupoFh2Context())
                {
                    var query = context.Database.ExecuteSqlRaw($"UpdateTicket {ticket.IdTicket},{ticket.AsignadoA},'{ticket.Comentarios}',{ticket.Area.IdArea},{ticket.CerradoPor},  {ticket.Error.IdError} ");
                    if (query > 0)
                    {


                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }

            }
            catch (Exception ex)
            {
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
                result.Correct = false;
            }
            return result;
        }

    }
}
