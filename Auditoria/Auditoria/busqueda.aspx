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
	background-color: #666666;
}
    .style1
    {
        height: 323px;
    }
    #botonera
    {
        height: 32px;
        width: 670px;
        margin-left: 0px;
    }
    .style2
    {
        height: 69px;
        width: 181px;
    }
</style>
</head>

<body>

    <form id="form1" runat="server">

<table align="center" class="auto-style1" 
        style="width: 671px; height: 431px; margin-right: 66px;">
	<tr>
		<td style="height: 69px; width: 83px">
		<img alt="" height="55" src="images/u82-fr.jpg" width="78" /></td>
		<td class="style2"></td>
	</tr>
	<tr>
		<td class="auto-style2" colspan="2" style="height: 6px">&nbsp;</td>
	</tr>
	<tr>
		<td colspan="2" class="style1">
            <div style="width: 101%; height: 353px;">

                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>

                <br />
                &nbsp;&nbsp;
                <asp:TextBox ID="TextBox2" runat="server" BorderColor="White" BorderWidth="0px" 
                    Font-Names="Arial" ForeColor="#666666" style="margin-left: 0px" 
                    Width="72px">Búsqueda :</asp:TextBox>
                <asp:TextBox ID="txtBusqueda" runat="server" Width="551px"></asp:TextBox>
                &nbsp;<asp:RadioButtonList ID="radBusqueda" runat="server" Font-Names="Arial" 
                    Font-Size="Small" ForeColor="#666666" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="NRO_REFERENCIA">NRO. REFERENCIA</asp:ListItem>
                    <asp:ListItem Value="DESCRIPCION">DESCRIPCIÓN</asp:ListItem>
                    <asp:ListItem Value="PALABRA_CLAVE">PALABRA CLAVE</asp:ListItem>
                </asp:RadioButtonList>
                <div id="busqueda" style="width:100%;height:175px">
                            <asp:GridView ID="GridViewData" runat="server" AutoGenerateColumns="False" 
                                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                                CellPadding="4" Font-Names="Arial" Font-Size="Small" ForeColor="#666666" 
                                GridLines="Horizontal" Height="175px" style="margin-top: 19px; margin-right: 0px;" 
                                Width="634px">
                                <Columns>
                                    <asp:TemplateField HeaderText="CAT.">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("CATEGORIA") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="NRO. REF.">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("NRO_REFERENCIA") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DESCRIPCIÓN">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescripcion" runat="server" Text='<%# Eval("DESCRIPCION") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ELIMINAR" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" 
                                                CommandName="Eliminar" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" Text="Eliminar"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="White" ForeColor="Black" />
                                <HeaderStyle BackColor="#DC002E" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                <SortedDescendingHeaderStyle BackColor="#242121" />
                            </asp:GridView>
                            <br />
                </div>
                <br />
                <div align="right" id="botonera">
                    <asp:TextBox ID="txtError" 
                runat="server" BorderWidth="0px" 
                Font-Names="Arial" Font-Size="Small" ForeColor="#DC002E" ReadOnly="True" 
                Width="392px" Height="16px" style="margin-left: 0px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="btnSearch" runat="server" 
                        ImageUrl="~/images/buttons/Buscar.png" />
                    <asp:ImageButton ID="btnPrevious" runat="server" Height="100%" 
                        ImageUrl="~/images/buttons/btnPrevious.png" />
                    <asp:ImageButton ID="btnNext" runat="server" 
                        ImageUrl="~/images/buttons/btnNext.png" />
            <asp:ImageButton ID="btnCancelar" runat="server" Height="32px" 
                ImageUrl="~/images/buttons/cancel.png" Width="32px" ToolTip="Cancelar" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <br />
                </div>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

            </div>
		</td>
	</tr>
	</table>

    </form>

</body>

</html>
