using api.Dtos;
using api.Errors;
using api.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepos;
        private readonly IGenericRepository<ProductBrand> _brandRepos;
        private readonly IGenericRepository<ProductType> _typeRepos;
        private readonly IMapper _mapper;
        public ProductsController(IGenericRepository<Product> productRepos, IGenericRepository<ProductBrand> brandRepos, IGenericRepository<ProductType> typeRepos, IMapper mapper)
        {
            _mapper = mapper;
            _typeRepos = typeRepos;
            _brandRepos = brandRepos;
            _productRepos = productRepos;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductDto>>> GetProducts([FromQuery]ProductSpecParams productParams)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
            var countSpec = new ProductWithFiltersForCountSpecification(productParams);
            var totalItems = await _productRepos.CountAsync(countSpec);
            var products = await _productRepos.ListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(products);

            return Ok(new Pagination<ProductDto>(productParams.PageIndex, productParams.PageSize, totalItems, data));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _productRepos.GetEntityWithSpec(spec);
            if(product == null) return NotFound(new ApiResponse(404));
            return _mapper.Map<Product, ProductDto>(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrandsAsync()
        {
            return Ok(await _brandRepos.GetAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypesAsync()
        {
            return Ok(await _typeRepos.GetAllAsync());
        }
    }
}