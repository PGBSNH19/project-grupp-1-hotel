using Hotel.Server.Persistence;
using Hotel.Server.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Server.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        protected readonly HotelContext ctx;
        public BaseRepository(HotelContext ctx) => this.ctx = ctx;

        public async Task AddAsync<T>(T entity) where T : class => await ctx.AddAsync(entity);
        public void Remove<T>(T entity) where T : class => ctx.Remove(entity);
        public void Update<T>(T entity) where T : class => ctx.Update(entity);
        public async Task<bool> Complete() => await ctx.SaveChangesAsync() > 0;
    }
}
