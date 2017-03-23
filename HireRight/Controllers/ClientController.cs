using HireRight.Models;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using HireRight.BusinessLogic.Abstract;

namespace HireRight.Controllers
{
    public class ClientController : Controller
    {
        private readonly IContactsBusinessLogic _contactsBusinessLogic;

        public ClientController(IContactsBusinessLogic contactsBusinessLogic)
        {
            _contactsBusinessLogic = contactsBusinessLogic;
        }

        public ActionResult NewClients(NewClientsViewModel model = null)
        {
            if (model == null)
                model = new NewClientsViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> SubmitNewClients(NewClientsViewModel model)
        {
            if (!ModelState.IsValid)
                return View("NewClients", model);

            try
            {
                if (model.ToTalkToConsultant)
                    await SendContactConsultantEmail(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error ocurred while sending the message.  Please try again later.");
#if DEBUG
                ModelState.AddModelError("", ex.Message);
#endif
            }

            if (!ModelState.IsValid)
                return View("NewClients", model);

            return RedirectToAction("Success", model);
        }

        public ActionResult Success(NewClientsViewModel model)
        {
            return View(model);
        }

        private async Task SendContactConsultantEmail(NewClientsViewModel model)
        {
            StringBuilder message = new StringBuilder();
            message.AppendLine(model.Name + "is interested in using HireRight!  They would like to contact a consultant directly at their earliest convenience.");
            message.AppendLine($"{model.Name} is a {model.CompanyPosition} at {model.Company}");

            await _contactsBusinessLogic.SendContactConsultantEmail(model.Email, message.ToString());
        }
    }
}