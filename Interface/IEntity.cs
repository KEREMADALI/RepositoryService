using System;

namespace RepositoryService.Interface
{
    public interface IEntity
    {
        /// <summary>
        /// Db Id
        /// </summary>
        string id { get; set; }

        /// <summary>
        /// Db creation date
        /// </summary>
        DateTime CreationDate { get; set; }
    }
}
