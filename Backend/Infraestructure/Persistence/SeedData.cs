using Domain.Entities;
using Domain.Enums;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Shared.Helpers;

public static class SeedData
{
    public static async Task InicializarAsync(EventosDbContext context)
    {
        //await context.Database.EnsureDeletedAsync(); // cuidado en producción
        await context.Database.MigrateAsync();

        List<int> idsUsuarios = new List<int> { 1233492138, 2001, 2002, 2003 };
        List<int> usuariosExistentes = await context.Usuarios
            .Where(u => idsUsuarios.Contains(u.Id))
            .Select(u => u.Id)
            .ToListAsync();

        if (usuariosExistentes.Count == idsUsuarios.Count)
            return;

        string password = "Adr14n22*";
        PasswordHasher.CrearPasswordHash(password, out byte[] hash, out byte[] salt);

        var usuarios = new List<Usuario>();

        if (!usuariosExistentes.Contains(1233492138))
        {
            usuarios.Add(new Usuario
            {
                Id = 1233492138,
                NombreUsuario = "admin",
                Nombre = "Dios de todo",
                Email = "diosdeltodo@sistema.com",
                PasswordHash = hash,
                PasswordSalt = salt,
                Rol = RolUsuario.Administrador,
                DebeCambiarPassword = false
            });
        }
        if (!usuariosExistentes.Contains(2001))
        {
            usuarios.Add(new Usuario
            {
                Id = 2001,
                NombreUsuario = "asistente1",
                Nombre = "Pedro Franco",
                Email = "pedrofranco05@alianzavalledupar.com",
                PasswordHash = hash,
                PasswordSalt = salt,
                Rol = RolUsuario.Asistente,
                DebeCambiarPassword = false
            });
        }
        if (!usuariosExistentes.Contains(2002))
        {
            usuarios.Add(new Usuario
            {
                Id = 2002,
                NombreUsuario = "gestionador1",
                Nombre = "Alberto Gamero",
                Email = "dongamero@desempleado.com",
                PasswordHash = hash,
                PasswordSalt = salt,
                Rol = RolUsuario.Gestionador,
                DebeCambiarPassword = false
            });
        }
        if (!usuariosExistentes.Contains(2003))
        {
            usuarios.Add(new Usuario
            {
                Id = 2003,
                NombreUsuario = "expositor1",
                Nombre = "Radamel Falcao",
                Email = "eltigredetucorazon@solomilloslokas.com",
                PasswordHash = hash,
                PasswordSalt = salt,
                Rol = RolUsuario.Expositor,
                DebeCambiarPassword = false
            });
        }

        if (usuarios.Count > 0)
        {
            context.Usuarios.AddRange(usuarios);
            await context.SaveChangesAsync();
        }
    }
}
