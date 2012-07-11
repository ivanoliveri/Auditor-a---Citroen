Imports Auditoria.TableClass
Public Class login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtError.Text = ""
        txtUsuario.Focus()
        txtPassword.TextMode = TextBoxMode.Password
    End Sub

    Protected Sub btnConfirmar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnConfirmar.Click
        Dim unaTablaDeLogin As TablaSQL = New TablaSQL
        unaTablaDeLogin.setConnectionString(unConnectionStringDeBasesComunes)
        unaTablaDeLogin.getDataSet("SELECT Usuario,Contrasena,AccesosPG FROM Usuarios WHERE Usuario='" & txtUsuario.Text & "' AND Contrasena='" & txtPassword.Text & "'")
        If InStr(txtUsuario.Text, "'", CompareMethod.Text) Or InStr(txtUsuario.Text, "%", CompareMethod.Text) Or InStr(txtPassword.Text, "'", CompareMethod.Text) Or InStr(txtPassword.Text, "%", CompareMethod.Text) Then
            txtError.Text = "Error: Debes ingresar una cadena de caracteres válida."
            Exit Sub
        End If
        If unaTablaDeLogin.getRowsCount() = 0 Then
            txtError.Text = "Has ingresado una combinación inválida de Usuario y Password."
        Else
            If Left(unaTablaDeLogin.getItem(0, 2), 1) = "M" Then
                Application("nivelUsuario") = "ADMIN"
            ElseIf Left(unaTablaDeLogin.getItem(0, 2), 1) = "U" Then
                Application("nivelUsuario") = "OPERARIO"
            End If
            Application("nombreUsuario") = Trim(unaTablaDeLogin.getItem(0, 0))
            Response.Redirect("ingreso.aspx")
        End If
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnCancelar.Click
        Response.Write("<script>window.close();</script>")
    End Sub
End Class