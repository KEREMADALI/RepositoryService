namespace RepositoryService.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IRepository<TSource>
        where TSource : IEntity
    {
        void Add(TSource entity);

        void Add(IEnumerable<TSource> entityList);

        void Delete(Expression<Func<TSource, bool>> predicate);

        Task<TSource> Find(Expression<Func<TSource, bool>> predicate);

        void Update(Expression<Func<TSource, bool>> predicate, TSource entity);

        Task<IEnumerable<TSource>> Get(Expression<Func<TSource, bool>> predicate);

        Task<int> Count(Expression<Func<TSource, bool>> predicate);

        Task<bool> Any(Expression<Func<TSource, bool>> predicate);

        void DropCollection();
    }
}
