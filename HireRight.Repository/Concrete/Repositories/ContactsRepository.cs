using DataTransferObjects.Filters;
using DataTransferObjects.Filters.Concrete;
using HireRight.EntityFramework.CodeFirst.Database_Context;
using HireRight.EntityFramework.CodeFirst.Models;
using HireRight.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;

namespace HireRight.Repository.Concrete
{
    public class ContactsRepository : IContactsRepository
    {
        private readonly IRepositoryBase<Contact> _repositoryBase;

        public ContactsRepository(IRepositoryBase<Contact> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public async Task<Contact> Add(Contact itemToAdd)
        {
            using (HireRightDbContext context = new HireRightDbContext())
            {
                return await _repositoryBase.AddBase(itemToAdd, context.Contacts, context).ConfigureAwait(false);
            }
        }

        public async Task<List<Contact>> Get(ContactFilter filter)
        {
            List<Contact> contacts;

            using (HireRightDbContext context = new HireRightDbContext())
            {
                IQueryable<Contact> contactsQuery = context.Contacts;

                contactsQuery = contactsQuery.FilterByAddress(filter.AddressFilter);

                contactsQuery = contactsQuery.Where(x => string.IsNullOrWhiteSpace(filter.CellNumber) || x.CellNumber.Contains(filter.CellNumber));

                contactsQuery = contactsQuery.Where(x => string.IsNullOrWhiteSpace(filter.Email) || x.Email.Contains(filter.Email));

                contactsQuery = contactsQuery.Where(x => filter.IsAdmin == null || x.IsAdmin == filter.IsAdmin.Value);

                contactsQuery = contactsQuery.Where(x => filter.IsPrimary == null || x.IsPrimary == filter.IsPrimary.Value);

                contactsQuery = contactsQuery.Where(x => string.IsNullOrWhiteSpace(filter.Name) || x.Name.Contains(filter.Name));

                contactsQuery = contactsQuery.Where(x => string.IsNullOrWhiteSpace(filter.OfficeNumber) || x.OfficeNumber.Contains(filter.OfficeNumber));

                contacts = await _repositoryBase.TakePage(contactsQuery, filter).ConfigureAwait(false);
            }

            return contacts;
        }

        public async Task<Contact> Get(Guid itemGuid)
        {
            Contact contact;

            using (HireRightDbContext context = new HireRightDbContext())
            {
                contact = await _repositoryBase.GetBase(itemGuid, context.Contacts).ConfigureAwait(false);
            }

            return contact;
        }

        public async Task<Contact> Update(Contact itemToUpdate)
        {
            using (HireRightDbContext context = new HireRightDbContext())
            {
                return await _repositoryBase.UpdateBase(itemToUpdate, context.Contacts, context).ConfigureAwait(false);
            }
        }
    }
}