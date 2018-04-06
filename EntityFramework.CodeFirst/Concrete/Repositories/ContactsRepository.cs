using System;
using System.Linq;
using System.Threading.Tasks;
using DataTransferObjects.Filters.Concrete;
using HireRight.Persistence.Abstract;
using HireRight.Persistence.Database_Context;
using HireRight.Persistence.Models;
using HireRight.Persistence.Models.CompanyAggregate;

namespace HireRight.Persistence.Concrete.Repositories
{
    public class ContactsRepository : RepositoryBase<Contact>, IContactsRepository
    {
        public ContactsRepository() : base(() => new HireRightDbContext())
        {
        }

        public ContactsRepository(Func<HireRightDbContext> contextFunc) : base(contextFunc)
        {
        }

        public async Task<Contact> Add(Contact itemToAdd)
        {
            using (HireRightDbContext context = ContextFunc.Invoke())
            {
                return await AddBase(itemToAdd, context.Contacts, context).ConfigureAwait(false);
            }
        }

        public async Task<PageResult<Contact>> Get(ContactFilter filter)
        {
            using (HireRightDbContext context = ContextFunc.Invoke())
            {
                IQueryable<Contact> contactsQuery = context.Contacts;

                contactsQuery = contactsQuery.Where(x => string.IsNullOrWhiteSpace(filter.CellNumber) || x.CellNumber.Contains(filter.CellNumber));

                contactsQuery = contactsQuery.Where(x => string.IsNullOrWhiteSpace(filter.Email) || x.Email.Contains(filter.Email));

                contactsQuery = contactsQuery.Where(x => filter.IsAdmin == null || x.IsAdmin == filter.IsAdmin.Value);

                contactsQuery = contactsQuery.Where(x => filter.IsPrimary == null || x.IsPrimary == filter.IsPrimary.Value);

                contactsQuery = contactsQuery.Where(x => string.IsNullOrWhiteSpace(filter.Name) || x.Name.Contains(filter.Name));

                contactsQuery = contactsQuery.Where(x => string.IsNullOrWhiteSpace(filter.OfficeNumber) || x.OfficeNumber.Contains(filter.OfficeNumber));

                var contacts = await TakePage(contactsQuery, filter).ConfigureAwait(false);
                return contacts;
            }
        }

        public async Task<Contact> Get(Guid itemGuid)
        {
            using (HireRightDbContext context = ContextFunc.Invoke())
            {
                return await GetBase(itemGuid, context.Contacts).ConfigureAwait(false);
            }
        }

        public async Task<Contact> Update(Contact itemToUpdate)
        {
            using (HireRightDbContext context = ContextFunc.Invoke())
            {
                return await UpdateBase(itemToUpdate, context.Contacts, context).ConfigureAwait(false);
            }
        }
    }
}