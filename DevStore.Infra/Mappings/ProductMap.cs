using DevStore.Domain;
using System.Data.Entity.ModelConfiguration;

namespace DevStore.Infra.Mappings
{

    //Classe para gerenciar as configurações que as tabelas terão
    public class ProductMap : EntityTypeConfiguration<Products>
    {
        public ProductMap()
        {
            ToTable("Product");

            HasKey(x => x.Id);

            Property(x => x.Title).HasMaxLength(160).IsRequired();
            Property(x => x.Price).IsRequired();
            Property(x => x.AcquireDate).IsRequired();

            HasRequired(x => x.Category);
        }
    }
}
