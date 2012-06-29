Public Class agregar
    Inherits System.Web.UI.Page
    Private unaCategoria As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtCategoria.Text = "G"
        txtNroReferencia.Focus()
        btnCancelar.Attributes.Add("onclick", "javascript:unloadPage()")
        txtCategoria.Attributes.CssStyle.Add("TEXT-ALIGN", "center")
        txtDescripcion.Attributes.CssStyle.Add("TEXT-ALIGN", "center")
        txtNroReferencia.Attributes.CssStyle.Add("TEXT-ALIGN", "center")
        txtStock.Attributes.CssStyle.Add("TEXT-ALIGN", "center")
    End Sub

    Protected Sub btnConfirmar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnConfirmar.Click
        Dim unaCategoria As String
        If Len(txtDescripcion.Text) = 0 Then
            unaCategoria = "G"
        Else
            unaCategoria = "F"
        End If
        Dim unaTablaTemporal As TablaSQL = New TablaSQL()
        unaTablaTemporal.setConnectionString(unConnectionString)
        'Verifica que la referencia ingresada no exista previamente.
        unaTablaTemporal.getDataSet("SELECT COUNT(*) FROM AUD_REFERENCIAS WHERE NRO_REFERENCIA='" & Trim(txtNroReferencia.Text) & "'")
        If CInt(unaTablaTemporal.getItem(0, 0)) >= 1 Then
            Response.Write("<script>alert('El número de referencia ingresado ya existe.');</script>")
            txtNroReferencia.Text = ""
            txtNroReferencia.Focus()
            Exit Sub
        End If
        If IsNumeric(txtStock.Text) = False Then
            Response.Write("<script>alert('El stock debe ser un entero positivo.');</script>")
            txtStock.Text = ""
            txtStock.Focus()
            Exit Sub
        End If
        If CInt(txtStock.Text) < 0 Then
            Response.Write("<script>alert('El stock debe ser un entero positivo.');</script>")
            txtStock.Text = ""
            txtStock.Focus()
            Exit Sub
        End If
        If InStr(txtStock.Text, ".", CompareMethod.Text) Then
            Response.Write("<script>alert('El stock debe ser un entero positivo.');</script>")
            txtStock.Text = ""
            txtStock.Focus()
            Exit Sub
        End If
        Dim unaTablaIdCategoria As TablaSQL = New TablaSQL()
        unaTablaIdCategoria.setConnectionString(unConnectionString)
        unaTablaIdCategoria.getDataSet("SELECT ID FROM AUD_CATEGORIAS WHERE CODIGO='" & unaCategoria & "'")
        unasReferencias.execQuery("INSERT INTO AUD_REFERENCIAS VALUES('" & txtNroReferencia.Text & "','" & txtDescripcion.Text & "'," & CInt(unaTablaIdCategoria.getItem(0, 0)) & ")")
        Dim unAno As String = Now.Year()
        Dim unMes As String = Now.Month()
        Dim unDia As String = Now.Day
        If unDia.Length = 1 Then
            unDia = "0" & unDia
        End If
        If unMes.Length = 1 Then
            unMes = "0" & unMes
        End If
        Dim unaTablaIdReferencia As TablaSQL = New TablaSQL
        unaTablaIdReferencia.setConnectionString(unConnectionString)
        unaTablaIdReferencia.getDataSet("SELECT ID FROM AUD_REFERENCIAS WHERE NRO_REFERENCIA='" & txtNroReferencia.Text & "'")
        unasReferencias.execQuery("INSERT INTO AUD_RELEVAMIENTOS VALUES(" & unNumeroDeCE & "," & unNumeroDeSucursal & ",'" & unPeriodoActual & "'," & unAno & unMes & unDia & "," & CInt(unaTablaIdReferencia.getItem(0, 0)) & ",'" & txtStock.Text & "','" & radEstado.SelectedValue & "','')")
        If (lastCat = "F" And unaCategoria = "F") Or (lastCat = "G" And unaCategoria = "G") Then
            Response.Write("<script>opener.location.reload();</script>")
        End If
        Response.Write("<script>window.location.reload();</script>")
    End Sub
End Class