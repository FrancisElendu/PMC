namespace PMC.Application.Dtos
{
    // Drug DTO
    public class DrugDto
    {
        public int DrugId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SideEffects { get; set; }
        public string Interactions { get; set; }
        public int StockQuantity { get; set; }

        // Navigation property
        public ICollection<PrescriptionItemDto> PrescriptionItems { get; set; }
    }
}
