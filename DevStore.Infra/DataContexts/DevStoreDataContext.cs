using DevStore.Domain;
using DevStore.Infra.Mappings;
using System.Data.Entity;

namespace DevStore.Infra
{
    public class DevStoreDataContext : DbContext
    {
        //O construtor pega a connection string para começar, se não for informada a connection string utilizada será igual ao nome da classe
        public DevStoreDataContext()
            : base("DevStoreConnectionString")
        {
            //A fazer

            //Database.SetInitializer<DevStoreDataContext>(new DevStoreDataContextInitializer());

            //Se vai ou não carregar os dados das propriedades complexas
            Configuration.LazyLoadingEnabled = false;

            //Desabilita a resolução do proxy
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Products> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            base.OnModelCreating(modelBuilder);
        }
    }

    public class DevStoreDataContextInitializer : DropCreateDatabaseIfModelChanges<DevStoreDataContext>
    {
        protected override void Seed(DevStoreDataContext context)
        {
            context.Categories.Add(new Category { Id = 1, Title = "Informática" });
            context.Categories.Add(new Category { Id = 2, Title = "Games" });
            context.Categories.Add(new Category { Id = 3, Title = "Papelaria" });
            context.SaveChanges();

            context.Products.Add(new Products { Id = 1, CategoryId = 1, Title = "Mouse Pad", IsActive = true, Price = 3 });
            context.Products.Add(new Products { Id = 2, CategoryId = 1, Title = "Teclado Sem fio multilaser", IsActive = true, Price = 1 });
            context.Products.Add(new Products { Id = 3, CategoryId = 1, Title = "Monitor ultra hardcore", IsActive = true, Price = 7 });

            context.Products.Add(new Products { Id = 4, CategoryId = 2, Title = "Console PS4", IsActive = true, Price = 2500 });
            context.Products.Add(new Products { Id = 5, CategoryId = 2, Title = "Console Dinavision", IsActive = true, Price = 3000 });
            context.Products.Add(new Products { Id = 6, CategoryId = 3, Title = "Caneta quatro cores", IsActive = true, Price = 7000 });

            Products tinteiro = new Products { Id = 7, CategoryId = 3, Title = "Tinteiro de Visconde do milho verde", IsActive = true, Price = 120 };

            context.Products.Add(tinteiro);
            context.SaveChanges();
        }
    }
}
