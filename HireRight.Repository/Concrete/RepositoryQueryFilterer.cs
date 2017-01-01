using DataTransferObjects.Filters.Concrete;
using HireRight.EntityFramework.CodeFirst.Abstract;
using HireRight.EntityFramework.CodeFirst.Models;
using System;
using System.Linq;
using HireRight.EntityFramework.CodeFirst.Abstract.ContainingCollections;
using HireRight.EntityFramework.CodeFirst.Abstract.ContainingObjects;

namespace HireRight.Repository.Concrete
{
    internal static class RepositoryQueryFilterer
    {
        internal static IQueryable<T> FilterByAccount<T>(this IQueryable<T> query, AccountFilter filter)
            where T : PocoBase, IContainingAccount
        {
            if (filter == null)
                return query;

            query = query.Where(x => string.IsNullOrWhiteSpace(filter.Notes) || x.Account.Notes.Contains(filter.Notes));
            query = query.Where(x => filter.ItemGuids == null || !filter.ItemGuids.Any() || filter.ItemGuids.Contains(x.AccountId));

            return query;
        }

        internal static IQueryable<T> FilterByAddress<T>(this IQueryable<T> query, AddressFilter filter)
                    where T : PocoBase, IContainingAddress
        {
            if (filter == null)
                return query;

            query = query.Where(x => string.IsNullOrWhiteSpace(filter.City) || x.Address.City.Contains(filter.City));
            query = query.Where(x => string.IsNullOrWhiteSpace(filter.Country) || x.Address.Country.Contains(filter.Country));
            query = query.Where(x => string.IsNullOrWhiteSpace(filter.PostalCode) || x.Address.PostalCode.Contains(filter.PostalCode));
            query = query.Where(x => string.IsNullOrWhiteSpace(filter.State) || x.Address.State.Contains(filter.State));
            query = query.Where(x => string.IsNullOrWhiteSpace(filter.StreetAddress) || x.Address.StreetAddress.Contains(filter.StreetAddress));
            query = query.Where(x => string.IsNullOrWhiteSpace(filter.UnitNumber) || x.Address.UnitNumber.Contains(filter.UnitNumber));

            return query;
        }

        internal static IQueryable<T> FilterByAdmin<T>(this IQueryable<T> query, ContactFilter filter)
            where T : PocoBase, IContainingAdmin
        {
            if (filter == null)
                return query;

            query = query.Where(x => string.IsNullOrWhiteSpace(filter.CellNumber) || x.Admin.CellNumber.Contains(filter.CellNumber));
            query = query.Where(x => string.IsNullOrWhiteSpace(filter.Email) || x.Admin.Email.Contains(filter.Email));
            query = query.Where(x => string.IsNullOrWhiteSpace(filter.Name) || x.Admin.Name.Contains(filter.Name));
            query = query.Where(x => string.IsNullOrWhiteSpace(filter.OfficeNumber) || x.Admin.OfficeNumber.Contains(filter.OfficeNumber));
            query = query.Where(x => x.Admin.IsAdmin);
            query = query.Where(x => filter.ItemGuids == null || !filter.ItemGuids.Any() || filter.ItemGuids.Contains(x.AdminContactId));

            return query;
        }

        internal static IQueryable<T> FilterByClient<T>(this IQueryable<T> query, ClientFilter filter)
                    where T : PocoBase, IContainingClient
        {
            if (filter == null)
                return query;

            query = query.Where(x => filter.ItemGuids == null || !filter.ItemGuids.Any() || filter.ItemGuids.Contains(x.ClientId));

            return query;
        }

        internal static IQueryable<T> FilterByClients<T>(this IQueryable<T> query, ClientFilter filter)
                            where T : PocoBase, IContainingClients
        {
            if (filter == null)
                return query;

            query = query.Where(x => filter.ItemGuids == null || !filter.ItemGuids.Any() || x.Clients.Any(y => filter.ItemGuids.Contains(y.Id)));

            return query;
        }

        internal static IQueryable<T> FilterByCompany<T>(this IQueryable<T> query, CompanyFilter filter)
                    where T : PocoBase, IContainingCompany
        {
            if (filter == null)
                return query;

            query = query.Where(x => string.IsNullOrWhiteSpace(filter.Name) || x.Company.Name.Contains(filter.Name));
            query = query.Where(x => filter.ItemGuids == null || !filter.ItemGuids.Any() || filter.ItemGuids.Contains(x.CompanyId));

            return query;
        }

