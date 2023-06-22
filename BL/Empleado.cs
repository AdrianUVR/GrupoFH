using Microsoft.EntityFrameworkCore;
using ML;

namespace BL
{
    public class Empleado
    {
        public static Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.GrupoFh2Context context = new DL.GrupoFh2Context())
                {
                    var query = context.Empleados.FromSqlRaw("GetEmpleado").ToList();

                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Empleado empleado = new ML.Empleado();

                            empleado.IdEmpleado = obj.IdEmpleado;
                            empleado.NombreEmpleado = obj.NombreEmpleado;
                            empleado.Usuario = obj.Usuario;
                            empleado.Password = obj.Password;
                            empleado.Email = obj.Email;
                            empleado.Telefono = obj.Telefono;
                            result.Objects.Add(empleado);

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

        public static Result Add(ML.Empleado Empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.GrupoFh2Context context = new DL.GrupoFh2Context())
                {

                    var query = context.Database.ExecuteSqlRaw($"AddEmpleado  '{Empleado.NombreEmpleado}','{Empleado.Usuario}','{Empleado.Password}','{Empleado.Email}', '{Empleado.Telefono}'");

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


        public static ML.Result Update(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.GrupoFh2Context context = new DL.GrupoFh2Context())
                {
                    var query = context.Database.ExecuteSqlRaw($"UpdateEmpleado {empleado.IdEmpleado},'{empleado.NombreEmpleado}','{empleado.Usuario}','{empleado.Password}','{empleado.Email}',  '{empleado.Telefono}' ");
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

        public static ML.Result Delete(int IdEmpleado)
        {
            ML.Result resultDelete = new ML.Result();
            try
            {
                using (DL.GrupoFh2Context context = new DL.GrupoFh2Context())
                {
                    var query = context.Database.ExecuteSqlRaw($"DeleteEmpleado {IdEmpleado}");
                    if (query > 0)
                    {
                        resultDelete.Correct = true;
                    }
                    else { resultDelete.Correct = false; }
                }
            }
            catch (Exception ex)
            {
                resultDelete.Ex = ex;
                resultDelete.Correct = false;
                resultDelete.ErrorMessage = ex.Message;
            }
            return resultDelete;
        }


        public static ML.Result GetAllByIdEmpleado(int IdEmpleado)
        {
            ML.Result resultById = new ML.Result();
            try
            {
                using (DL.GrupoFh2Context context = new DL.GrupoFh2Context())
                {
                    var query = context.Empleados.FromSqlRaw($"GetByIdEmpleado {IdEmpleado}").AsEnumerable().FirstOrDefault();



                    if (query != null)
                    {
                        ML.Empleado empleado = new ML.Empleado();
                        empleado.IdEmpleado = query.IdEmpleado;
                        empleado.NombreEmpleado = query.NombreEmpleado;
                        empleado.Usuario = query.Usuario;
                        empleado.Password = query.Password;
                        empleado.Email = query.Email;
                        empleado.Telefono = query.Telefono;
                        resultById.Object = empleado;
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
    }
}