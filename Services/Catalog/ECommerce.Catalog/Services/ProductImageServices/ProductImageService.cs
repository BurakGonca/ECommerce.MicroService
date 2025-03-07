using AutoMapper;
using ECommerce.Catalog.Dtos.ProductImageDtos;
using ECommerce.Catalog.Entities;
using ECommerce.Catalog.Settings;
using MongoDB.Driver;

namespace ECommerce.Catalog.Services.ProductImageServices
{
    public class ProductImageService: IProductImageService
    {

        private readonly IMongoCollection<ProductImage> _productImageCollection;
        private readonly IMapper _mapper;

        public ProductImageService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _productImageCollection = database.GetCollection<ProductImage>(_databaseSettings.ProductImageCollectionName);

            _mapper = mapper;
        }

        public async Task CreateProductImageAsync(CreateProductImageDto createProductImageDto)
        {
            var value = _mapper.Map<ProductImage>(createProductImageDto);
            await _productImageCollection.InsertOneAsync(value);

        }

        public async Task DeleteProductImageAsync(string id)
        {
            await _productImageCollection.DeleteOneAsync(c => c.ProductImageId == id);
        }

        public async Task<List<ResultProductImageDto>> GetAllProductImageAsync()
        {
            var values = await _productImageCollection.Find(c => true).ToListAsync();

            return _mapper.Map<List<ResultProductImageDto>>(values);
        }

        public async Task<GetByIdProductImageDto> GetByIdProductImageAsync(string id)
        {
            var value = await _productImageCollection.Find<ProductImage>(c => c.ProductImageId == id).FirstOrDefaultAsync();

            return _mapper.Map<GetByIdProductImageDto>(value);
        }

		public async Task UpdateProductImageAsync(UpdateProductImageDto updateProductImageDto)
        {
            var value = _mapper.Map<ProductImage>(updateProductImageDto);
            await _productImageCollection.FindOneAndReplaceAsync(c => c.ProductImageId == updateProductImageDto.ProductImageId, value);
        }

        /// <summary>
        /// ProductID'sine göre ProductImage'leri getiren method.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
		public async Task<GetByIdProductImageDto> GetByProductIdProductImageAsync(string id)
		{
			var value = await _productImageCollection.Find(c => c.ProductId == id).FirstOrDefaultAsync();

			return _mapper.Map<GetByIdProductImageDto>(value);
		}


        



    }
}
