namespace RepositoryService.Implementation
{
    using MongoDB.Driver;

    using RepositoryService.Data;
	using RepositoryService.Interface;

	/// <summary>
	/// This class can be used for bussiness logic specific db calls
	/// </summary>
	public class RepositoryManager
    {
		#region Private & Const Variables

		private const string s_ConnectionString = "localhost:27017";
		private const string s_DatabaseName = "Entities";
		private const string s_DummyEntityCollection = "DummyEntity";

		#endregion

		#region Public & Protected Variables

		/// <summary>
		/// Singleton
		/// </summary>
        public static RepositoryManager Default = new RepositoryManager();

		/// <summary>
		/// Repository sample
		/// </summary>
		public IRepository<DummyEntity> DummyEntityRepository { get; }

		#endregion

		#region Constructors

		/// <summary>
		/// Called in project
		/// </summary>
		public RepositoryManager() 
		{
			var mongoClient = new MongoClient(s_ConnectionString);
			var mongoDb = mongoClient.GetDatabase(s_DatabaseName);
			DummyEntityRepository = new MongoRepository<DummyEntity>(mongoDb, s_DummyEntityCollection);
		}

		/// <summary>
		/// Used to Mock repositories
		/// </summary>
		/// <param name="dummyEntityRepository"></param>
		public RepositoryManager(IRepository<DummyEntity> dummyEntityRepository) 
		{
			DummyEntityRepository = dummyEntityRepository;
			Default = this;
		}

		#endregion

		#region Private Methods

		#endregion

		#region Public & Protected Methods		

		#endregion
	}
}
