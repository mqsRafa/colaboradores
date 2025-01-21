using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace colaboradores.Models;

public partial class ColaboradorModel
{
    [Key]
    public int IdColaborador { get; set; }

    public string NomeColaborador { get; set; } = null!;

    public int? IdCargoColaborador { get; set; }

    public int? IdDepartamentoColaborador { get; set; }

    public virtual ICollection<EmpresaModel> Empresas { get; set; } = new List<EmpresaModel>();
}
