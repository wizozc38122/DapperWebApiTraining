using System.ComponentModel.DataAnnotations;

namespace DapperWebApiTraining.Parameters
{
    public class PostCreateParameter
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        [Required]

        public int BlogId { get; set; }
    }
}
