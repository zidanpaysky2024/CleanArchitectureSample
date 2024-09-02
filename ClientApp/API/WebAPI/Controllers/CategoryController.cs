using Architecture.Application.Categories.Commands.AddCategory;
using Architecture.Application.Categories.Queries.GetAllCategories;
using Architecture.Domain.Constants;
using Architecture.WebAPI.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Architecture.WebAPI.Controllers
{
    public class CategoryController : BaseApiController
    {
        #region Actions
        [HttpPost("add-category")]
        public async Task<IActionResult> AddCategpry([FromBody] AddCategoryCommand model)
        {
            return Result(await Mediator.Send(model));
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpPost("get-Category-list")]
        public async Task<IActionResult> GetCategoryList([FromBody] GetAllCategoriesQuery model)
        {
            return Result(await Mediator.Send(model));
        }
        #endregion
    }
}
