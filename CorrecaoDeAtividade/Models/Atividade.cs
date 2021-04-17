namespace CorrecaoDeAtividade.Models
{
    public class Atividade
    {
        public long IdAtividade { get; set; }
        public long IdAluno { get; set; }
        public long IdProfessor { get; set; }
        public decimal Nota { get; set; }

        public override string ToString()
        {
            return $@"ID Aluno: {IdAluno} | ID Atividade: {IdAtividade} | ID Professor: {IdProfessor} | Nota: {Nota}";
        }
    }
}
