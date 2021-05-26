using System.ComponentModel.DataAnnotations;

namespace Demo.DDD.Requests
{
    public class CreateUserRequest
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; }
    }
}
