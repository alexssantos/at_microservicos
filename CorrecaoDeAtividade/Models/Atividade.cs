namespace CorrecaoDeAtividade.Models
{
    public class Atividade
    {
        public long IdAtividade { get; set; }
        public long IdAluno { get; set; }
        public long IdProfessor { get; set; }
        public decimal Nota { get; set; }
    }
}
