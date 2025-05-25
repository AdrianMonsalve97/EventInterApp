namespace Shared.Contracts.Autenticacion;

public sealed record JwtResponseDto(
    string Token,
    int IdUsuario,
    string Nombre,
    string Rol,
    bool DebeCambiarPassword
);
