using DataTransferObjects.Data_Transfer_Objects;
using HireRight.Models;
using System.Threading.Tasks;
using System.Web.Mvc;
using HireRight.BusinessLogic.Abstract;

namespace HireRight.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactsBusinessLogic _contactsBusinessLogic;

        public ContactController(IContactsBusinessLogic contactsBusinessLogic)
        {
            _contactsBusinessLogic = contactsBusinessLogic;
        }

        public ActionResult Contact()
        {
            return View(new ContactUsViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> SubmitContact(ContactUsViewModel model)
        {
            //check for the default message provided as an example for the Message text area.
            if (model.Message == new ContactUsViewModel().Message)
                ModelState.AddModelError("Message", "Please give us details as to how we can assist you.");

            try
            {
                ContactDTO dto = await _contactsBusinessLogic.Add(model.ConvertToContactDTO());

                await _contactsBusinessLogic.SendNewContactEmail(dto.Id, model.Message);
            }
            catch
            {
                ModelState.AddModelError("", "An error ocurred while saving.  Please try again later.");
            }

            if (!ModelState.IsValid)
                return View("Contact", model);

            return RedirectToAction("Success", model);
        }

        public ActionResult Success(ContactUsViewModel model)
        {
            return View(model);
        }
    }
}