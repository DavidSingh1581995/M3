using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M3Test_2.Models
{
    public class EditViewModel
    {
        public int id { get; set; }
        public string img { get; set; }
        public HttpPostedFileBase ProfilePic { get; set; }
        public List<string> Qualification { get; set; }
        public string Email { get; set; }
        public string Uid { get; set; }
    }
}