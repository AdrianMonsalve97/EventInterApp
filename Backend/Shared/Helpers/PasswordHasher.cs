using System.Security.Cryptography;
using System.Text;

namespace Shared.Helpers;

/// <summary>
/// Proporciona métodos seguros para crear y verificar contraseñas utilizando hash con salt.
/// </summary>
public static class PasswordHasher
{
    /// <summary>
    /// Genera un hash seguro y un salt para la contraseña proporcionada.
    /// </summary>
    public static void CrearPasswordHash(string password, out byte[] hash, out byte[] salt)
    {
        using HMACSHA512 hmac = new HMACSHA512(); // Crea un salt aleatorio automáticamente
        salt = hmac.Key;
        hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    /// <summary>
    /// Verifica si una contraseña ingresada corresponde al hash y salt guardados.
    /// </summary>
    public static bool VerificarPassword(string password, byte[] hashGuardado, byte[] saltGuardado)
    {
        using HMACSHA512 hmac = new HMACSHA512(saltGuardado);
        byte[] hashCalculado = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return hashCalculado.SequenceEqual(hashGuardado);
    }
}
