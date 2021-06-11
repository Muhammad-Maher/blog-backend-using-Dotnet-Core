using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// ADDED NAMESPACES
using API.Models;
//


namespace API.Repository
{
   public interface IPostRepository
    {
        List<Post> getall();
        Post getbyid(int id);
        void add(Post pst);
        void edit(Post pst);
        void delete(Post pst);


    }
}
