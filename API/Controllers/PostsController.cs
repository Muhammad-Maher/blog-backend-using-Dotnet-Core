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
    public class PostsController : ControllerBase
    {
        IPostRepository pstRepo;

        public PostsController(IPostRepository _pstRepo)
        {
            pstRepo = _pstRepo;
        }

        [HttpPost]
        public ActionResult<string> addPost(Post pst)
        {
            pstRepo.add(pst);

            return "Post created successfully";
        }

        [HttpGet]
        public ActionResult getPosts()
        {
            return Ok(pstRepo.getall());
        }

        [HttpGet("{Id}")]
        public ActionResult<Post> getPostbyId(int Id)
        {
            var pst = pstRepo.getbyid(Id);

            if (pst == null)            
                return NotFound();            

            return pst;
        }

        [HttpPut("{Id}")]
        public ActionResult<string> editPost(int Id, Post pst)
        {
            if (Id != pst.Id)
            {
                return BadRequest();
            }

            try
            {
                pstRepo.edit(pst);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (pstRepo.getbyid(Id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return "Post edited successfully";
        }

        [HttpDelete("{Id}")]
        public ActionResult<string> DeletePost(int Id)
        {
            var pst = pstRepo.getbyid(Id);
            if (pst == null)
            {
                return NotFound();
            }

            pstRepo.delete(pst);

            return "Post deleted successfully";
        }

    }
}
