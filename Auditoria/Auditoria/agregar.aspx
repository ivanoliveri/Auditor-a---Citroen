﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="agregar.aspx.vb" Inherits="Auditoria.agregar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<title>Agregar Referencia @ Auditoría</title>
<script type="text/javascript">
    function unloadPage() {
        window.close();
    }
</script>
<style type="text/css">
.auto-style1 {
	border: 2px solid #000000;
}
.auto-style2 {
	background-color: #DC002E;
}
.auto-style3 {
	background-color: #868689;
}
    .style1
    {
        height: 69px;
        width: 200px;
    }
    .style2
    {
        height: 186px;
    }
</style>
</head>

<body>

    <form id="form1" runat="server">

<table align="center" class="auto-style1" style="width: 426px; height: 259px">
	<tr>
		<td style="height: 69px; width: 83px">
		<img alt="" height="55" src="images/u82-fr.jpg" width="78" /></td>
		<td class="style1"></td>
	</tr>
	<tr>
		<td class="auto-style2" colspan="2" style="height: 6px">&nbsp;</td>
	</tr>
	<tr>
		<td colspan="2" class="style2">
            <asp:TextBox ID="TextBox1" runat="server" BorderWidth="0px" Font-Names="Arial" 
                Font-Size="Small" ForeColor="#868689" Width="144px" ReadOnly="True">Número de Referencia :</asp:TextBox>
            <asp:TextBox ID="txtNroReferencia" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:TextBox ID="TextBox3" runat="server" BorderWidth="0px" Font-Names="Arial" 
                Font-Size="Small" ForeColor="#868689" Width="144px" ReadOnly="True">Descripción :</asp:TextBox>
            <asp:TextBox ID="txtDescripcion" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:TextBox ID="TextBox4" runat="server" BorderWidth="0px" Font-Names="Arial" 
                Font-Size="Small" ForeColor="#868689" Width="144px" ReadOnly="True">Categoría :</asp:TextBox>
            <asp:TextBox ID="txtCategoria" runat="server" BorderWidth="0px" ReadOnly="True"></asp:TextBox>
            <br />
            <br />
            <asp:ImageButton ID="btnConfirmar" runat="server" Height="40px" 
                ImageUrl="~/images/buttons/Confirmar.png" Width="41px" 
                ToolTip="Confirmar" />
&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:ImageButton ID="btnCancelar" runat="server" Height="40px" 
                ImageUrl="~/images/buttons/Cancelar.png" Width="41px" ToolTip="Cancelar" />
		</td>
	</tr>
	</table>

    </form>

</body>

</html>
