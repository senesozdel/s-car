using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRenting.Entities
{
    public class VehicleWorkTimeModel
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int UserId { get; set; }
        public decimal ActiveHours { get; set; }
        public decimal MaintenanceHours { get; set; }

        public decimal IdleHours { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