        internal static IQueryable<T> FilterByDiscounts<T>(this IQueryable<T> query, DiscountFilter filter)
                            where T : PocoBase, IContainingDiscounts
        {
            if (filter == null)
                return query;

            query = FilterByAmount(query, filter.Amount, filter.AmountComparator);
            query = FilterByThreshold(query, filter.Threshold, filter.ThresholdComparator);
            query = query.Where(x => filter.IsPercent == null || x.Discounts.Any(y => y.IsPercent == filter.IsPercent.Value));

            return query;
        }

        internal static IQueryable<T> FilterByLocations<T>(this IQueryable<T> query, LocationFilter filter)
                                            where T : PocoBase, IContainingLocations
        {
            if (filter == null)
                return query;

            query = query.Where(x => string.IsNullOrWhiteSpace(filter.Description) || x.Locations.Any(y => y.Description.Contains(filter.Description)));
            query = query.Where(x => string.IsNullOrWhiteSpace(filter.Label) || x.Locations.Any(y => y.Label.Contains(filter.Label)));

            return query;
        }

        internal static IQueryable<T> FilterByOrders<T>(this IQueryable<T> query, OrderFilter filter)
                                            where T : PocoBase, IContainingOrders
        {
            if (filter == null)
                return query;

            query = query.Where(x => string.IsNullOrWhiteSpace(filter.Notes) || x.Orders.Any(y => y.Notes.Contains(filter.Notes)));

            query = query.Where(x => filter.PositionsOfInterest == null
                                                || !filter.PositionsOfInterest.Any()
                                                || x.Orders.Any(y => y.PositionsOfInterest.Any(z => filter.PositionsOfInterest.Contains(z))));

            query = FilterByQuantity(query, filter.Quantity, filter.QuantityComparator);
            query = FilterByDateCompleted(query, filter.Completed, filter.CompletedComparator);

            return query;
        }

        internal static IQueryable<T> FilterByPrimaryContact<T>(this IQueryable<T> query, ContactFilter filter)
                                    where T : PocoBase, IContainingContact
        {
            if (filter == null)
                return query;

            query = query.Where(x => string.IsNullOrWhiteSpace(filter.CellNumber) || x.PrimaryContact.CellNumber.Contains(filter.CellNumber));
            query = query.Where(x => string.IsNullOrWhiteSpace(filter.Email) || x.PrimaryContact.Email.Contains(filter.Email));
            query = query.Where(x => string.IsNullOrWhiteSpace(filter.Name) || x.PrimaryContact.Name.Contains(filter.Name));
            query = query.Where(x => string.IsNullOrWhiteSpace(filter.OfficeNumber) || x.PrimaryContact.OfficeNumber.Contains(filter.OfficeNumber));
            query = query.Where(x => filter.IsAdmin == null || x.PrimaryContact.IsAdmin == filter.IsAdmin.Value);
            query = query.Where(x => x.PrimaryContact.IsPrimary);

            return query;
        }

        internal static IQueryable<T> FilterByProduct<T>(this IQueryable<T> query, ProductFilter filter)
                                            where T : PocoBase, IContainingProduct
        {
            if (filter == null)
                return query;

            query = FilterByPrice(query, filter.Price, filter.PriceComparator);
            query = query.Where(x => string.IsNullOrWhiteSpace(filter.Title) || x.Product.Title.Contains(filter.Title));

            return query;
        }

        private static IQueryable<T> FilterByAmount<T>(IQueryable<T> query, decimal? value, NumericSearchComparators? comparator)
                                            where T : PocoBase, IContainingDiscounts
        {
            if (value == null || comparator == null) return query;

            switch (comparator)
            {
                case NumericSearchComparators.GreaterThan:
                    return query.Where(x => x.Discounts.Any(y => y.Amount > value.Value));

                case NumericSearchComparators.GreaterThanOrEqualTo:
                    return query.Where(x => x.Discounts.Any(y => y.Amount >= value.Value));

                case NumericSearchComparators.EqualTo:
                    return query.Where(x => x.Discounts.Any(y => y.Amount == value.Value));

                case NumericSearchComparators.LessThanOrEqualTo:
                    return query.Where(x => x.Discounts.Any(y => y.Amount <= value.Value));

                case NumericSearchComparators.LessThan:
                    return query.Where(x => x.Discounts.Any(y => y.Amount < value.Value));

                default:
                    return query;
            }
        }

