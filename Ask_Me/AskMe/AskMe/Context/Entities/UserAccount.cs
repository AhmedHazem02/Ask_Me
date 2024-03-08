using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskMe.Context.Entities
{
    public class UserAccount
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public int ?Age {  get; set; }

        public string?Country {  get; set; }

        public bool Is_Unkown {  get; set; }


        [InverseProperty("FromUser")]
        public virtual ICollection<Question> Question_From { get; set; }

        [InverseProperty("ToUser")]
        public virtual ICollection<Question> Question_To { get; set; }
    }
}
