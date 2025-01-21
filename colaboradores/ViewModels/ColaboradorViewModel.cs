using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using colaboradores.Data;
using colaboradores.Models;

namespace colaboradores.ViewModels
{
    public class ColaboradorViewModel
    {
        public List<ColaboradorModel> Colaboradores { get; set; } = null!;
        public List<EmpresaModel> Empresas { get; set; } = null!;
    }
}
