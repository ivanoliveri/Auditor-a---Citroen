<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="busqueda.aspx.vb" Inherits="Auditoria.busqueda" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<title>Busqueda de Herramientas @ Auditoría</title>
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
        height: 323px;
    }
    #botonera
    {
        height: 32px;
        width: 566px;
    }
</style>
</head>

<body>

    <form id="form1" runat="server">

<table align="center" class="auto-style1" style="width: 591px; height: 400px">
	<tr>
		<td style="height: 69px; width: 83px">
		<img alt="" height="55" src="images/u82-fr.jpg" width="78" /></td>
		<td style="height: 69px; width: 181px;"></td>
	</tr>
	<tr>
		<td class="auto-style2" colspan="2" style="height: 6px">&nbsp;</td>
	</tr>
	<tr>
		<td colspan="2" class="style1">
            <div style="width: 99%; height: 332px;">

                <br />
                &nbsp;&nbsp;
                <asp:TextBox ID="TextBox2" runat="server" BorderColor="White" BorderWidth="0px" 
                    Font-Names="Arial" ForeColor="#868689" style="margin-left: 0px" 
                    Width="72px">Búsqueda :</asp:TextBox>
                <asp:TextBox ID="txtBusqueda" runat="server" Width="466px"></asp:TextBox>
                &nbsp;<asp:RadioButtonList ID="radBusqueda" runat="server" Font-Names="Arial" 
                    Font-Size="Small" ForeColor="#868689" RepeatDirection="Horizontal">
                    <asp:ListItem>NRO_REFERNCIA</asp:ListItem>
                    <asp:ListItem>DESCRIPCIÓN</asp:ListItem>
                    <asp:ListItem Value="PALABRA_CLAVE">PALABRA CLAVE</asp:ListItem>
                </asp:RadioButtonList>
                <div id="busqueda" style="width:100%;height:175px">
                <asp:GridView ID="GridViewData" runat="server" BackColor="White" 
                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                    Font-Names="Arial" Font-Size="Small" ForeColor="#868689" GridLines="Horizontal" 
                    Height="175px" style="margin-top: 19px" Width="550px">
                    <FooterStyle BackColor="White" ForeColor="Black" />
                    <HeaderStyle BackColor="#DC002E" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                    <SortedDescendingHeaderStyle BackColor="#242121" />
                </asp:GridView>
                </div>
                <br />
                <div align="right" id="botonera">
                    &nbsp;<asp:ImageButton ID="btnPrevious" runat="server" Height="32px" 
                        ImageUrl="~/images/buttons/btnPrevious.png" />
                    <asp:ImageButton ID="btnNext" runat="server" 
                        ImageUrl="~/images/buttons/btnNext.png" />
                    <asp:ImageButton ID="btnSearch" runat="server" 
                        ImageUrl="~/images/buttons/Buscar.png" />
                    <br />
                </div>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

            </div>
		</td>
	</tr>
	</table>

    </form>

</body>

</html>
