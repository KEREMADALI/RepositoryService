namespace RepositoryService.Data
{
    using RepositoryService.Interface;
    using System;

    public class AbstractEntity : IEntity
    {
        public string id { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
