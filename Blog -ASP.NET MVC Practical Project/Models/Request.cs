using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Blog__ASP.NET_MVC_Practical_Project.Models
{
    public class Request
    {

        public Request()
        {
            this.Date = DateTime.Now;
        }
        [Key]
        public int Id { get; set; }


        [Required]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public ApplicationUser Author { get; set; }


    }
}