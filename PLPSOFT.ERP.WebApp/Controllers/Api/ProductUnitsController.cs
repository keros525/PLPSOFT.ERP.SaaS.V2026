using Microsoft.AspNetCore.Mvc;
using PLPSOFT.ERP.Module.Sales.Repositories;

namespace PLPSOFT.ERP.WebApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductUnitsController : ControllerBase
    {
        private readonly ProductUnitRepository _repo;
        public ProductUnitsController(ProductUnitRepository repo) => _repo = repo;

        [HttpGet("{companyId}")]
        public async Task<IActionResult> Get(long companyId)
        {
            var data = await _repo.GetAllAsync(companyId);
            return Ok(data);
        }
    }
}