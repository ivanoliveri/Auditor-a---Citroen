<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="agregar.aspx.vb" Inherits="Auditoria.agregar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<title>Agregar Referencia @ Auditoría</title>
<script type="text/javascript">
    function unloadPage() {
        window.close();
    }
    function ponerCategoriaF() {
        if (document.getElementById('txtDescripcion').value == '') {
               document.getElementById('txtCategoria').value = 'G';
        }else{
               document.getElementById('txtCategoria').value = 'F';
        }
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
	background-color: #666666;
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
    .style3
    {
        width: 100%;
    }
    .style4
    {
        width: 221px;
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
            <table class="style3">
                <tr>
                    <td class="style4">
                        <br />
            <asp:TextBox ID="TextBox4" runat="server" BorderWidth="0px" Font-Names="Arial" 
                Font-Size="Small" ForeColor="#666666" Width="144px" ReadOnly="True">Categoría :</asp:TextBox>
                    </td>
                    <td>
            <asp:TextBox ID="txtCategoria" runat="server" BorderWidth="0px" ReadOnly="True" 
                            Font-Names="Arial" Font-Size="Small" ForeColor="#666666"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style4">
            <asp:TextBox ID="TextBox1" runat="server" BorderWidth="0px" Font-Names="Arial" 
                Font-Size="Small" ForeColor="#666666" Width="144px" ReadOnly="True">Número de Referencia :</asp:TextBox>
                    </td>
                    <td>
            <asp:TextBox ID="txtNroReferencia" runat="server" Font-Names="Arial" Font-Size="Small" 
                            ForeColor="#666666"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style4">
            <asp:TextBox ID="TextBox3" runat="server" BorderWidth="0px" Font-Names="Arial" 
                Font-Size="Small" ForeColor="#666666" Width="144px" ReadOnly="True">Descripción :</asp:TextBox>
                    </td>
                    <td>
            <asp:TextBox ID="txtDescripcion" runat="server" onKeyUp="ponerCategoriaF()" Font-Names="Arial" Font-Size="Small" 
                            ForeColor="#666666"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style4">
            <asp:TextBox ID="TextBox5" runat="server" BorderWidth="0px" Font-Names="Arial" 
                Font-Size="Small" ForeColor="#666666" Width="144px" ReadOnly="True">Stock :</asp:TextBox>
                    </td>
                    <td>
            <asp:TextBox ID="txtStock" runat="server" Font-Names="Arial" Font-Size="Small" 
                            ForeColor="#666666"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style4">
            <asp:TextBox ID="TextBox6" runat="server" BorderWidth="0px" Font-Names="Arial" 
                Font-Size="Small" ForeColor="#666666" Width="144px" ReadOnly="True">Estado :</asp:TextBox>
                    </td>
                    <td>
                        <div align=left id="divRadEstado">
                            <asp:RadioButtonList ID="radEstado" runat="server" Font-Names="Arial" 
                                Font-Size="Small" ForeColor="#666666" RepeatDirection="Horizontal" Height="20px" 
                                            Width="124px">
                                <asp:ListItem>B</asp:ListItem>
                                <asp:ListItem>R</asp:ListItem>
                                <asp:ListItem>M</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </td>
                </tr>
            </table>
            <br />
            <asp:ImageButton ID="btnConfirmar" runat="server" Height="32px" 
                ImageUrl="~/images/buttons/confirm.png" Width="32px" 
                ToolTip="Confirmar" />
&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:ImageButton ID="btnCancelar" runat="server" Height="32px" 
                ImageUrl="~/images/buttons/cancel.png" Width="32px" ToolTip="Cancelar" />
		</td>
	</tr>
	</table>

    </form>

</body>

</html>

