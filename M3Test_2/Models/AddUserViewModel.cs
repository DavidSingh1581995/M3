using M3Test_2.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace M3Test_2.Models
{
    public class AddUserViewModel
    {
        public int id { get; set; }
        public HttpPostedFileBase ProfilePic { get; set; }
        public List<string> Qualification { get; set; }
        public string Email { get; set; }
        public string Uid { get; set; }

        //public DbSet<Country> Countries { get; set; }
       
    }
}