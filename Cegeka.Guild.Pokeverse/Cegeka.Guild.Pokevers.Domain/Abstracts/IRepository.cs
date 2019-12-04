using System;
using System.Collections.Generic;
using Cegeka.Guild.Pokevers.Domain.Entities;

namespace Cegeka.Guild.Pokevers.Domain.Abstracts
{
    public interface IRepository<T>
        where T : Entity
    {
        IEnumerable<T> GetAll();

        T GetById(Guid id);

        void Add(T entity);
    }
}