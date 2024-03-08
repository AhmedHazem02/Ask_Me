using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskMe.Context.Entities
{
    public class Question
    {
        [Key]
        public int Id_Question { get; set; }
        public string Body { get; set; }
        public string? Answer {  get; set; }

        public int?AnonymouseQuestion { get; set; }
        public Question?Thread {  get; set; }

        [ForeignKey("From_User")]
        public int From_ID {  get; set; }
        public virtual UserAccount FromUser { get; set; }

        [ForeignKey("To_User")]
        public int To_ID {  get; set; }
        public virtual UserAccount ToUser { get; set; }


    }
}
