namespace PMC.Domain.Entities
{
    // Drug entity
    public class Drug
    {
        public int DrugId { get; set; } // PK
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? SideEffects { get; set; }
        public string? Interactions { get; set; }
        public int StockQuantity { get; set; }

        // Navigation property
        public virtual ICollection<PrescriptionItem>? PrescriptionItems { get; set; } // 1 Drug -> N PrescriptionItems
    }
}
