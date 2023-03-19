using Features.Auth.Comedor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Core.AuthUser
{
    public class User: UserComedor
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        
        public string Token { get; set; }
        public Membresia Membresia { get; set; }
    }
}
