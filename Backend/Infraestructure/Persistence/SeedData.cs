using Domain.Entities;
using Domain.Enums;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Shared.Helpers;

namespace Infrastructure.Persistence;

public static class SeedData
{
    public static async Task InicializarAsync(EventosDbContext context)
    {
        // Aplica migraciones si es necesario
        await context.Database.MigrateAsync();

        // Verifica si ya existe el usuario Admin
        bool existeAdmin = await context.Usuarios.AnyAsync(u => u.Id == 1233492139);
        if (existeAdmin) return;

        // Generar contraseña segura
        string password = "Adr14n22*";
        PasswordHasher.CrearPasswordHash(password, out byte[] hash, out byte[] salt);

        Usuario admin = new Usuario
        {
            Id = 1233492139,
            NombreUsuario = "admin",
            Nombre = "Dios de todo",
            Email = "diosdeltodo@sistema.com",
            PasswordHash = hash,
            PasswordSalt = salt,
            Rol = RolUsuario.Administrador,
            DebeCambiarPassword = false 
        };


        context.Usuarios.Add(admin);
        await context.SaveChangesAsync();
    }
}
