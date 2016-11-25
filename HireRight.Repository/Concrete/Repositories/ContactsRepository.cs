using HireRight.EntityFramework.CodeFirst.Database_Context;
using HireRight.EntityFramework.CodeFirst.Models;
using HireRight.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DataTransferObjects.Filters;
using DataTransferObjects.Filters.Concrete;

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
                return await _repositoryBase.AddBase(itemToAdd, context.Contacts).ConfigureAwait(false);
            }
        }

        public async Task<List<Contact>> Get(ContactFilter filter)
        {
            List<Contact> contacts;

            using (HireRightDbContext context = new HireRightDbContext())
            {
                IQueryable<Contact> contactsQuery = context.Contacts.Include(x => x.Client).Include(x => x.Company);

                contactsQuery = RepositoryQueryFilterer.FilterContactQuery(contactsQuery, filter);

                contacts = await _repositoryBase.TakePage(contactsQuery, filter).ConfigureAwait(false);
            }

            return contacts;
        }

        public async Task<Contact> Get(Guid itemGuid)
        {
            Contact contact;

            using (HireRightDbContext context = new HireRightDbContext())
            {
                contact = await _repositoryBase.GetBase(itemGuid, context.Contacts.Include(x => x.Client).Include(x => x.Company)).ConfigureAwait(false);
            }

            return contact;
        }

        public async Task<Contact> Update(Contact itemToUpdate)
        {
            using (HireRightDbContext context = new HireRightDbContext())
            {
                return await _repositoryBase.UpdateBase(itemToUpdate, context.Contacts).ConfigureAwait(false);
            }
        }
    }
}