using AutoMapper;
using ECommerce.Catalog.Dtos.ProductDtos;
using ECommerce.Catalog.Entities;
using ECommerce.Catalog.Settings;
using MongoDB.Driver;

namespace ECommerce.Catalog.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMongoCollection<Category> _categoryCollection;



		public ProductService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _productCollection = database.GetCollection<Product>(_databaseSettings.ProductCollectionName);
			_categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
			_mapper = mapper;
        }


        public async Task CreateProductAsync(CreateProductDto createProductDto)
        {
            var value = _mapper.Map<Product>(createProductDto);
            await _productCollection.InsertOneAsync(value);
        }

        public async Task DeleteProductAsync(string id)
        {
            await _productCollection.DeleteOneAsync(p=>p.ProductID == id);
        }

        public async Task<List<ResultProductDto>> GetAllProductAsync()
        {
            var values = await _productCollection.Find(p=> true).ToListAsync();
            return _mapper.Map<List<ResultProductDto>>(values);
        }

        public async Task<GetByIdProductDto> GetByIdProductAsync(string id)
        {
            var value = await _productCollection.Find<Product>(p=>p.ProductID == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdProductDto>(value);
        }


		public async Task UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            var value = _mapper.Map<Product>(updateProductDto);
            await _productCollection.FindOneAndReplaceAsync(p => p.ProductID == updateProductDto.ProductID, value);
        }

        /// <summary>
        /// Ürünleri kategoriyle birliklte getirme methodudur.
        /// </summary>
        /// <returns></returns>
		public async Task<List<ResultProductsWithCategoryDto>> GetProductsWithCategoryAsync()
		{
			var values = await _productCollection.Find(p=>true).ToListAsync();
            foreach (var item in values)
            {
                item.Category = await _categoryCollection.Find<Category>(c=>c.CategoryID==item.CategoryId).FirstAsync();
            }

            return _mapper.Map<List<ResultProductsWithCategoryDto>>(values);

		}

        /// <summary>
        /// Ürünleri kategoriye göre filtre ederek getiren methoddur.
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
		public async Task<List<ResultProductsWithCategoryDto>> GetProductsWithCategoryByCategoryIdAsync(string categoryId)
		{
			var values = await _productCollection.Find(p => p.CategoryId==categoryId).ToListAsync();
			foreach (var item in values)
			{
				item.Category = await _categoryCollection.Find<Category>(c => c.CategoryID == item.CategoryId).FirstAsync();
			}

			return _mapper.Map<List<ResultProductsWithCategoryDto>>(values);
		}



	}
}
