using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoginUser;

namespace _3._13Routing.Models
{
    public class ViewModel
    {
        public IEnumerable<User> LoginUsers { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<Link> Alllink { get; set; }
        public IEnumerable<Link> linksByUser { get; set; }
        public IEnumerable<int> Upvotes { get; set; }
        //public IEnumerable<Link> linkToShow { get;set; }
    }
}