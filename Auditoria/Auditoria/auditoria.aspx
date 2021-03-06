﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="auditoria.aspx.vb" Inherits="Auditoria.auditoria" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head>

<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<title>Auditoría</title>
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
        height: 27px;
    }
    .style2
    {
        height: 21px;
    }
    .style3
    {
        width: 876px;
    }
    .style4
    {
        height: 69px;
        width: 946px;
    }
    .style5
    {
        width: 7px;
    }
    #botonera
    {
        width: 975px;
    }
    </style>
	<script type="text/javascript">
	    function llenarLabel(unString) {
	        document.all("txtCategoria").value = unString;
	    }
	    function mostrarPopupBuscar(unString) {
	        hidden = open(unString, 'Buscar', 'top=0,left=0,width=690,height=495,status=yes,resizable=yes,scrollbars=yes');
	    }
	    function mostrarPopupImprimir(unString) {
	        hidden = open(unString, 'Imprimir', 'top=0,left=0,width=630,height=320,status=yes,resizable=yes,scrollbars=yes');
        }
	    function mostrarPopupAgregar(unString) {
	        hidden = open(unString, 'Agregar', 'top=0,left=0,width=500,height=360,status=yes,resizable=yes,scrollbars=yes');
	        return false;
	    }
	    function printGrid(nombre) {
	        var ficha = document.getElementById(nombre);
	        var ventimp = window.open(' ', 'popimpr');
	        ventimp.document.write(ficha.innerHTML);
	        ventimp.document.close();
	        ventimp.print();
	        ventimp.close();
	    }
        </script>
</head>

<body>

    <form id="form1" runat="server">

