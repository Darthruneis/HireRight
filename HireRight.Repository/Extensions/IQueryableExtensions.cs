using DataTransferObjects.Filters;
using DataTransferObjects.Filters.Abstract;
using DataTransferObjects.Filters.Concrete;
using HireRight.EntityFramework.CodeFirst.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace HireRight.Repository.Extensions
{
    internal static class IQueryableExtensions
    {
        internal static IQueryable<TContainerModel> FilterByAnyContaining<TContainerModel, TCollectionModel>(this IQueryable<TContainerModel> query, string value, Expression<Func<TContainerModel, ICollection<TCollectionModel>>> collection, Expression<Func<TCollectionModel, string>> property)
        {
            return query.Where(x => (value == null) || collection.Compile().Invoke(x).Any(y => property.Compile().Invoke(y).Contains(value)));
        }

        internal static IQueryable<TContainerModel> FilterByAnyContaining<TContainerModel, TPropertyType>(this IQueryable<TContainerModel> query, ICollection<TPropertyType> collection, Expression<Func<TContainerModel, TPropertyType>> property)
        {
            return query.Where(x => !collection.Any() || !collection.Contains(property.Compile().Invoke(x)));
        }

        internal static IQueryable<TModel> FilterByAtLeast<TModel, TPropertyType>(this IQueryable<TModel> query, TPropertyType value, Expression<Func<TModel, TPropertyType>> property)
            where TPropertyType : IComparable
        {
            return value == null ? query : FilterByExpression(query, Expression.GreaterThanOrEqual(property, Expression.Constant(value)));
        }

        internal static IQueryable<TModel> FilterByAtMost<TModel, TPropertyType>(this IQueryable<TModel> query, TPropertyType value, Expression<Func<TModel, TPropertyType>> property)
            where TPropertyType : IComparable
        {
            return value == null ? query : FilterByExpression(query, Expression.LessThanOrEqual(property, Expression.Constant(value)));
        }

        internal static IQueryable<TModel> FilterByContains<TModel>(this IQueryable<TModel> query, string value, Expression<Func<TModel, string>> property)
        {
            if (value == null) return query;

            ParameterExpression parameter = Expression.Parameter(typeof(TModel));

            Expression<Func<TModel, bool>> expression = Expression.Lambda<Func<TModel, bool>>(Expression.Call(Expression.Constant(value), typeof(ICollection<string>).GetMethod("Contains"),
                                                                         (MemberExpression)property.Body), parameter);

            return query.Where(expression);
        }

        internal static IQueryable<TModel> FilterByExact<TModel, TPropertyType>(this IQueryable<TModel> query, TPropertyType value, Expression<Func<TModel, TPropertyType>> property)
            where TPropertyType : IComparable
        {
            return value == null ? query : FilterByExpression(query, Expression.Equal(property, Expression.Constant(value)));
        }

        internal static IQueryable<TModel> FilterByGreaterThan<TModel, TPropertyType>(this IQueryable<TModel> query, TPropertyType value, Expression<Func<TModel, TPropertyType>> property)
            where TPropertyType : IComparable
        {
            return value == null ? query : FilterByExpression(query, Expression.GreaterThan(property, Expression.Constant(value)));
        }

        internal static IQueryable<TModel> FilterByGuids<TModel>(this IQueryable<TModel> query, Filter<TModel> filter)
            where TModel : PocoBase
        {
            return query.Where(x => !filter.ItemGuids.Any() || filter.ItemGuids.Contains(x.Id));
        }

        internal static IQueryable<TModel> FilterByLessThan<TModel, TPropertyType>(this IQueryable<TModel> query, TPropertyType value, Expression<Func<TModel, TPropertyType>> property)
            where TPropertyType : IComparable
        {
            return value == null ? query : FilterByExpression(query, Expression.LessThan(property, Expression.Constant(value)));
        }

        internal static IQueryable<TModel> FilterByNumericComparator<TModel, TPropertyType>(this IQueryable<TModel> query, NumericSearchComparators? comparator, TPropertyType? value, Expression<Func<TModel, TPropertyType?>> property)
            where TPropertyType : struct, IComparable
        {
            if ((value == null) || (comparator == null)) return query;

            switch (comparator)
            {
                case NumericSearchComparators.GreaterThan:
                    return FilterByExpression(query, Expression.GreaterThan(property, Expression.Constant(value)));

                case NumericSearchComparators.GreaterThanOrEqualTo:
                    return FilterByExpression(query, Expression.GreaterThanOrEqual(property, Expression.Constant(value)));

                case NumericSearchComparators.EqualTo:
                    return FilterByExpression(query, Expression.Equal(property, Expression.Constant(value)));

                case NumericSearchComparators.LessThan:
                    return FilterByExpression(query, Expression.LessThan(property, Expression.Constant(value)));

                case NumericSearchComparators.LessThanOrEqualTo:
                    return FilterByExpression(query, Expression.LessThanOrEqual(property, Expression.Constant(value)));

                default:
                    return query;
            }
        }

        private static Expression<Func<TModel, bool>> CreateExpression<TModel, TPropertyType>(Expression<Func<TModel, TPropertyType>> property, TPropertyType value, string methodName)
        {
            PropertyInfo propertyInfo = (PropertyInfo)((MemberExpression)property.Body).Member;

            ParameterExpression parameter = Expression.Parameter(typeof(TModel));

            return Expression.Lambda<Func<TModel, bool>>(Expression.Call(Expression.Constant(value), typeof(ICollection<TPropertyType>).GetMethod(methodName),
                                                                         (MemberExpression)property.Body), parameter);
        }

        private static IQueryable<TModel> FilterByExpression<TModel>(IQueryable<TModel> query, BinaryExpression expression)
        {
            return query.Where((Expression<Func<TModel, bool>>)expression.Conversion);
        }
    }
}