using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// ADDED NAMESPACES
using API.Models;
//


namespace API.Repository
{
   public interface ICommentRepository
    {
        List<Comment> getall();
        Comment getbyid(int id);
        void add(Comment cmnt);
        void edit(Comment cmnt);
        void delete(Comment cmnt);


    }
}
