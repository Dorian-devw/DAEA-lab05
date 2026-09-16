using Mejora_NeptunoAPP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Mejora_NeptunoAPP.Data
{
    public class PedidoRepository
    {
        public List<Pedido> ListarActivos()
        {
            var list = new List<Pedido>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                using (var cmd = new SqlCommand("sp_ListarPedidosActivos", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Pedido
                            {
                                PedidoID = (int)reader["PedidoID"],
                                ClienteID = reader["ClienteID"] as int?,
                                EmpleadoID = reader["EmpleadoID"] as int?,
                                FechaPedido = (DateTime)reader["FechaPedido"],
                                FechaRequerida = reader["FechaRequerida"] as DateTime?,
                                FechaEnvio = reader["FechaEnvio"] as DateTime?,
                                TransportistaID = reader["TransportistaID"] as int?,
                                Destinatario = reader["Destinatario"].ToString(),
                                CiudadDestino = reader["CiudadDestino"].ToString(),
                                PaisDestino = reader["PaisDestino"].ToString(),
                                Activo = (bool)reader["Activo"]
                            });
                        }
                    }
                }
            }
            return list;
        }
        
        public List<DetallePedido> ReporteDetallesPorFecha(DateTime inicio, DateTime fin)
        {
            var list = new List<DetallePedido>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                using (var cmd = new SqlCommand("sp_ListarDetallesPedidoFecha", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FechaInicio", inicio);
                    cmd.Parameters.AddWithValue("@FechaFin", fin);
                    
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new DetallePedido
                            {
                                PedidoID = (int)reader["PedidoID"],
                                ProductoID = (int)reader["ProductoID"],
                                PrecioUnidad = (decimal)reader["PrecioUnidad"],
                                Cantidad = (short)reader["Cantidad"],
                                Descuento = (decimal)reader["Descuento"],
                                FechaPedido = (DateTime)reader["FechaPedido"]
                            });
                        }
                    }
                }
            }
            return list;
        }

        public void Insertar(Pedido p)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                using (var cmd = new SqlCommand("sp_InsertarPedido", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ClienteID", (object)p.ClienteID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@EmpleadoID", (object)p.EmpleadoID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@FechaPedido", p.FechaPedido);
                    cmd.Parameters.AddWithValue("@FechaRequerida", (object)p.FechaRequerida ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@FechaEnvio", (object)p.FechaEnvio ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TransportistaID", (object)p.TransportistaID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Destinatario", p.Destinatario ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@CiudadDestino", p.CiudadDestino ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@PaisDestino", p.PaisDestino ?? (object)DBNull.Value);
                    
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Actualizar(Pedido p)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                using (var cmd = new SqlCommand("sp_ActualizarPedido", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PedidoID", p.PedidoID);
                    cmd.Parameters.AddWithValue("@ClienteID", (object)p.ClienteID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@EmpleadoID", (object)p.EmpleadoID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@FechaPedido", p.FechaPedido);
                    cmd.Parameters.AddWithValue("@FechaRequerida", (object)p.FechaRequerida ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@FechaEnvio", (object)p.FechaEnvio ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TransportistaID", (object)p.TransportistaID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Destinatario", p.Destinatario ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@CiudadDestino", p.CiudadDestino ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@PaisDestino", p.PaisDestino ?? (object)DBNull.Value);
                    
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void EliminarLogico(int id)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                using (var cmd = new SqlCommand("sp_EliminarPedidoLogico", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PedidoID", id);
                    
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
