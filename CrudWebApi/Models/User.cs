using System.ComponentModel.DataAnnotations;

namespace CrudWebApi.Models
{
    public class User
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(1,100)]
        public uint Age { get; set; }
    }
}
