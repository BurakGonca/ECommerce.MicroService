using ECommerce.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using ECommerce.Order.Application.Interfaces;
using ECommerce.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers
{
    public class UpdateOrderDetailCommandHandler
    {
        private readonly IRepository<OrderDetail> _repository;

        public UpdateOrderDetailCommandHandler(IRepository<OrderDetail> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateOrderDetailCommand command)
        {
            var value = await _repository.GetByIdAsync(command.OrderDetailId);

            value.OrderingId = command.OrderingId;
            value.ProductId = command.ProductId;
            value.ProductPrice = command.ProductPrice;
            value.ProductName = command.ProductName;
            value.ProductTotalPrice = command.ProductTotalPrice;
            value.ProductAmount = command.ProductAmount;

            await _repository.UpdateAsync(value);

        }

    }
}
