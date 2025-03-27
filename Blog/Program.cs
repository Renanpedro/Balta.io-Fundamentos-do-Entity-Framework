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

            #region "Modulo Introdução"
            //Abriu a conexão no banco de dados
            // using (var context = new BlogDataContext())
            // {
            //     ////INSERT - Criando algo novo no banco
            //     var tagInsert = new Tag {Name = "ASP.NET00", Slug="aspnet00"};
            //     //Aqui ele ta salvando na memoria
            //     context.Tags.Add(tagInsert);
            //     //Aqui ele ta salvando no banco
            //     context.SaveChanges();

            //     //UPDATE - Antes de dar o update é sempre melhor pegar do banco o objeto mais atual do que iremos atualizar ao invés de utilizar os da memoria
            //     var tagUpdate = context.Tags.FirstOrDefault(x => x.Name == "ASP.NET00");
            //     tagUpdate.Name = ".NET";
            //     tagUpdate.Slug = "dotnet";
            //     context.Update(tagUpdate);
            //     context.SaveChanges();

            //     //Delete - Antes de dar o delete é sempre melhor pegar do banco o objeto mais atual do que iremos remover ao invés de utilizar os da memoria
            //     var tagDelete = context.Tags.FirstOrDefault(x => x.Id == 2);
            //     context.Remove(tagDelete);
            //     context.SaveChanges();

            //     //SELECT
            //     //Sem o .ToList() ele não irá pegar os dados no banco nesse momento, sempre fazer o WHERE ANTES DO TOLIST
            //     //O where deve vir antes do list senão ele ira pegar todos os dados da tabela e so depois filtra e isso quebra a performance.
            //     var tagSelect = context
            //                 .Tags
            //                 .Where(x => x.Id == 3)
            //                 .ToList();

            //     foreach (var tagSel in tagSelect)
            //     {
            //         System.Console.WriteLine(tagSel.Name );
            //     }

            //     //SELECT - Porém sem trazer os METADADOS do banco, só ira trazer os valores das colunas
            //     var TagSelectSemMetaDado = context
            //                 .Tags
            //                 .AsNoTracking()
            //                 .ToList();
                

            //     var tag = context
            //                 .Tags
            //                 .AsNoTracking()
            //                 .FirstOrDefault(t => t.Id == 2); //Pega o primeiro e caso não tenha valor ira passar vazio.
            //                 // .SingleOrDefault(t => t.Id == 2) se tiver mais de um item com o mesmo ID irá dar erro.

            //     Console.WriteLine(tag?.Name);
            // }
            #endregion
        
            #region "Até a aula trabalhando com subconjuntos"
            // var user = new User
            // {
            //     Name = "Renan Silva2",
            //     Slug = "RenanSilva2",
            //     Email = "Renan@silva.com2",
            //     Bio = "Dev2",
            //     Image = "Https://insta.com2",
            //     PasswordHash = "1234552"
            // };

            // var category = new Category(name:"Backend2", slug:"backend2");
            

            // var post = new Post
            // {
            //     Author = user,
            //     Category = category,
            //     Body = "Hello World",
            //     Slug = "Comecando-com-entyity",
            //     Summary = "Neste artigo iremos aprender sobre EF",
            //     Title = "Começando com EF",
            //     CreateDate = DateTime.Now,
            //     LastUpdateDate = DateTime.Now,
            // };

            // context.Posts.Add(post);    
            // context.SaveChanges();

            // var posts = context.Posts.AsNoTracking()
            // .Include(x => x.Author) //.Include funciona como o INNER JOIN
            // .Include(x => x.Category)
            // .OrderByDescending(x => x.LastUpdateDate)
            // .ToList();

            // foreach (var post in posts)
            //     System.Console.WriteLine($"{post.Title} escrito por {post.Author?.Name} em {post.Category?.Name}");
            #endregion

            #region "Finalizado o Módulo Operações Básicas"
            // Fazendo um update no autor de um post
            var post = context
                .Posts
                .Include(x => x.Author) 
                .Include(x => x.Category)
                .OrderByDescending(x => x.LastUpdateDate)
                .FirstOrDefault();

            post.Author.Name = "Renan Alterado";

            context.Posts.Update(post);
            context.SaveChanges();
            #endregion

            
        }
    }
}