using Blog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.Mappings
{
    public class PostMap : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Post");

            //Chave primária da tabela
            builder.HasKey(x => x.Id);

            //Identity(1,1) irá gerar os ids sequêncial somando +1
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();
            
            builder.Property(x => x.LastUpdateDate)
                .IsRequired()
                .HasColumnName("LastUpdateDate")
                .HasColumnType("SMALLLDATETIME")
                .HasDefaultValueSql("GETDATE()"); //Esse valor será gerado pela função do SQL Server
                // .HasDefaultValue(DateTime.Now.ToUniversalTime()); //irá gerar as datas pelo .NET ao invés do banco

            //Indices Um índice pode ser criado para um campo específico que sempre é consultado em uma tabela, com esse indice criado a resposta das consultas ficam mais rápidas.
            builder.HasIndex(x => x.Slug, "IX_Post_Slug")
                .IsUnique();


            // Relacionamento entre as tabelas
            builder.HasOne(x => x.Author) //1 Autor pode ter muitos posts
                .WithMany(x => x.Posts)
                .HasConstraintName("FK_Post_Author")
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasOne(x => x.Category) //1 Categoria pode ter muitos posts
                .WithMany(x => x.Posts)
                .HasConstraintName("FK_Post_Category")
                .OnDelete(DeleteBehavior.Cascade);

            //Relacionamento N para N
            builder.HasMany(x => x.Tags)
                .WithMany(x => x.Posts)
                .UsingEntity<Dictionary<string, object>>(
                    "PostTag", 
                    post => post.HasOne<Tag>()
                        .WithMany()
                        .HasForeignKey("PostId")
                        .HasConstraintName("FK_PostTag_PostId")
                        .OnDelete(DeleteBehavior.Cascade),
                    tag => tag.HasOne<Post>()
                        .WithMany()
                        .HasForeignKey("TagId")
                        .HasConstraintName("FK_PostTag_TagId")
                        .OnDelete(DeleteBehavior.Cascade));
        }
    }
}