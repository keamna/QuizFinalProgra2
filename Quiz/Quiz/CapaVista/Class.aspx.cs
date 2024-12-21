using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Quiz.CapaLogica;
using Quiz.CapaModelo;

namespace Quiz.CapaVista
{
    public partial class Class : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenarGrid();

            }
        }

        protected void LlenarGrid()
        {
            List<Cls_Class> clases = Bussiness_Class.ObtenerClase();

            if (clases.Count > 0)
            {
                GridViewClass.DataSource = clases;
                GridViewClass.DataBind();


                foreach (GridViewRow row in GridViewClass.Rows)
                {
                    DropDownList ddlEstado = (DropDownList)row.FindControl("DropDownListClass");
                    if (ddlEstado != null)
                    {
                        string estado = ((Label)row.FindControl("lblEstado")).Text;
                    }
                }
            }
            else
            {
                DBConn.JavaScriptHelper.MostrarAlerta(this, "No hay clases disponibles");
            }

        }

        protected void Bagregar_Click(object sender, EventArgs e)
        {
            try
            {
                string estadoSeleccionado = DropDownListClass.SelectedItem.Text;

                int schoolID = int.Parse(TcodigoSchool.Text);

                if (Bussiness_Class.AgregarClase(schoolID, Tnombre.Text, Tdescripcion.Text, estadoSeleccionado) > 0)
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Clase ingresada correctamente");
                    LlenarGrid();

                    Tnombre.Text = "";
                    TcodigoSchool.Text = "";
                    Tdescripcion.Text = "";
                    DropDownListClass.SelectedIndex = 0;
                }
                else
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Error al agregar clase");
                }
            }
            catch (Exception ex)
            {
                DBConn.JavaScriptHelper.MostrarAlerta(this, "Error al agregar clase: " + ex.Message);
            }
        }

        protected void BconsultarFiltro_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TcodigoClass.Text))
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Por favor ingresa un código válido");
                    return;
                }

                // Verifica que el codigo sea un int 
                int codigo;
                if (!int.TryParse(TcodigoClass.Text, out codigo))
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "El código ingresado no es un número válido");
                    return;
                }

                List<Cls_Class> clases = Bussiness_Class.ObtenerClassFiltro(codigo);

                // Verifica si hay resultados
                if (clases.Count > 0)
                {
                    // Muestra solo los datos deseados
                    GridViewClass.DataSource = clases;
                    GridViewClass.DataBind();

                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Código encontrado");
                }
                else
                {
                    // Si no hay datos muestra mensaje y limpia el GridView
                    GridViewClass.DataSource = null;
                    GridViewClass.DataBind();

                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Código no encontrado");
                    LlenarGrid();

                }
            }
            catch (FormatException)
            {
                DBConn.JavaScriptHelper.MostrarAlerta(this, "El código ingresado no es válido. Por favor ingresa un número.");
            }
            catch (Exception ex)
            {
                DBConn.JavaScriptHelper.MostrarAlerta(this, "Ocurrió un error: " + ex.Message);
            }
        }

        protected void Bborrar_Click(object sender, EventArgs e)
        {
            try
            {
                int codigo = int.Parse(TcodigoClass.Text);

                bool isDeleted = Bussiness_Class.BorrarClase(codigo);

                if (isDeleted)
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Clase borrada correctamente");
                }
                else
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Código no existe o no se pudo borrar");

                }

                LlenarGrid();
                TcodigoClass.Text = "";

            }
            catch (Exception ex)
            {
                DBConn.JavaScriptHelper.MostrarAlerta(this, "Ocurrió un error: " + ex.Message);

                LlenarGrid();
            }
        }

        protected void Bmodificar_Click(object sender, EventArgs e)
        {
            try
            {
                int classID = int.Parse(TcodigoClass.Text);
                int schoolID = int.Parse(TcodigoSchool.Text);
                string nombre = Tnombre.Text;
                string descripcion = Tdescripcion.Text;
                string estado = DropDownListClass.SelectedItem.Text;

                bool isUpdated = Bussiness_Class.ModificarClase(classID, schoolID, nombre, descripcion, estado);

                if (isUpdated)
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Clase modificada correctamente");

                    // Recarga los datos actualizados en el grid view
                    LlenarGrid();

                    TcodigoClass.Text = "";
                    TcodigoSchool.Text = "";
                    Tnombre.Text = "";
                    Tdescripcion.Text = "";
                    DropDownListClass.SelectedIndex = 0;
                }
                else
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Código no existe o no se pudo modificar");
                }
            }
            catch (Exception ex)
            {
                // Muestra el mensaje del error en especifico
                DBConn.JavaScriptHelper.MostrarAlerta(this, "Ocurrió un error: " + ex.Message);
            }
        }
    }
}