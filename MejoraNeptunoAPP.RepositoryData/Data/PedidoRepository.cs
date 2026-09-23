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
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        var dt = new DataTable();
                        da.Fill(dt);
                        foreach (DataRow row in dt.Rows)
                        {
                            list.Add(new Pedido
                            {
                                PedidoID = (int)row["PedidoID"],
                                ClienteID = row["ClienteID"] == DBNull.Value ? (int?)null : (int)row["ClienteID"],
                                EmpleadoID = row["EmpleadoID"] == DBNull.Value ? (int?)null : (int)row["EmpleadoID"],
                                FechaPedido = (DateTime)row["FechaPedido"],
                                FechaRequerida = row["FechaRequerida"] == DBNull.Value ? (DateTime?)null : (DateTime)row["FechaRequerida"],
                                FechaEnvio = row["FechaEnvio"] == DBNull.Value ? (DateTime?)null : (DateTime)row["FechaEnvio"],
                                TransportistaID = row["TransportistaID"] == DBNull.Value ? (int?)null : (int)row["TransportistaID"],
                                Destinatario = row["Destinatario"].ToString(),
                                CiudadDestino = row["CiudadDestino"].ToString(),
                                PaisDestino = row["PaisDestino"].ToString(),
                                Activo = (bool)row["Activo"]
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
                    
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        var dt = new DataTable();
                        da.Fill(dt);
                        foreach (DataRow row in dt.Rows)
                        {
                            list.Add(new DetallePedido
                            {
                                PedidoID = (int)row["PedidoID"],
                                ProductoID = (int)row["ProductoID"],
                                PrecioUnidad = (decimal)row["PrecioUnidad"],
                                Cantidad = (short)row["Cantidad"],
                                Descuento = (decimal)row["Descuento"],
                                FechaPedido = (DateTime)row["FechaPedido"]
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
