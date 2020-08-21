using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.DTOs;

namespace SoftServe.ITAcademy.BackendDubbingProject.Web.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly IAdministrationService _administrationMicroservice;

        public LanguageController(IAdministrationService administrationMicroservice)
        {
            _administrationMicroservice = administrationMicroservice;
        }

        /// <summary>Controller method for getting a list of all languages.</summary>
        /// <returns>List of all languages.</returns>
        /// <response code="200">Is returned when the list has at least one language.</response>
        /// <response code="404">Is returned when the list of languages is empty.</response>
        [HttpGet]
        public async Task<ActionResult<List<LanguageDTO>>> GetAll()
        {
            var listOfLanguageDTOs = await _administrationMicroservice.GetAllLanguagesAsync();

            return Ok(listOfLanguageDTOs);
        }

        /// <summary>Controller method for getting a language by id.</summary>
        /// <param name="id">Id of language that need to receive.</param>
        /// <returns>The language with the following id.</returns>
        /// <response code="200">Is returned when language does exist.</response>
        /// <response code="404">Is returned when language with such Id doesn't exist.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<LanguageDTO>> GetById(int id)
        {
            var languageDTO = await _administrationMicroservice.GetLanguageByIdAsync(id);

            if (languageDTO == null)
                return NotFound();

            return Ok(languageDTO);
        }

        /// <summary>Controller method for creating new language.</summary>
        /// <param name="languageDTO">Language model which needed to create.</param>
        /// <returns>Status code and Language.</returns>
        /// <response code="201">Is returned when language is successfully created.</response>
        /// <response code="400">Is returned when invalid data is passed.</response>
        /// <response code="409">Is returned when language with such parameters already exists.</response>
        [HttpPost]
        public async Task<ActionResult<LanguageDTO>> Create(LanguageDTO languageDTO)
        {
            await _administrationMicroservice.CreateLanguageAsync(languageDTO);

            return CreatedAtAction(nameof(GetById), new {id = languageDTO.Id}, languageDTO);
        }

        /// <summary>Controller method for updating an already existing language with following id.</summary>
        /// <param name="id">Id of the language that is needed to be updated.</param>
        /// <param name="languageDTO">The language model that is needed to be created.</param>
        /// <returns>Status code and optionally exception message.</returns>
        /// <response code="204">Is returned when language is successfully updated.</response>
        /// <response code="400">Is returned when language with or invalid data is passed.</response>
        /// <response code="404">Is returned when language with such Id is not founded</response>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, LanguageDTO languageDTO)
        {
            if (languageDTO.Id != id)
                BadRequest();

            await _administrationMicroservice.UpdateLanguageAsync(id, languageDTO);

            return NoContent();
        }

        /// <summary>Controller method for deleting an already existing language with following id.</summary>
        /// <param name="id">Id of the language that needed to delete.</param>
        /// <returns>Status code</returns>
        /// <response code="204">Is returned when language is successfully deleted.</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _administrationMicroservice.DeleteLanguageAsync(id);

            return NoContent();
        }
    }
}