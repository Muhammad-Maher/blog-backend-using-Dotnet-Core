using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// ADDED NAMESPACES
using API.Models;
using Microsoft.EntityFrameworkCore;
//

namespace API.Repository
{
    public class postRepository:IPostRepository
    {
        Context db;
        public postRepository(Context _db)
        {
            db = _db;
        }

        
        // GET ALL POSTS
        public List<Post> getall()
        {
            return db.posts.ToList();
        }


        // GET POST BY ID
         public Post getbyid(int id)
        {
            return db.posts.Where(n => n.Id == id).FirstOrDefault();
        }


        // ADD POST
        public void add(Post pst)
        {
            db.posts.Add(pst);
            db.SaveChangesAsync();            
        }

        // EDIT COMMENT
        public void edit(Post pst)
        {
             db.Entry(pst).State = EntityState.Modified;
             db.SaveChanges();
             
        }

        // DELETE POST
        public void delete(Post pst)
        {
            db.posts.Remove(pst);
            db.SaveChanges();
        }
    }
}
