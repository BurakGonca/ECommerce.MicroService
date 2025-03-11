using AutoMapper;
using ECommerce.Catalog.Dtos.ProductDetailDtos;
using ECommerce.Catalog.Dtos.ProductImageDtos;
using ECommerce.Catalog.Entities;
using ECommerce.Catalog.Settings;
using MongoDB.Driver;

namespace ECommerce.Catalog.Services.ProductDetailServices
{
    public class ProductDetailService: IProductDetailService
    {

        private readonly IMapper _mapper;
        private readonly IMongoCollection<ProductDetail> _productDetailCollection;


        public ProductDetailService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _productDetailCollection = database.GetCollection<ProductDetail>(_databaseSettings.ProductDetailCollectionName);

            _mapper = mapper;
        }


        public async Task CreateProductDetailAsync(CreateProductDetailDto createProductDetailDto)
        {
            var value = _mapper.Map<ProductDetail>(createProductDetailDto);
            await _productDetailCollection.InsertOneAsync(value);
        }

        public async Task DeleteProductDetailAsync(string id)
        {
            await _productDetailCollection.DeleteOneAsync(p => p.ProductDetailId == id);
        }

        public async Task<List<ResultProductDetailDto>> GetAllProductDetailAsync()
        {
            var values = await _productDetailCollection.Find(p => true).ToListAsync();
            return _mapper.Map<List<ResultProductDetailDto>>(values);
        }

        public async Task<GetByIdProductDetailDto> GetByIdProductDetailAsync(string id)
        {
            var value = await _productDetailCollection.Find<ProductDetail>(p => p.ProductDetailId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdProductDetailDto>(value);
        }

        public async Task UpdateProductDetailAsync(UpdateProductDetailDto updateProductDetailDto)
        {
            var value = _mapper.Map<ProductDetail>(updateProductDetailDto);
            await _productDetailCollection.FindOneAndReplaceAsync(p => p.ProductDetailId == updateProductDetailDto.ProductDetailId, value);
        }

		/// <summary>
		/// ProductID'sine göre ProductDetail'leri getiren method.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task<GetByIdProductDetailDto> GetByProductIdProductDetailAsync(string id)
		{
			var value = await _productDetailCollection.Find(c => c.ProductId == id).FirstOrDefaultAsync();

			return _mapper.Map<GetByIdProductDetailDto>(value);
		}



	}
}
