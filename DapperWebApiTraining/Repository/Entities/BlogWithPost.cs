namespace DapperWebApiTraining.Repository.Entities
{
    public class BlogWithPost
    {
        public int BlogId { get; set; }
        public string? Url { get; set; }

        public List<Post>? posts { get; set; }
    }
}
