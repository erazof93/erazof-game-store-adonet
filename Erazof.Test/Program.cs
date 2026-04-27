
using Erazof.Domain;
using Erazof.Infrastructure;

Console.WriteLine("=== PRUEBA DE FLUJO COMPLETO ===\n");

// Crear un nuevo usuario
Console.WriteLine("***********************Creando un nuevo usuario*********************\n");
Usuario nuevoUsuario = new Usuario
{
    Username = "Usuario_prueba_" + DateTime.Now.Ticks,
    Email = $"Test{DateTime.Now.Ticks}@erazof.com",
    Password = "Password123"
};

UsuarioDALC dalc = new UsuarioDALC();
int idGenerado = await dalc.insertarEmpleado(nuevoUsuario);

if (idGenerado > 0)
{
    Console.WriteLine("****** ¡Éxito! El usuario se creó con el ID: " + idGenerado);
}

// Listar usuarios
Console.WriteLine("************************lista de Todos los usuarios creado********************* \n");
UsuarioDALC listaDalc = new UsuarioDALC();
var lista = await listaDalc.listarUsuario();
Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(lista));


//Listar usuarios por ID
Console.WriteLine("************************lista del usuario por ID*********************\n");
Usuario usuarioID = new Usuario();
var usuarioPorID = await dalc.listarUsuarioPorID(idGenerado);
Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(usuarioPorID));

