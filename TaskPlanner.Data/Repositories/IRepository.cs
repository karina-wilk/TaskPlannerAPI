﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TaskPlanner.Data.Repositories
{
    public interface IRepository<T>
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
        IQueryable<T> GetAll();
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        IEnumerable<T> SaveOrUpdateAll(params T[] entities);
        T SaveOrUpdate(T entity);
    }
}
