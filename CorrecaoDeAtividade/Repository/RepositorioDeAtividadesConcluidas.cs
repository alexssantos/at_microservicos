using CorrecaoDeAtividade.Models;
using System.Collections.Generic;
using System.Linq;

namespace CorrecaoDeAtividade.Repository
{
    public class RepositorioDeAtividadesConcluidas
    {
        private static List<Atividade> AtividadesConcluidas = new List<Atividade>();
        public RepositorioDeAtividadesConcluidas()
        {

        }

        public void AdicionarAtividade(Atividade atividade) => AtividadesConcluidas.Add(atividade);

        public Atividade ObterAtividadePorId(long idAtividade)
            => AtividadesConcluidas.Where(at => at.IdAtividade == idAtividade).FirstOrDefault(null);

        public List<Atividade> ListarAtividadesConcluidas() => AtividadesConcluidas;
    }
}
