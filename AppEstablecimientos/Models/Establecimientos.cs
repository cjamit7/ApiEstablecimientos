using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AppEstablecimientos.Models
{
    public class Establecimientos
    {
        string ConnectionString = "Server=tcp:azurefinal.database.windows.net,1433;Initial Catalog=azureFinal;Persist Security Info=False;User ID=cjamit;Password=Cesar123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public int IDEstablecimiento { get; set; }
        public string Nombre { get; set; }
        public string Giro { get; set; }
        public string Horario { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string PictureBase64 { get; set; }
        

        public List<Establecimientos> GetAll()
        {
            List<Establecimientos> list = new List<Establecimientos>();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string tsql = "getAll";
                    using (SqlCommand cmd = new SqlCommand(tsql, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new Establecimientos
                                {
                                    IDEstablecimiento = (int)reader["id"],
                                    Nombre = reader["nombre"].ToString(),
                                    Giro = reader["giro"].ToString(),
                                    Horario = reader["horario"].ToString(),
                                    Latitude = (double)reader["latitude"],
                                    Longitude = (double)reader["longitude"],
                                    PictureBase64 = reader["foto"].ToString()

                                });
                            }
                        }
                    }
                }
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Establecimientos Get(int id)

        {
            Establecimientos est = new Establecimientos();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string tsql = "getById";
                    using (SqlCommand cmd = new SqlCommand(tsql, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@id", id));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                est = new Establecimientos
                                {
                                    IDEstablecimiento = (int)reader["id"],
                                    Nombre = reader["nombre"].ToString(),
                                    Giro = reader["giro"].ToString(),
                                    Horario = reader["horario"].ToString(),
                                    Latitude = (double)reader["latitude"],
                                    Longitude = (double)reader["longitude"],
                                    PictureBase64 = reader["foto"].ToString()
                                };
                            }
                        }
                    }
                }
                return est;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ApiResponse Insert()
        {
            object id;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("CreateEstablecimiento", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombre", Nombre);
                        cmd.Parameters.AddWithValue("@giro", Giro);
                        cmd.Parameters.AddWithValue("@horario", Horario);
                        cmd.Parameters.AddWithValue("@latitude", Latitude);
                        cmd.Parameters.AddWithValue("@longitude", Longitude);
                        cmd.Parameters.AddWithValue("@foto", PictureBase64);
                        id = cmd.ExecuteNonQuery();
                    }
                }
                if (id != null && id.ToString().Length > 0)
                {
                    return new ApiResponse
                    {
                        IsSuccess = true,
                        Result = int.Parse(id.ToString()),
                        Message = "Establecimiento Creado"
                    };
                    
                }
                else
                {
                    return new ApiResponse
                    {
                        IsSuccess = false,
                        Result = 0,
                        Message = "ERROR"
                    };
                   
                }
            }
            catch (Exception exc)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Result = null,
                    Message = exc.Message
                };
                throw;
            }
        }

        public ApiResponse Update(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UpdateEstablecimiento", conn))
                    {
                        
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", IDEstablecimiento);
                        cmd.Parameters.AddWithValue("@nombre", Nombre);
                        cmd.Parameters.AddWithValue("@giro", Giro);
                        cmd.Parameters.AddWithValue("@horario", Horario);
                        cmd.Parameters.AddWithValue("@latitude", Latitude);
                        cmd.Parameters.AddWithValue("@longitude", Longitude);
                        cmd.Parameters.AddWithValue("@foto", PictureBase64);
                        id = cmd.ExecuteNonQuery();
                    }
                }
                return new ApiResponse
                {
                    IsSuccess = true,
                    Result = IDEstablecimiento,
                    Message = "Establecimiento actualizado"
                };
            }
            catch (Exception exc)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Result = null,
                    Message = exc.Message
                };
                throw;
            }
        }

        public ApiResponse Delete(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string tsql = "DeleteEstablecimiento";

                    using (SqlCommand cmd = new SqlCommand(tsql, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                return new ApiResponse
                {
                    IsSuccess = true,
                    Result = id,
                    Message = "Establecimiento eliminado"
                };
            }
            catch (Exception exc)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Result = null,
                    Message = exc.Message
                };
                throw;
            }
        }
    }
}
    