namespace Blog.Models
{
    public class Category
    {
        // public Category(string name, string slug)
        // {
        //     Name = name;
        //     Slug = slug;
        // }
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Slug { get; set; }

        public List<Post>? Posts { get; set; }
    }
}