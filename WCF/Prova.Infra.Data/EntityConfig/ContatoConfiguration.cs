using Prova.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova.Infra.Data.EntityConfig
{
    public class ContatoConfiguration : EntityTypeConfiguration<Contato>
    {

        public ContatoConfiguration()
        {
            HasKey(c => c.Id);
            Property(c => c.Nome)
                    .IsRequired()
                    .HasMaxLength(150);
            Property(c => c.Telefone)
                    .IsRequired()
                    .HasMaxLength(20);

            Property(c => c.Email)
                .IsRequired();

            HasRequired(c => c.Usuario)
                    .WithMany()
                    .HasForeignKey(u => u.UsuarioId);
            
        }
    }
}
