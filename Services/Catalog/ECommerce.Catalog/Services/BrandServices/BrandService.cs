using AutoMapper;
using ECommerce.Catalog.Dtos.BrandDtos;
using ECommerce.Catalog.Entities;
using ECommerce.Catalog.Settings;
using MongoDB.Driver;

namespace ECommerce.Catalog.Services.BrandServices
{
	public class BrandService : IBrandService
	{
		private readonly IMongoCollection<Brand> _brandCollection;
		private readonly IMapper _mapper;

		public BrandService(IMapper mapper, IDatabaseSettings _databaseSettings)
		{
			var client = new MongoClient(_databaseSettings.ConnectionString);
			var database = client.GetDatabase(_databaseSettings.DatabaseName);
			_brandCollection = database.GetCollection<Brand>(_databaseSettings.BrandCollectionName);

			_mapper = mapper;
		}

		public async Task CreateBrandAsync(CreateBrandDto createBrandDto)
		{
			var value = _mapper.Map<Brand>(createBrandDto);
			await _brandCollection.InsertOneAsync(value);

		}

		public async Task DeleteBrandAsync(string id)
		{
			await _brandCollection.DeleteOneAsync(c => c.BrandID == id);
		}

		public async Task<List<ResultBrandDto>> GetAllBrandAsync()
		{
			var values = await _brandCollection.Find(c => true).ToListAsync();

			return _mapper.Map<List<ResultBrandDto>>(values);
		}

		public async Task<GetByIdBrandDto> GetByIdBrandAsync(string id)
		{
			var value = await _brandCollection.Find<Brand>(c => c.BrandID == id).FirstOrDefaultAsync();

			return _mapper.Map<GetByIdBrandDto>(value);
		}

		public async Task UpdateBrandAsync(UpdateBrandDto updateBrandDto)
		{
			var value = _mapper.Map<Brand>(updateBrandDto);
			await _brandCollection.FindOneAndReplaceAsync(c => c.BrandID == updateBrandDto.BrandID, value);
		}
	}
}
