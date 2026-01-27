using System.ComponentModel.DataAnnotations;

namespace BookStoreApi;

public class AutherRequest
{
        [Required(ErrorMessage = "الاسم مطلوب")] 
        [MinLength(3, ErrorMessage = "At least should be 3 ")] 
        public required string Name { get; set; }
    }
