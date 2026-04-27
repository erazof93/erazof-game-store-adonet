using Erazof.Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Erazof.Infrastructure
{
    public class UsuarioDALC
    {
        //listar
        public async Task<List<Usuario>> listarUsuario()
        {
            List<Usuario> usuarios = new List<Usuario>();
            using (SqlConnection cn = DBConexion.obtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("usp_ListarUsuario", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        await cn.OpenAsync();
                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {
                            while (await dr.ReadAsync())
                            {
                                usuarios.Add(new Usuario
                                {
                                    //UsuarioID = dr.GetInt32(dr.GetOrdinal("UsuarioID")),
                                    UsuarioID = dr.GetInt32(0),
                                    Username = dr.GetString(1),
                                    Email = dr.GetString(2),
                                    Password = dr.GetString(3),
                                    FechaReg = dr.GetDateTime(4),
                                    RolID = new Rol
                                    {
                                        NombreRol = dr.GetString(5)
                                    }
                                });
                            }
                        }
                        //Console.WriteLine("**************** lista de empleados: " + JsonSerializer.Serialize(usuarios));
                        System.Diagnostics.Debug.WriteLine("**************** lista de empleados: " + JsonSerializer.Serialize(usuarios));
                        return usuarios;

                    }
                    catch (SqlException ex)
                    {
                        throw new Exception("Error en la base de datos al lista usuario", ex);
                    }

                }
            }
        }
        //Crear
        public async Task<int> insertarUsuario(Usuario usuario)
        {
            int resultado = 0;
            using (SqlConnection cn = DBConexion.obtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("usp_CrearUsuario", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Username", usuario.Username);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@Password", usuario.Password);
                    // OUTPUT
                    SqlParameter pId = new SqlParameter("@UsuarioID", SqlDbType.Int);
                    pId.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(pId);
                    try
                    {
                        await cn.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                        resultado = (int)pId.Value;
                        //Console.WriteLine("**************** Usuario insertado con ID: " + resultado);
                        System.Diagnostics.Debug.WriteLine("**************** Usuario insertado con ID: " + resultado);
                        return resultado;
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception("Error en la base de datos al insertar usuario", ex);
                    }
                }
            }
        }
        //Listar por ID
        public async Task<Usuario> listarUsuarioPorID(int id)
        {
            Usuario usuario = null;
            using (SqlConnection cn = DBConexion.obtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("usp_ListarUsuarioID", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UsuarioID", id);
                    try
                    {
                        await cn.OpenAsync();
                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {
                            if (await dr.ReadAsync())
                            {
                                usuario = new Usuario
                                {
                                    UsuarioID = dr.GetInt32(0),
                                    Username = dr.GetString(1),
                                    Email = dr.GetString(2),
                                    Password = dr.GetString(3),
                                    FechaReg = dr.GetDateTime(4),
                                    RolID = new Rol
                                    {
                                        NombreRol = dr.GetString(5),
                                        RolID = dr.GetInt32(6)
                                    }
                                };
                            }
                        }
                        //Console.WriteLine("**************** Usuario encontrado: " + JsonSerializer.Serialize(usuario));
                        System.Diagnostics.Debug.WriteLine("**************** Usuario encontrado: " + JsonSerializer.Serialize(usuario));
                        return usuario;
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception("Error en la base de datos al listar usuario por ID", ex);
                    }
                }
            }
        }
        //Actualizar
        public async Task<bool> actualizarUsuario(Usuario obj)
        {
            bool resultado = false;
            using (SqlConnection cn = DBConexion.obtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("usp_ActualizarUsuario", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UsuarioID", obj.UsuarioID);
                    cmd.Parameters.AddWithValue("@Username", obj.Username);
                    cmd.Parameters.AddWithValue("@Email", obj.Email);
                    cmd.Parameters.AddWithValue("@Password", obj.Password);
                    //output
                    // Parámetro de SALIDA (OUTPUT)
                    SqlParameter pRespuesta = new SqlParameter("@Respuesta", SqlDbType.Bit);
                    pRespuesta.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(pRespuesta);
                    try
                    {
                        await cn.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                        // se captura @Respuesta
                        resultado = (bool)pRespuesta.Value;

                        return resultado;
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception("Error en la base de datos al actualizar usuario", ex);
                    }
                }

            }
        }

        //Eliminar
        public async Task<bool> eliminarUsuario(int id)
        {
            bool resultado = false;
            using (SqlConnection cn = DBConexion.obtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("usp_EliminarUsuario", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UsuarioID", id);
                    //output
                    SqlParameter pRespuesta = new SqlParameter("@Respuesta", SqlDbType.Bit);
                    pRespuesta.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(pRespuesta);
                    try
                    {
                        await cn.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                        // se captura @Respuesta
                        resultado = (bool)pRespuesta.Value;
                        return resultado;
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception("Error en la base de datos al eliminar usuario", ex);
                    }
                }
            }

        }

        //Validacion
        public async Task<Usuario> obtenerPorCredenciales(string userOrEmail, string password)
        {
            Usuario usuario = null;
            try
            {
                using (SqlConnection cn = DBConexion.obtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("usp_ValidarUsuario", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Usamos una validación para que si es null, mande DBNull.Value
                        cmd.Parameters.AddWithValue("@UserOrEmail", userOrEmail);
                        cmd.Parameters.AddWithValue("@Password", password);

                        await cn.OpenAsync();

                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {
                            if (await dr.ReadAsync())
                            {
                                usuario = new Usuario
                                {
                                    UsuarioID = dr.GetInt32(0),
                                    Username = dr.GetString(1),
                                    Email = dr.GetString(2),
                                    RolID = new Rol
                                    {
                                        RolID = dr.GetInt32(3),
                                        NombreRol = dr.GetString(4)
                                    }
                                };
                            }
                        }
                        // Ahora sí debería llegar aquí si la DB responde
                        System.Diagnostics.Debug.WriteLine("**************** Usuario CAPTURADO********: " + JsonSerializer.Serialize(usuario));
                    }
                }
            }
            catch (Exception ex)
            {
                // Esto te dirá exactamente qué pasó si vuelve a fallar
                System.Diagnostics.Debug.WriteLine("XXXXXXX ERROR EN DALC XXXXXXX: " + ex.Message);
                throw;
            }
            return usuario;
        }
    }
}
