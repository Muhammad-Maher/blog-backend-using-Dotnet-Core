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
    public class commentRepository:ICommentRepository
    {
        Context db;
        public commentRepository(Context _db)
        {
            db = _db;
        }

        
        // GET ALL COMMENTS
        public List<Comment> getall()
        {
            return db.comments.ToList();
        }


        // GET COMMENT BY ID
         public Comment getbyid(int id)
        {
            return db.comments.Where(n => n.Id == id).FirstOrDefault();
        }


        // ADD COMMENT
        public void add(Comment cmnt)
        {
            db.comments.Add(cmnt);
            db.SaveChangesAsync();            
        }

        // EDIT COMMENT
        public void edit(Comment cmnt)
        {
             db.Entry(cmnt).State = EntityState.Modified;
             db.SaveChanges();
             
        }

        // DELETE COMMENT
        public void delete(Comment cmnt)
        {
            db.comments.Remove(cmnt);
            db.SaveChanges();
        }
    }
}
