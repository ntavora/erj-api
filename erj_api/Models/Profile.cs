using System;
using Dapper.Contrib.Extensions;

namespace erj_api.Models
{
    [Table("Profile")]
    public class Profile
    {
        [ExplicitKey]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int Orden { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Dni { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime BirthdayDate { get; set; }
        public string MaritalStatus { get; set; } 
        public string ChurchName { get; set; }
        public string BelieverTime { get; set; }
        public bool LetterOfRecommendation { get; set; }
        public string BriefTestimony { get; set; }
        public bool IsActiveMember { get; set; }
        public bool IsBaptized { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
            
    }
}
