using System.Net;
using Catalog.Application.Commands;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

public class CatalogController: BaseController
{
    private readonly IMediator _mediator;

    public CatalogController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get a Product By Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>ProductResponse</returns>
    [HttpGet, Route("[action]/{id}", Name = "GetProductById")]
    [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ProductResponse>> GetProductById(string id)
    {
        var query = new GetProductByIdQuery(id: id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    /// <summary>
    /// Get a Product By ProductName
    /// </summary>
    /// <param name="productName"></param>
    /// <returns>List of ProductResponse</returns>
    [HttpGet, Route("[action]/{productName}", Name = "GetProductByProductName")]
    [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IList<ProductResponse>>> GetProductByProductName(string productName)
    {
        var query = new GetProductByNameQuery(name: productName);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    /// <summary>
    /// Get all products
    /// </summary>
    /// <returns>List of ProductResponse</returns>
    [HttpGet, Route("GetAllProducts")]
    [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IList<ProductResponse>>> GetAllProducts()
    {
        var query = new GetAllProductsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    /// <summary>
    /// Get all brands
    /// </summary>
    /// <returns>List of BrandResponse</returns>
    [HttpGet, Route("GetAllBrands")]
    [ProducesResponseType(typeof(IList<BrandResponse>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IList<BrandResponse>>> GetAllBrands()
    {
        var query = new GetAllBrandsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    /// <summary>
    /// Get all types
    /// </summary>
    /// <returns>List of TypesResponse</returns>
    [HttpGet, Route("GetAllTypes")]
    [ProducesResponseType(typeof(IList<TypesResponse>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IList<TypesResponse>>> GetAllTypes()
    {
        var query = new GetAllTypesQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }


    /// <summary>
    /// Get a Product By Brand Name
    /// </summary>
    /// <param name="brandName"></param>
    /// <returns>List of ProductResponse</returns>
    [HttpGet, Route("[action]/{brandName}", Name = "GetProductByBrandName")]
    [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IList<ProductResponse>>> GetProductByBrandName(string brandName)
    {
        var query = new GetProductByBrandQuery(brandname: brandName);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Create a product
    /// </summary>
    /// <param name="productCommand"></param>
    /// <returns>ProductResponse</returns>
    [HttpPost, Route("CreateProduct")]
    [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ProductResponse>> CreateProduct([FromBody] CreateProductCommand productCommand)
    {
        var result = await _mediator.Send(productCommand);
        return Ok(result);
    }
    
    
    /// <summary>
    /// Update a product
    /// </summary>
    /// <param name="productCommand"></param>
    /// <returns>ProductResponse</returns>
    [HttpPut, Route("UpdateProduct")]
    [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand productCommand)
    {
        var result = await _mediator.Send(productCommand);
        return Ok(result);
    }

    /// <summary>
    /// Delete a product
    /// </summary>
    /// <param name="id"></param>
    /// <returns>ProductResponse</returns>
    [HttpPut, Route("{id}",Name = "DeleteProduct")]
    [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteProduct(string id)
    {
        var query = new DeleteProductByIdQuery(id: id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}