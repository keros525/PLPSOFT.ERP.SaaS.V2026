using Microsoft.AspNetCore.Mvc;
using PLPSOFT.ERP.Domain.Entities;
using PLPSOFT.ERP.Module.Sales.Repositories;

namespace PLPSOFT.ERP.WebApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductRepository _repo;
        public ProductsController(ProductRepository repo) => _repo = repo;

        [HttpGet("{companyId}")]
        public async Task<IActionResult> Get(long companyId)
        {
            // Gọi hàm lấy danh sách sản phẩm từ Repository
            var data = await _repo.GetAllAsync(companyId);
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product model)
        {
            try
            {
                // Gán các giá trị mặc định để không bị lỗi Database
                model.CompanyID = 1;
                model.ProductTypeID = 1;

                var result = await _repo.InsertAsync(model);
                return Ok(new { success = true, message = "Thêm thành công!" });
            }
            catch (Exception ex)
            {
                return BadRequest("Lỗi khi lưu: " + ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _repo.DeleteAsync(id);
            return Ok(new { success = true, message = "Đã xóa sản phẩm!" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] Product model)
        {
            try
            {
                model.ProductID = id; // Đảm bảo ID khớp với đường dẫn URL
                var result = await _repo.UpdateAsync(model);
                return Ok(new { success = true, message = "Cập nhật thành công!" });
            }
            catch (Exception ex)
            {
                return BadRequest("Lỗi khi cập nhật: " + ex.Message);
            }
        }
    }

}