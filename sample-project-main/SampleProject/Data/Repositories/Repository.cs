using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessEntities;
using Common;
using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Indexes;

namespace Data.Repositories
{
    [AutoRegister]
    public class Repository<T> : IRepository<T> where T : IdObject
    {
        private readonly IDocumentSession _documentSession;

        public Repository(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }

        public Task SaveAsync(T entity)
        {
            _documentSession.Store(entity);
            // RavenDB's session operations are synchronous, but you can wrap in Task for async signature
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity)
        {
            _documentSession.Delete(entity);
            return Task.CompletedTask;
        }

        public Task<T> GetAsync(Guid id)
        {
            var result = _documentSession.Load<T>(id);
            return Task.FromResult(result);
        }

        protected Task DeleteAllAsync<TIndex>() where TIndex : AbstractIndexCreationTask<T>
        {
            _documentSession.Advanced.DocumentStore.DatabaseCommands.DeleteByIndex(typeof(TIndex).Name, new IndexQuery());
            return Task.CompletedTask;
        }
    }
}