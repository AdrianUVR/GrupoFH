using Microsoft.EntityFrameworkCore;
using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Error
    {
        public static Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.GrupoFh2Context context = new DL.GrupoFh2Context())
                {
                    var query = context.Errors.FromSqlRaw("GetError").ToList();

                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Error error = new ML.Error();

                            error.IdError = obj.IdError;
                            error.DescripcionE = obj.DescripcionE;
                            error.Paso1 = obj.Paso1;
                            error.Paso2 = obj.Paso2;
                            error.Paso3 = obj.Paso3;
                            error.Area=new ML.Area();
                            error.Area.IdArea = obj.IdArea;
                            error.Area.NombreArea = obj.NombreArea;
                            result.Objects.Add(error);

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

        public static Result Add(ML.Error error)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.GrupoFh2Context context = new DL.GrupoFh2Context())
                {

                    var query = context.Database.ExecuteSqlRaw($"AddError  '{error.DescripcionE}','{error.Paso1}','{error.Paso2}','{error.Paso3}', {error.Area.IdArea}");

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


        public static ML.Result Update(ML.Error error)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.GrupoFh2Context context = new DL.GrupoFh2Context())
                {
                    var query = context.Database.ExecuteSqlRaw($"UpdateError {error.IdError},'{error.DescripcionE}','{error.Paso1}','{error.Paso2}','{error.Paso3}',  {error.Area.IdArea} ");
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

        public static ML.Result Delete(int IdError)
        {
            ML.Result resultDelete = new ML.Result();
            try
            {
                using (DL.GrupoFh2Context context = new DL.GrupoFh2Context())
                {
                    var query = context.Database.ExecuteSqlRaw($"DeleteError {IdError}");
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


        public static ML.Result GetByIdError(int IdError)
        {
            ML.Result resultById = new ML.Result();
            try
            {
                using (DL.GrupoFh2Context context = new DL.GrupoFh2Context())
                {
                    var query = context.Errors.FromSqlRaw($"GetByIdError {IdError}").AsEnumerable().FirstOrDefault();



                    if (query != null)
                    {
                        ML.Error error = new ML.Error();
                        error.IdError = query.IdError;
                        error.DescripcionE = query.DescripcionE;
                        error.Paso1 = query.Paso1;
                        error.Paso2 = query.Paso2;
                        error.Paso3 = query.Paso3;
                        error.Area = new ML.Area();
                        error.Area.IdArea = query.IdArea;
                        error.Area.NombreArea = query.NombreArea;   

                        resultById.Object = error;
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
