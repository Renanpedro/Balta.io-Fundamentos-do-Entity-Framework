# Balta.io - Fundamentos do Entity FrameWork
Link do curso: https://balta.io/player/assistir/dbb3fad9-8c65-4509-929f-b5fbb2622e8e

Neste curso foi abordado de forma simples as diferentes formas de trabalhar com o Entity Framework sendo com DataAnnotations e Fluent Map, Operações Básicas (Create, Update, Delete, Select), trabalhar com migrations e excelentes dicas de perfomance e como elas devem ser aplicadas no meu código. 

# Introdução
**ORMs - Object Relation Mapping** (Mapeamento objeto relacional)
    Responsável por fazer o DePara parte essencial do Entity FrameWork similar ao Dapper.

**EntityFramework** - Um framework é um conjunto de biblioteca.
    - Muito mais poderoso que o Dapper porém é mais pesado.
    - Permite trabalhar com CRUD, Migrations e etc..

**Quando usar o Entity ou quando usar o Dapper?**
Geralmente projetos pequenos usar o Dapper, projetos mais complexos preferir o Entity. Porém vai de cada caso, cada um tem seu beneficio e maleficio.

**Database First**
O banco já está feito, mapeamos o que existe para os novos objetos criados.

**Code First**
Também conhecida como Model First, começamos pelo código, geramos o banco automaticamente via Migrations, Modelo amplamente usado.

**Data Context**
Unico objeto que o EF precisa, define o banco de dados em memoria, é composto por subconjuntos(propriedades) de dados chamados de DBSet

# Operações Básicas
**Mapeamento** (DePara) - Diz qual classe no C# se refere a qual tabela no banco de dados, diz quais propriedades de classe se referem a quais colunas da tabela relacionada, Informa os tipos de dados, permite gerar o banco automaticamente.

**Tipos de Mapeamentos**
    Fluent Mapping (Mapeamento fluente) - Feito em uma classe externa, não polui a classe principal, não cria dependencias na classe/projeto principal.
    Data Annotations - Feitos diretamente nas classes, mais simples e direto, Dependem do System.ComponentModel.DataAnnotations, alguns dependem do EF tbm.

**Função .Include** - Funciona como Inner join para fazer o relacionamento e trazer as informações de outra tabela, nesse caso irá trazer o post e as informações da tabela Autor e Tabela Category.

Caso eu queira que o Entity me mostra as querys que esta executando eu preciso acrescentar o Options.LogTo no metodo OnConfiguring.


# Migrations
**Migration** - Feito o mapemanto fluent ou data annotations é possível gerar um banco de dados com o EntityFramework.

**Pacotes que precisam serem instalados para gerar as migrations:**
dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore.Design

**Para gerar a migrations** - dotnet ef migrations add InitialCreation

**Para pegar as migrations e executar a criação do banco e tabelas é o comando abaixo** - dotnet ef database update InitialCreation

**Caso eu queira pegar a migration e gerar o script sql para rodar diretamente no banco sem precisar usar o ef eu executo este comando** - dotnet ef migrations script -o ./script.sql


# Dicas de perfomance com o EntityFramework

**NÃO É TODA TELA QUE PRECISA TER PERFOMANCE.** Somente algumas telas geralmente as que serão mais usadas.

**Função .AsNoTracking -** Em relatórios ou somente para popular uma tela com o .AsNotracking o EF não irá trazer os metadatos da tabela, com isso a consulta ficará mais rápida. (**Update** precisa que tenha os metadados para serem realizados).

**Async** e **Await** - Quando eu crio um metodo com **Async Task<Tipo>** ele deixa eu executar tarefas dentro do metodo, assim o metodo irá deixar de processar de forma Sincrona para Assincrona com isso eu consigo executar várias chamadas diferentes ao mesmo tempo ao invés de ter que esperar uma tarefa finalizar para executar a próxima.

**Lazy Loading(Carregamento Preguiçoso) -**  Dentro da minha classe eu posso marcar um atributo como VIRTUAL com isso ele só irá realizar o carregamento do atributo quando o mesmo for utilizado. 
    Ex: Classe post tem um list de Tag se a tag for virtual ele só ira realizar o select das tags quando o atributo for usado. **(LAZY LOADING NÃO É MUITO RECOMENDO POR PERDA DE PERFOMANCE POIS FICA EXECUTANDO VÁRIOS SELECTS).**

**Eager Loading -** É a forma padrão do EF trabalhar, eu deixo explícito quando quero carregar subitems ou não.

**ThenInclude -** Caso eu precise fazer um subselect dentro de um inner join eu posso usar o ThenInclude.

**Mapeando Queries puras e Views -** Caso fique muito complexo fazer as relações usando as funções do entity eu posso gerar uma query. Para isso eu preciso criar uma classe tendo os atributos que a minha query irá retornar, fazer o mapeamento de DbSet dessa nova classe no meu Context e no Builder do context eu especifico que será uma query.
