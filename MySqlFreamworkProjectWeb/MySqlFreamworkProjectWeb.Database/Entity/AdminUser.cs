﻿using MySqlFreamworkProjectWeb.Database.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlFreamworkProjectWeb.Database.Entity
{
    public class AdminUser : InharitanceEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}