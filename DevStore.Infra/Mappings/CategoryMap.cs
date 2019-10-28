﻿using DevStore.Domain;
using System.Data.Entity.ModelConfiguration;

namespace DevStore.Infra.Mappings
{
    public class CategoryMap : EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            //Altera o nome da tabela
            ToTable("Category");


            HasKey(x => x.Id);
            Property(x => x.Title).HasMaxLength(60).IsRequired();
        }
    }
}
