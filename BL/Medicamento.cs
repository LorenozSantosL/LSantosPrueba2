using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Medicamento
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.LsantosPrueba2Context context = new DL.LsantosPrueba2Context())
                {
                    var medicamentos = context.Medicamentos.FromSqlRaw("MedicamentoGetAll").ToList();
                    result.Objects = new List<object>();

                    if (medicamentos != null)
                    {
                        foreach (var ObjMedicamento in medicamentos)
                        {
                            ML.Medicamento medicamento = new ML.Medicamento();

                            medicamento.IdMedicamento = ObjMedicamento.IdMedicamento;
                            medicamento.Nombre = ObjMedicamento.Nombre;
                            medicamento.Descripcion = ObjMedicamento.Desripcion;
                            medicamento.FechaCaducidad = ObjMedicamento.FechaCaducidad.Value.ToString();
                            medicamento.PreciUnitario = ObjMedicamento.PrecioUnitario.Value;
                            medicamento.Stock = ObjMedicamento.Stock.Value;

                            medicamento.Proveedor = new ML.Proveedor();

                            medicamento.Proveedor.IdProveedor = ObjMedicamento.IdProveedor.Value;

                            medicamento.Proveedor.Nombre = ObjMedicamento.NombreProveedor;

                            result.Objects.Add(medicamento);

                        }

                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = true;
                result.Ex = ex;
                result.Message = "Error: " + result.Ex;
            }
            return result;
        }

        public static ML.Result GetById(int IdMedicamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.LsantosPrueba2Context context = new DL.LsantosPrueba2Context())
                {
                    var ObjMedicamento = context.Medicamentos.FromSqlRaw($" MedicamentoGetById {IdMedicamento}").AsEnumerable().SingleOrDefault();
                    result.Objects = new List<object>();

                    if (ObjMedicamento != null)
                    {

                        ML.Medicamento medicamento = new ML.Medicamento();

                        medicamento.IdMedicamento = ObjMedicamento.IdMedicamento;
                        medicamento.Nombre = ObjMedicamento.Nombre;
                        medicamento.Descripcion = ObjMedicamento.Desripcion;
                        medicamento.FechaCaducidad = ObjMedicamento.FechaCaducidad.Value.ToString();
                        medicamento.PreciUnitario = ObjMedicamento.PrecioUnitario.Value;
                        medicamento.Stock = ObjMedicamento.Stock.Value;

                        medicamento.Proveedor = new ML.Proveedor();

                        medicamento.Proveedor.IdProveedor = ObjMedicamento.IdProveedor.Value;

                        medicamento.Proveedor.Nombre = ObjMedicamento.NombreProveedor;

                        result.Object = medicamento;



                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = true;
                result.Ex = ex;
                result.Message = "Error: " + result.Ex;
            }

            return result;
        }


        public static ML.Result Add(ML.Medicamento medicamento)
        {
            ML.Result result = new ML.Result(); 
            
            try
            {
                using(DL.LsantosPrueba2Context context = new DL.LsantosPrueba2Context())
                {
                    int query = context.Database.ExecuteSqlRaw($"MedicamentoAdd '{medicamento.Nombre}',  '{medicamento.Descripcion}', '{medicamento.FechaCaducidad}', {medicamento.PreciUnitario}, {medicamento.Stock}, {medicamento.Proveedor.IdProveedor} ");

                    if(query > 0)
                    {
                        result.Correct = true;
                        result.Message = "Se ha Agregado el Medicamento";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = true;
                result.Ex = ex;
                result.Message = "Error: " + result.Ex;
            }
            return result;
        }

        public static ML.Result Update(ML.Medicamento medicamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.LsantosPrueba2Context context = new DL.LsantosPrueba2Context())
                {
                    int query = context.Database.ExecuteSqlRaw($"MedicamentoUpdate {medicamento.IdMedicamento}, '{medicamento.Nombre}',  '{medicamento.Descripcion}', '{medicamento.FechaCaducidad}', {medicamento.PreciUnitario}, {medicamento.Stock}, {medicamento.Proveedor.IdProveedor} ");
                   
                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Message = "Se ha Actualizado el Medicamento";
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = true;
                result.Ex = ex;
                result.Message = "Error: " + result.Ex;
            }

            return result;
        }


        public static ML.Result Delete(int IdMedicamento)
        {
            ML.Result result = new ML.Result();

            return result;
        }
    }
}