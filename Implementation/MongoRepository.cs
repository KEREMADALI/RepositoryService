namespace RepositoryService.Implementation
{
	using System.Collections.Generic;
	using System.Linq.Expressions;
	using System.Threading.Tasks;
	using System;
	using MongoDB.Driver;

	using RepositoryService.Interface;
	using MongoDB.Driver.Linq;
	using System.Linq;

	public class MongoRepository<TSource> : IRepository<TSource> 
		where TSource : IEntity
	{
		#region Private & Const Variables

		private readonly string _collectionName;
		private readonly IMongoDatabase _mongoDatabase;

		#endregion

		#region Public & Protected Variables

		public readonly IMongoCollection<TSource> Collection;

		#endregion

		#region Constructors

		public MongoRepository(IMongoDatabase mongoDatabase, string collectionName) 
		{
			_mongoDatabase = mongoDatabase;
			_collectionName = collectionName;
		}

		#endregion

		#region Private Methods

		#endregion

		#region Public & Protected Methods		

		/// <summary>
		/// Updates CreationDate to achieve correct results from Find()
		/// </summary>
		/// <param name="entity"></param>
		public async void Add(TSource entity) 
		{
			entity.CreationDate = DateTime.Now;
			await Collection.InsertOneAsync(entity);
		}

		/// <summary>
		/// Updates CreationDate to achieve correct results from Find()
		/// </summary>
		/// <param name="entityList"></param>
		public async void Add(IEnumerable<TSource> entityList) 
		{
			entityList.ToList().ForEach(x => x.CreationDate = DateTime.Now);
			await Collection.InsertManyAsync(entityList);
		}

		public async void Delete(Expression<Func<TSource, bool>> predicate) 
		{
			await Collection.DeleteManyAsync(predicate);
		}

		/// <summary>
		/// Acts as FirstOrDefault in LinQ
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		public async Task<TSource> Find(Expression<Func<TSource, bool>> predicate) 
		{
			return await Collection.AsQueryable().OrderBy(x => x.CreationDate).FirstOrDefaultAsync(predicate);
		}

		public async void Update(Expression<Func<TSource, bool>> predicate, TSource entity) 
		{
			await Collection.ReplaceOneAsync(predicate, entity);
		}

		/// <summary>
		/// Sorts before returning
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		public async Task<IEnumerable<TSource>> Get(Expression<Func<TSource, bool>> predicate) 
		{
			return await Collection.AsQueryable().OrderBy(x=> x.CreationDate).Where(predicate).ToListAsync();
		}

		public async Task<int> Count(Expression<Func<TSource, bool>> predicate = null) 
		{
			return predicate == null 
				? await Collection.AsQueryable().CountAsync() 
				: await Collection.AsQueryable().CountAsync(predicate);
		}

		public async Task<bool> Any(Expression<Func<TSource, bool>> predicate = null) 
		{
			return predicate == null
				? await Collection.AsQueryable().AnyAsync()
				: await Collection.AsQueryable().AnyAsync(predicate);
		}

		public async void DropCollection() 
		{
			await _mongoDatabase.DropCollectionAsync(_collectionName);
		}

		#endregion
	}
}