        private static IQueryable<T> FilterByDateCompleted<T>(IQueryable<T> query, DateTime? value, DateTimeSearchComparators? comparator)
                                            where T : PocoBase, IContainingOrders
        {
            if (value == null || comparator == null) return query;

            switch (comparator)
            {
                case DateTimeSearchComparators.After:
                    return query.Where(x => x.Orders.Any(y => y.Completed > value));

                case DateTimeSearchComparators.Before:
                    return query.Where(x => x.Orders.Any(y => y.Completed > value));

                case DateTimeSearchComparators.DayOf:
                    return query.Where(x => x.Orders.Any(y => y.Completed >= (value - TimeSpan.FromDays(1)) && y.Completed <= (value + TimeSpan.FromDays(1))));

                case DateTimeSearchComparators.Exactly:
                    return query.Where(x => x.Orders.Any(y => y.Completed == value));

                case DateTimeSearchComparators.HourOf:
                    return query.Where(x => x.Orders.Any(y => y.Completed >= (value - TimeSpan.FromHours(1)) && y.Completed <= (value + TimeSpan.FromHours(1))));

                default:
                    return query;
            }
        }

        private static IQueryable<T> FilterByPrice<T>(IQueryable<T> query, decimal? value, NumericSearchComparators? comparator)
                            where T : PocoBase, IContainingProduct
        {
            if (value == null || comparator == null) return query;

            switch (comparator)
            {
                case NumericSearchComparators.GreaterThan:
                    return query.Where(x => x.Product.Price > value.Value);

                case NumericSearchComparators.GreaterThanOrEqualTo:
                    return query.Where(x => x.Product.Price >= value.Value);

                case NumericSearchComparators.EqualTo:
                    return query.Where(x => x.Product.Price == value.Value);

                case NumericSearchComparators.LessThanOrEqualTo:
                    return query.Where(x => x.Product.Price <= value.Value);

                case NumericSearchComparators.LessThan:
                    return query.Where(x => x.Product.Price < value.Value);

                default:
                    return query;
            }
        }

        private static IQueryable<T> FilterByQuantity<T>(IQueryable<T> query, decimal? value, NumericSearchComparators? comparator)
            where T : PocoBase, IContainingOrders
        {
            if (value == null || comparator == null) return query;

            switch (comparator)
            {
                case NumericSearchComparators.GreaterThan:
                    return query.Where(x => x.Orders.Any(y => y.Quantity > value.Value));

                case NumericSearchComparators.GreaterThanOrEqualTo:
                    return query.Where(x => x.Orders.Any(y => y.Quantity >= value.Value));

                case NumericSearchComparators.EqualTo:
                    return query.Where(x => x.Orders.Any(y => y.Quantity == value.Value));

                case NumericSearchComparators.LessThanOrEqualTo:
                    return query.Where(x => x.Orders.Any(y => y.Quantity <= value.Value));

                case NumericSearchComparators.LessThan:
                    return query.Where(x => x.Orders.Any(y => y.Quantity < value.Value));

                default:
                    return query;
            }
        }

        private static IQueryable<T> FilterByThreshold<T>(IQueryable<T> query, int? value, NumericSearchComparators? comparator)
                            where T : PocoBase, IContainingDiscounts
        {
            if (value == null || comparator == null) return query;

            switch (comparator)
            {
                case NumericSearchComparators.GreaterThan:
                    return query.Where(x => x.Discounts.Any(y => y.Threshold > value.Value));

                case NumericSearchComparators.GreaterThanOrEqualTo:
                    return query.Where(x => x.Discounts.Any(y => y.Threshold >= value.Value));

                case NumericSearchComparators.EqualTo:
                    return query.Where(x => x.Discounts.Any(y => y.Threshold == value.Value));

                case NumericSearchComparators.LessThanOrEqualTo:
                    return query.Where(x => x.Discounts.Any(y => y.Threshold <= value.Value));

                case NumericSearchComparators.LessThan:
                    return query.Where(x => x.Discounts.Any(y => y.Threshold < value.Value));

                default:
                    return query;
            }
        }
    }
}