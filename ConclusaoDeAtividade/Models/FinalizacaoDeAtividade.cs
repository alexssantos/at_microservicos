using System.ComponentModel.DataAnnotations;

namespace ConclusaoDeAtividade.Models
{
    public class FinalizacaoDeAtividade
    {
        [Required]
        public long IdAtividade { get; set; }
        [Required]
        public long IdAluno { get; set; }
    }
}
