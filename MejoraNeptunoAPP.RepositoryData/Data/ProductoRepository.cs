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
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        var dt = new DataTable();
                        da.Fill(dt);
                        foreach (DataRow row in dt.Rows)
                        {
                            list.Add(new Producto
                            {
                                ProductoID = (int)row["ProductoID"],
                                NombreProducto = row["NombreProducto"].ToString(),
                                ProveedorID = row["ProveedorID"] == DBNull.Value ? (int?)null : (int)row["ProveedorID"],
                                CategoriaID = row["CategoriaID"] == DBNull.Value ? (int?)null : (int)row["CategoriaID"],
                                CantidadPorUnidad = row["CantidadPorUnidad"].ToString(),
                                PrecioUnidad = (decimal)row["PrecioUnidad"],
                                UnidadesEnExistencia = (short)row["UnidadesEnExistencia"],
                                UnidadesEnPedido = (short)row["UnidadesEnPedido"],
                                NivelDeReorden = (short)row["NivelDeReorden"],
                                Descontinuado = (bool)row["Descontinuado"],
                                Activo = (bool)row["Activo"]
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
                    cmd.ExecuteNonQuery(); 
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
                    cmd.ExecuteNonQuery(); 
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
                    cmd.ExecuteNonQuery(); 
                }
            }
        }
    }
}
