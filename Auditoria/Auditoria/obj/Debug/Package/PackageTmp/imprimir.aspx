﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="imprimir.aspx.vb" Inherits="Auditoria.imprimir" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<title>Impresión @ Auditoría</title>
<script type="text/javascript">
    function cargarImpresion() {
        var ficha = document.getElementById('impresion');
        var ventimp = window.open(' ', 'popimpr');
        ventimp.document.write(ficha.innerHTML);
        ventimp.document.close();
        ventimp.print();
    }
    function unloadPage() {
        window.close();
    }


    </script>
<style type="text/css">
.invisible {
	 opacity:0;
	 display:none;
}
.auto-style1 {
	border: 2px solid #000000;
}
.auto-style2 {
	background-color: #DC002E;
}
.auto-style3 {
	background-color: #666666;

</style>
</head>

<body>

    <form id="form1" runat="server" >

<table align="center" class="auto-style1" style="width: 572px; height: 273px">
	<tr>
		<td style="height: 69px; width: 83px">
		<img alt="" height="55" src="images/u82-fr.jpg" width="78" /></td>
		<td style="height: 69px; width: 181px;"></td>
	</tr>
	<tr>
		<td class="auto-style2" colspan="2" style="height: 6px">&nbsp;</td>
	</tr>
	<tr>
		<td colspan="2">
            <br />

            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
            <asp:RadioButtonList ID="radImpresion" runat="server" AutoPostBack="True" 
                Font-Names="Arial" Font-Size="Small" ForeColor="#666666">
                <asp:ListItem>Imprimir Categoría</asp:ListItem>
                <asp:ListItem>Imprimir Todas Las Categorías</asp:ListItem>
            </asp:RadioButtonList>
            <br />
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:ImageButton ID="btnConfirmar" runat="server" Height="32px" 
                ImageUrl="~/images/buttons/confirm.png" Width="32px" 
                ToolTip="Confirmar" />
            &nbsp;<asp:ImageButton ID="btnCancelar" runat="server" Height="32px" 
                ImageUrl="~/images/buttons/cancel.png" Width="32px" ToolTip="Cancelar" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
		        <br />
                <div id="impresion" class=invisible>
                                <asp:TextBox ID="txtTitulo" runat="server" BorderWidth="0px" Font-Names="Arial" 
                                    Font-Size="Medium" ForeColor="#000000" ReadOnly="True" Width="971px"></asp:TextBox>
                                <br />

                                <asp:GridView ID="GridViewData" runat="server" BackColor="White" 
                                    BorderColor="#666666" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                                    Font-Names="Arial" Font-Size="Small" ForeColor="#000000" GridLines="Horizontal" 
                                    Height="175px" 
                                    style="margin-top: 19px; margin-bottom: 0px; margin-right: 1px;" Width="981px" 
                                    AutoGenerateColumns="False">
                                    <Columns>
        
                                        <asp:TemplateField HeaderText="NRO. REF.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("NRO_REFERENCIA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DESCRIPCIÓN">
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("DESCRIPCION") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
        
                                        <asp:templatefield headertext="STK. ENV.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label8" runat="server" text='<%# Eval("STOCK_ENVIADO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:templatefield>   
                                        <asp:TemplateField HeaderText="EST. ENV.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label5" runat="server" text='<%# Eval("ESTADO_ENVIADO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FEC. ENV.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label9" runat="server" text='<%# Eval("FECHA_ENVIADA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="STK. VERIF.">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server" Height="20px" 
                                                    text='<%# Eval("STOCK") %>'
                                                    Width="54px" Font-Names="Arial" Font-Size="Small" ForeColor="#666666" style="text-align: center" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ESTADO">
                                            <ItemTemplate>
                                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
                                                    RepeatDirection="Horizontal" AutoPostBack="True" 
                                                    SelectedValue='<%#Eval("ESTADO")%>'
                                                    onselectedindexchanged="RadioButtonList1_SelectedIndexChanged">
                                                    <asp:ListItem Value="B">B</asp:ListItem>
                                                    <asp:ListItem Value="R">R</asp:ListItem>
                                                    <asp:ListItem Value="M">M</asp:ListItem>
                                                    <asp:ListItem Value="N" style="display:none">N</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FECHA">
                                            <ItemTemplate>
                                                <asp:Label ID="Label7" runat="server" text='<%# Eval("FECHA") %>' ></asp:Label>
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
      
        </td>
	</tr>
	</table>

    </form>

</body>

</html>