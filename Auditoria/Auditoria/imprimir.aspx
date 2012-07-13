<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="imprimir.aspx.vb" Inherits="Auditoria.imprimir" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<title>Impresión @ Auditoría</title>
<script type="text/javascript">
    function cargarImpresion() {
        var radioButtons = document.getElementsByName('radImpresion');
        var unNombre;
        for (var x = 0; x < radioButtons.length; x++) {
            if (radioButtons[1].checked) {
                unNombre = "categoria";
            }
            if (radioButtons[2].checked) {
                unNombre = "todas";
            }
        }
        print(unNombre);
    }
    function print(unNombre) {
        var ficha = document.getElementById(unNombre);
        var ventimp = window.open(' ', 'popimpr');
        ventimp.document.write(ficha.innerHTML);
        ventimp.document.close();
        ventimp.print();
        window.close()
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
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:ImageButton ID="btnConfirmar" runat="server" Height="32px" 
                ImageUrl="~/images/buttons/confirm.png" Width="32px" 
                ToolTip="Confirmar" />
            &nbsp;<asp:ImageButton ID="btnCancelar" runat="server" Height="32px" 
                ImageUrl="~/images/buttons/cancel.png" Width="32px" ToolTip="Cancelar" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
		        <br />
                <div id="categoria" class=invisible >
                                        <asp:TextBox ID="txtDatosCategoria" runat="server" 
                        BorderWidth="0px" Font-Names="Arial" 
                                    Font-Size="Medium" ForeColor="#000000" ReadOnly="True" 
                        Width="971px"></asp:TextBox>
                                <asp:TextBox ID="txtTituloCategoria" runat="server" 
                        BorderWidth="0px" Font-Names="Arial" 
                                    Font-Size="Medium" ForeColor="#000000" ReadOnly="True" 
                        Width="971px"></asp:TextBox>

                                <asp:GridView ID="GridViewDataCategoria" runat="server" BackColor="White" 
                                    BorderColor="#666666" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                                    Font-Names="Arial" Font-Size="Small" ForeColor="#000000" GridLines="Horizontal" 
                                    Height="175px" 
                                    
                        style="margin-top: 19px; margin-bottom: 0px; margin-right: 1px;" Width="981px" 
                                    AutoGenerateColumns="False">
                                    <Columns>
        
                                        <asp:TemplateField HeaderText="NRO. REF.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label46" runat="server" Text='<%# Eval("NRO_REFERENCIA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DESCRIPCIÓN">
                                            <ItemTemplate>
                                                <asp:Label ID="Label47" runat="server" Text='<%# Eval("DESCRIPCION") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
        
                                        <asp:templatefield headertext="STK. ENV.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label48" runat="server" text='<%# Eval("STOCK_ENVIADO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:templatefield>   
                                        <asp:TemplateField HeaderText="EST. ENV.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label49" runat="server" text='<%# Eval("ESTADO_ENVIADO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FEC. ENV.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label9" runat="server" text='<%# Eval("FECHA_ENVIADA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="STK. VERIF.">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox8" runat="server" Height="20px" 
                                                    text='<%# Eval("STOCK") %>'
                                                    Width="54px" Font-Names="Arial" Font-Size="Small" ForeColor="#666666" 
                                                    style="text-align: center" />
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
      
                </div>
                <div id="todas" class=invisible>
                                        <asp:TextBox ID="txtDatosTodas" runat="server" 
                        BorderWidth="0px" Font-Names="Arial" 
                                    Font-Size="Medium" ForeColor="#000000" ReadOnly="True" 
                        Width="971px"></asp:TextBox>
                                    <div id="CatG"  style="page-break-after: always;">
                                <asp:TextBox ID="txtTituloTodas" runat="server" BorderWidth="0px" Font-Names="Arial" 
                                    Font-Size="Medium" ForeColor="#000000" ReadOnly="True" Width="971px"></asp:TextBox>
                                <br />

                                <asp:GridView ID="GridViewDataTodas" runat="server" BackColor="White" 
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
                                </div>
                                <br />
                                <div id="CatB"  style="page-break-after: always;">
                                <asp:TextBox ID="txtTituloTodas0" runat="server" BorderWidth="0px" Font-Names="Arial" 
                                    Font-Size="Medium" ForeColor="#000000" ReadOnly="True" Width="971px"></asp:TextBox>

                                <asp:GridView ID="GridViewDataTodas0" runat="server" BackColor="White" 
                                    BorderColor="#666666" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                                    Font-Names="Arial" Font-Size="Small" ForeColor="#000000" GridLines="Horizontal" 
                                    Height="175px" 
                                    style="margin-top: 19px; margin-bottom: 0px; margin-right: 1px;" Width="981px" 
                                    AutoGenerateColumns="False">
                                    <Columns>
        
                                        <asp:TemplateField HeaderText="NRO. REF.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label10" runat="server" Text='<%# Eval("NRO_REFERENCIA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DESCRIPCIÓN">
                                            <ItemTemplate>
                                                <asp:Label ID="Label11" runat="server" Text='<%# Eval("DESCRIPCION") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
        
                                        <asp:templatefield headertext="STK. ENV.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label12" runat="server" text='<%# Eval("STOCK_ENVIADO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:templatefield>   
                                        <asp:TemplateField HeaderText="EST. ENV.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label13" runat="server" text='<%# Eval("ESTADO_ENVIADO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FEC. ENV.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label9" runat="server" text='<%# Eval("FECHA_ENVIADA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="STK. VERIF.">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox2" runat="server" Height="20px" 
                                                    text='<%# Eval("STOCK") %>'
                                                    Width="54px" Font-Names="Arial" Font-Size="Small" ForeColor="#666666" 
                                                    style="text-align: center" />
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
      
                                <br />
                                </div>
                                 <div id="CatC"  style="page-break-after: always;">
                                <asp:TextBox ID="txtTituloTodas1" runat="server" BorderWidth="0px" Font-Names="Arial" 
                                    Font-Size="Medium" ForeColor="#000000" ReadOnly="True" Width="971px"></asp:TextBox>

                                <asp:GridView ID="GridViewDataTodas1" runat="server" BackColor="White" 
                                    BorderColor="#666666" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                                    Font-Names="Arial" Font-Size="Small" ForeColor="#000000" GridLines="Horizontal" 
                                    Height="175px" 
                                    style="margin-top: 19px; margin-bottom: 0px; margin-right: 1px;" Width="981px" 
                                    AutoGenerateColumns="False">
                                    <Columns>
        
                                        <asp:TemplateField HeaderText="NRO. REF.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label16" runat="server" Text='<%# Eval("NRO_REFERENCIA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DESCRIPCIÓN">
                                            <ItemTemplate>
                                                <asp:Label ID="Label17" runat="server" Text='<%# Eval("DESCRIPCION") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
        
                                        <asp:templatefield headertext="STK. ENV.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label18" runat="server" text='<%# Eval("STOCK_ENVIADO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:templatefield>   
                                        <asp:TemplateField HeaderText="EST. ENV.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label19" runat="server" text='<%# Eval("ESTADO_ENVIADO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FEC. ENV.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label9" runat="server" text='<%# Eval("FECHA_ENVIADA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="STK. VERIF.">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox3" runat="server" Height="20px" 
                                                    text='<%# Eval("STOCK") %>'
                                                    Width="54px" Font-Names="Arial" Font-Size="Small" ForeColor="#666666" 
                                                    style="text-align: center" />
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
      
                                <br />
                                </div>
                                <div id="CatD"  style="page-break-after: always;">
                                <asp:TextBox ID="txtTituloTodas2" runat="server" BorderWidth="0px" Font-Names="Arial" 
                                    Font-Size="Medium" ForeColor="#000000" ReadOnly="True" Width="971px"></asp:TextBox>

                                <asp:GridView ID="GridViewDataTodas2" runat="server" BackColor="White" 
                                    BorderColor="#666666" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                                    Font-Names="Arial" Font-Size="Small" ForeColor="#000000" GridLines="Horizontal" 
                                    Height="175px" 
                                    style="margin-top: 19px; margin-bottom: 0px; margin-right: 1px;" Width="981px" 
                                    AutoGenerateColumns="False">
                                    <Columns>
        
                                        <asp:TemplateField HeaderText="NRO. REF.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label22" runat="server" Text='<%# Eval("NRO_REFERENCIA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DESCRIPCIÓN">
                                            <ItemTemplate>
                                                <asp:Label ID="Label23" runat="server" Text='<%# Eval("DESCRIPCION") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
        
                                        <asp:templatefield headertext="STK. ENV.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label24" runat="server" text='<%# Eval("STOCK_ENVIADO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:templatefield>   
                                        <asp:TemplateField HeaderText="EST. ENV.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label25" runat="server" text='<%# Eval("ESTADO_ENVIADO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FEC. ENV.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label9" runat="server" text='<%# Eval("FECHA_ENVIADA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="STK. VERIF.">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox4" runat="server" Height="20px" 
                                                    text='<%# Eval("STOCK") %>'
                                                    Width="54px" Font-Names="Arial" Font-Size="Small" ForeColor="#666666" 
                                                    style="text-align: center" />
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
      
                                <br />
                                </div>
                                <div id="CatE"  style="page-break-after: always;">
                                <asp:TextBox ID="txtTituloTodas3" runat="server" BorderWidth="0px" Font-Names="Arial" 
                                    Font-Size="Medium" ForeColor="#000000" ReadOnly="True" Width="971px"></asp:TextBox>

                                <asp:GridView ID="GridViewDataTodas3" runat="server" BackColor="White" 
                                    BorderColor="#666666" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                                    Font-Names="Arial" Font-Size="Small" ForeColor="#000000" GridLines="Horizontal" 
                                    Height="175px" 
                                    style="margin-top: 19px; margin-bottom: 0px; margin-right: 1px;" Width="981px" 
                                    AutoGenerateColumns="False">
                                    <Columns>
        
                                        <asp:TemplateField HeaderText="NRO. REF.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label28" runat="server" Text='<%# Eval("NRO_REFERENCIA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DESCRIPCIÓN">
                                            <ItemTemplate>
                                                <asp:Label ID="Label29" runat="server" Text='<%# Eval("DESCRIPCION") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
        
                                        <asp:templatefield headertext="STK. ENV.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label30" runat="server" text='<%# Eval("STOCK_ENVIADO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:templatefield>   
                                        <asp:TemplateField HeaderText="EST. ENV.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label31" runat="server" text='<%# Eval("ESTADO_ENVIADO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FEC. ENV.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label9" runat="server" text='<%# Eval("FECHA_ENVIADA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="STK. VERIF.">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox5" runat="server" Height="20px" 
                                                    text='<%# Eval("STOCK") %>'
                                                    Width="54px" Font-Names="Arial" Font-Size="Small" ForeColor="#666666" 
                                                    style="text-align: center" />
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
      
                                <br />
                                </div>
                                <div id="CatF"  style="page-break-after: always;">
                                <asp:TextBox ID="txtTituloTodas4" runat="server" BorderWidth="0px" Font-Names="Arial" 
                                    Font-Size="Medium" ForeColor="#000000" ReadOnly="True" Width="971px"></asp:TextBox>

                                <asp:GridView ID="GridViewDataTodas4" runat="server" BackColor="White" 
                                    BorderColor="#666666" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                                    Font-Names="Arial" Font-Size="Small" ForeColor="#000000" GridLines="Horizontal" 
                                    Height="175px" 
                                    style="margin-top: 19px; margin-bottom: 0px; margin-right: 1px;" Width="981px" 
                                    AutoGenerateColumns="False">
                                    <Columns>
        
                                        <asp:TemplateField HeaderText="NRO. REF.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label34" runat="server" Text='<%# Eval("NRO_REFERENCIA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DESCRIPCIÓN">
                                            <ItemTemplate>
                                                <asp:Label ID="Label35" runat="server" Text='<%# Eval("DESCRIPCION") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
        
                                        <asp:templatefield headertext="STK. ENV.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label36" runat="server" text='<%# Eval("STOCK_ENVIADO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:templatefield>   
                                        <asp:TemplateField HeaderText="EST. ENV.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label37" runat="server" text='<%# Eval("ESTADO_ENVIADO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FEC. ENV.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label9" runat="server" text='<%# Eval("FECHA_ENVIADA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="STK. VERIF.">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox6" runat="server" Height="20px" 
                                                    text='<%# Eval("STOCK") %>'
                                                    Width="54px" Font-Names="Arial" Font-Size="Small" ForeColor="#666666" 
                                                    style="text-align: center" />
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
      
                                <br />
                                </div>
                                <asp:TextBox ID="txtTituloTodas5" runat="server" BorderWidth="0px" Font-Names="Arial" 
                                    Font-Size="Medium" ForeColor="#000000" ReadOnly="True" Width="971px"></asp:TextBox>

                                <asp:GridView ID="GridViewDataTodas5" runat="server" BackColor="White" 
                                    BorderColor="#666666" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                                    Font-Names="Arial" Font-Size="Small" ForeColor="#000000" GridLines="Horizontal" 
                                    Height="175px" 
                                    style="margin-top: 19px; margin-bottom: 0px; margin-right: 1px;" Width="981px" 
                                    AutoGenerateColumns="False">
                                    <Columns>
        
                                        <asp:TemplateField HeaderText="NRO. REF.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label40" runat="server" Text='<%# Eval("NRO_REFERENCIA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DESCRIPCIÓN">
                                            <ItemTemplate>
                                                <asp:Label ID="Label41" runat="server" Text='<%# Eval("DESCRIPCION") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
        
                                        <asp:templatefield headertext="STK. ENV.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label42" runat="server" text='<%# Eval("STOCK_ENVIADO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:templatefield>   
                                        <asp:TemplateField HeaderText="EST. ENV.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label43" runat="server" text='<%# Eval("ESTADO_ENVIADO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FEC. ENV.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label9" runat="server" text='<%# Eval("FECHA_ENVIADA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="STK. VERIF.">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox7" runat="server" Height="20px" 
                                                    text='<%# Eval("STOCK") %>'
                                                    Width="54px" Font-Names="Arial" Font-Size="Small" ForeColor="#666666" 
                                                    style="text-align: center" />
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
      
                                <br />
                                                     
        </td>
	</tr>
	</table>

    </form>

</body>

</html>
