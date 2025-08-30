namespace AnimeHub.Domain.DomainExceptions
{
    public class AnimeHubValidationException : Exception
    {
        public AnimeHubValidationException()
        { }

        public AnimeHubValidationException(string mensagem)
            : base(mensagem)
        { }
    }
}
