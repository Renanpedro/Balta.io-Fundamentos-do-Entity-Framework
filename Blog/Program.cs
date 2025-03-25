using Blog.Data;
using Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog
{
    class Program
    {
        static void Main(string[] args)
        {
            //Abriu a conexão no banco de dados
            using (var context = new BlogDataContext())
            {
                ////INSERT - Criando algo novo no banco
                var tagInsert = new Tag {Name = "ASP.NET00", Slug="aspnet00"};
                //Aqui ele ta salvando na memoria
                context.Tags.Add(tagInsert);
                //Aqui ele ta salvando no banco
                context.SaveChanges();

                //UPDATE - Antes de dar o update é sempre melhor pegar do banco o objeto mais atual do que iremos atualizar ao invés de utilizar os da memoria
                var tagUpdate = context.Tags.FirstOrDefault(x => x.Name == "ASP.NET00");
                tagUpdate.Name = ".NET";
                tagUpdate.Slug = "dotnet";
                context.Update(tagUpdate);
                context.SaveChanges();

                //Delete - Antes de dar o delete é sempre melhor pegar do banco o objeto mais atual do que iremos remover ao invés de utilizar os da memoria
                var tagDelete = context.Tags.FirstOrDefault(x => x.Id == 2);
                context.Remove(tagDelete);
                context.SaveChanges();

                //SELECT
                //Sem o .ToList() ele não irá pegar os dados no banco nesse momento, sempre fazer o WHERE ANTES DO TOLIST
                //O where deve vir antes do list senão ele ira pegar todos os dados da tabela e so depois filtra e isso quebra a performance.
                var tagSelect = context
                            .Tags
                            .Where(x => x.Id == 3)
                            .ToList();

                foreach (var tagSel in tagSelect)
                {
                    System.Console.WriteLine(tagSel.Name );
                }

                //SELECT - Porém sem trazer os METADADOS do banco, só ira trazer os valores das colunas
                var TagSelectSemMetaDado = context
                            .Tags
                            .AsNoTracking()
                            .ToList();
                

                var tag = context
                            .Tags
                            .AsNoTracking()
                            .FirstOrDefault(t => t.Id == 2); //Pega o primeiro e caso não tenha valor ira passar vazio.
                            // .SingleOrDefault(t => t.Id == 2) se tiver mais de um item com o mesmo ID irá dar erro.

                Console.WriteLine(tag?.Name);
            }
        }
    }
}