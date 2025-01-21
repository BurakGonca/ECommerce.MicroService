using ECommerce.Order.Application.Features.CQRS.Results.AddressResults;
using ECommerce.Order.Application.Interfaces;
using ECommerce.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Order.Application.Features.CQRS.Handlers.AddressHandlers
{
    public class GetAddressQueryHandler
    {

        private readonly IRepository<Address> _repository;

        public GetAddressQueryHandler(IRepository<Address> repository)
        {
            _repository = repository;
        }

        public async Task<List<GetAddressQueryResult>> Handle()
        {
            var values = await _repository.GetAllAsync();
            return values.Select(v => new GetAddressQueryResult
            {
                AddressId=v.AddressId,
                City=v.City,
                Detail=v.Detail,
                District=v.District,
                UserId=v.UserId

            }).ToList();
        }


    }
}
