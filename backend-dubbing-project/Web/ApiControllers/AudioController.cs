using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.DTOs;

namespace SoftServe.ITAcademy.BackendDubbingProject.Web.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AudioController : ControllerBase
    {
        private readonly IAdministrationService _administrationMicroservice;

        public AudioController(IAdministrationService administrationMicroservice)
        {
            _administrationMicroservice = administrationMicroservice;
        }

        /// <summary>Controller method for getting a list of all audios.</summary>
        /// <returns>List of all audios.</returns>
        /// <response code="200">Is returned when the list has at least one audio.</response>
        /// <response code="404">Is returned when the list of audios is empty.</response>
        [HttpGet]
        public async Task<ActionResult<List<AudioDTO>>> GetAll()
        {
            var listOfAudiosDTOs = await _administrationMicroservice.GetAllAudiosAsync();

            return Ok(listOfAudiosDTOs);
        }

        /// <summary>Controller method for getting a audio by id.</summary>
        /// <param name="id">Id of audio that need to receive.</param>
        /// <returns>The audio with the following id.</returns>
        /// <response code="200">Is returned when audio does exist.</response>
        /// <response code="404">Is returned when audio with such Id doesn't exist.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<AudioDTO>> GetById(int id)
        {
            var audioDTO = await _administrationMicroservice.GetAudioByIdAsync(id);

            if (audioDTO == null)
                return NotFound();

            return Ok(audioDTO);
        }

        /// <summary>Controller method for creating new audio.</summary>
        /// <param name="audioDTO">Audio model which needed to create.</param>
        /// <returns>Status code and audio.</returns>
        /// <response code="201">Is returned when audio is successfully created.</response>
        /// <response code="400">Is returned when invalid data is passed.</response>
        /// <response code="409">Is returned when audio with such parameters already exists.</response>
        [HttpPost]
        public async Task<ActionResult<AudioDTO>> Create(AudioDTO audioDTO)
        {
            await _administrationMicroservice.CreateAudioAsync(audioDTO);

            return CreatedAtAction(nameof(GetById), new {id = audioDTO.Id}, audioDTO);
        }

        /// <summary>Controller method for uploading a file to server and saving it to a local storage.</summary>
        /// <returns>Status code, URL of audio file and audio model.</returns>
        /// <param name="audioFileDTO">Audio file which needed to create.</param>
        /// <response code="201">Is returned when audio is successfully uploaded.</response>
        /// <response code="400">Is returned when invalid data is passed.</response>
        [HttpPost("upload")]
        public async Task<ActionResult<AudioFileDTO>> Upload([FromForm] AudioFileDTO audioFileDTO)
        {
            await _administrationMicroservice.UploadAudioAsync(audioFileDTO);

            var urlOfAudioFile = HttpContext.Request.Host.Value + "/audio/" + audioFileDTO.File.FileName;

            return Created(urlOfAudioFile, audioFileDTO);
        }

        /// <summary>Controller method for updating an already existing audio with following id.</summary>
        /// <param name="id">Id of the audio that is needed to be updated.</param>
        /// <param name="audioDTO">The audio model to which is needed to be updated existing audio.</param>
        /// <returns>Status code and optionally exception message.</returns>
        /// <response code="204">Is returned when speech is successfully updated.</response>
        /// <response code="400">Is returned when speech with or invalid data is passed.</response>
        /// <response code="404">Is returned when speech with such Id is not founded</response>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, AudioDTO audioDTO)
        {
            if (audioDTO.Id != id)
                BadRequest();

            await _administrationMicroservice.UpdateAudioAsync(id, audioDTO);

            return NoContent();
        }

        /// <summary>
        /// Unload audio from server
        /// </summary>
        /// <param name="files"></param>
        /// <returns>Delete</returns>
        [HttpDelete("unload")]
        public ActionResult Delete([FromQuery] string[] files)
        {
            _administrationMicroservice.DeleteAudioFiles(files);

            return NoContent();
        }

        /// <summary>
        /// Delete info about audio from DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Delete</returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _administrationMicroservice.DeleteFileAsync(id);

            return NoContent();
        }

        /// <summary>Controller method for uploading a file to server and saving it to a local storage.</summary>
        /// <returns>Status code, URL of audio file and audio model.</returns>
        /// <param name="audioFileDTO">Audio file which needed to create.</param>
        /// <response code="201">Is returned when audio is successfully uploaded.</response>
        /// <response code="400">Is returned when invalid data is passed.</response>
        [HttpPost("upload/waiting")]
        public async Task<ActionResult<AudioFileDTO>> UploadWaiting([FromForm] AudioFileDTO audioFileDTO)
        {
            await _administrationMicroservice.UploadWaitingAudioAsync(audioFileDTO);

            var urlOfAudioFile = HttpContext.Request.Host.Value + "/audio/" + audioFileDTO.File.FileName;

            return Created(urlOfAudioFile, audioFileDTO);
        }
    }
}