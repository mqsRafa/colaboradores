using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace colaboradores.Models;

public partial class EmpresaModel
{
    [Key]
    public int IdEmpresa { get; set; }

    public string RazaoSocialEmpresa { get; set; } = null!;

    public string NomeFantasiaEmpresa { get; set; } = null!;

    public string? TelefoneEmpresa { get; set; }

    public string? EnderecoEmpresa { get; set; }

    public string? NumeroEndereco { get; set; }

    public string? ComplementoEndereco { get; set; }

    public string? CepEmpresa { get; set; }

    public string? CidadeEmpresa { get; set; }

    public string? UfEmpresa { get; set; }

    public virtual ICollection<ColaboradorModel> IdColaboradores { get; set; } = new List<ColaboradorModel>();
}
