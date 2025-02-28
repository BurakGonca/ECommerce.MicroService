using AutoMapper;
using ECommerce.Catalog.Dtos.CategoryDtos;
using ECommerce.Catalog.Dtos.SpecialOfferDtos;
using ECommerce.Catalog.Entities;
using ECommerce.Catalog.Settings;
using MongoDB.Driver;

namespace ECommerce.Catalog.Services.SpecialOfferServices
{
	public class SpecialOfferService : ISpecialOfferService
	{

		private readonly IMongoCollection<SpecialOffer> _specialOfferCollection;
		private readonly IMapper _mapper;

		public SpecialOfferService(IMapper mapper, IDatabaseSettings _databaseSettings)
		{
			var client = new MongoClient(_databaseSettings.ConnectionString);
			var database = client.GetDatabase(_databaseSettings.DatabaseName);
			_specialOfferCollection = database.GetCollection<SpecialOffer>(_databaseSettings.SpecialOfferCollectionName);

			_mapper = mapper;
		}

		public async Task CreateSpecialOfferAsync(CreateSpecialOfferDto createSpecialOfferDto)
		{
			var value = _mapper.Map<SpecialOffer>(createSpecialOfferDto);
			await _specialOfferCollection.InsertOneAsync(value);
		}

		public async Task DeleteSpecialOfferAsync(string id)
		{
			await _specialOfferCollection.DeleteOneAsync(c => c.SpecialOfferId == id);
		}

		public async Task<List<ResultSpecialOfferDto>> GetAllSpecialOfferAsync()
		{
			var values = await _specialOfferCollection.Find(c => true).ToListAsync();

			return _mapper.Map<List<ResultSpecialOfferDto>>(values);
		}

		public async Task<GetByIdSpecialOfferDto> GetByIdSpecialOfferAsync(string id)
		{
			var value = await _specialOfferCollection.Find<SpecialOffer>(c => c.SpecialOfferId == id).FirstOrDefaultAsync();

			return _mapper.Map<GetByIdSpecialOfferDto>(value);
		}

		public async Task UpdateSpecialOfferAsync(UpdateSpecialOfferDto updateSpecialOfferDto)
		{
			var value = _mapper.Map<SpecialOffer>(updateSpecialOfferDto);
			await _specialOfferCollection.FindOneAndReplaceAsync(c => c.SpecialOfferId == updateSpecialOfferDto.SpecialOfferId, value);
		}
	}
}
