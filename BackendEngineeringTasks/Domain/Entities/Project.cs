using System.ComponentModel.DataAnnotations;

namespace BackendEngineeringTasks.Domain.Entities
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Tasks> Tasks { get; set; }
    }
}
