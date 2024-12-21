<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaMenu.Master" AutoEventWireup="true" CodeBehind="Class.aspx.cs" Inherits="Quiz.CapaVista.Class" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>CLASS CATALOG</h2>
<br />
<div>
    <asp:GridView ID="GridViewClass" runat="server"></asp:GridView>
</div>
<br />
<br />
<div>

    <asp:Label ID="LcodigoClass" runat="server" Text="Codigo class"></asp:Label>
    <asp:TextBox ID="TcodigoClass" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="LcodigoSchool" runat="server" Text="Codigo school"></asp:Label>
    <asp:TextBox ID="TcodigoSchool" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="Lnombre" runat="server" Text="Nombre"></asp:Label>
    <asp:TextBox ID="Tnombre" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="Ldescripcion" runat="server" Text="Descripcion"></asp:Label>
    <asp:TextBox ID="Tdescripcion" runat="server"></asp:TextBox>
    <br />
<asp:DropDownList ID="DropDownListClass" runat="server">
    <asp:ListItem>Inactivo</asp:ListItem>
    <asp:ListItem>Activo</asp:ListItem>
</asp:DropDownList>

</div>
<div>
    <br />
    <asp:Button ID="Bagregar" runat="server" Text="Agregar" OnClick="Bagregar_Click" />
    <br />
    <br />
    <asp:Button ID="BconsultarFiltro" runat="server" Text="Consultar" OnClick="BconsultarFiltro_Click" />
    <br />
    <br />
    <asp:Button ID="Bborrar" runat="server" Text="Borrar" OnClick="Bborrar_Click" />
    <br />
    <br />
    <asp:Button ID="Bmodificar" runat="server" Text="Modificar" OnClick="Bmodificar_Click" />
</div>
</asp:Content>
