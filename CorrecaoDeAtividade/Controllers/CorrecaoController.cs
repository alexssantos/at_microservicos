using CorrecaoDeAtividade.Models;
using CorrecaoDeAtividade.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CorrecaoDeAtividade.Controllers
{
    [ApiController]
    [Route("correcao")]
    public class CorrecaoController : ControllerBase
    {
        private readonly RepositorioDeAtividadesConcluidas repositorio;
        public CorrecaoController()
        {
            repositorio = new RepositorioDeAtividadesConcluidas();
        }

        [HttpGet]
        public ActionResult<List<Atividade>> BuscarTodasAtividades()
        {
            return Ok(repositorio.ListarAtividadesConcluidas());
        }

    }
}
