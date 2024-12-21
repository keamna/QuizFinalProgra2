using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using Quiz.CapaModelo;

namespace Quiz.CapaLogica
{
    public class Bussiness_Class
    {
        #region Listado de Clases

        public static List<Cls_Class> ObtenerClase()
        {
            int retorno = 0;
            SqlConnection Conn = new SqlConnection();
            List<Cls_Class> clases = new List<Cls_Class>();
            try
            {

                using (Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("consultarClass", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cls_Class clase = new Cls_Class();
                            clase.classId = reader.GetInt32(0);
                            clase.schoolId = reader.GetInt32(1);
                            clase.classNombre = reader.GetString(2);
                            clase.classDescripcion = reader.GetString(3);
                            clase.classEstado = reader.GetString(4);

                            clases.Add(clase);
                        }

                    }
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                return clases;
            }
            finally
            {
                Conn.Close();
                Conn.Dispose();
            }

            return clases;
        }
        #endregion

        #region Agregar Clase
        public static int AgregarClase(int schoolID, string nombre, string descripcion, string estado)
        {
            int retorno = 0;

            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("ingresarClass", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@SchoolId", schoolID));
                    cmd.Parameters.Add(new SqlParameter("@ClassName", nombre));
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", descripcion));
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
        public static List<Cls_Class> ObtenerClassFiltro(int classID)
        {
            List<Cls_Class> clases = new List<Cls_Class>();

            try
            {
                using (SqlConnection Conn = DBConn.obtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("consultarClassFiltro", Conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@ClassId", classID));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Cls_Class clase = new Cls_Class
                                {
                                    classId = reader.GetInt32(reader.GetOrdinal("ClassId")),
                                    schoolId = reader.GetInt32(reader.GetOrdinal("SchoolId")),
                                    classNombre = reader.GetString(reader.GetOrdinal("ClassName")),
                                    classDescripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                                    classEstado = reader.GetString(reader.GetOrdinal("Estado"))
                                };

                                clases.Add(clase);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Manejo de errores
                Console.WriteLine("Error al obtener clase por código: " + ex.Message);
            }

            return clases;
        }

        #endregion

        #region Borrar Clases

        public static bool BorrarClase(int claseID)
        {
            SqlConnection Conn = null;

            try
            {
                Conn = DBConn.obtenerConexion();

                // Verifica si el usuario existe
                string queryExistencia = "SELECT COUNT(*) FROM Class WHERE ClassId = @ClassId";
                using (SqlCommand cmdVerificar = new SqlCommand(queryExistencia, Conn))
                {
                    cmdVerificar.Parameters.Add(new SqlParameter("@ClassId", claseID));

                    int count = (int)cmdVerificar.ExecuteScalar();
                    if (count == 0)
                    {
                        return false;
                    }
                }

                string actualizarEstado = "UPDATE Class SET Estado = 'Inactivo' WHERE ClassId = @ClassId";
                using (SqlCommand cmdEliminar = new SqlCommand(actualizarEstado, Conn))
                {
                    cmdEliminar.Parameters.Add(new SqlParameter("@ClassId", claseID));
                    int filasAfectadas = cmdEliminar.ExecuteNonQuery();

                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al borrar clase: " + ex.Message);
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

        #region Modificar Clase
        public static bool ModificarClase(int claseID, int schoolID, string nombre, string descripcion, string estado)
        {
            SqlConnection Conn = null;
            try
            {

                Conn = DBConn.obtenerConexion();

                // Verifica si el usuario existe
                string queryExistencia = "SELECT COUNT(*) FROM Class WHERE ClassId = @ClassId";
                using (SqlCommand cmdExistencia = new SqlCommand(queryExistencia, Conn))
                {
                    cmdExistencia.Parameters.Add(new SqlParameter("@ClassId", claseID));

                    int count = (int)cmdExistencia.ExecuteScalar();

                    if (count == 0)
                    {
                        return false;
                    }

                    string procedimientoAlmacenado = "modificarClass";
                    using (SqlCommand cmd = new SqlCommand(procedimientoAlmacenado, Conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@ClassId", claseID));
                        cmd.Parameters.Add(new SqlParameter("@SchoolId", schoolID));
                        cmd.Parameters.Add(new SqlParameter("@ClassName", nombre));
                        cmd.Parameters.Add(new SqlParameter("@Descripcion", descripcion));
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
                Console.WriteLine("Error al modificar clase: " + ex.Message);
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