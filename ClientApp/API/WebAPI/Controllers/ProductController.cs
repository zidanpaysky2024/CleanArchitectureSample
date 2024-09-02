using Architecture.Application.Products.Commands.AddProduct;
using Architecture.Application.Products.Commands.ChangeProductItemAmount;
using Architecture.Application.Products.Commands.DeleteProduct;
using Architecture.Application.Products.Commands.UpdateProduct;
using Architecture.Application.Products.Queries.GetPagedProducts;
using Architecture.Domain.Constants;
using Architecture.WebAPI.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Architecture.WebAPI.Controllers
{
    public class ProductController : BaseApiController
    {
        #region Actions

        [HttpPost("add-Product")]
        public async Task<IActionResult> AddProdcut([FromBody] AddProductCommand model)
        {
            return Result(await Mediator.Send(model));
        }

        [HttpPost("update-Product")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand model)
        {
            return Result(await Mediator.Send(model));
        }

        [HttpPost("change-productItem-amount")]
        public async Task<IActionResult> ChangeProductItemAmount([FromBody] ChangeProductItemAmountCommand model)
        {
            return Result(await Mediator.Send(model));
        }

        [HttpPost("Delete-Product")]
        public async Task<IActionResult> DeleteProduct([FromBody] DeleteProductCommand model)
        {
            return Result(await Mediator.Send(model));
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpPost("get-product-list")]
        public async Task<IActionResult> GetProductList([FromBody] GetPagedProductsQuery model)
        {
            return Result(await Mediator.Send(model));
        }
        #endregion
    }
}
