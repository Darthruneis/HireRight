using DataTransferObjects.Filters;
using DataTransferObjects.Filters.Concrete;
using HireRight.EntityFramework.CodeFirst.Abstract;
using HireRight.EntityFramework.CodeFirst.Models;
using HireRight.Repository.Extensions;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;

namespace HireRight.Repository.Concrete
{
    internal static class RepositoryQueryFilterer
    {
        internal static IQueryable<Account> FilterAccountQuery(IQueryable<Account> query, AccountFilter filter)
        {
            return !string.IsNullOrWhiteSpace(filter.Notes) ? query.FilterByContains(filter.Notes, x => x.Notes) : query;
        }

        internal static IQueryable<Client> FilterClientQuery(IQueryable<Client> query, ClientFilter filter)
        {
            return filter != null ? FilterClientQueryFilters(query, filter) : query;
        }

        internal static IQueryable<Client> FilterClientQueryByAccountSearch(IQueryable<Client> query, AccountFilter filter)
        {
            if (filter == null)
                return query;

            query = query.FilterByContains(filter.Notes, x => x.Account.Notes);
            query = query.Where(x => !filter.ItemGuids.Any() || filter.ItemGuids.Contains(x.Id));

            return query;
        }

        internal static IQueryable<Client> FilterClientQueryByAdminSeach(IQueryable<Client> query, ContactFilter filter)
        {
            if (filter == null) return query;

            query = query.FilterByContains(filter.CellNumber, x => x.Admin.CellNumber);
            query = query.FilterByContains(filter.Email, x => x.Admin.Email);
            query = query.FilterByContains(filter.OfficeNumber, x => x.Admin.OfficeNumber);

            query = query.Where(x => !filter.ItemGuids.Any() || filter.ItemGuids.Contains(x.AdminContactId));

            return query;
        }

        internal static IQueryable<Client> FilterClientQueryByPrimaryContactSeach(IQueryable<Client> query, ContactFilter filter)
        {
            if (filter == null)
                return query;

            query = query.FilterByContains(filter.Email, x => x.PrimaryContact.Email);
            query = query.FilterByContains(filter.CellNumber, x => x.PrimaryContact.CellNumber);
            query = query.FilterByContains(filter.OfficeNumber, x => x.PrimaryContact.OfficeNumber);

            query = query.Where(x => filter.ItemGuids != null && filter.ItemGuids.Any() || filter.ItemGuids.Contains(x.PrimaryContactId));

            return query;
        }

        internal static IQueryable<Company> FilterCompanyQuery(IQueryable<Company> query, CompanyFilter filter)
        {
            if (filter == null)
                return query;

            query = FilterCompanyQuery(query, filter.ClientFilter);
            query = FilterCompanyQuery(query, filter.LocationFilter);

            query = query.Where(x => !string.IsNullOrWhiteSpace(filter.Name) || x.Name.Contains(filter.Name));

            //if(!string.IsNullOrWhiteSpace(search.BillingAddress))
            //companiesQuery = companiesQuery.Where(x => x.BillingAddress)

            query = query.FilterByGuids(filter);

            return query;
        }

        internal static IQueryable<Company> FilterCompanyQuery(IQueryable<Company> query, ClientFilter filter)
        {
            if (filter == null) return query;

            query = FilterCompanyQuery(query, filter.AccountFilter);
            query = FilterCompanyQueryByAdmin(query, filter.AdminFilter);
            query = FilterCompanyQueryByPrimaryContact(query, filter.PrimaryContactFilter);

            query = query.Where(x => !filter.ItemGuids.Any() || x.Clients.Any(y => filter.ItemGuids.Contains(y.Id)));

            return query;
        }

        internal static IQueryable<Company> FilterCompanyQuery(IQueryable<Company> query, AccountFilter filter)
        {
            if (filter == null) return query;

            query = query.FilterByAnyContaining(filter.Notes, x => x.Clients, y => y.Account.Notes);
            query = query.Where(x => filter.ItemGuids != null && filter.ItemGuids.Any() || x.Clients.Any(y => filter.ItemGuids.Contains(y.AccountId)));

            return query;
        }

        internal static IQueryable<Company> FilterCompanyQuery(IQueryable<Company> query, LocationFilter filter)
        {
            if (filter == null) return query;

            query = query.FilterByAnyContaining(filter.Description, x => x.Locations, y => y.Description);
            query = query.FilterByAnyContaining(filter.Label, x => x.Locations, y => y.Label);

            //if(!string.IsNullOrWhiteSpace(search.Address)) DoSomething();

            query = query.Where(x => !filter.ItemGuids.Any() || x.Locations.Any(y => filter.ItemGuids.Contains(y.Id)));

            return query;
        }

