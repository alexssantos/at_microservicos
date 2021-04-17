namespace ConclusaoDeAtividade.Models
{
    public class Contador
    {
        private int _valorAtual { get; set; }
        public int ValorAtual { get => _valorAtual; }

        public void Incrementar()
        {
            _valorAtual++;
        }
    }
}
