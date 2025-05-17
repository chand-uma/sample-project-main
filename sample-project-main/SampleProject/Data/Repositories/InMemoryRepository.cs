using BusinessEntities;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    /// <summary>
    /// Generic in-memory repository for managing entities of type T.
    /// </summary>
    /// <typeparam name="T">The entity type, must inherit from IdObject.</typeparam>
    public class InMemoryRepository<T> where T : IdObject
    {
        private readonly List<T> _storage = new List<T>();

        /// <summary>
        /// Asynchronously retrieves an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <returns>The entity if found; otherwise, null.</returns>
        public async Task<T> GetAsync(Guid id)
        {
            return await Task.FromResult(_storage.FirstOrDefault(x => (x as dynamic).Id == id));
        }

        /// <summary>
        /// Asynchronously retrieves all entities.
        /// </summary>
        /// <returns>An enumerable of all entities in storage.</returns>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Task.FromResult(_storage.AsEnumerable());
        }

        /// <summary>
        /// Asynchronously adds a new entity to the in-memory storage.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        public async Task CreateAsync(T entity)
        {
            _storage.Add(entity);
            await Task.CompletedTask;
        }

        /// <summary>
        /// Asynchronously updates an existing entity in the in-memory storage.
        /// </summary>
        /// <param name="entity">The entity with updated values.</param>
        public async Task UpdateAsync(T entity)
        {
            var existing = await GetAsync((entity as dynamic).Id);
            if (existing != null)
            {
                _storage.Remove(existing);
                _storage.Add(entity);
            }
            await Task.CompletedTask;
        }

        /// <summary>
        /// Asynchronously deletes an entity from the in-memory storage.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        public async Task DeleteAsync(T entity)
        {
            if (entity == null)
                return;

            var existing = _storage.FirstOrDefault(x => (x as dynamic).Id == (entity as dynamic).Id);
            if (existing != null)
            {
                _storage.Remove(existing);
            }
            await Task.CompletedTask;
        }
    }
}
