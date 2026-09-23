using Mejora_NeptunoAPP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Mejora_NeptunoAPP.Data
{
    public class ProveedorRepository
    {
        public List<Proveedor> ListarActivos()
        {
            var list = new List<Proveedor>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                using (var cmd = new SqlCommand("sp_ListarProveedoresActivos", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        var dt = new DataTable();
                        da.Fill(dt);
                        foreach (DataRow row in dt.Rows)
                        {
                            list.Add(MapRowToProveedor(row));
                        }
                    }
                }
            }
            return list;
        }
        
        public List<Proveedor> Buscar(string nombreContacto, string ciudad)
        {
            var list = new List<Proveedor>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                using (var cmd = new SqlCommand("sp_ListarProveedoresFiltro", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NombreContacto", string.IsNullOrEmpty(nombreContacto) ? (object)DBNull.Value : nombreContacto);
                    cmd.Parameters.AddWithValue("@Ciudad", string.IsNullOrEmpty(ciudad) ? (object)DBNull.Value : ciudad);
                    
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        var dt = new DataTable();
                        da.Fill(dt);
                        foreach (DataRow row in dt.Rows)
                        {
                            list.Add(MapRowToProveedor(row));
                        }
                    }
                }
            }
            return list;
        }
        
        private Proveedor MapRowToProveedor(DataRow row)
        {
            return new Proveedor
            {
                ProveedorID = (int)row["ProveedorID"],
                CompaniaNombre = row["CompaniaNombre"].ToString(),
                NombreContacto = row["NombreContacto"].ToString(),
                CargoContacto = row["CargoContacto"].ToString(),
                Direccion = row["Direccion"].ToString(),
                Ciudad = row["Ciudad"].ToString(),
                CodigoPostal = row["CodigoPostal"].ToString(),
                Pais = row["Pais"].ToString(),
                Telefono = row["Telefono"].ToString(),
                Fax = row["Fax"].ToString(),
                Activo = (bool)row["Activo"]
            };
        }

        public void Insertar(Proveedor p)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                using (var cmd = new SqlCommand("sp_InsertarProveedor", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CompaniaNombre", p.CompaniaNombre);
                    cmd.Parameters.AddWithValue("@NombreContacto", p.NombreContacto ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@CargoContacto", p.CargoContacto ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Direccion", p.Direccion ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Ciudad", p.Ciudad ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@CodigoPostal", p.CodigoPostal ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Pais", p.Pais ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Telefono", p.Telefono ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Fax", p.Fax ?? (object)DBNull.Value);
                    
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Actualizar(Proveedor p)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                using (var cmd = new SqlCommand("sp_ActualizarProveedor", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProveedorID", p.ProveedorID);
                    cmd.Parameters.AddWithValue("@CompaniaNombre", p.CompaniaNombre);
                    cmd.Parameters.AddWithValue("@NombreContacto", p.NombreContacto ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@CargoContacto", p.CargoContacto ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Direccion", p.Direccion ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Ciudad", p.Ciudad ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@CodigoPostal", p.CodigoPostal ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Pais", p.Pais ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Telefono", p.Telefono ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Fax", p.Fax ?? (object)DBNull.Value);
                    
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void EliminarLogico(int id)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                using (var cmd = new SqlCommand("sp_EliminarProveedorLogico", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProveedorID", id);
                    
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
