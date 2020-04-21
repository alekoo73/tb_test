using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Core.Classes;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Web.Middleware;
using Web.Models;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public class PersonsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPersonService _personsService;
        private readonly IRelationService _relationService;
        private readonly IAppLogger<PersonsController> _logger;
        public readonly IStringLocalizer<PersonsController> Localizer;
        private readonly IWebHostEnvironment _env;

        public PersonsController(IMapper mapper, IPersonService personService, IRelationService relationService, IAppLogger<PersonsController> logger, IStringLocalizer<PersonsController> localizer, IWebHostEnvironment env)
        {
            this._personsService = personService;
            this._relationService = relationService;
            this._mapper = mapper;
            this._logger = logger;
            this.Localizer = localizer;
            this._env = env;
        }


        [HttpGet]
        [Route("error")]
        public async Task<IActionResult> TestException()
        {
            throw new Exception("something went wrong in action....");
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddPersonAsync([FromBody]PersonModel model)
        {
            Person person = _mapper.Map<Person>(model);
            await _personsService.AddPerson(person);
            return Ok();   
        }


        [HttpGet]
        [Route("records")]
        public async Task<ActionResult> GetRecordsAsync([FromQuery] QueryParameters queryParameters)
        {
            var result =await _personsService.GetRecordsAsync(queryParameters);            

            var paginationMetadata = new
            {
                totalCount = result.cnt,
                pageSize = queryParameters.PageCount,
                currentPage = queryParameters.Page,
                totalPages = queryParameters.GetTotalPages(result.cnt)
            };

            Response.Headers.Add("X-Pagination",JsonSerializer.Serialize(paginationMetadata));
            return Ok(result.records);
        }


        [HttpPost]
        [Route("addrelation")]
        public async Task<IActionResult> AddRelationAsync([FromBody]RelationModel model)
        {
            Relation relation = _mapper.Map<Relation>(model);
            await _relationService.AddRelation(relation);
            return Ok();
        
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdatePersonAsync([FromBody]PersonModel model)
        {
            Person person = _mapper.Map<Person>(model);
            await _personsService.UpdatePerson(person);
            return Ok();
       
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPersonAsync(int id)
        {

            Person person;
            if ((person=await _personsService.GetPersonAsync(id))==null)
            {
                return NotFound();
            }            
            return Ok(person);
        }


        [HttpGet]
        [Route("delete/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePersonAsync(int id)
        {

            if (!await _personsService.DeletePerson(id))
            {
                return NotFound();
            }
            return Ok();
        }



        [HttpGet]
        [Route("deleterelation/{id:int}/{id2:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePersonAsync(int id,int id2)
        {

            if (!await _relationService.DeleteRelation(id,id2))
            {
                return NotFound();
            }
            return Ok();
        }



        [HttpPost]
        [Route("upload/{id:int}")]
        public async Task<IActionResult> UploadPictureAsync([FromForm(Name = "file")] IFormFile file,int id)
        {
            

            string folderName = "Pictures";
            string webRootPath = _env.ContentRootPath;
            string newPath = Path.Combine(webRootPath, folderName);
            string fullPath = Path.Combine(newPath, file.FileName);
            string filepath =$@"{folderName}/{file.FileName}";
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }

            using (var stream = new FileStream(fullPath, FileMode.Create))
                await file.CopyToAsync(stream);

            await _personsService.UpdatePicture(filepath, id);

            return Ok(new { count = 1, path = filepath });
        }

        [HttpGet]
        [Route("report")]
        public dynamic Report()
        {
            return Ok(_personsService.GetReportData());
        }

    }
}

