<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ingreso.aspx.vb" Inherits="Auditoria.ingreso" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">



<head>

<meta content="text/html; charset=windows-1252" http-equiv="Content-Type" />

<title> Auditoría @ Selección de CE</title>

<script type="text/javascript">



</script>



</head>



<body style="font-family:Arial, Helvetica, sans-serif">

    <form id="form1" runat="server">

<div style="background-color:rgb(235,235,235)">

<table style="width: 100%; table-layout:fixed">

	<tr>

		<td style="text-align:right">Concesioria</td>

		<td style="text-align:left">		

		

				

		





		    <asp:DropDownList ID="dropConcesionaria"  runat="server" Height="20px"  AutoPostback=true Width="138px"  >  
                          
            </asp:DropDownList>

		

				

		





		</td>

	</tr>

	<tr>

		<td style="text-align:right">
            <asp:Label ID="Label1" runat="server" Text="Sucursal"></asp:Label>
        </td>

		<td style="text-align:left">		

		

				

		





		    <asp:DropDownList ID="dropSucursal" runat="server" Height="20px" Width="137px" 
                AutoPostBack="True">
            </asp:DropDownList>

		

				

		





		</td>

	</tr>

	</table>

</div>



    </form>



</body>



</html>


