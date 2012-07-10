Imports Auditoria.TableClass
Public Class agregar
    Inherits System.Web.UI.Page
    Private unaCategoria As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lastErrorAgregar = ""
        txtNroReferencia.Focus()
        txtNroReferencia.MaxLength = 15
        txtStock.MaxLength = 4
        txtDescripcion.MaxLength = 80
        btnCancelar.Attributes.Add("onclick", "javascript:unloadPage()")
        txtCategoria.Attributes.CssStyle.Add("TEXT-ALIGN", "center")
        txtDescripcion.Attributes.CssStyle.Add("TEXT-ALIGN", "center")
        txtNroReferencia.Attributes.CssStyle.Add("TEXT-ALIGN", "center")
        txtStock.Attributes.CssStyle.Add("TEXT-ALIGN", "center")
    End Sub
    Protected Sub btnConfirmar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnConfirmar.Click
        If InStr(txtNroReferencia.Text, "'", CompareMethod.Text) Or InStr(txtNroReferencia.Text, "%", CompareMethod.Text) Or InStr(txtDescripcion.Text, "'", CompareMethod.Text) Or InStr(txtDescripcion.Text, "%", CompareMethod.Text) Then
            txtError.Text = "Has ingresado una cadena de carácteres inválida."
            txtNroReferencia.Focus()
            Exit Sub
        End If
        Dim unaCategoria As String
        If Len(txtNroReferencia.Text) = 0 Then
            popupCat = "G"
            unaCategoria = "G"
        Else
            popupCat = "F"
            unaCategoria = "F"
        End If
        Dim unaTablaTemporal As TablaSQL = New TablaSQL()
        unaTablaTemporal.setConnectionString(unConnectionString)
        'Verifica que la referencia ingresada no exista previamente.
        If popupCat = "F" Then
            unaTablaTemporal.getDataSet("SELECT COUNT(*) FROM AUD_REFERENCIAS WHERE NRO_REFERENCIA='" & Trim(txtNroReferencia.Text) & "'")
            If CInt(unaTablaTemporal.getItem(0, 0)) >= 1 Then
                lastErrorAgregar = "Error: La pieza ingresada ya existe."
                txtNroReferencia.Focus()
                Exit Sub
            End If
        Else
            unaTablaTemporal.getDataSet("SELECT COUNT(*) FROM AUD_REFERENCIAS WHERE DESCRIPCION='" & Trim(txtDescripcion.Text) & "' AND NRO_REFERENCIA=''")
            If CInt(unaTablaTemporal.getItem(0, 0)) >= 1 Then
                lastErrorAgregar = "Error: La pieza ingresada ya existe."
                txtDescripcion.Focus()
                Exit Sub
            End If
        End If
        If Trim(txtDescripcion.Text) = "" Then
            lastErrorAgregar = "Error: No has ingresado la descripción."
            txtDescripcion.Focus()
            Exit Sub
        End If
        If IsNumeric(txtStock.Text) = False Then
            lastErrorAgregar = "Error: El stock debe ser un entero positivo."
            txtStock.Focus()
            Exit Sub
        End If
        If CInt(txtStock.Text) < 0 Then
            lastErrorAgregar = "Error: El stock debe ser un entero positivo."
            txtStock.Focus()
            Exit Sub
        End If
        If InStr(txtStock.Text, ".", CompareMethod.Text) Then
            lastErrorAgregar = "Error: El stock debe ser un entero positivo."
            txtStock.Focus()
            Exit Sub
        End If
        If radEstado.SelectedIndex = -1 Then
            lastErrorAgregar = "Error: Debes seleccionar un estado(B/M/R)"
            radEstado.Focus()
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
        If popupCat = "G" Then
            unaTablaIdReferencia.getDataSet("SELECT ID FROM AUD_REFERENCIAS WHERE DESCRIPCION='" & txtDescripcion.Text & "' AND NRO_REFERENCIA=''")
        Else
            unaTablaIdReferencia.getDataSet("SELECT ID FROM AUD_REFERENCIAS WHERE NRO_REFERENCIA='" & txtNroReferencia.Text & "'")
        End If
        unasReferencias.execQuery("INSERT INTO AUD_RELEVAMIENTOS VALUES(" & Application("unNumeroDeCE") & "," & Application("unNumeroDeSucursal") & ",'" & unPeriodoActual & "'," & unAno & unMes & unDia & "," & CInt(unaTablaIdReferencia.getItem(0, 0)) & ",'" & txtStock.Text & "','" & radEstado.SelectedValue & "')")
        If (Application("lastCat") = "F" And unaCategoria = "F") Or (Application("lastCat") = "G" And unaCategoria = "G") Then
            agregoDesdePopup = True
            Response.Write("<script>opener.location.href='http://normasymetodos.com/citroen.ar/Auditoria/auditoria.aspx';</script>")
        End If
        lastErrorAgregar = ""
        Response.Write("<script>window.close();</script>")
    End Sub

    Private Sub agregar_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        If lastErrorAgregar <> "" Then
            If Len(txtNroReferencia.Text) = 0 Then
                txtCategoria.Text = "G"
            Else
                txtCategoria.Text = "F"
            End If
        End If
        txtError.Text = lastErrorAgregar
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnCancelar.Click

    End Sub
End Class