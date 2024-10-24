namespace Task_managment_system.Models
{
    public class Category
    {
            public int CategoryId { get; set; }
            public string Name { get; set; }

            // Navigation property for related tasks
            public ICollection<Atask> Tasks { get; set; }
        

    }
}
