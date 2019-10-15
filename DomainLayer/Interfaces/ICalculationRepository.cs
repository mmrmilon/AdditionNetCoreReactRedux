using DomainLayer.DataModel;
using DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Interfaces
{
    public interface ICalculationRepository
    {
        Task<IEnumerable<CalculationDataModel>> GetAll();

        Task<CalculationDataModel> GetBy(string userName);

        Task<Calculations> Get(long id);

        Task<Calculations> Insert(CalculationDataModel model);

        Task<Calculations> Update(Calculations model);

        Task<bool> DeleteBy(long id);
    }
}
