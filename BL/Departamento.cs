using Microsoft.EntityFrameworkCore;
using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Departamento
    {

        public static Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.GrupoFh2Context context = new DL.GrupoFh2Context())
                {
                    var query = context.Departamentos.FromSqlRaw("[GetDepartamento]").ToList();

                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Departamento departamento = new ML.Departamento();

                            departamento.IdDepartamento = obj.IdDepartamento;
                            departamento.NombreDepartamento = obj.NombreDepartamento;
                            departamento.DescripcionD = obj.DescripcionD;

                            departamento.Area = new ML.Area();
                            departamento.Area.IdArea =(int) obj.IdArea;
                            departamento.Area.NombreArea = obj.NombreArea;

                            result.Objects.Add(departamento);

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

        public static Result Add(ML.Departamento departamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.GrupoFh2Context context = new DL.GrupoFh2Context())
                {

                    var query = context.Database.ExecuteSqlRaw($"[AddDepartamento]  '{departamento.NombreDepartamento}','{departamento.DescripcionD}',{departamento.Area.IdArea}");

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


        public static ML.Result Update(ML.Departamento departamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.GrupoFh2Context context = new DL.GrupoFh2Context())
                {
                    var query = context.Database.ExecuteSqlRaw($"[UpdateDepartamento] {departamento.IdDepartamento},'{departamento.NombreDepartamento}','{departamento.DescripcionD}' ");
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

        public static ML.Result Delete(int IdDepartamento)
        {
            ML.Result resultDelete = new ML.Result();
            try
            {
                using (DL.GrupoFh2Context context = new DL.GrupoFh2Context())
                {
                    var query = context.Database.ExecuteSqlRaw($"[DeleteDepartamento] {IdDepartamento}");
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


        public static ML.Result GetAllByIdDepartamento(int IdDepartamento)
        {
            ML.Result resultById = new ML.Result();
            try
            {
                using (DL.GrupoFh2Context context = new DL.GrupoFh2Context())
                {
                    var query = context.Departamentos.FromSqlRaw($"[GetByIdDepartamento] {IdDepartamento}").AsEnumerable().FirstOrDefault();



                    if (query != null)
                    {
                        ML.Departamento departamento = new ML.Departamento();
                        departamento.IdDepartamento= query.IdDepartamento;
                        departamento.NombreDepartamento = query.NombreDepartamento;
                        departamento.DescripcionD = query.DescripcionD;
                        departamento.Area = new ML.Area();
                        departamento.Area.IdArea = (int)query.IdArea;
                        departamento.Area.NombreArea = query.NombreArea;
                        resultById.Object= departamento;
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
