namespace Task_managment_system.Models.DTO
{
    public class Addtask
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public int Priority { get; set; } // 0 = Low, 1 = Medium, 2 = High
        public bool Status { get; set; } // false = Incomplete, true = Completed
        public DateTime CreatedAt { get; set; }
       
        // Foreign key reference to Category
        public int CategoryId { get; set; }
    }
}
