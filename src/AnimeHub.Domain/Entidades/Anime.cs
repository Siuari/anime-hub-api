using System.ComponentModel;

namespace AnimeHub.Domain.Entidades
{
    public class Anime
    {
        protected Anime() { }

        public Anime(string nome, string diretor, string resumo)
        {
            Nome = nome;
            Diretor = diretor;
            Resumo = resumo;
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Diretor { get; private set; }
        public string Resumo { get; private set; }

        public void Atualizar(string nome, string diretor, string resumo)
        {
            Nome = nome;
            Diretor = diretor;
            Resumo = resumo;
        }
    }
}
