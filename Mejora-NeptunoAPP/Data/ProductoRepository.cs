using Mejora_NeptunoAPP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Mejora_NeptunoAPP.Data
{
    public class ProductoRepository
    {
        public List<Producto> ListarActivos()
        {
            var list = new List<Producto>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                using (var cmd = new SqlCommand("sp_ListarProductosActivos", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Producto
                            {
                                ProductoID = (int)reader["ProductoID"],
                                NombreProducto = reader["NombreProducto"].ToString(),
                                ProveedorID = reader["ProveedorID"] as int?,
                                CategoriaID = reader["CategoriaID"] as int?,
                                CantidadPorUnidad = reader["CantidadPorUnidad"].ToString(),
                                PrecioUnidad = (decimal)reader["PrecioUnidad"],
                                UnidadesEnExistencia = (short)reader["UnidadesEnExistencia"],
                                UnidadesEnPedido = (short)reader["UnidadesEnPedido"],
                                NivelDeReorden = (short)reader["NivelDeReorden"],
                                Descontinuado = (bool)reader["Descontinuado"],
                                Activo = (bool)reader["Activo"]
                            });
                        }
                    }
                }
            }
            return list;
        }

        public void Insertar(Producto p)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                using (var cmd = new SqlCommand("sp_InsertarProducto", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NombreProducto", p.NombreProducto);
                    cmd.Parameters.AddWithValue("@ProveedorID", (object)p.ProveedorID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CategoriaID", (object)p.CategoriaID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CantidadPorUnidad", p.CantidadPorUnidad ?? "");
                    cmd.Parameters.AddWithValue("@PrecioUnidad", p.PrecioUnidad);
                    cmd.Parameters.AddWithValue("@UnidadesEnExistencia", p.UnidadesEnExistencia);
                    cmd.Parameters.AddWithValue("@UnidadesEnPedido", p.UnidadesEnPedido);
                    cmd.Parameters.AddWithValue("@NivelDeReorden", p.NivelDeReorden);
                    cmd.Parameters.AddWithValue("@Descontinuado", p.Descontinuado);
                    
                    conn.Open();
                    cmd.ExecuteNonQuery(); // As requested
                }
            }
        }

        public void Actualizar(Producto p)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                using (var cmd = new SqlCommand("sp_ActualizarProducto", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductoID", p.ProductoID);
                    cmd.Parameters.AddWithValue("@NombreProducto", p.NombreProducto);
                    cmd.Parameters.AddWithValue("@ProveedorID", (object)p.ProveedorID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CategoriaID", (object)p.CategoriaID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CantidadPorUnidad", p.CantidadPorUnidad ?? "");
                    cmd.Parameters.AddWithValue("@PrecioUnidad", p.PrecioUnidad);
                    cmd.Parameters.AddWithValue("@UnidadesEnExistencia", p.UnidadesEnExistencia);
                    cmd.Parameters.AddWithValue("@UnidadesEnPedido", p.UnidadesEnPedido);
                    cmd.Parameters.AddWithValue("@NivelDeReorden", p.NivelDeReorden);
                    cmd.Parameters.AddWithValue("@Descontinuado", p.Descontinuado);
                    
                    conn.Open();
                    cmd.ExecuteNonQuery(); // As requested
                }
            }
        }

        public void EliminarLogico(int id)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                using (var cmd = new SqlCommand("sp_EliminarProductoLogico", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductoID", id);
                    
                    conn.Open();
                    cmd.ExecuteNonQuery(); // As requested
                }
            }
        }
    }
}
