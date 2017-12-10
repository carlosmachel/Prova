using Prova.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova.Infra.Data.EntityConfig
{
    public class UsuarioConfiguration : EntityTypeConfiguration<Usuario>
    {

        public UsuarioConfiguration()
        {
            HasKey(c => c.Id);

            Property(c => c.Nome)
                    .IsRequired()
                    .HasMaxLength(150);            

            Property(c => c.Email)
                .IsRequired();

            Property(c => c.Senha)
                .IsRequired();            
        }
    }
}
