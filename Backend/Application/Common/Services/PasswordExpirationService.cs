namespace Application.Common.Services;

public interface IPasswordExpirationService
{
    bool EstaVencida(DateTime fechaGeneracion);
}

public class PasswordExpirationService : IPasswordExpirationService
{
    private readonly TimeSpan _duracionValidez;

    public PasswordExpirationService()
    {
        _duracionValidez = TimeSpan.FromHours(24); // puedes cambiarlo o parametrizarlo
    }

    public bool EstaVencida(DateTime fechaGeneracion)
    {
        return DateTime.UtcNow - fechaGeneracion > _duracionValidez;
    }
}