        internal static IQueryable<Company> FilterCompanyQueryByAdmin(IQueryable<Company> query, ContactFilter filter)
        {
            if (filter == null) return query;

            query = query.FilterByAnyContaining(filter.CellNumber, x => x.Clients, y => y.Admin.CellNumber);
            query.FilterByAnyContaining(filter.OfficeNumber, x => x.Clients, y => y.Admin.OfficeNumber);
            query.FilterByAnyContaining(filter.Email, x => x.Clients, y => y.Admin.Email);
            query.FilterByAnyContaining(filter.Name, x => x.Clients, y => y.Admin.Name);

            query = query.Where(x => !filter.ItemGuids.Any() || x.Clients.Any(y => filter.ItemGuids.Contains(y.AdminContactId)));

            return query;
        }

        internal static IQueryable<Company> FilterCompanyQueryByPrimaryContact(IQueryable<Company> query, ContactFilter filter)
        {
            if (filter == null) return query;

            query.FilterByAnyContaining(filter.Name, x => x.Clients, y => y.PrimaryContact.Name);
            query.FilterByAnyContaining(filter.CellNumber, x => x.Clients, y => y.PrimaryContact.CellNumber);
            query.FilterByAnyContaining(filter.OfficeNumber, x => x.Clients, y => y.PrimaryContact.OfficeNumber);
            query.FilterByAnyContaining(filter.Email, x => x.Clients, y => y.PrimaryContact.Email);

            query = query.Where(x => !filter.ItemGuids.Any() || x.Clients.Any(y => filter.ItemGuids.Contains(y.PrimaryContactId)));

            return query;
        }

        internal static IQueryable<Contact> FilterContactQuery(IQueryable<Contact> query, ClientFilter filter)
        {
            if (filter == null) return query;

            query = FilterContactQuery(query, filter.AccountFilter);
            query = FilterContactQueryByAdmin(query, filter.AdminFilter);
            query = FilterContactQueryByPrimaryContact(query, filter.PrimaryContactFilter);

            query = query.Where(x => !filter.ItemGuids.Any() || filter.ItemGuids.Contains(x.ClientId));

            return query;
        }

        internal static IQueryable<Contact> FilterContactQuery(IQueryable<Contact> query, CompanyFilter filter)
        {
            if (filter == null) return query;

            query = FilterContactQuery(query, filter.LocationFilter);
            query = FilterContactQuery(query, filter.ClientFilter);
            query = FilterContactQuery(query, filter.LocationFilter);
            query = query.FilterByContains(filter.Name, x => x.Name);

            //if (!string.IsNullOrWhiteSpace(search.BillingAddress))
            //    query = query.Where(x => x.Address.Contains(search.BillingAddress));

            query = query.Where(x => !filter.ItemGuids.Any() || filter.ItemGuids.Contains(x.Id));

            return query;
        }

        internal static IQueryable<Contact> FilterContactQuery(IQueryable<Contact> query, AccountFilter filter)
        {
            if (filter == null) return query;

            query = query.FilterByContains(filter.Notes, x => x.Client.Account.Notes);
            query = query.Where(x => !filter.ItemGuids.Any() || filter.ItemGuids.Contains(x.Client.AccountId));

            return query;
        }

        internal static IQueryable<Contact> FilterContactQuery(IQueryable<Contact> query, LocationFilter filter)
        {
            if (filter == null) return query;

            query.FilterByAnyContaining(filter.Description, x => x.Company.Locations, y => y.Description);
            query.FilterByAnyContaining(filter.Label, x => x.Company.Locations, y => y.Label);
            query = query.Where(x => !filter.ItemGuids.Any() || x.Company.Locations.Any(y => filter.ItemGuids.Contains(y.Id)));

            //if(!string.IsNullOrWhiteSpace(search.Address)) DoSomething();

            return query;
        }

        internal static IQueryable<Contact> FilterContactQuery(IQueryable<Contact> query, ContactFilter filter)
        {
            if (filter == null)
                return query;

            query = FilterContactQuery(query, filter.ClientFilter);
            query = FilterContactQuery(query, filter.CompanyFilter);

            query = query.FilterByContains(filter.CellNumber, x => x.CellNumber);
            query = query.FilterByContains(filter.OfficeNumber, x => x.OfficeNumber);
            query = query.FilterByContains(filter.Email, x => x.Email);
            query = query.FilterByContains(filter.Name, x => x.Name);

            //if (!string.IsNullOrWhiteSpace(search.Address))
            //    query = query.Where(x => x.Address.Contains(search.Address));

            query = query.FilterByGuids(filter);

            return query;
        }

