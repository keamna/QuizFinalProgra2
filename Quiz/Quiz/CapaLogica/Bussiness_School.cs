using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using Quiz.CapaModelo;

namespace Quiz.CapaLogica
{
    public class Bussiness_School
    {
        #region Listado de Escuelas

        public static List<Cls_School> ObtenerSchool()
        {
            int retorno = 0;
            SqlConnection Conn = new SqlConnection();
            List<Cls_School> schools = new List<Cls_School>();
            try
            {

                using (Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("consultarSchool", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cls_School school = new Cls_School();
                            school.schoolId = reader.GetInt32(0);
                            school.schoolName = reader.GetString(1);
                            school.descripcion = reader.GetString(2);
                            school.direccion = reader.GetString(3);
                            school.phone = reader.GetString(4);
                            school.postCode = reader.GetString(5);
                            school.postAdress = reader.GetString(6);
                            school.estadoSchool = reader.GetString(7);

                            schools.Add(school);
                        }

                    }
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                return schools;
            }
            finally
            {
                Conn.Close();
                Conn.Dispose();
            }

            return schools;
        }
        #endregion

        #region Agregar Escuelas
        public static int AgregarSchool(string nombre, string descripcion, string direccion, string phone, string postCode, string postAdress, string estado)
        {
            int retorno = 0;

            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("ingresarSchool", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@SchoolName", nombre));
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", descripcion));
                    cmd.Parameters.Add(new SqlParameter("@Direccion", direccion));
                    cmd.Parameters.Add(new SqlParameter("@Phone", phone));
                    cmd.Parameters.Add(new SqlParameter("@PostCode", postCode));
                    cmd.Parameters.Add(new SqlParameter("@PostAdress", postAdress));
                    cmd.Parameters.Add(new SqlParameter("@Estado", estado));

                    retorno = cmd.ExecuteNonQuery();
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                retorno = -1;
            }
            finally
            {
                Conn.Close();
            }

            return retorno;
        }
        #endregion


        #region Listado con Filtro
        public static List<Cls_School> ObtenerSchoolFiltro(int usuarioID)
        {
            List<Cls_School> schools = new List<Cls_School>();

            try
            {
                using (SqlConnection Conn = DBConn.obtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("consultarSchoolFiltro", Conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@SchoolId", usuarioID));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Cls_School school = new Cls_School
                                {
                                    schoolId = reader.GetInt32(reader.GetOrdinal("SchoolId")),
                                    schoolName = reader.GetString(reader.GetOrdinal("SchoolName")),
                                    descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                                    direccion = reader.GetString(reader.GetOrdinal("Direccion")),
                                    phone = reader.GetString(reader.GetOrdinal("Phone")),
                                    postCode = reader.GetString(reader.GetOrdinal("PostCode")),
                                    postAdress = reader.GetString(reader.GetOrdinal("PostAdress")),
                                    estadoSchool = reader.GetString(reader.GetOrdinal("Estado"))
                                };

                                schools.Add(school);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Manejo de errores
                Console.WriteLine("Error al obtener escuela por código: " + ex.Message);
            }

            return schools;
        }

        #endregion

        #region Borrar Escuela

        public static bool BorrarEscuela(int schoolId)
        {
            SqlConnection Conn = null;

            try
            {
                Conn = DBConn.obtenerConexion();

                string queryExistencia = "SELECT COUNT(*) FROM School WHERE SchoolId = @SchoolId";
                using (SqlCommand cmdVerificar = new SqlCommand(queryExistencia, Conn))
                {
                    cmdVerificar.Parameters.Add(new SqlParameter("@SchoolId", schoolId));

                    int count = (int)cmdVerificar.ExecuteScalar();
                    if (count == 0)
                    {
                        return false;
                    }
                }

                string actualizarEstado = "UPDATE School SET Estado = 'Inactivo' WHERE SchoolId = @SchoolId";
                using (SqlCommand cmdEliminar = new SqlCommand(actualizarEstado, Conn))
                {
                    cmdEliminar.Parameters.Add(new SqlParameter("@SchoolId", schoolId));
                    int filasAfectadas = cmdEliminar.ExecuteNonQuery();

                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al borrar escuela: " + ex.Message);
                return false;
            }
            finally
            {
                if (Conn != null && Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        #endregion

        #region Modificar Escuela
        public static bool ModificarEscuela(int schoolId, string nombre, string descripcion, string direccion, string phone, string postCode, string postAdress, string estado)
        {
            SqlConnection Conn = null;
            try
            {

                Conn = DBConn.obtenerConexion();

                string queryExistencia = "SELECT COUNT(*) FROM School WHERE SchoolId = @SchoolId";
                using (SqlCommand cmdExistencia = new SqlCommand(queryExistencia, Conn))
                {
                    cmdExistencia.Parameters.Add(new SqlParameter("@SchoolId", schoolId));

                    int count = (int)cmdExistencia.ExecuteScalar();

                    if (count == 0)
                    {
                        return false;
                    }

                    string procedimientoAlmacenado = "modificarSchool";
                    using (SqlCommand cmd = new SqlCommand(procedimientoAlmacenado, Conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@SchoolId", schoolId));
                        cmd.Parameters.Add(new SqlParameter("@SchoolName", nombre));
                        cmd.Parameters.Add(new SqlParameter("@Descripcion", descripcion));
                        cmd.Parameters.Add(new SqlParameter("@Direccion", direccion));
                        cmd.Parameters.Add(new SqlParameter("@Phone", phone));
                        cmd.Parameters.Add(new SqlParameter("@PostCode", postCode));
                        cmd.Parameters.Add(new SqlParameter("@PostAdress", postAdress));
                        cmd.Parameters.Add(new SqlParameter("@Estado", estado));

                        int filasAfectadas = cmd.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error al modificar escuela: " + ex.Message);
                return false;
            }
            finally
            {
                if (Conn != null && Conn.State == ConnectionState.Open)
                {
                    Conn.Close();

                    #endregion

                }
            }
        }
    }
}