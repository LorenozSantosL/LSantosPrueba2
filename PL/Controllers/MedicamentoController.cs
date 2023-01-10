using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class MedicamentoController : Controller
    {
        public ActionResult GetAll()
        {
            ML.Medicamento medicamento = new ML.Medicamento();
            medicamento.Medicamentos = new List<object>();

            ML.Result result = BL.Medicamento.GetAll();

            if (result.Correct)
            {
                medicamento.Medicamentos = result.Objects;
            }

            return View(medicamento);
        }
        [HttpGet]
        public ActionResult Form(int? IdMedicamento)
        {
            ML.Medicamento medicamento = new ML.Medicamento();
            if(IdMedicamento != null)
            {
                ML.Result result = BL.Medicamento.GetById(IdMedicamento.Value);

                if (result.Correct)
                {
                    medicamento = (ML.Medicamento)result.Object;
                }
                return View(medicamento);
            }
            else
            {


                return View(medicamento);
            }

    
        }

        [HttpPost]
        public ActionResult Form(ML.Medicamento medicamento)
        {
            if(medicamento.IdMedicamento > 0)
            {
                ML.Result result = BL.Medicamento.Update(medicamento);

                if(result.Correct)
                {
                    ViewBag.Message = result.Message;
                }
                else
                {
                    ViewBag.Message = result.Message;
                }
            }
            else
            {
                ML.Result result = BL.Medicamento.Add(medicamento);

                if (result.Correct)
                {
                    ViewBag.Message = result.Message;
                    
                }
                else
                {
                    ViewBag.Message = result.Message;
                }
            }


            return PartialView("Modal");
        }
    }
}
