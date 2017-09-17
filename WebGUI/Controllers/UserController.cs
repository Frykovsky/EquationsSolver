using System.Linq;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.UsersDB;
using WebGUI.Models;
using System.Text;

namespace WebGUI.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private IExperimentsRepository Expers;
        private IParametersRepository EquParams;
        
        public UserController (IExperimentsRepository exp, IParametersRepository par)
        {
            Expers = exp;
            EquParams = par;
        }

        public ActionResult List()
        {
            return View(Expers.Experiments.Where(x=>x.User == HttpContext.User.Identity.Name));
        }

        [HttpPost]
        public ActionResult Save(EquationParameters data, string graph)
        {
            if (ModelState.IsValid)
            {
                ExperimentsDB experiment = new ExperimentsDB();
                experiment.Gif = Encoding.Default.GetBytes(graph);
                experiment.MimeType = "json";
                EquParams.SaveParams(data);
                experiment.User = HttpContext.User.Identity.Name;
                Expers.SaveExperiment(experiment);
                TempData["Message"] = string.Format("{0} equation has been saved", data.EquationName);
                return RedirectToAction("Equation", "Home", new { EquationType = data.EquationName });
            }
            else
            {
                TempData["FailMessage"] = string.Format("Not all necessary data was provided");
                return View("List", Expers.Experiments );
            }
        }

        [HttpPost]
        public ActionResult Delete(int ExperId)
        {
            ExperimentsDB deletedProduct = Expers.DeleteExperiment(ExperId);
            EquationParameters deletedParams = EquParams.DeleteParams(ExperId);
            if (deletedProduct != null)
            {
                TempData["Message"] = string.Format("{0} equation was deleted", deletedParams.EquationName);
            } else
            {
                TempData["FailMessage"] = string.Format("The equation might have already been deleted");
            }
            return RedirectToAction("List", Expers.Experiments.Select(x=>x));
        }

        public ActionResult Parameters(int ExperId)
        {
            return View(EquParams.EquationParameters.Where(x => x.EquationID == ExperId).FirstOrDefault());   
        }

        public ContentResult GetImage(int ExperId)
        {
            
            ExperimentsDB experiments = Expers.Experiments
                .FirstOrDefault(p => p.EquationID == ExperId);
            if (experiments != null)
            {
                string jsonfile = System.Text.Encoding.Default.GetString(experiments.Gif);
                return Content(jsonfile, "application/json");
            }
            else
            {
                return null;
            }
        }
    }
}