        internal static IQueryable<Contact> FilterContactQueryByAdmin(IQueryable<Contact> query, ContactFilter filter)
        {
            if (filter == null) return query;

            query = query.FilterByContains(filter.CellNumber, x => x.Client.Admin.CellNumber);
            query = query.FilterByContains(filter.OfficeNumber, x => x.Client.Admin.OfficeNumber);
            query = query.FilterByContains(filter.Email, x => x.Client.Admin.Email);
            query = query.FilterByContains(filter.Name, x => x.Client.Admin.Name);

            query = query.Where(x => !filter.ItemGuids.Any() || filter.ItemGuids.Contains(x.Client.AdminContactId));

            return query;
        }

        internal static IQueryable<Contact> FilterContactQueryByPrimaryContact(IQueryable<Contact> query, ContactFilter filter)
        {
            if (filter == null) return query;

            query = query.FilterByContains(filter.CellNumber, x => x.Client.PrimaryContact.CellNumber);
            query = query.FilterByContains(filter.OfficeNumber, x => x.Client.PrimaryContact.OfficeNumber);
            query = query.FilterByContains(filter.Email, x => x.Client.PrimaryContact.Email);
            query = query.FilterByContains(filter.Name, x => x.Client.PrimaryContact.Name);

            query = query.Where(x => !filter.ItemGuids.Any() || filter.ItemGuids.Contains(x.Client.PrimaryContactId));

            return query;
        }

        internal static IQueryable<Discount> FilterDiscountQuery(DiscountFilter filter, IQueryable<Discount> query)
        {
            if (filter == null)
                return query;

            query = FilterDiscountQuery(filter.ProductFilter, query);
            query = query.Where(x => filter.IsPercent == null || x.IsPercent == filter.IsPercent.Value);

            query = query.FilterByNumericComparator<Discount, decimal>(filter.AmountComparator, filter.Amount, x => x.Amount);
            query = query.FilterByNumericComparator<Discount, int>(filter.ThresholdComparator, filter.Threshold, x => x.Threshold);

            query = query.FilterByGuids(filter);

            return query;
        }

        internal static IQueryable<Discount> FilterDiscountQuery(ProductFilter filter, IQueryable<Discount> query)
        {
            if (filter == null) return query;

            query = query.FilterByContains(filter.Title, x => x.Product.Title);
            query = query.FilterByNumericComparator<Discount, decimal>(filter.PriceComparator, filter.Price, x => x.Product.Price);

            query = query.Where(x => !filter.ItemGuids.Any() || filter.ItemGuids.Contains(x.Id));

            return query;
        }

        internal static IQueryable<CompanyLocation> FilterLocationQuery(IQueryable<CompanyLocation> query, LocationFilter filter)
        {
            if (filter == null)
                return query;

            query = FilterLocationQuery(query, filter.CompanyFilter);
            query = query.FilterByContains(filter.Description, x => x.Description);
            query = query.FilterByContains(filter.Label, x => x.Label);
            query = query.FilterByGuids(filter);

            //if (!string.IsNullOrWhiteSpace(search.Address))
            //    locationsQuery = locationsQuery.Where(x => x.Address.Contains(search.Address));

            return query;
        }

        internal static IQueryable<CompanyLocation> FilterLocationQuery(IQueryable<CompanyLocation> query, CompanyFilter filter)
        {
            if (filter == null) return query;

            query = query.FilterByContains(filter.Name, x => x.Company.Name);
            query = query.Where(x => !filter.ItemGuids.Any() || filter.ItemGuids.Contains(x.Id));

            return query;
        }

        internal static IQueryable<Order> FilterOrderQuery(IQueryable<Order> query, OrderFilter filter)
        {
            if (filter == null)
                return query;

            query = FilterOrderQuery(query, filter.CompanyFilter);
            query = FilterOrderQuery(query, filter.ProductFilter);
            query = query.FilterByContains(filter.Notes, x => x.Notes);

            query = query.Where(x => !filter.PositionsOfInterest.Any() || x.PositionsOfInterest.Any(y => filter.PositionsOfInterest.Contains(y)));

            query = filter.OrderStatus != null && filter.OrderStatus.Any()
                        ? query.Where(x => filter.OrderStatus.Contains(x.Status))
                        : query;

            query = query.FilterByNumericComparator<Order, int>(filter.QuantityComparator, filter.Quantity, x => x.Quantity);

            query = query.FilterByGuids(filter);

            if (filter.Completed != null && filter.CompletedComparator != null)
            {
                query = query.Where(x => x.Status == OrderStatus.Completed);

                switch (filter.CompletedComparator.Value)
                {
                    case DateTimeSearchComparators.After:
                        query = query.Where(x => x.Completed > filter.Completed.Value);
                        break;

                    case DateTimeSearchComparators.Before:
                        query = query.Where(x => x.Completed < filter.Completed.Value);
                        break;

                    case DateTimeSearchComparators.DayOf:
                        query = query.Where(x => x.Completed.Value.DayOfYear == filter.Completed.Value.DayOfYear);
                        break;

                    case DateTimeSearchComparators.HourOf:
                        query = query.Where(x => x.Completed.Value.Hour == filter.Completed.Value.Hour);
                        break;

                    case DateTimeSearchComparators.Exactly:
                        query = query.Where(x => x.Completed.Value == filter.Completed.Value);
                        break;

                    default:
                        break;
                }
            }

            return query;
        }

