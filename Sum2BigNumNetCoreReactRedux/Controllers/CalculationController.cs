using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainLayer.DataModel;
using DomainLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Sum2BigNumNetCoreReactRedux.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculationController : ControllerBase
    {
        private readonly ICalculationRepository calculationRepository;

        public CalculationController(ICalculationRepository calculationRepository)
        {
            this.calculationRepository = calculationRepository;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await calculationRepository.GetAll();

            return Ok(result);
        }

        [HttpGet]
        [Route("GetBy")]
        public async Task<IActionResult> GetByUserName(string userName)
        {
            var result = await calculationRepository.GetBy(userName);

            return Ok(result);
        }

        [HttpPost]
        [Route("Insert")]
        public async Task<IActionResult> Insert([FromBody] CalculationDataModel model)
        {
            return Ok(await calculationRepository.Insert(model));
        }
    }
}