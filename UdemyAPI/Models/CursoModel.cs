using System.Data.SqlClient;

namespace UdemyAPI.Models
{
    public class CursoModel
    {
        // Variables
        string ConnectionString = "Server=tcp:sqlservercursoslfm.database.windows.net,1433;Initial Catalog=sqlcursoslfm;Persist Security Info=False;User ID=pancho;Password=contraSql19;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";


        // Propiedades
        public int ID { get; set; }
        public string? Nombre { get; set; }
        public double Precio { get; set; }
        public string? Descripcion { get; set; }
        public string? FechaDeCreacion { get; set; }
        public string? FotoDelCursoBase64 { get; set; }
        public string? NombreDelProfesor { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        //Metodos

        public ApiResponse GetAll()
        {
            List<CursoModel> list = new List<CursoModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string tsql = "SELECT * FROM Curso ";
                    using (SqlCommand cmd = new SqlCommand(tsql, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new CursoModel
                                {
                                    ID = (int)reader["IDCurso"],
                                    Nombre = reader["Nombre"].ToString(),
                                    Precio = (double)reader["Precio"],
                                    Descripcion = reader["Descripcion"].ToString(),
                                    FechaDeCreacion = reader["FechaDeCreacion"].ToString(),
                                    FotoDelCursoBase64 = reader["FotoDelCursoBase64"].ToString(),
                                    NombreDelProfesor = reader["NombreDelProfesor"].ToString(),
                                    Latitude = (double)reader["Latitude"],
                                    Longitude = (double)reader["Longitude"]
                                });
                            }
                        }
                    }
                }
                int IdIncrementable = list.Last().ID;
                return new ApiResponse
                {
                    IsSuccess = true,
                    Message = "Los cursos fueron obtenidos correctamente",
                    Result = list

                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = $"Se genero un error al obtener los cursos: {ex.Message}",
                    Result = null

                };
            }

        }

        public ApiResponse Get(int id)
        {

            CursoModel model = new CursoModel();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    string tsql = "SELECT * FROM Curso WHERE IDCurso = @ID";
                    using (SqlCommand cmd = new SqlCommand(tsql, con))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                model = new CursoModel()
                                {
                                    ID = (int)reader["IDCurso"],
                                    Nombre = reader["Nombre"].ToString(),
                                    Precio = (double)reader["Precio"],
                                    Descripcion = reader["Descripcion"].ToString(),
                                    FechaDeCreacion = reader["FechaDeCreacion"].ToString(),
                                    FotoDelCursoBase64 = reader["FotoDelCursoBase64"].ToString(),
                                    NombreDelProfesor = reader["NombreDelProfesor"].ToString(),
                                    Latitude = (double)reader["Latitude"],
                                    Longitude = (double)reader["Longitude"]
                                };
                            }
                        }
                    }
                }
                return new ApiResponse
                {
                    IsSuccess = true,
                    Message = "EL curso especificado fue obtenido correctamente",
                    Result = model

                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = $"Ocurrio un erro al intentar obtener el curso especificado: {ex.Message}",
                    Result = null

                };
            }
        }

        public ApiResponse Add(CursoModel model) 
        {
            try
            {
                //object newID;
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    string tsql = "INSERT INTO Curso " +
                                    "(Nombre, " +
                                    "Precio, " +
                                    "Descripcion, " +
                                    "FechaDeCreacion, " +
                                    "FotoDelCursoBase64, " +
                                    "NombreDelProfesor, " +
                                    "Latitude, " +
                                    "Longitude) " +
                                    "VALUES " +
                                    "(@Nombre, " +
                                    "@Precio, " +
                                    "@Descripcion, " +
                                    "@FechaDeCreacion, " +
                                    "@FotoDelCursoBase64, " +
                                    "@NombreDelProfesor, " +
                                    "@Latitude, " +
                                    "@Longitude); ";
                    using (SqlCommand cmd = new SqlCommand(tsql, con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@Nombre", model.Nombre);
                        cmd.Parameters.AddWithValue("@Precio", model.Precio);
                        cmd.Parameters.AddWithValue("@Descripcion", model.Descripcion);
                        cmd.Parameters.AddWithValue("@FechaDeCreacion", model.FechaDeCreacion);
                        cmd.Parameters.AddWithValue("@FotoDelCursoBase64", model.FotoDelCursoBase64);
                        cmd.Parameters.AddWithValue("@NombreDelProfesor", model.NombreDelProfesor);
                        cmd.Parameters.AddWithValue("@Latitude", model.Latitude);
                        cmd.Parameters.AddWithValue("@Longitude", model.Longitude);
                        cmd.ExecuteNonQuery();

                        //if (newID != null && newID.ToString().Length > 0)
                        //{
                        //    return int.Parse(newID.ToString());
                        //}
                    }
                }
                return new ApiResponse
                {
                    IsSuccess = true,
                    Message = "EL curso fue dado de alta exitosamente",
                    Result = model

                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = $"Ocurrio un erro al intentar dar de alta el curso: {ex.Message}",
                    Result = null

                };
            }
        }

        public ApiResponse Update(CursoModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    string tsql = "UPDATE Curso SET Nombre = @Nombre, Precio = @Precio, Descripcion = @Descripcion, FechaDeCreacion = @FechaDeCreacion, FotoDelCursoBase64 = @FotoDelCursoBase64, NombreDelProfesor = @NombreDelProfesor, Latitude = @Latitude, Longitude = @Longitude " +
                    "WHERE IDCurso = @IDCurso";
                    using (SqlCommand cmd = new SqlCommand(tsql, con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@IDCurso", model.ID);
                        cmd.Parameters.AddWithValue("@Nombre", model.Nombre);
                        cmd.Parameters.AddWithValue("@Precio", model.Precio);
                        cmd.Parameters.AddWithValue("@Descripcion", model.Descripcion);
                        cmd.Parameters.AddWithValue("@FechaDeCreacion", model.FechaDeCreacion);
                        cmd.Parameters.AddWithValue("@FotoDelCursoBase64", model.FotoDelCursoBase64);
                        cmd.Parameters.AddWithValue("@NombreDelProfesor", model.NombreDelProfesor);
                        cmd.Parameters.AddWithValue("@Latitude", model.Latitude);
                        cmd.Parameters.AddWithValue("@Longitude", model.Longitude);
                        cmd.ExecuteNonQuery();
                    }
                }

                return new ApiResponse
                {
                    IsSuccess = true,
                    Message = "EL curso fue actualizado exitosamente",
                    Result = model

                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = $"Ocurrio un erro al intentar actualizar el curso: {ex.Message}",
                    Result = null

                };
            }
        }

        public ApiResponse Delete(int id)
        {
            //Products.Remove(Get(id));

            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    string tsql = "DELETE FROM Curso WHERE IDCurso = @IDCurso";
                    using (SqlCommand cmd = new SqlCommand(tsql, con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@IDCurso", id);
                        cmd.ExecuteNonQuery();
                    }
                }

                return new ApiResponse
                {
                    IsSuccess = true,
                    Message = "EL curso fue eliminado exitosamente",
                    Result = id

                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = $"Ocurrio un erro al intentar eliminar el curso: {ex.Message}",
                    Result = null

                };
            }
        }

    }
}
