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
    public partial class School : System.Web.UI.Page
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
            List<Cls_School> schools = Bussiness_School.ObtenerSchool();

            if (schools.Count > 0)
            {
                GridViewSchool.DataSource = schools;
                GridViewSchool.DataBind();


                foreach (GridViewRow row in GridViewSchool.Rows)
                {
                    DropDownList ddlEstado = (DropDownList)row.FindControl("DropDownListSchool");
                    if (ddlEstado != null)
                    {
                        string estado = ((Label)row.FindControl("lblEstado")).Text;
                    }
                }
            }
            else
            {
                DBConn.JavaScriptHelper.MostrarAlerta(this, "No hay escuelas disponibles");
            }

        }

        protected void Bagregar_Click(object sender, EventArgs e)
        {
            try
            {
                string estadoSeleccionado = DropDownListSchool.SelectedItem.Text;


                if (Bussiness_School.AgregarSchool(TnombreSchool.Text, Tdescripcion.Text, Tdireccion.Text, Tphone.Text, TpostCode.Text, TpostAdress.Text, estadoSeleccionado) > 0)
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Clase ingresada correctamente");
                    LlenarGrid();

                    Tdescripcion.Text = "";
                    Tdireccion.Text = "";
                    Tphone.Text = "";
                    TpostCode.Text = "";
                    TpostAdress.Text = "";
                    DropDownListSchool.SelectedIndex = 0;
                }
                else
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Error al agregar escuela");
                }
            }
            catch (Exception ex)
            {
                DBConn.JavaScriptHelper.MostrarAlerta(this, "Error al agregar escuela: " + ex.Message);
            }
        }

        protected void BconsultarFiltro_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica que el codigo no este vacío
                if (string.IsNullOrWhiteSpace(TcodigoSchool.Text))
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Por favor ingresa un código válido");
                    return;
                }

                // Verifica que el codigo sea un int 
                int codigo;
                if (!int.TryParse(TcodigoSchool.Text, out codigo))
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "El código ingresado no es un número válido");
                    return;
                }

                List<Cls_School> schools = Bussiness_School.ObtenerSchoolFiltro(codigo);

                // Verifica si hay resultados
                if (schools.Count > 0)
                {
                    // Muestra solo los datos deseados
                    GridViewSchool.DataSource = schools;
                    GridViewSchool.DataBind();

                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Código encontrado");

                    TcodigoSchool.Text = "";
                }
                else
                {
                    // Si no hay datos muestra mensaje y limpia el GridView
                    GridViewSchool.DataSource = null;
                    GridViewSchool.DataBind();

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
                int codigo = int.Parse(TcodigoSchool.Text);

                bool isDeleted = Bussiness_School.BorrarEscuela(codigo);

                if (isDeleted)
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Escuela borrada correctamente");
                }
                else
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Código no existe o no se pudo borrar");

                }

                LlenarGrid();
                TcodigoSchool.Text = "";

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
                int codigoSchool = int.Parse(TcodigoSchool.Text);
                string nombre = TnombreSchool.Text;
                string descripcion = Tdescripcion.Text;
                string direccion = Tdireccion.Text;
                string phone = Tphone.Text;
                string postCode = TpostCode.Text;
                string postAdress = TpostAdress.Text;
                string estado = DropDownListSchool.SelectedItem.Text;

                bool isUpdated = Bussiness_School.ModificarEscuela(codigoSchool, nombre, descripcion, direccion, phone, postCode, postAdress, estado);

                if (isUpdated)
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Escuela modificada correctamente");

                    // Recarga los datos actualizados en el grid view
                    LlenarGrid();

                    TcodigoSchool.Text = "";
                    TcodigoClass.Text = "";
                    TnombreSchool.Text = "";
                    Tdescripcion.Text = "";
                    Tdireccion.Text = "";
                    Tphone.Text = "";
                    TpostCode.Text = "";
                    TpostAdress.Text = "";
                    DropDownListSchool.SelectedIndex = 0;
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