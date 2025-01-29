using ECommerce.Cargo.BusinessLayer.Abstract;
using ECommerce.Cargo.DtoLayer.CargoOperationDtos;
using ECommerce.Cargo.EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Cargo.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoOperationsController : ControllerBase
    {
        private readonly ICargoOperationService _operationService;

        public CargoOperationsController(ICargoOperationService operationService)
        {
            _operationService = operationService;
        }


        [HttpGet]
        public IActionResult CargoOperationList()
        {
            return Ok(_operationService.TGetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetCargoOperationById(int id)
        {
            var value = _operationService.TGetById(id);
            return Ok(value);
        }

        [HttpPost]
        public IActionResult CreateCargoOperation(CreateCargoOperationDto dto)
        {
            CargoOperation cargoOperation = new CargoOperation()
            {
                Barcode = dto.Barcode,
                OperationDate = dto.OperationDate,
                Description = dto.Description

            };
            _operationService.TInsert(cargoOperation);
            return Ok("Kargo islemi basariyla eklendi.");
        }

        [HttpDelete]
        public IActionResult RemoveCargoOperation(int id)
        {
            _operationService.TDelete(id);
            return Ok("Kargo islemi basariyla silindi");
        }

        [HttpPut]
        public IActionResult UpdateCargoOperation(UpdateCargoOperationDto dto)
        {
            CargoOperation cargoOperation = new CargoOperation()
            {
                CargoOperationId = dto.CargoOperationId,
                OperationDate = dto.OperationDate,
                Description = dto.Description,
                Barcode =   dto.Barcode
                
            };
            _operationService.TUpdate(cargoOperation);
            return Ok("Kargo islemi basariyla düzenlendi.");
        }
    }
}
