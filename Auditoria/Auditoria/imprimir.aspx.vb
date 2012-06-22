Public Class imprimir
    Inherits System.Web.UI.Page

    Protected Sub btnConfirmar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnConfirmar.Click

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnConfirmar.Attributes.Add("onclick", "javascript:cargarImpresion()")
        btnCancelar.Attributes.Add("onclick", "javascript:unloadPage()")
    End Sub

    Protected Sub RadioButtonList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles radImpresion.SelectedIndexChanged
        If radImpresion.Items(0).Selected = True Then
            unasReferencias.fillGridView(GridViewData)
        ElseIf radImpresion.Items(1).Selected = True Then
            Dim unaTablaIdCategoria As TablaSQL = New TablaSQL()
            unaTablaIdCategoria.setConnectionString(unConnectionString)
            unaTablaIdCategoria.getDataSet("SELECT ID FROM AUD_CATEGORIAS WHERE CODIGO='" & lastCat & "'")
            Dim todaLaCategoria As TablaSQL = New TablaSQL
            todaLaCategoria.setConnectionString(unConnectionString)
            todaLaCategoria.getDataSet("SELECT AUD_REFERENCIAS.NRO_REFERENCIA, AUD_REFERENCIAS.DESCRIPCION,AUD_RELEVAMIENTOS.STOCK, AUD_RELEVAMIENTOS.FECHA,AUD_RELEVAMIENTOS.ESTADO FROM AUD_REFERENCIAS LEFT OUTER JOIN AUD_RELEVAMIENTOS ON AUD_REFERENCIAS.ID=AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS WHERE ID_CATEGORIA=" & CInt(unaTablaIdCategoria.getItem(0, 0)) & " ORDER BY NRO_REFERENCIA ASC")
            todaLaCategoria.fillGridView(GridViewData)
        End If
    End Sub


End Class