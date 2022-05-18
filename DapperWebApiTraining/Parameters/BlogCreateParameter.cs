using System.ComponentModel.DataAnnotations;

namespace DapperWebApiTraining.Parameters
{
    public class BlogCreateParameter
    {
        [Required]
        public string? Url { get; set; }
    }
}
