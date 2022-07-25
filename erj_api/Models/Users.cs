using System;
using Dapper.Contrib.Extensions;

namespace erj_api.Models
{
    [Table("Users")]
    public class Users
    {
        [ExplicitKey]
        public Guid Id { get; set; }
        public string UserName { get; set; } 
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
