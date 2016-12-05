using DataTransferObjects.Filters.Concrete;
using HireRight.EntityFramework.CodeFirst.Abstract;
using System.Linq;

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

            //TBI

            return query;
        }

        internal static IQueryable<T> FilterByAdmin<T>(this IQueryable<T> query, ContactFilter filter)
            where T : PocoBase, IContainingAdmin
        {
            if (filter == null)
                return query;

            //TBI

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

            //TBI

            return query;
        }

        internal static IQueryable<T> FilterByLocations<T>(this IQueryable<T> query, LocationFilter filter)
                                            where T : PocoBase, IContainingLocations
        {
            if (filter == null)
                return query;

            //TBI

            return query;
        }

        internal static IQueryable<T> FilterByOrders<T>(this IQueryable<T> query, OrderFilter filter)
                                            where T : PocoBase, IContainingOrders
        {
            if (filter == null)
                return query;

            //TBI

            return query;
        }

        internal static IQueryable<T> FilterByPrimaryContact<T>(this IQueryable<T> query, ContactFilter filter)
                                    where T : PocoBase, IContainingContact
        {
            if (filter == null)
                return query;

            //TBI

            return query;
        }

        internal static IQueryable<T> FilterByProduct<T>(this IQueryable<T> query, ProductFilter filter)
                                            where T : PocoBase, IContainingProduct
        {
            if (filter == null)
                return query;

            //TBI

            return query;
        }
    }
}