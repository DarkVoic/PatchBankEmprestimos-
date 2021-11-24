using BankEmprestimoConsignado.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;

namespace BankEmprestimoConsignado.Data
{
    public partial class BankContext : DbContext
    {
        public BankContext(DbContextOptions<BankContext> options) : base(options)
        {
            
        }
        //Sets dos models
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Emprestimo> Emprestimos { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<IdentityUserLogin<string>>()
                .HasNoKey();
            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasNoKey();
            modelBuilder.Entity<IdentityUserClaim<string>>()
                .HasNoKey();
            modelBuilder.Entity<IdentityUserToken<string>>()
                .HasNoKey();
            modelBuilder.Entity<IdentityUser<string>>()
                .HasNoKey();

            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente)
                    .HasName("PRIMARY");

                entity.ToTable("clientes");

                entity.Property(e => e.IdCliente)
                    .ValueGeneratedNever()
                    .HasColumnName("idCLIENTE");

                entity.Property(e => e.Cpf).HasColumnName("CPF");

                entity.Property(e => e.Margem)
                    .HasPrecision(3, 2)
                    .HasColumnName("MARGEM");

                entity.Property(e => e.MargemCartao)
                    .HasPrecision(3, 2)
                    .HasColumnName("MARGEM_CARTAO");

                entity.Property(e => e.Nascimento)
                    .HasColumnType("datetime")
                    .HasColumnName("NASCIMENTO");

                entity.Property(e => e.Nome)
                    .HasMaxLength(45)
                    .HasColumnName("NOME");

                entity.Property(e => e.Profissao)
                    .HasMaxLength(45)
                    .HasColumnName("PROFISSAO");

                entity.Property(e => e.Salario)
                    .HasPrecision(10, 2)
                    .HasColumnName("SALARIO");

                entity.Property(e => e.Status).HasColumnName("STATUS");
            });

            modelBuilder.Entity<Emprestimo>(entity =>
            {
                entity.HasKey(e => e.IdEmprestimo)
                    .HasName("PRIMARY");

                entity.ToTable("emprestimos");

                entity.HasIndex(e => e.IdCliente, "fk_EMPRESTIMO_CLIENTE_idx");

                entity.Property(e => e.IdEmprestimo)
                    .ValueGeneratedNever()
                    .HasColumnName("idEMPRESTIMO");

                entity.Property(e => e.DataVenc)
                    .HasColumnType("datetime")
                    .HasColumnName("DATA_VENC");

                entity.Property(e => e.IdCliente).HasColumnName("idCLIENTE");

                entity.Property(e => e.QtdParcela).HasColumnName("QTD_PARCELA");

                entity.Property(e => e.QtdParcelaRest).HasColumnName("QTD_PARCELA_REST");

                entity.Property(e => e.StatusEmprest).HasColumnName("STATUS_EMPREST");

                entity.Property(e => e.TaxaJuros)
                    .HasPrecision(3, 2)
                    .HasColumnName("TAXA_JUROS");

                entity.Property(e => e.TipoEmprest).HasColumnName("TIPO_EMPREST");

                entity.Property(e => e.ValorEmprestimo)
                    .HasColumnType("double(10,2)")
                    .HasColumnName("VALOR_EMPRESTIMO");

                entity.Property(e => e.ValorLiberado)
                    .HasColumnType("double(10,2)")
                    .HasColumnName("VALOR_LIBERADO");

                entity.Property(e => e.ValorParcela)
                    .HasColumnType("double(10,2)")
                    .HasColumnName("VALOR_PARCELA");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Emprestimos)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_EMPRESTIMO_CLIENTE");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuarios)
                    .HasName("PRIMARY");

                entity.ToTable("usuarios");

                entity.HasIndex(e => e.User, "USER_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.IdCliente, "fk_USUARIOS_CLIENTES1_idx");

                entity.Property(e => e.IdUsuarios)
                    .ValueGeneratedNever()
                    .HasColumnName("idUSUARIOS");

                entity.Property(e => e.Celular)
                    .HasMaxLength(50)
                    .HasColumnName("CELULAR");

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.IdCliente).HasColumnName("idCLIENTE");

                entity.Property(e => e.Senha)
                    .HasMaxLength(6)
                    .HasColumnName("SENHA");

                entity.Property(e => e.User)
                    .HasMaxLength(45)
                    .HasColumnName("USER");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_USUARIOS_CLIENTES1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        
       
    }
}

