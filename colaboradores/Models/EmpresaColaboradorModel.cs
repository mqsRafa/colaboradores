using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace colaboradores.Models;

public partial class EmpresaColaboradorModel
{
    [Key]
    public int IdColaborador { get; set; }
    [Key]
    public string IdEmpresa { get; set; } = null!;

}
