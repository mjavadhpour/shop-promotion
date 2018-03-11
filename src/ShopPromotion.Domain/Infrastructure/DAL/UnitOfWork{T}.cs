// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;
using System.Threading.Tasks;

namespace ShopPromotion.Domain.Infrastructure.DAL
{
    using EntityLayer;
    using Models.Resource;
    using Services;

    /// <summary>
    /// The unit of work class serves one purpose: to make sure that when you use multiple repositories, they share a
    /// single database context.
    /// </summary>
    /// <remarks>
    /// Used for default entity services.
    /// </remarks>
    public class UnitOfWork<TForm, TMinimumTListResource, TMinimumTResource, T> : IDisposable
        where T : BaseEntity
        where TForm : BaseEntity
        where TMinimumTResource : MinimumBaseEntity
    {
        private readonly ShopPromotionDomainContext _context;
        private readonly IBaseService<TForm, TMinimumTListResource, TMinimumTResource, T, ShopPromotionDomainContext> _targetEntityService;

        public UnitOfWork(ShopPromotionDomainContext context,
            IBaseService<TForm, TMinimumTListResource, TMinimumTResource, T, ShopPromotionDomainContext> baseService)
        {
            _context = context;
            _targetEntityService = baseService;
        }

        /// <summary>
        /// Get instance of resolved repository.
        /// </summary>
        /// <returns></returns>
        public IBaseService<TForm, TMinimumTListResource, TMinimumTResource, T,ShopPromotionDomainContext> GenericRepository()
        {
            return _targetEntityService;
        }

        /// <summary>
        /// Commit with the resolved context.
        /// </summary>
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}