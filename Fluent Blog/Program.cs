using Blog.Data;
using Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog
{
    class Program
    {
        static void Main(string[] args)
        {
            using var context = new BlogDataContext();

            // context.Users.Add(new User{
            //     Name = "Renan Silva10",
            //     Slug = "RenanSilva10",
            //     Email = "Renan@silva.com10",
            //     Bio = "Dev10",
            //     Image = "Https://insta.com10",
            //     PasswordHash = "123455210"
            // });
            // context.SaveChanges();
            
            var user = context.Users.FirstOrDefault();

            var post = new Post
            {
                Author = user,
                Category = new Category
                    {
                        Name = "teste10",
                        Slug = "teste22215"
                    },
                Body = "Hello World15",
                Slug = "Comecando-com-entyity15",
                Summary = "Neste artigo iremos aprender sobre EF15",
                Title = "Começando com EF15",
                CreateDate = DateTime.Now
                // Tags = null
                // LastUpdateDate = 
            };

            context.Posts.Add(post);
            context.SaveChanges();
        }
    }
}