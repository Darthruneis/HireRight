using DataTransferObjects.Filters.Concrete;
using HireRight.EntityFramework.CodeFirst.Database_Context;
using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;
using HireRight.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HireRight.Repository.Concrete
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

        public async Task<List<Contact>> Get(ContactFilter filter)
        {
            using (HireRightDbContext context = ContextFunc.Invoke())
            {
                IQueryable<Contact> contactsQuery = context.Contacts;

                contactsQuery = contactsQuery.FilterByAddress(filter.AddressFilter);

                contactsQuery = contactsQuery.Where(x => string.IsNullOrWhiteSpace(filter.CellNumber) || x.CellNumber.Contains(filter.CellNumber));

                contactsQuery = contactsQuery.Where(x => string.IsNullOrWhiteSpace(filter.Email) || x.Email.Contains(filter.Email));

                contactsQuery = contactsQuery.Where(x => filter.IsAdmin == null || x.IsAdmin == filter.IsAdmin.Value);

                contactsQuery = contactsQuery.Where(x => filter.IsPrimary == null || x.IsPrimary == filter.IsPrimary.Value);

                contactsQuery = contactsQuery.Where(x => string.IsNullOrWhiteSpace(filter.Name) || x.Name.Contains(filter.Name));

                contactsQuery = contactsQuery.Where(x => string.IsNullOrWhiteSpace(filter.OfficeNumber) || x.OfficeNumber.Contains(filter.OfficeNumber));

                List<Contact> contacts = await TakePage(contactsQuery, filter).ConfigureAwait(false);
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