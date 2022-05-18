
using System.ComponentModel.DataAnnotations;

namespace DapperWebApiTraining.Parameters
{
    public class PostUpdateParameter
    {
        [Required]
        public int PostId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
    }
}
