<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="login.aspx.vb" Inherits="Auditoria.login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<title>Login @ Auditoría</title>
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
        width: 136px;
    }
    .style2
    {
        width: 151px;
    }
</style>
</head>

<body>

    <form id="form1" runat="server">

<table align="center" class="auto-style1" style="width: 800px; height: 100px">
	<tr>
		<td style="height: 69px; width: 83px">
		<img alt="" height="55" src="images/u82-fr.jpg" width="78" /></td>
		<td style="height: 69px"></td>
	</tr>
	<tr>
		<td class="auto-style2" colspan="2" style="height: 6px">&nbsp;</td>
	</tr>
	<tr>
		<td colspan="2" style="height: 6px"></td>
	</tr>
	<tr>
		<td colspan="2" style="height: 17px">
            <br />
            <br />
            <br />
            <br />
            <br />
		<br />
		<table style="width: 100%">
			<tr>
				<td>&nbsp;</td>
				<td class="style1">
                    &nbsp;</td>
				<td class="style2">&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td>&nbsp;</td>
				<td class="style1">
            <asp:TextBox ID="lblUsuario" runat="server"
                        Font-Names="Arial" Font-Size="Small" 
                            ForeColor="#DC002E" BorderWidth="0px" ReadOnly="True" TabIndex="4">Usuario :</asp:TextBox>
                    </td>
				<td class="style2">&nbsp;&nbsp;
            <asp:TextBox ID="txtUsuario" runat="server" 
                        Font-Names="Arial" Font-Size="Small" 
                            ForeColor="#666666"></asp:TextBox>
                    </td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td>&nbsp;</td>
				<td class="style1">
            <asp:TextBox ID="lblPassword" runat="server"
                        Font-Names="Arial" Font-Size="Small" 
                            ForeColor="#DC002E" BorderWidth="0px" ReadOnly="True" TabIndex="5">Password :</asp:TextBox>
                    </td>
				<td class="style2">&nbsp;&nbsp;
            <asp:TextBox ID="txtPassword" runat="server" 
                        Font-Names="Arial" Font-Size="Small" 
                            ForeColor="#666666"></asp:TextBox>
                    </td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td>&nbsp;</td>
				<td class="style1">&nbsp;</td>
				<td class="style2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:ImageButton ID="btnConfirmar" runat="server" Height="32px" 
                ImageUrl="~/images/buttons/confirm.png" Width="32px" 
                ToolTip="Confirmar" TabIndex="2" />
                <asp:ImageButton ID="btnCancelar" runat="server" Height="32px" 
                ImageUrl="~/images/buttons/cancel.png" Width="32px" ToolTip="Cancelar" 
                        TabIndex="3" />
		        </td>
				<td>&nbsp;</td>
			</tr>
		</table>
		<br />
            <asp:TextBox ID="txtError" runat="server" 
                        Font-Names="Arial" Font-Size="Small" 
                            ForeColor="#666666" BorderWidth="0px" Width="780px"></asp:TextBox>
		    <br />
            <br />
		<br />
		<br />
		<br />
		<br />
		</td>
	</tr>
	<tr>
		<td colspan="2"></td>
	</tr>
	<tr>
		<td class="auto-style2" colspan="2" style="height: 49px"></td>
	</tr>
</table>

    </form>

</body>

</html>
