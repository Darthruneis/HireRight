using System;
using DataTransferObjects.Data_Transfer_Objects;
using HireRight.Models;
using System.Threading.Tasks;
using System.Web.Mvc;
using HireRight.BusinessLogic.Abstract;

namespace HireRight.Controllers
{
    public class ContactController : Controller
    {
        private readonly ICompanyBusinessLogic _companyBusinessLogic;
        private readonly IContactsBusinessLogic _contactsBusinessLogic;

        public ContactController(IContactsBusinessLogic contactsBusinessLogic, ICompanyBusinessLogic companyBusinessLogic)
        {
            _contactsBusinessLogic = contactsBusinessLogic;
            _companyBusinessLogic = companyBusinessLogic;
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

            if (!ModelState.IsValid)
                return View("Contact", model);

            try
            {
                CompanyDTO companyDto = await _companyBusinessLogic.Add(new CompanyDTO() { BillingAddress = model.Address, Name = model.CompanyName });

                ContactDTO contactDto = model.ConvertToContactDTO();
                contactDto.CompanyId = companyDto.Id;
                ContactDTO dto = await _contactsBusinessLogic.Add(contactDto);

                await _contactsBusinessLogic.SendNewContactEmail(dto.Id, model.Message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error ocurred while saving.  Please try again later.");
                return View("Contact", model);
            }

            return RedirectToAction("Success", model);
        }

        public ActionResult Success(ContactUsViewModel model)
        {
            return View(model);
        }
    }
}