using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using colaboradores.Data;
using colaboradores.Models;
using colaboradores.ViewModels;
using Newtonsoft.Json;

namespace colaboradores.Controllers
{
    public class ColaboradoresController : Controller
    {
        private TesteContext _context;

        public ColaboradoresController(TesteContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var colaboradores = _context.Colaboradores
                                .Include(c => c.Empresas)
                                .OrderBy(c => c.NomeColaborador)
                                .ToList();

            var empresas = _context.Empresas.OrderBy(e => e.NomeFantasiaEmpresa).ToList();

            var viewModel = new ColaboradorViewModel
            {
                Colaboradores = colaboradores,
                Empresas = empresas,
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SalvarColaborador([FromBody] SalvarColaboradorRequest request)
        {
            ColaboradorModel? colaborador;

            if (request.IdColaborador == null)
            {
                colaborador = new ColaboradorModel
                {
                    NomeColaborador = request.NomeColaborador,
                    IdCargoColaborador = request.IdCargoColaborador,
                    IdDepartamentoColaborador = request.IdDepartamentoColaborador
                };

                _context.Add(colaborador);
                _context.SaveChanges();
            }
            else
            {
                colaborador = _context.Colaboradores
                              .Include(c => c.Empresas)
                              .FirstOrDefault(c => c.IdColaborador == request.IdColaborador);

                if (colaborador == null)
                {
                    return Json(new { success = false, message = "O colaborador não foi encontrado!" });
                }
            }

            if (colaborador != null)
            {
                var empresasRequest = new HashSet<int>(request.ListaEmpresas ?? new List<int>());
                var empresasColaborador = new HashSet<int>(colaborador.Empresas.Select(e => e.IdEmpresa));

                // Adiciona empresas novas (presentes no request, mas não associadas ao colaborador)
                var empresasAdicionar = empresasRequest.Except(empresasColaborador);
                foreach (var empresaId in empresasAdicionar)
                {
                    var empresaAdd = _context.Empresas.FirstOrDefault(e => e.IdEmpresa == empresaId);
                    if (empresaAdd != null)
                    {
                        colaborador.Empresas.Add(empresaAdd);
                    }
                }

                // Remove empresas que não estão mais no request (presentes no colaborador, mas não no request)
                var empresasRemover = empresasColaborador.Except(empresasRequest);
                foreach (var empresaId in empresasRemover)
                {
                    var empresaRemover = colaborador.Empresas.FirstOrDefault(e => e.IdEmpresa == empresaId);
                    if (empresaRemover != null)
                    {
                        colaborador.Empresas.Remove(empresaRemover);
                    }
                }

                // Atualiza os dados do colaborador
                colaborador.NomeColaborador = request.NomeColaborador;
                colaborador.IdCargoColaborador = request.IdCargoColaborador;
                colaborador.IdDepartamentoColaborador = request.IdDepartamentoColaborador;

                _context.SaveChanges();
                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Não foi possível criar o novo colaborador!" });
        }

        public class SalvarColaboradorRequest
        {
            public int? IdColaborador { get; set; }

            public string NomeColaborador { get; set; } = null!;

            public int? IdCargoColaborador { get; set; }

            public int? IdDepartamentoColaborador { get; set; }

            public List<int>? ListaEmpresas { get; set; }
        }

        [HttpPost]
        public IActionResult ExcluirColaborador([FromBody] ExcluirColaboradorRequest request)
        {
            var colaborador = _context.Colaboradores
                                      .Include(c => c.Empresas)
                                      .FirstOrDefault(c => c.IdColaborador == request.IdColaborador);

            if (colaborador != null)
            {
                _context.Remove(colaborador);
                _context.SaveChanges();

                return Json(new { success = true });
            }

            return Json(new { success = false, message = "O colaborador não foi encontrado!" });
        }

        public class ExcluirColaboradorRequest
        {
            public int IdColaborador { get; set; }
        }
    }
}
