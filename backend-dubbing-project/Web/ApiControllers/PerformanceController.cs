using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.DTOs;

namespace SoftServe.ITAcademy.BackendDubbingProject.Web.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerformanceController : ControllerBase
    {
        private readonly IAdministrationService _administrationMicroservice;

        public PerformanceController(IAdministrationService administrationMicroservice)
        {
            _administrationMicroservice = administrationMicroservice;
        }

        /// <summary>Controller method for getting a list of all performances.</summary>
        /// <returns>List of all performances.</returns>
        /// <response code="200">Is returned when the list has at least one performance.</response>
        /// <response code="404">Is returned when the list of performances is empty.</response>
        [HttpGet]
        public async Task<ActionResult<List<PerformanceDTO>>> GetAll()
        {
            var listOfPerformanceDTOs = await _administrationMicroservice.GetAllPerformancesAsync();

            return Ok(listOfPerformanceDTOs);
        }

        /// <summary>Controller method for getting a performance by id.</summary>
        /// <param name="id">Id of performance that need to receive.</param>
        /// <returns>The performance with the following id.</returns>
        /// <response code="200">Is returned when performance does exist.</response>
        /// <response code="404">Is returned when performance with such Id doesn't exist.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<PerformanceDTO>> GetById(int id)
        {
            var performanceDTO = await _administrationMicroservice.GetPerformanceByIdAsync(id);

            if (performanceDTO == null)
                return NotFound();

            return Ok(performanceDTO);
        }

        /// <summary>Controller method for creating new performance.</summary>
        /// <param name="performanceDTO">Performance model which needed to create.</param>
        /// <returns>Status code and performance.</returns>
        /// <response code="201">Is returned when performance is successfully created.</response>
        /// <response code="400">Is returned when invalid data is passed.</response>
        /// <response code="409">Is returned when performance with such parameters already exists.</response>
        [HttpPost]
        public async Task<ActionResult<PerformanceDTO>> Create(PerformanceDTO performanceDTO)
        {
            await _administrationMicroservice.CreatePerformanceAsync(performanceDTO);

            return CreatedAtAction(nameof(GetById), new {id = performanceDTO.Id}, performanceDTO);
        }

        /// <summary>Controller method for updating an already existing performance with following id.</summary>
        /// <param name="id">Id of the performance that is needed to be updated.</param>
        /// <param name="performanceDTO">The performance model that is needed to be created.</param>
        /// <returns>Status code and optionally exception message.</returns>
        /// <response code="204">Is returned when performance is successfully updated.</response>
        /// <response code="400">Is returned when performance with or invalid data is passed.</response>
        /// <response code="404">Is returned when performance with such Id is not founded</response>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, PerformanceDTO performanceDTO)
        {
            if (performanceDTO.Id != id)
                BadRequest();

            await _administrationMicroservice.UpdatePerformanceAsync(id, performanceDTO);

            return NoContent();
        }

        /// <summary>Controller method for deleting an already existing performance with following id.</summary>
        /// <param name="id">Id of the performance that needed to delete.</param>
        /// <returns>Status code</returns>
        /// <response code="204">Is returned when performance is successfully deleted.</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _administrationMicroservice.DeletePerformanceAsync(id);

            return NoContent();
        }

        /// <summary>Controller method for getting a speeches by id of performance.</summary>
        /// <param name="id">Id of performance which speeches that need to receive.</param>
        /// <returns>List of a speeches.</returns>
        /// <response code="200">Is returned when speeches does exist.</response>
        /// <response code="400">Is returned when performance with such Id doesn't exist.</response>
        /// <response code="404">Is returned when speeches doesn't exist.</response>
        [HttpGet("{id}/speeches")]
        public async Task<ActionResult<List<SpeechDTO>>> GetByIdWithChildren(int id)
        {
            var listOfSpeechDTOs = await _administrationMicroservice.GetSpeechesByPerformanceIdAsync(id);

            if (listOfSpeechDTOs == null)
                return BadRequest($"Performance with Id: {id} doesn't exist!");

            return Ok(listOfSpeechDTOs);
        }

        /// <summary>dd</summary>
        /// <param name="id"></param>
        /// <returns>ff</returns>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="404"></response>
        [HttpGet("{id}/languages")]
        public async Task<ActionResult<List<LanguageDTO>>> GetByIdLanguages(int id)
        {
            var listOfLanguagesDTOs = await _administrationMicroservice.GetLanguagesByPerformanceIdAsync(id);

            return Ok(listOfLanguagesDTOs);
        }
    }
}