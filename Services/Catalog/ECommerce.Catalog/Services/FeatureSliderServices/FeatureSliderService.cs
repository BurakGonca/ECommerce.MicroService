using AutoMapper;
using ECommerce.Catalog.Dtos.CategoryDtos;
using ECommerce.Catalog.Dtos.FeatureSliderDtos;
using ECommerce.Catalog.Entities;
using ECommerce.Catalog.Settings;
using MongoDB.Driver;

namespace ECommerce.Catalog.Services.FeatureSliderServices
{
	public class FeatureSliderService : IFeatureSliderService
	{
		private readonly IMongoCollection<FeatureSlider> _featureSliderCollection;
		private readonly IMapper _mapper;

		public FeatureSliderService(IMapper mapper, IDatabaseSettings _databaseSettings)
		{
			var client = new MongoClient(_databaseSettings.ConnectionString);
			var database = client.GetDatabase(_databaseSettings.DatabaseName);
			_featureSliderCollection = database.GetCollection<FeatureSlider>(_databaseSettings.FeatureSliderCollectionName);

			_mapper = mapper;
		}

		public async Task CreateFeatureSliderAsync(CreateFeatureSliderDto createFeatureSliderDto)
		{
			var value = _mapper.Map<FeatureSlider>(createFeatureSliderDto);
			await _featureSliderCollection.InsertOneAsync(value);
		}

		public async Task DeleteFeatureSliderAsync(string id)
		{
			await _featureSliderCollection.DeleteOneAsync(c => c.FeatureSliderID == id);
		}

		public Task FeatureSliderChangeStatusToFalse(string id)
		{
			throw new NotImplementedException();
		}

		public Task FeatureSliderChangeStatusToTrue(string id)
		{
			throw new NotImplementedException();
		}

		public async Task<List<ResultFeatureSliderDto>> GetAllFeatureSliderAsync()
		{
			var values = await _featureSliderCollection.Find(f => true).ToListAsync();

			return _mapper.Map<List<ResultFeatureSliderDto>>(values);
		}

		public async Task<GetByIdFeatureSliderDto> GetByIdFeatureSliderAsync(string id)
		{
			var value = await _featureSliderCollection.Find<FeatureSlider>(f => f.FeatureSliderID == id).FirstOrDefaultAsync();

			return _mapper.Map<GetByIdFeatureSliderDto>(value);
		}

		public async Task UpdateFeatureSliderAsync(UpdateFeatureSliderDto updateFeatureSliderDto)
		{
			var value = _mapper.Map<FeatureSlider>(updateFeatureSliderDto);
			await _featureSliderCollection.FindOneAndReplaceAsync(f => f.FeatureSliderID == updateFeatureSliderDto.FeatureSliderID, value);
		}
	}
}
