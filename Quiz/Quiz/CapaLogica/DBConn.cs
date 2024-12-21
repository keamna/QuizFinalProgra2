using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Quiz.CapaLogica
{
    public class DBConn
    {
        public static SqlConnection obtenerConexion()
        {
            string s = System.Configuration.ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            SqlConnection conexion = new SqlConnection(s);
            conexion.Open();
            return conexion;
        }

        public static class JavaScriptHelper
        {
            public static void MostrarAlerta(Page page, string message)
            {
                string script = $"<script type='text/javascript'>alert('{message}');</script>";
                ClientScriptManager cs = page.ClientScript;
                cs.RegisterStartupScript(page.GetType(), "AlertScript", script);
            }
        }
    }
}