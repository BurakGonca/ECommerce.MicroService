using AutoMapper;
using ECommerce.Catalog.Dtos.ContactDtos;
using ECommerce.Catalog.Entities;
using ECommerce.Catalog.Settings;
using MongoDB.Driver;

namespace ECommerce.Catalog.Services.ContactServices
{
	public class ContactService : IContactService
	{
		private readonly IMongoCollection<Contact> _contactCollection;
		private readonly IMapper _mapper;

		public ContactService(IMapper mapper, IDatabaseSettings _databaseSettings)
		{
			var client = new MongoClient(_databaseSettings.ConnectionString);
			var database = client.GetDatabase(_databaseSettings.DatabaseName);
			_contactCollection = database.GetCollection<Contact>(_databaseSettings.ContactCollectionName);

			_mapper = mapper;
		}

		public async Task CreateContactAsync(CreateContactDto createContactDto)
		{
			var value = _mapper.Map<Contact>(createContactDto);
			await _contactCollection.InsertOneAsync(value);

		}

		public async Task DeleteContactAsync(string id)
		{
			await _contactCollection.DeleteOneAsync(c => c.ContactID == id);
		}

		public async Task<List<ResultContactDto>> GetAllContactAsync()
		{
			var values = await _contactCollection.Find(c => true).ToListAsync();

			return _mapper.Map<List<ResultContactDto>>(values);
		}

		public async Task<GetByIdContactDto> GetByIdContactAsync(string id)
		{
			var value = await _contactCollection.Find<Contact>(c => c.ContactID == id).FirstOrDefaultAsync();

			return _mapper.Map<GetByIdContactDto>(value);
		}

		public async Task UpdateContactAsync(UpdateContactDto updateContactDto)
		{
			var value = _mapper.Map<Contact>(updateContactDto);
			await _contactCollection.FindOneAndReplaceAsync(c => c.ContactID == updateContactDto.ContactID, value);
		}
	}
}
