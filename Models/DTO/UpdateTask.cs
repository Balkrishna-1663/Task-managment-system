namespace Task_managment_system.Models.DTO
{
    public class UpdateTask
    {
        public string? Title { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public int Priority { get; set; } // 0 = Low, 1 = Medium, 2 = High
        public bool Status { get; set; } // false = Incomplete, true = Completed
    }
}
