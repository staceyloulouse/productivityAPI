using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SendNotice.Data;
using SendNotice.Models;
using AutoMapper;
using SendNotice.Dtos;
using Microsoft.AspNetCore.JsonPatch;

namespace SendNotice.Controllers
{
    [Route("api/units")]
    [ApiController]
    public class UnitsController : ControllerBase
    {
        private readonly ISendNoticeRepo _repository;
        private readonly IMapper _mapper;

        public UnitsController(ISendNoticeRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UnitReadDto>> GetAllUnits()
        {
            var unitItems = _repository.GetUnits();
           return Ok(_mapper.Map<IEnumerable<UnitReadDto>>(unitItems)); 
        }
       
        [HttpGet("{id}")]
        public ActionResult<UnitReadDto> GetUnitById(int id)
        {
            var unitItem = _repository.GetUnitById(id);
           if(unitItem != null)
            {
                return Ok(_mapper.Map<UnitReadDto>(unitItem));
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult<UnitReadDto> CreateUnit(UnitCreateDto unitCreateDto)
        {
            var unitModel =  _mapper.Map<Unit>(unitCreateDto);
            _repository.InsertUnit(unitModel);
            _repository.SaveChanges();

            return Ok(unitModel);
        }
        
        [HttpPut("{id}")]
        public ActionResult UpdateUnit(int id, UnitUpdateDto unitUpdateDto)
        {
            var unitModel = _repository.GetUnitById(id);
            if (unitModel == null)
            {
                return NotFound();
            }

            _mapper.Map(unitUpdateDto, unitModel);
            _repository.UpdateUnit(unitModel);
            _repository.SaveChanges();
            return NoContent();
        }

        
        [HttpPatch("{id}")]
        public ActionResult PartialUnitUpdate(int id, JsonPatchDocument<UnitUpdateDto> patchUnit)
        {
            var unitModel = _repository.GetUnitById(id);
            if (unitModel == null)
            {
                return NotFound();
            }

            var unitToPatch = _mapper.Map<UnitUpdateDto>(unitModel);
            patchUnit.ApplyTo(unitToPatch, ModelState);
            if(!TryValidateModel(unitToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(unitToPatch,unitModel);
            _repository.UpdateUnit(unitModel);
            _repository.SaveChanges();
            return NoContent();
        }
    }
}