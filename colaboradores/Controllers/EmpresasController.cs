using Microsoft.AspNetCore.Mvc;
using colaboradores.Data;
using colaboradores.Models;
using colaboradores.ViewModels;
using System.Linq;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace colaboradores.Controllers
{
    public class EmpresasController : Controller
    {
        private TesteContext _context;

        public EmpresasController(TesteContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Busca as empresas do banco
            var empresas = _context.Empresas.OrderBy(e => e.NomeFantasiaEmpresa).ToList();

            var viewModel = new EmpresaViewModel
            {
                Empresas = empresas
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SalvarEmpresa([FromBody] SalvarEmpresaRequest request)
        {
            EmpresaModel? empresa;

            if (request.IdEmpresa == null)
            {
                empresa = new EmpresaModel
                {
                    RazaoSocialEmpresa = request.RazaoSocialEmpresa,
                    NomeFantasiaEmpresa = request.NomeFantasiaEmpresa,
                    TelefoneEmpresa = request.TelefoneEmpresa,
                    EnderecoEmpresa = request.EnderecoEmpresa,
                    NumeroEndereco = request.NumeroEndereco,
                    ComplementoEndereco = request.ComplementoEndereco,
                    CepEmpresa = request.CepEmpresa,
                    CidadeEmpresa = request.CidadeEmpresa,
                    UfEmpresa = request.UfEmpresa
                };

                _context.Add(empresa);
                _context.SaveChanges();

                return Json(new { success = true });
            }

            empresa = _context.Empresas
                        .Include(e => e.IdColaboradores)
                        .FirstOrDefault(e => e.IdEmpresa == request.IdEmpresa);

            if (empresa == null)
            {
                return Json(new { success = false, message = "A empresa não foi encontrada!" });
            }

            if (empresa != null)
            {
                // Atualiza os dados da empresa
                empresa.RazaoSocialEmpresa = request.RazaoSocialEmpresa;
                empresa.NomeFantasiaEmpresa = request.NomeFantasiaEmpresa;
                empresa.TelefoneEmpresa = request.TelefoneEmpresa;
                empresa.EnderecoEmpresa = request.EnderecoEmpresa;
                empresa.NumeroEndereco = request.NumeroEndereco;
                empresa.ComplementoEndereco = request.ComplementoEndereco;
                empresa.CepEmpresa = request.CepEmpresa;
                empresa.CidadeEmpresa = request.CidadeEmpresa;
                empresa.UfEmpresa = request.UfEmpresa;

                _context.SaveChanges();
                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Não foi possível criar ou atualizar a empresa!" });
        }

        public class SalvarEmpresaRequest
        {
            public int? IdEmpresa { get; set; }
            public string RazaoSocialEmpresa { get; set; } = null!;
            public string NomeFantasiaEmpresa { get; set; } = null!;
            public string? TelefoneEmpresa { get; set; }
            public string? EnderecoEmpresa { get; set; }
            public string? NumeroEndereco { get; set; }
            public string? ComplementoEndereco { get; set; }
            public string? CepEmpresa { get; set; }
            public string? CidadeEmpresa { get; set; }
            public string? UfEmpresa { get; set; }
        }

        [HttpPost]
        public IActionResult ExcluirEmpresa([FromBody] ExcluirEmpresaRequest request)
        {
            var empresa = _context.Empresas
                                  .Include(e => e.IdColaboradores)
                                  .FirstOrDefault(e => e.IdEmpresa == request.IdEmpresa);

            if (empresa != null)
            {
                _context.Remove(empresa);
                _context.SaveChanges();
                return Json(new { success = true });
            }

            return Json(new { success = false, message = "A empresa não foi encontrada!" });
        }

        public class ExcluirEmpresaRequest
        {
            public int IdEmpresa { get; set; }
        }
    }
}
