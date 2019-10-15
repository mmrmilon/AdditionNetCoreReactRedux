using DomainLayer.DataModel;
using DomainLayer.Entities;
using DomainLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Implement
{
    public class CalculationRepository : ICalculationRepository
    {
        private readonly DatabaseContext context;
        private readonly IRepository<Users> userRepository;
        private readonly IAddition addition;

        public CalculationRepository(DatabaseContext context, IRepository<Users> userRepository, IAddition addition)
        {
            this.context = context;
            this.userRepository = userRepository;
            this.addition = addition;
        }

        public async Task<IEnumerable<CalculationDataModel>> GetAll()
        {
            var result = await (from c in context.Calculations
                                join u in context.Users on c.UserId equals u.Id
                                select new CalculationDataModel
                                {
                                    CalculationId = c.Id,
                                    UserId = u.Id,
                                    UserName = u.UserName,
                                    FirstNumber = c.FirstNumber,
                                    SecondNumber = c.SecondNumber,
                                    Summation = c.Summation,
                                    CalculatedOn = c.CalculatedOn
                                }).ToArrayAsync();

            return result;
        }

        public async Task<CalculationDataModel> GetBy(string userName)
        {
            var result = (from c in context.Calculations
                          join u in context.Users on c.UserId equals u.Id
                          where u.UserName.Equals(userName)
                          select new CalculationDataModel
                          {
                              CalculationId = c.Id,
                              UserId = u.Id,
                              UserName = u.UserName,
                              FirstNumber = c.FirstNumber,
                              SecondNumber = c.SecondNumber,
                              Summation = c.Summation,
                              CalculatedOn = c.CalculatedOn
                          }).ToAsyncEnumerable().Where(x => x.UserName.Equals(userName)).OrderByDescending(x => x.CalculationId);

            return await result.FirstOrDefault();
        }

        public async Task<Calculations> Get(long id)
        {
            return await context.Calculations.Where(x=>x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Calculations> Insert(CalculationDataModel model)
        {
            var result = userRepository.Get(x => x.UserName == model.UserName).Result;
            if (result == null)
            {
                var insert = new Users
                {
                    UserName = model.UserName,
                    CreatedOn = DateTime.Now,
                    IsActive = true
                };
                var inserUser = await userRepository.Insert(insert);
                model.UserId = inserUser.Id;
            }
            else
                model.UserId = result.Id;

            var calculation = new Calculations
            {
                UserId = model.UserId,
                FirstNumber = model.FirstNumber,
                SecondNumber = model.SecondNumber,
                Summation = addition.SumOfTwoBigNumbers(model.FirstNumber, model.SecondNumber),
                CalculatedOn = DateTime.Now
            };

            context.Calculations.Add(calculation);
            await context.SaveChangesAsync();

            return calculation;
        }

        public async Task<Calculations> Update(Calculations model)
        {
            model.CalculatedOn = DateTime.Now;

            context.Set<Calculations>().Update(model);
            await context.SaveChangesAsync();

            return model;
        }

        public async Task<bool> DeleteBy(long id)
        {
            var result = Get(id);
            if (result != null)
            {
                context.Calculations.Remove(await result);
                context.SaveChanges();
                return true;
            }
            else
                return false;
        }
    }
}
