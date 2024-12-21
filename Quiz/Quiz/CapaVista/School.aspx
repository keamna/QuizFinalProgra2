<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaMenu.Master" AutoEventWireup="true" CodeBehind="School.aspx.cs" Inherits="Quiz.CapaVista.School" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <h2>SCHOOL CATALOG</h2> 
    <br />
    <div>
        <asp:GridView ID="GridViewSchool" runat="server"></asp:GridView>
    </div>
     <br />
 <br />
 <div>
     <asp:Label ID="LcodigoSchool" runat="server" Text="Codigo escuela"></asp:Label>
     <asp:TextBox ID="TcodigoSchool" runat="server"></asp:TextBox>
     <br />
     <asp:Label ID="LcodigoClass" runat="server" Text="Codigo clase"></asp:Label>
     <asp:TextBox ID="TcodigoClass" runat="server"></asp:TextBox>
     <br />
     <asp:Label ID="LnombreSchool" runat="server" Text="Nombre"></asp:Label>
     <asp:TextBox ID="TnombreSchool" runat="server"></asp:TextBox>
     <br />
     <asp:Label ID="Ldescripcion" runat="server" Text="Descripcion"></asp:Label>
     <asp:TextBox ID="Tdescripcion" runat="server"></asp:TextBox>
     <br />
     <asp:Label ID="Ldireccion" runat="server" Text="Direccion"></asp:Label>
     <asp:TextBox ID="Tdireccion" runat="server"></asp:TextBox>
     <br />
     <asp:Label ID="Lphone" runat="server" Text="Telefono"></asp:Label>
     <asp:TextBox ID="Tphone" runat="server"></asp:TextBox>
     <br />
      <asp:Label ID="LpostCode" runat="server" Text="PostCode"></asp:Label>
     <asp:TextBox ID="TpostCode" runat="server"></asp:TextBox>
     <br />
     <asp:Label ID="LpostAdress" runat="server" Text="PostAddres"></asp:Label>
     <asp:TextBox ID="TpostAdress" runat="server"></asp:TextBox>
     <br />
<asp:DropDownList ID="DropDownListSchool" runat="server">
    <asp:ListItem>Inactivo</asp:ListItem>
    <asp:ListItem>Activo</asp:ListItem>
</asp:DropDownList>

 </div>
 <div>
     <br />
     <asp:Button ID="Bagregar" runat="server" Text="Agregar" OnClick="Bagregar_Click" />
     <asp:Button ID="BconsultarFiltro" runat="server" Text="Consultar" OnClick="BconsultarFiltro_Click" />
     <asp:Button ID="Bborrar" runat="server" Text="Borrar" OnClick="Bborrar_Click" />
     <asp:Button ID="Bmodificar" runat="server" Text="Modificar" OnClick="Bmodificar_Click" />
 </div>
</asp:Content>