        internal static IQueryable<Order> FilterOrderQuery(IQueryable<Order> query, CompanyFilter filter)
        {
            if (filter == null) return query;

            query = query.FilterByContains(filter.Name, x => x.Company.Name);
            query = query.Where(x => !filter.ItemGuids.Any() || filter.ItemGuids.Contains(x.Id));

            return query;
        }

        internal static IQueryable<Order> FilterOrderQuery(IQueryable<Order> query, ProductFilter filter)
        {
            if (filter == null) return query;

            query = query.FilterByContains(filter.Title, x => x.Product.Title);
            query = query.FilterByAnyContaining(filter.ItemGuids, x => x.Id);

            query = query.FilterByNumericComparator<Order, decimal>(filter.PriceComparator, filter.Price, x => x.Product.Price);

            return query;
        }

        internal static IQueryable<Product> FilterProductQuery(IQueryable<Product> query, ProductFilter filter)
        {
            if (filter == null)
                return query;

            FilterProductQuery(query, filter.DiscountFilter);

            query = query.Where(x => filter.Title == null || x.Title.Contains(filter.Title));
            //query.FilterByContains(filter.Title, x => x.Title);
            query = query.FilterByGuids(filter);

            query = query.FilterByNumericComparator<Product, decimal>(filter.PriceComparator, filter.Price, x => x.Price);

            return query;
        }

        internal static IQueryable<Product> FilterProductQuery(IQueryable<Product> query, DiscountFilter filter)
        {
            if (filter == null) return query;

            if (filter.Amount != null && filter.AmountComparator != null)
            {
                switch (filter.AmountComparator.Value)
                {
                    case NumericSearchComparators.GreaterThan:
                        query = query.Where(x => x.Discounts.Any(y => y.Amount > filter.Amount));
                        break;

                    case NumericSearchComparators.GreaterThanOrEqualTo:
                        query = query.Where(x => x.Discounts.Any(y => y.Amount >= filter.Amount));
                        break;

                    case NumericSearchComparators.EqualTo:
                        query = query.Where(x => x.Discounts.Any(y => y.Amount == filter.Amount));
                        break;

                    case NumericSearchComparators.LessThan:
                        query = query.Where(x => x.Discounts.Any(y => y.Amount < filter.Amount));
                        break;

                    case NumericSearchComparators.LessThanOrEqualTo:
                        query = query.Where(x => x.Discounts.Any(y => y.Amount <= filter.Amount));
                        break;

                    default:
                        break;
                }
            }

            if (filter.Threshold != null && filter.ThresholdComparator != null)
            {
                switch (filter.ThresholdComparator.Value)
                {
                    case NumericSearchComparators.GreaterThan:
                        query = query.Where(x => x.Discounts.Any(y => y.Threshold > filter.Threshold.Value));
                        break;

                    case NumericSearchComparators.GreaterThanOrEqualTo:
                        query = query.Where(x => x.Discounts.Any(y => y.Threshold >= filter.Threshold.Value));
                        break;

                    case NumericSearchComparators.EqualTo:
                        query = query.Where(x => x.Discounts.Any(y => y.Threshold == filter.Threshold.Value));
                        break;

                    case NumericSearchComparators.LessThan:
                        query = query.Where(x => x.Discounts.Any(y => y.Threshold < filter.Threshold.Value));
                        break;

                    case NumericSearchComparators.LessThanOrEqualTo:
                        query = query.Where(x => x.Discounts.Any(y => y.Threshold <= filter.Threshold.Value));
                        break;

                    default:
                        break;
                }
            }

            query = query.Where(x => filter.IsPercent == null || x.Discounts.Any(y => y.IsPercent == filter.IsPercent.Value));

            return query;
        }

        private static IQueryable<Client> FilterClientQueryFilters(IQueryable<Client> query, ClientFilter filter)
        {
            query = FilterClientQueryByAccountSearch(query, filter.AccountFilter);
            query = FilterClientQueryByAdminSeach(query, filter.AdminFilter);
            query = FilterClientQueryByPrimaryContactSeach(query, filter.PrimaryContactFilter);
            query = query.FilterByGuids(filter);

            return query;
        }
    }
}