using System;
using System.Collections.Generic;
using System.Linq;
using Cegeka.Guild.Pokeverse.Domain.Abstracts;
using Cegeka.Guild.Pokeverse.Domain.Entities;

namespace Cegeka.Guild.Pokeverse.Persistence.EntityFramework.Repositories
{
    public class GenericRepository<T> : IRepository<T>
        where T : Entity
    {
        private readonly PokeverseContext context;

        public GenericRepository(PokeverseContext context)
        {
            this.context = context;
        }

        public IEnumerable<T> GetAll() => context.Set<T>();

        public T GetById(Guid id) => context.Set<T>().FirstOrDefault(x => x.Id == id);

        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
            context.SaveChanges();
        }
    }
}