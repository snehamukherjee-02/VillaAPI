using AutoMapper;
using Azure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using practiceApi.Models;
using practiceApi.Models.Dto;
using practiceApi.Repository.IRepository;

namespace practiceApi.Controllers
{
    [Route("api/CommentAPI")]
    [ApiController]
    public class CommentAPIController : ControllerBase
    {
        private readonly ICommentRepository _dbCommentRep;
        private readonly IVillaRepository _dbVillaRepExist;
        private readonly IMapper _mapper;
        public CommentAPIController(ICommentRepository dbCommentRep, IVillaRepository dbVillaRepExist, IMapper mapper)
        {
            _dbCommentRep = dbCommentRep;
            _dbVillaRepExist = dbVillaRepExist;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetComments()
        {
            IEnumerable<Comment> objCommentList = await _dbCommentRep.GetAllAsync();
            return Ok(_mapper.Map<List<CommentDTO>>(objCommentList));
        }

        [HttpGet("{villaId:Guid}")]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetCommentsByVilla(Guid villaId)
        {
            var comments = await _dbCommentRep.GetAllAsync(c => c.VillaId == villaId);
            return Ok(_mapper.Map<List<CommentDTO>>(comments));
        }

        [HttpGet("{id:int}", Name = "GetComment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CommentDTO>> GetComment([FromRoute] int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            Comment? comment = await _dbCommentRep.GetAsync(u => u.Id == id);

            if(comment == null)
            {
                return NotFound();
            }

            CommentDTO commentdto = _mapper.Map<CommentDTO>(comment);

            return Ok(commentdto);
        }
        [HttpPost("{villaid:Guid}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CommentDTO>> CreateComment(Guid villaid,[FromBody]CommentCreateDTO createdto)
        {
            if(await _dbVillaRepExist.GetAsync(u => u.Id == villaid) == null)
            {
                return BadRequest("Villa doesn't exist");
            }
            if(createdto == null)
            {
                return BadRequest();
            }
            //if(await _dbCommentRep.GetAsync(u => u.Title == createdto.Title) != null)
            //{
            //    ModelState.AddModelError("CustomCommentError","Same type of Title already exists.");
            //    return BadRequest(ModelState);
            //}

            Comment comment = _mapper.Map<Comment>(createdto);

            comment.VillaId = villaid;

            await _dbCommentRep.CreateAsync(comment);

            CommentDTO commentdto = _mapper.Map<CommentDTO>(comment);

            return CreatedAtRoute("GetComment", new { id = commentdto.Id }, commentdto);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteComment(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            Comment? comment = await _dbCommentRep.GetAsync(u => u.Id == id);
            if(comment == null)
            {
                return NotFound();
            }
            await _dbCommentRep.RemoveAsync(comment);
            return NoContent();
        }
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateComment(int id, [FromBody]CommentUpdateDTO updatedto)
        {
            if(updatedto==null || id != updatedto.Id)
            {
                return BadRequest();
            }

            Comment comment = await _dbCommentRep.GetAsync(u => u.Id == id);

            if(comment == null)
            {
                return BadRequest();
            }

            Villa existingvillaid = await _dbVillaRepExist.GetAsync(u => u.Id == comment.VillaId);

            if (existingvillaid == null)
            {
                return BadRequest("The associated villa doesnot exist.");
            }

           _mapper.Map(updatedto, comment);

            await _dbCommentRep.UpdateAsync(comment);
            return NoContent();

        }
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdatePartialComment(int id, [FromBody] JsonPatchDocument<CommentUpdateDTO> patchdto)
        {
            if (patchdto == null || id == 0)
            {
                return BadRequest();
            }

            Comment comment = await _dbCommentRep.GetAsync(u => u.Id == id, tracked: false);

            if (comment == null)
            {
                return BadRequest();
            }

            Villa existingvillaid = await _dbVillaRepExist.GetAsync(u => u.Id == comment.VillaId);

            if (existingvillaid == null)
            {
                return BadRequest("The associated villa doesnot exist.");
            }

            CommentUpdateDTO updto = _mapper.Map<CommentUpdateDTO>(comment);

            patchdto.ApplyTo(updto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            updto.Id = comment.Id;

            _mapper.Map(updto, comment);

            await _dbCommentRep.UpdateAsync(comment);

            return NoContent();
        }
    }
}
