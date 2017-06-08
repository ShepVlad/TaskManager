using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _01_Client.Models.DbSets
{
    public class User
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public string Surname { get; set; }
        public int RoleId { get; set; }
        public string Title { get; set; }

    }
}