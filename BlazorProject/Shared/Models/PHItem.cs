using BlazorProject.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace BlazorProject.Shared.Models
{
    public class PHItem : IDbase
    {
        [Required]
        [StringLength(300, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }
        public phItemType Type { get; set; }
        [Required]
        public float Value { get; set; }
    }
}
