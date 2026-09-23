using Mejora_NeptunoAPP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Mejora_NeptunoAPP.Data
{
    public class CategoriaRepository
    {
        public List<Categoria> ListarActivos()
        {
            var list = new List<Categoria>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                using (var cmd = new SqlCommand("sp_ListarCategoriasActivas", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        var dt = new DataTable();
                        da.Fill(dt);
                        foreach (DataRow row in dt.Rows)
                        {
                            list.Add(new Categoria
                            {
                                CategoriaID = (int)row["CategoriaID"],
                                NombreCategoria = row["NombreCategoria"].ToString(),
                                Descripcion = row["Descripcion"].ToString(),
                                Activo = (bool)row["Activo"]
                            });
                        }
                    }
                }
            }
            return list;
        }

        public void Insertar(Categoria c)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                using (var cmd = new SqlCommand("sp_InsertarCategoria", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NombreCategoria", c.NombreCategoria);
                    cmd.Parameters.AddWithValue("@Descripcion", c.Descripcion ?? (object)DBNull.Value);
                    
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Actualizar(Categoria c)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                using (var cmd = new SqlCommand("sp_ActualizarCategoria", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CategoriaID", c.CategoriaID);
                    cmd.Parameters.AddWithValue("@NombreCategoria", c.NombreCategoria);
                    cmd.Parameters.AddWithValue("@Descripcion", c.Descripcion ?? (object)DBNull.Value);
                    
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void EliminarLogico(int id)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                using (var cmd = new SqlCommand("sp_EliminarCategoriaLogico", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CategoriaID", id);
                    
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
