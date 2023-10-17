using System.ComponentModel.DataAnnotations;

namespace PD.ChatHistory.Domain.Entities.Common
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; init; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOnUTC { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime UpdatedOnUTC { get; set; }
    }
}
