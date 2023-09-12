using BackendEngineeringTasks.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace BackendEngineeringTasks.Domain.Entities
{
    public class Tasks
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public int? ProjectId { get; set; }
        public int UserId { get; set; }
    }
}
