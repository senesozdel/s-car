namespace VehicleRenting.Entities
{
    public class VehicleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PlateNumber { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
