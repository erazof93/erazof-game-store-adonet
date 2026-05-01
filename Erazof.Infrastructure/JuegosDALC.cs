using Erazof.Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Erazof.Infrastructure
{
    public class JuegosDALC
    {
        public async Task<List<Genero>> listarGenero()
        {
            List<Genero> genero = new List<Genero>();
            using (SqlConnection cn = DBConexion.obtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Generos", cn))
                {
                    cmd.CommandType = CommandType.Text;
                    try
                    {
                        await cn.OpenAsync();
                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {
                            while (await dr.ReadAsync())
                            {
                                genero.Add(new Genero()
                                {
                                    GeneroID = dr.GetInt32(0),
                                    NombreGenero = dr.GetString(1)
                                    
                                });
                            }
                        }
                        System.Diagnostics.Debug.WriteLine("**************** lista de empleados: " + JsonSerializer.Serialize(genero));
                        return genero;

                    }
                    catch (SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
        }


        public async Task<List<Swiper>> listarswiper()
        {
            List<Swiper> swiper = new List<Swiper>();
            using (SqlConnection cn = DBConexion.obtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("usp_CatalogoPrimeraFotoJuego", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        await cn.OpenAsync();
                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {
                            while (await dr.ReadAsync())
                            {
                                swiper.Add(new Swiper()
                                {
                                    juegoid = dr.GetInt32(0),
                                    titulo = dr.GetString(1),
                                    url = dr.GetString(2),
                                    portada = dr.GetBoolean(3),
                                    generoid = dr.GetInt32(4),
                                    generoNombre = dr.GetString(5)
                                });
                            }
                        }
                        System.Diagnostics.Debug.WriteLine("**************** lista de swiper: " + JsonSerializer.Serialize(swiper));
                        return swiper;

                    }
                    catch (SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
        }


        public async Task<int> insertarJuego(Juego juego)
        {
            int resultado = 0;
            using (SqlConnection cn = DBConexion.obtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("usp_AgregarJuego", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Titulo", juego.Titulo);
                    cmd.Parameters.AddWithValue("@Descripcion", juego.Descripcion);
                    cmd.Parameters.AddWithValue("@Precio", juego.Precio);

                    //output
                    SqlParameter pId = new SqlParameter("@respuesta", SqlDbType.Int);
                    pId.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(pId);
                    try
                    {
                        await cn.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                        resultado = (int)pId.Value;
                        return resultado;

                    }
                    catch (SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }   
        }


        public async Task<int> insertarJuegoImagen(JuegoImagen ji)
        {
            int resultado = 0;
            using (SqlConnection cn = DBConexion.obtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("usp_agregar_imagenCompleto", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Titulo", ji.titulo);
                    cmd.Parameters.AddWithValue("@Descripcion", ji.descripcion);
                    cmd.Parameters.AddWithValue("@Precio", ji.precio);
                    cmd.Parameters.AddWithValue("@Url", ji.url);

                    //output
                    SqlParameter pId = new SqlParameter("@Juegoid", SqlDbType.Int);
                    pId.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(pId);
                    try
                    {
                        await cn.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                        resultado = (int)pId.Value;
                        return resultado;

                    }
                    catch (SqlException ex)
                    {
                        throw new Exception("Error en la base de datos: " + ex.Message, ex);
                    }
                }
            }
        }



        //------------------- PRUEBA
        public async Task insertarJuegoGenero(int juegoId, int generoId)
        {
            using (SqlConnection cn = DBConexion.obtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand(@"
                        INSERT INTO JuegoGeneros (JuegoID, GeneroID)
                            VALUES (@JuegoID, @GeneroID)
                    ", cn))
                {
                    cmd.Parameters.AddWithValue("@JuegoID", juegoId);
                    cmd.Parameters.AddWithValue("@GeneroID", generoId);

                    await cn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }


        public async Task<List<BibliotecaUsuario>> ObtenerBibliotecaUsuario()
        {
            List<BibliotecaUsuario> biblioteca = new List<BibliotecaUsuario>();
            using (SqlConnection cn = DBConexion.obtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("usp_Lista_Biblioteca", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        await cn.OpenAsync();
                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {
                            while (await dr.ReadAsync())
                            {
                                biblioteca.Add(new BibliotecaUsuario()
                                {
                                    UserId = dr.GetInt32(0),
                                    UserName = dr.GetString(1),
                                    Email = dr.GetString(2),
                                    GameID = dr.GetInt32(3),
                                    titulo = dr.GetString(4),
                                    precio = dr.GetDecimal(5),
                                    FechaCompra = dr.GetDateTime(6)
                                });
                            }
                        }
                        System.Diagnostics.Debug.WriteLine("**************** lista de biblioteca: " + JsonSerializer.Serialize(biblioteca));
                        return biblioteca;
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
        }



        public async Task<JuegoDetalle> ObtenerJuegoPorId(int id)
        {
            JuegoDetalle juego = null;

            using (SqlConnection cn = DBConexion.obtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("usp_Listar_Por_Id", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idJuego", id);
                    try
                    {
                        await cn.OpenAsync();
                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {
                            if (await dr.ReadAsync())
                            {
                                juego = new JuegoDetalle
                                {
                                    JuegoID = dr.GetInt32(0),
                                    Titulo = dr.GetString(1),
                                    Url = dr.GetString(2),
                                    portada = dr.GetBoolean(3),
                                    Precio = dr.GetDecimal(4),
                                    FechaLanz = dr.GetDateTime(5),
                                    Generos = dr.GetString(6)
                                };
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("ERORRRRRRR *************************** " + ex.Message);
                        throw new Exception(ex.Message);
                    }
                }
            }
            System.Diagnostics.Debug.WriteLine("****************ID CAPTURADO " + JsonSerializer.Serialize(juego));
            return juego;
        }

        // ------------------- BIBLIOTECA DEL USUARIO

        public async Task<List<BibliotecaUser>> ObtenerBibliotecaPorID(int id)
        {
            List<BibliotecaUser> listaBiblioteca = new List<BibliotecaUser>();

            using (SqlConnection cn = DBConexion.obtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_ObtenerBibliotecaUsuario", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UsuarioID", id);
                    try
                    {
                        await cn.OpenAsync();
                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {
                            while (await dr.ReadAsync())
                            {
                                listaBiblioteca.Add(new BibliotecaUser
                                {
                                    JuegoID = dr.GetInt32(0),
                                    Titulo = dr.GetString(1),
                                    Descripcion = dr.GetString(2),
                                    Precio = dr.GetDecimal(3),
                                    Fechala = dr.GetDateTime(4),
                                    url = dr.GetString(5),
                                    FechaCompra = dr.GetDateTime(6)
                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("ERORRRRRRR *************************** ID " + ex.Message);
                        throw new Exception(ex.Message);
                    }
                }
            }
            System.Diagnostics.Debug.WriteLine("****************ID CAPTURADO " + JsonSerializer.Serialize(listaBiblioteca));
            return listaBiblioteca;
        }

        public async Task agregarJuego(int usuarioID, int juegoID)
        {
            using (SqlConnection cn = DBConexion.obtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("usp_agregarJuegoDeUsuario", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UsuarioID", usuarioID);
                    cmd.Parameters.AddWithValue("@JuegoID", juegoID);

                    await cn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }



    }
}
