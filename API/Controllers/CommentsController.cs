using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// ADDED NAMESPACES
using API.Models;
using API.Repository;
//

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        ICommentRepository cmntRepo;

        public CommentsController(ICommentRepository _cmntRepo)
        {
            cmntRepo = _cmntRepo;
        }

        [HttpPost]
        public ActionResult<string> addComment(Comment cmnt)
        {
            cmntRepo.add(cmnt);

            return "Comment created successfully";
        }

        [HttpGet]
        public ActionResult getComments()
        {
            return Ok(cmntRepo.getall());
        }
         
        [HttpGet("{Id}")]
        public ActionResult<Comment> getCommentbyId(int Id)
        {
            var cmnt = cmntRepo.getbyid(Id);

            if (cmnt == null)            
                return NotFound();            

            return cmnt;
        }

        [HttpPut("{Id}")]
        public ActionResult<string> editComment(int Id, Comment cmnt)
        {
            if (Id != cmnt.Id)
            {
                return BadRequest();
            }

            try
            {
                cmntRepo.edit(cmnt);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (cmntRepo.getbyid(Id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return "Comment edited successfully";
        }

        [HttpDelete("{Id}")]
        public ActionResult<string> DeleteComment(int Id)
        {
            var cmnt = cmntRepo.getbyid(Id);
            if (cmnt == null)
            {
                return NotFound();
            }

            cmntRepo.delete(cmnt);

            return "Comment deleted successfully";
        }

    }
}
