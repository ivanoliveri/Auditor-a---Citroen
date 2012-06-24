Public Class agregar
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtCategoria.Text = lastCat
        btnCancelar.Attributes.Add("onclick", "javascript:unloadPage()")
    End Sub

    Protected Sub btnConfirmar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnConfirmar.Click
        Dim unaTablaIdCategoria As TablaSQL = New TablaSQL()
        unaTablaIdCategoria.setConnectionString(unConnectionString)
        unaTablaIdCategoria.getDataSet("SELECT ID FROM AUD_CATEGORIAS WHERE CODIGO='" & lastCat & "'")
        unasReferencias.execQuery("INSERT INTO AUD_REFERENCIAS VALUES('" & txtNroReferencia.Text & "','" & txtDescripcion.Text & "'," & CInt(unaTablaIdCategoria.getItem(0, 0)) & ")")
    End Sub
End Class