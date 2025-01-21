using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using colaboradores.Models;

namespace colaboradores.Data;

public partial class TesteContext : DbContext
{
    public TesteContext()
    {
    }

    public TesteContext(DbContextOptions<TesteContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ColaboradorModel> Colaboradores { get; set; }

    public virtual DbSet<EmpresaModel> Empresas { get; set; }

    public virtual DbSet<EmpresaColaboradorModel> EmpresasColaboradores { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<ColaboradorModel>(entity =>
        {
            entity.HasKey(e => e.IdColaborador).HasName("PRIMARY");

            entity.ToTable("colaborador");

            entity.Property(e => e.IdColaborador).HasColumnName("id_colaborador").ValueGeneratedOnAdd();
            entity.Property(e => e.IdCargoColaborador).HasColumnName("id_cargo_colaborador");
            entity.Property(e => e.IdDepartamentoColaborador).HasColumnName("id_departamento_colaborador");
            entity.Property(e => e.NomeColaborador)
                .HasMaxLength(40)
                .HasColumnName("nome_colaborador");

            entity.HasMany(c => c.Empresas)
                .WithMany(e => e.IdColaboradores)
                .UsingEntity<Dictionary<string, object>>(
                    "empresa_colaborador",
                    r => r.HasOne<EmpresaModel>().WithMany().HasForeignKey("id_empresa"),
                    l => l.HasOne<ColaboradorModel>().WithMany().HasForeignKey("id_colaborador"));
        });

        modelBuilder.Entity<EmpresaModel>(entity =>
        {
            entity.HasKey(e => e.IdEmpresa).HasName("PRIMARY");

            entity.ToTable("empresa");

            entity.Property(e => e.IdEmpresa).HasColumnName("id_empresa").ValueGeneratedOnAdd();
            entity.Property(e => e.CepEmpresa)
                .HasMaxLength(8)
                .HasColumnName("cep_empresa");
            entity.Property(e => e.CidadeEmpresa)
                .HasMaxLength(35)
                .HasColumnName("cidade_empresa");
            entity.Property(e => e.ComplementoEndereco)
                .HasMaxLength(20)
                .HasColumnName("complemento_endereco");
            entity.Property(e => e.EnderecoEmpresa)
                .HasMaxLength(50)
                .HasColumnName("endereco_empresa");
            entity.Property(e => e.NomeFantasiaEmpresa)
                .HasMaxLength(100)
                .HasColumnName("nome_fantasia_empresa");
            entity.Property(e => e.NumeroEndereco)
                .HasMaxLength(6)
                .HasColumnName("numero_endereco");
            entity.Property(e => e.RazaoSocialEmpresa)
                .HasMaxLength(100)
                .HasColumnName("razao_social_empresa");
            entity.Property(e => e.TelefoneEmpresa)
                .HasMaxLength(14)
                .HasColumnName("telefone_empresa");
            entity.Property(e => e.UfEmpresa)
                .HasMaxLength(2)
                .HasColumnName("uf_empresa");
        });

        modelBuilder.Entity<EmpresaColaboradorModel>(entity =>
        {
            entity.HasKey(ec => new { ec.IdEmpresa, ec.IdColaborador });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
