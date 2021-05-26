using System.ComponentModel.DataAnnotations;

namespace Demo.DDD.Requests
{
    public class UpdatePhoneNumberRequest
    {
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string  CountryCode { get; set; }
    }
}