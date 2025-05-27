using System.ComponentModel;

namespace BeYou.Application.Enums;

public enum RoleApplication
{
    [Description("Administrador")]
    ADMINISTRADOR = 1,

    [Description("Usuario")]
    USUARIO = 2,

    [Description("Moderador")]
    MODERADOR = 3,

    [Description("Invitado")]
    INVITADO = 4
}
