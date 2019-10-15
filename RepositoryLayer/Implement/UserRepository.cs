using System;
using System.Collections.Generic;
using System.Linq;
using DomainLayer.Entities;
using DomainLayer.Interfaces;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer.Implement
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext context;

        public UserRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Users>> GetAllAsync()
        {
            return await context.Users.OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<Users> Get(long id)
        {
            return await context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Users> Insert(Users model)
        {
            model.CreatedOn = DateTime.Now;

            context.Users.Add(model);
            await context.SaveChangesAsync();

            return model;
        }
    }
}
