using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using practiceApi.Data;
using practiceApi.Logging;
using practiceApi.Models;
using practiceApi.Models.Dto;
using practiceApi.Repository.IRepository;

namespace practiceApi.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        //private readonly ILogging _logger;
        //public VillaAPIController(ILogging logger)
        //{
        //    _logger = logger;
        //}

        private readonly IVillaRepository _dbVillaRep;
        private readonly IMapper _mapper;
        public VillaAPIController(IVillaRepository dbVillaRep, IMapper mapper)
        {
            _dbVillaRep = dbVillaRep;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
        {
            //_logger.Log("Get villa list","");
            IEnumerable<Villa> objvillaList = await _dbVillaRep.GetAllAsync(includes: v=>v.Comments, sort:v=>v.CreatedDate);
            return Ok(_mapper.Map<IEnumerable<VillaDTO>>(objvillaList));
        }
        [HttpGet("{id:Guid}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200,Type= typeof(VillaDTO))]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(404)]
        public async Task<ActionResult<VillaDTO?>> GetVilla(Guid id)
        {
            if (id == null)
            {
                //_logger.Log("Get villa with Id=" + id + " not exist" , "error");
                return BadRequest();
            }
            Villa? villa = await _dbVillaRep.GetAsync(u => u.Id == id, includes: v=>v.Comments);

            if (villa == null)
            {
                return NotFound();
            }
            VillaDTO villadto = _mapper.Map<VillaDTO>(villa);
            return Ok(villadto);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDTO>> CreateVilla([FromBody] VillaCreateDTO createdto)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            if (await _dbVillaRep.GetAsync(u => u.Name == createdto.Name) != null)
            {
                ModelState.AddModelError("CutomError", "Villa name already exist");
                return BadRequest(ModelState);
            }

            if (createdto == null)
            {
                return BadRequest();
            }

            Villa model = _mapper.Map<Villa>(createdto);

            //Villa model = new ()
            //{
            //    Name = createdto.Name,
            //    Details = createdto.Details,
            //    Rate = createdto.Rate,
            //    Sqft = createdto.Sqft,
            //    Occupency = createdto.Occupency,
            //    Imageurl = createdto.Imageurl,
            //    Amenity = createdto.Amenity
            //};

            await _dbVillaRep.CreateAsync(model);

            VillaDTO villadto = _mapper.Map<VillaDTO>(model);

            return CreatedAtRoute("GetVilla", new { id = villadto.Id }, villadto);
        }

        // File upload --- start

        [HttpPost("upload-image")]
        [HttpPut("upload-image")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file selected for upload.");

            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

            if (!Directory.Exists(uploadsPath))
                Directory.CreateDirectory(uploadsPath);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Return the URL to the uploaded image
            var fileUrl = $"{Request.Scheme}://{Request.Host}/uploads/{fileName}";
            return Ok(new { url = fileUrl });
        }

        // File upload --- end

        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteVilla(Guid id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Villa? villa = await _dbVillaRep.GetAsync(u => u.Id == id);
            if (villa == null)
            {
                return BadRequest();
            }
            await _dbVillaRep.RemoveAsync(villa);
            return NoContent();
        }
        [HttpPut("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVilla(Guid id, [FromBody] VillaUpdateDTO? updatedto)
        {
            if (updatedto == null || id != updatedto.Id)
            {
                return BadRequest();
            }

            Villa model = _mapper.Map<Villa>(updatedto);

            //Villa model = new()
            //{
            //    Id = updatedto.Id,
            //    Name = updatedto.Name,
            //    Details = updatedto.Details,
            //    Rate = updatedto.Rate,
            //    Sqft = updatedto.Sqft,
            //    Occupency = updatedto.Occupency,
            //    Imageurl = updatedto.Imageurl,
            //    Amenity = updatedto.Amenity,
            //};

            model.UpdatedDate = DateTime.Now;

            await _dbVillaRep.UpdateAsync(model);

            return NoContent();
        }
        [HttpPatch("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialVilla(Guid id, [FromBody]JsonPatchDocument<VillaUpdateDTO> patchvilladto)
        {
            if (patchvilladto == null || id==null)
            {
                return BadRequest();
            }
            Villa? villa = await _dbVillaRep.GetAsync(u => u.Id == id, tracked:false);

            VillaUpdateDTO updatedto = _mapper.Map<VillaUpdateDTO>(villa);

            //VillaUpdateDTO updatedto = new()
            //{
            //    Id = villa.Id,
            //    Name = villa.Name,
            //    Details = villa.Details,
            //    Rate = villa.Rate,
            //    Sqft = villa.Sqft,
            //    Occupency = villa.Occupency,
            //    Imageurl = villa.Imageurl,
            //    Amenity = villa.Amenity,
            //};

            if (villa == null)
            {
                return BadRequest();
            }
            patchvilladto.ApplyTo(updatedto, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Villa model = _mapper.Map<Villa>(updatedto);

            //Villa model = new()
            //{
            //    Id = updatedto.Id,
            //    Name = updatedto.Name,
            //    Details = updatedto.Details,
            //    Rate = updatedto.Rate,
            //    Sqft = updatedto.Sqft,
            //    Occupency = updatedto.Occupency,
            //    Imageurl = updatedto.Imageurl,
            //    Amenity = updatedto.Amenity,
            //};

            model.UpdatedDate = DateTime.Now;

            await _dbVillaRep.UpdateAsync(model);
            return NoContent();
        }
    }
}
