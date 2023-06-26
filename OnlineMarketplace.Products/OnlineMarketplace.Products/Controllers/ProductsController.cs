using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineMarketplace.Products.Api.Contracts;
using OnlineMarketplace.Products.Api.Infrastructure;
using OnlineMarketplace.Products.Api.Mappers;
using OnlineMarketplace.Products.BL.Contracts.Commands;
using OnlineMarketplace.Products.BL.Contracts.Queries;

namespace OnlineMarketplace.Products.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? sellerId)
        {
            if (sellerId.HasValue)
            {
                var products = await _mediator.Send(new GetProductsBySellerQuery(sellerId.Value));

                return Ok(products);
            }

            var query = new GetProductsQuery();

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id));

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProductRequest request)
        {
            var command = request.ToCommand();
            var product = await _mediator.Send(command);

            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromMultiSource] UpdateProductRequest request)
        {
            var command = request.ToCommand();
            var product = await _mediator.Send(command);

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteProductCommand(id);

            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound();
            }

            return Ok($"Product: {id} successfully deleted");
        }
    }
}
