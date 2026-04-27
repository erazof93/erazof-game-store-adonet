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



    }
}