<table align="center" class="auto-style1" style="width: 1015px; height: 100px">
	<tr>
		<td style="height: 69px; width: 83px">
		<img alt="" height="55" src="images/u82-fr.jpg" width="78" /></td>
		<td class="style4">
            <asp:TextBox ID="txtCE" runat="server" BorderWidth="0px" Font-Names="Arial" 
                Font-Size="Small" ForeColor="#666666" ReadOnly="True" Width="912px"></asp:TextBox>
            <asp:TextBox ID="txtSucursal" runat="server" BorderWidth="0px" 
                Font-Names="Arial" Font-Size="Small" ForeColor="#666666" ReadOnly="True" 
                Width="914px"></asp:TextBox>
            <br />
            <asp:TextBox ID="txtPeriodo" runat="server" BorderWidth="0px" 
                Font-Names="Arial" Font-Size="Small" ForeColor="#666666" ReadOnly="True" 
                Width="914px" Height="16px"></asp:TextBox>
            <br />
            <asp:TextBox ID="txtUsuario" runat="server" BorderWidth="0px" 
                Font-Names="Arial" Font-Size="Small" ForeColor="#666666" ReadOnly="True" 
                Width="914px" Height="16px"></asp:TextBox>
        </td>
	</tr>
	<tr>
		<td class="auto-style2" colspan="2" style="height: 2px">
            </td>
	</tr>
	<tr>
		<td colspan="2" class="style2">
            <div style="width: 100%; height: 528px; margin-right: 0px;">

                <table class="style1" style="height: 43px;width: 100%">
                    <tr>
                        <td>
                            <asp:ImageButton ID="btnA" runat="server" 
                                ImageUrl="~/images/buttons/btnA.png" />
                        </td>
                        <td>
                            <asp:ImageButton ID="btnB" runat="server" 
                                ImageUrl="~/images/buttons/btnB.png" />
                        </td>
                        <td>
                            <asp:ImageButton ID="btnC" runat="server" 
                                ImageUrl="~/images/buttons/btnC.png" />
                        </td>
                        <td>
                            <asp:ImageButton ID="btnD" runat="server" 
                                ImageUrl="~/images/buttons/btnD.png" />
                        </td>
                        <td>
                            <asp:ImageButton ID="btnE" runat="server" 
                                ImageUrl="~/images/buttons/btnE.png" />
                        </td>
                        <td>
                            <asp:ImageButton ID="btnF" runat="server"  OnClientClick="#"
                                ImageUrl="~/images/buttons/btnF.png" />
                        </td>
                        <td>
                            <asp:ImageButton ID="btnG" runat="server" 
                                ImageUrl="~/images/buttons/btnG.png" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <asp:TextBox ID="txtCategoria" runat="server" BorderWidth="0px" 
                                Font-Names="Arial" Font-Size="Small" ForeColor="#666666" ReadOnly="True" 
                                Width="568px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
                                          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                <table style="width: 100%; height: 430px">
                    <tr>
                        <td class="style5">
                            &nbsp;</td>
                        <td class="style3" >
                                <asp:ScriptManager ID="ScriptManager1" runat="server">
                                </asp:ScriptManager>
      
                                        <asp:GridView ID="GridViewData" runat="server" AutoGenerateColumns="False" 
                                            BackColor="White" BorderColor="#666666" BorderStyle="None" BorderWidth="1px" 
                                            CellPadding="4" Font-Names="Arial" Font-Size="Small" ForeColor="#666666" 
                                            GridLines="Horizontal" Height="175px" 
                                            style="margin-top: 2px; margin-bottom: 0px; margin-right: 1px;" Width="981px">
                                            <Columns>
                                                <asp:TemplateField HeaderText="CAT.">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox2" runat="server" ontextchanged="TextBox2_TextChanged" 
                                                            Text='<%# Eval("CATEGORIA")%>' Width="24px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
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
                                                        <asp:TextBox ID="TextBox1" runat="server" Font-Names="Arial" Font-Size="Small" 
                                                            ForeColor="#666666" Height="20px" ontextchanged="TextBox1_TextChanged" 
                                                            style="text-align: center" Text='<%# Eval("STOCK") %>' Width="54px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ESTADO">
                                                    <ItemTemplate>
                                                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" 
                                                            onselectedindexchanged="RadioButtonList1_SelectedIndexChanged" 
                                                            RepeatDirection="Horizontal" SelectedValue='<%#Eval("ESTADO")%>'>
                                                            <asp:ListItem Value="B">B</asp:ListItem>
                                                            <asp:ListItem Value="R">R</asp:ListItem>
                                                            <asp:ListItem Value="M">M</asp:ListItem>
                                                            <asp:ListItem style="display:none" Value="N">N</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FECHA">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label7" runat="server" text='<%# Eval("FECHA") %>'></asp:Label>
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
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style5">
                            &nbsp;</td>
                        <td class="style3" >

                        <div align="right" id="botonera">
                            <asp:TextBox ID="txtError" runat="server" BorderWidth="0px" 
                Font-Names="Arial" Font-Size="Small" ForeColor="#DC002E" ReadOnly="True" 
                Width="673px" Height="16px" style="margin-left: 0px" Font-Bold="True"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:ImageButton 
                                ID="btnAgregar" runat="server" UseSubmitBehavior="false"
                                ImageUrl="~/images/buttons/agregar.png" ToolTip="Agregar" />
                            <asp:ImageButton ID="btnSearch" runat="server" 
                                ImageUrl="~/images/buttons/Buscar.png" ToolTip="Buscar" />
                            <asp:ImageButton ID="btnImprimir" runat="server" Height="32px" 
                                ImageUrl="~/images/buttons/Imprimir.png" ToolTip="Imprimir" />
                            <asp:ImageButton ID="btnPrevious" runat="server" Height="32px" 
                                ImageUrl="~/images/buttons/btnPrevious.png" 
                        ToolTip="Página Anterior" />
                            <asp:ImageButton ID="btnNext" runat="server" 
                                ImageUrl="~/images/buttons/btnNext.png" ToolTip="Página Siguiente" />
                            <asp:ImageButton 
                                ID="btnSalir" runat="server" ImageUrl="~/images/buttons/exit.png" 
                                ToolTip="Salir" Height="32px" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;
                        </div>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
                </contenttemplate>
                </asp:UpdatePanel>


                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />

              </div>
        </td>
	</tr>
	</table>

    </form>

</body>

</html>
