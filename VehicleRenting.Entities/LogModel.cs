﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRenting.Entities
{
   public class LogModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int VehicleId { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsDeleted { get; set; } = false;


    }
}
