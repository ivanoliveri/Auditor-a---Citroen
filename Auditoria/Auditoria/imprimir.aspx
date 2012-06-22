<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="imprimir.aspx.vb" Inherits="Auditoria.imprimir" %>

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
}
.auto-style1 {
	border: 2px solid #000000;
}
.auto-style2 {
	background-color: #DC002E;
}
.auto-style3 {
	background-color: #868689;
}
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
        <div id="menu">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
            <asp:RadioButtonList ID="radImpresion" runat="server" AutoPostBack="True" 
                Font-Names="Arial" Font-Size="Small" ForeColor="#868689">
                <asp:ListItem>Imprimir Grilla</asp:ListItem>
                <asp:ListItem>Imprimir Categoría</asp:ListItem>
            </asp:RadioButtonList>
            <br />
            <br />
            <asp:ImageButton ID="btnConfirmar" runat="server" Height="40px" 
                ImageUrl="~/images/buttons/Confirmar.png" Width="41px" 
                ToolTip="Confirmar" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:ImageButton ID="btnCancelar" runat="server" Height="40px" 
                ImageUrl="~/images/buttons/Cancelar.png" Width="41px" ToolTip="Cancelar" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
		        <br />
                <div id="impresion" class=invisible>
                    <asp:GridView ID="GridViewData" runat="server" Height="16px" Width="34px">
                    </asp:GridView>
                </div>
        </td>
	</tr>
	</table>

    </form>

</body>

</html>
