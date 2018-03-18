// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;
using System.Linq;
using System.Linq.Expressions;

namespace ShopPromotion.Domain.Extensions
{
    using EntityLayer;
    using Infrastructure.Models.Parameter;

    public static class Extensions
    {
        /// <summary>
        /// Order the IQueryable by the given property or field.
        /// </summary>
        /// <typeparam name="T">The type of the IQueryable being ordered.</typeparam>
        /// <param name="queryable">The IQueryable being ordered.</param>
        /// <param name="propertyOrFieldName">
        /// The name of the property or field to order by.
        /// </param>
        /// <param name="ascending">
        /// Indicates whether or not
        /// the order should be ascending (true) or descending (false.)
        /// </param>
        /// <returns>Returns an IQueryable ordered by the specified field.</returns>
        public static IQueryable<T> OrderByPropertyOrField<T>
            (this IQueryable<T> queryable, string propertyOrFieldName, bool ascending = true)
        {
            var elementType = typeof(T);
            var orderByMethodName = ascending ? "OrderBy" : "OrderByDescending";

            var parameterExpression = Expression.Parameter(elementType);
            var propertyOrFieldExpression =
                Expression.PropertyOrField(parameterExpression, propertyOrFieldName);
            var selector = Expression.Lambda(propertyOrFieldExpression, parameterExpression);

            var orderByExpression = Expression.Call(typeof(Queryable), orderByMethodName,
                new[] {elementType, propertyOrFieldExpression.Type}, queryable.Expression, selector);

            return queryable.Provider.CreateQuery<T>(orderByExpression);
        }

        /// <summary>
        /// Filter the IQueryable by given option from given <see cref="Infrastructure.Models.Parameter.DateFilterParameterOptions"/>.
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="createDate"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">When given <see cref="Infrastructure.Models.Parameter.DateFilterParameterOptions"/> is out of range.</exception>
        public static IQueryable<T> FilterByCreateDate<T>(this IQueryable<T> queryable,
            DateFilterParameterOptions createDate) where T : BaseEntity
        {
            var dateNow = DateTime.Now;

            // Filter by create date.
            switch (createDate)
            {
                case DateFilterParameterOptions.LastHour:
                    queryable = queryable.Where(x =>
                        x.CreatedAt >= dateNow.AddHours(-1));
                    break;
                case DateFilterParameterOptions.ThisMonth:
                    queryable = queryable.Where(x =>
                        x.CreatedAt >= dateNow.AddMonths(-1));
                    break;
                case DateFilterParameterOptions.ThisWeek:
                    queryable = queryable.Where(x =>
                        x.CreatedAt >= dateNow.AddDays(-7));
                    break;
                case DateFilterParameterOptions.ThisYear:
                    queryable = queryable.Where(x =>
                        x.CreatedAt >= dateNow.AddYears(-1));
                    break;
                case DateFilterParameterOptions.Today:
                    queryable = queryable.Where(x =>
                        x.CreatedAt >= dateNow.AddDays(-1));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return queryable;
        }

        /// <summary>
        /// Generate unique 6 digit number.
        /// </summary>
        /// <returns></returns>
        public static string GenerateNewUniqueRandom(int maxValue = 1000000, int length = 6)
        {
            Random generator = new Random();
            String r = generator.Next(0, maxValue).ToString($@"D{length}");
            if (r.Distinct().Count() == 1)
            {
                r = GenerateNewUniqueRandom();
            }
            return r;
        }

        /// <summary>
        /// Is in given radius or not.
        /// </summary>
        /// <param name="originLatitude"></param>
        /// <param name="originLongitude"></param>
        /// <param name="destinationLatitude"></param>
        /// <param name="destinationLongitude"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static bool IsInRadius(double? originLatitude, double? originLongitude, double? destinationLatitude,
            double? destinationLongitude, int radius)
        {
            if (originLatitude == null || originLongitude == null || destinationLatitude == null ||
                destinationLongitude == null) return false;

            
            return GeoCalculator.GetDistance(originLatitude ?? 0, originLongitude ?? 0, destinationLatitude ?? 0,
                       destinationLongitude ?? 0) < radius;
        }
    }
}