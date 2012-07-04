Public Class imprimir
    Inherits System.Web.UI.Page
    Protected Sub formatDate(ByVal unaFecha As String, ByRef unaNuevaFecha As String)
        'Si el Label de FECHA_ENV está vacío no le tiene que dar formato a la fecha. Esto es por si es un período que NO fue relevado
        If unaFecha = "" Then
            unaNuevaFecha = ""
            Exit Sub
        End If
        If InStr(unaFecha, "/", CompareMethod.Text) Then
            unaNuevaFecha = unaFecha
        Else
            Dim unAno As String = Mid(unaFecha, 3, 2)
            Dim unMes As String = Mid(unaFecha, 5, 2)
            Dim unDia As String = Mid(unaFecha, 7, 2)
            unaNuevaFecha = unDia & "/" & unMes & "/" & unAno
        End If
    End Sub
    Protected Sub formatGridView()
        For Each row As GridViewRow In GridViewData.Rows
            Dim radEstado As RadioButtonList = CType(row.FindControl("RadioButtonList1"), RadioButtonList)
            radEstado.Items(3).Enabled = False
            Dim lblFecha As Label = CType(row.FindControl("Label7"), Label)
            Dim lblFechaEnviada As Label = CType(row.FindControl("Label9"), Label)
            Dim unaFechaVieja As String = lblFecha.Text
            Dim unaFecha As String
            formatDate(unaFechaVieja, unaFecha)
            lblFecha.Text = unaFecha
            unaFechaVieja = lblFechaEnviada.Text
            formatDate(unaFechaVieja, unaFecha)
            lblFechaEnviada.Text = unaFecha
            Dim txtStock As TextBox = CType(row.FindControl("TextBox1"), TextBox)
            txtStock.Attributes.CssStyle.Add("TEXT-ALIGN", "center")
        Next
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnConfirmar.Attributes.Add("onclick", "javascript:cargarImpresion()")
        btnCancelar.Attributes.Add("onclick", "javascript:unloadPage()")
        txtTitulo.Attributes.CssStyle.Add("TEXT-ALIGN", "center")
    End Sub

    Protected Sub RadioButtonList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles radImpresion.SelectedIndexChanged
        If radImpresion.Items(0).Selected = True Then
            Dim unaTablaIdCategoria As TablaSQL = New TablaSQL()
            unaTablaIdCategoria.setConnectionString(unConnectionString)
            unaTablaIdCategoria.getDataSet("SELECT ID,DESCRIPCION FROM AUD_CATEGORIAS WHERE CODIGO='" & lastCat & "'")
            Dim unaTablaTemporal As TablaSQL = New TablaSQL()
            unaTablaTemporal.setConnectionString(unConnectionString)
            unaTablaTemporal.getDataSet("CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[NRO_REFERENCIA] [char](20) NULL,[DESCRIPCION] [char] (75) NULL,[STOCK_ENVIADO] [int] NULL,[ESTADO_ENVIADO] [char] (1) NULL,[FECHA_ENVIADA] [char] (20) NULL,[STOCK] [char] (10) NULL,[ESTADO] [char] (1) NULL,[FECHA] [char] (20) NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (NRO_REFERENCIA,DESCRIPCION,STOCK_ENVIADO,ESTADO_ENVIADO,FECHA_ENVIADA,STOCK,ESTADO,FECHA)SELECT AUD_REFERENCIAS.NRO_REFERENCIA,AUD_REFERENCIAS.DESCRIPCION,(SELECT AUD_RELEVAMIENTOS.STOCK FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ") AS STOCK_ENVIADO, (SELECT AUD_RELEVAMIENTOS.ESTADO FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ") AS ESTADO_ENVIADO,(SELECT AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ") AS FECHA_ENVIADA,AUD_RELEVAMIENTOS.STOCK, AUD_RELEVAMIENTOS.ESTADO, AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS INNER JOIN AUD_REFERENCIAS ON AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID WHERE AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & " AND AUD_REFERENCIAS.ID_CATEGORIA=" & CInt(unaTablaIdCategoria.getItem(0, 0)) & " AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoActual & "' UNION SELECT NRO_REFERENCIA,DESCRIPCION,(SELECT AUD_RELEVAMIENTOS.STOCK FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ") AS STOCK_ENVIADO,(SELECT AUD_RELEVAMIENTOS.ESTADO FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ")AS ESTADO_ENVIADO,(SELECT AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ")AS FECHA_ENVIADA, '' AS STOCK, 'N' AS ESTADO, CONVERT(VARCHAR(8), GETDATE(), 112) AS FECHA FROM AUD_REFERENCIAS WHERE ID_CATEGORIA=" & CInt(unaTablaIdCategoria.getItem(0, 0)) & " AND ID NOT IN (SELECT ID_AUD_REFERENCIAS FROM AUD_RELEVAMIENTOS WHERE CE=" & Application("unNumeroDeCE") & " AND SUCURSAL=" & Application("unNumeroDeSucursal") & " AND PERIODO='" & unPeriodoActual & "' )SELECT NRO_REFERENCIA,DESCRIPCION,STOCK_ENVIADO,ESTADO_ENVIADO,FECHA_ENVIADA,STOCK,ESTADO,FECHA FROM #TEMP_REFERENCIAS")
            unasReferencias.dataSet = unaTablaTemporal.dataSet
            unasReferencias.fillGridView(GridViewData)
            txtTitulo.Text = "CATEGORÍA :" & unaTablaIdCategoria.getItem(0, 1).ToString()
        ElseIf radImpresion.Items(1).Selected = True Then
            Dim unaTablaIdCategoria As TablaSQL = New TablaSQL()
            unaTablaIdCategoria.setConnectionString(unConnectionString)
            unaTablaIdCategoria.getDataSet("SELECT ID FROM AUD_CATEGORIAS WHERE CODIGO='" & lastCat & "'")
            Dim unaTablaTemporal As TablaSQL = New TablaSQL()
            unaTablaTemporal.setConnectionString(unConnectionString)
            unaTablaTemporal.getDataSet("CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[NRO_REFERENCIA] [char](20) NULL,[DESCRIPCION] [char] (75) NULL,[STOCK_ENVIADO] [int] NULL,[ESTADO_ENVIADO] [char] (1) NULL,[FECHA_ENVIADA] [char] (20) NULL,[STOCK] [char] (10) NULL,[ESTADO] [char] (1) NULL,[FECHA] [char] (20) NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (NRO_REFERENCIA,DESCRIPCION,STOCK_ENVIADO,ESTADO_ENVIADO,FECHA_ENVIADA,STOCK,ESTADO,FECHA)SELECT AUD_REFERENCIAS.NRO_REFERENCIA,AUD_REFERENCIAS.DESCRIPCION,(SELECT AUD_RELEVAMIENTOS.STOCK FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ") AS STOCK_ENVIADO, (SELECT AUD_RELEVAMIENTOS.ESTADO FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ") AS ESTADO_ENVIADO,(SELECT AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ") AS FECHA_ENVIADA,AUD_RELEVAMIENTOS.STOCK, AUD_RELEVAMIENTOS.ESTADO, AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS INNER JOIN AUD_REFERENCIAS ON AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID WHERE AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & " AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoActual & "' UNION SELECT NRO_REFERENCIA,DESCRIPCION,(SELECT AUD_RELEVAMIENTOS.STOCK FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ") AS STOCK_ENVIADO,(SELECT AUD_RELEVAMIENTOS.ESTADO FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ")AS ESTADO_ENVIADO,(SELECT AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ")AS FECHA_ENVIADA, '' AS STOCK, 'N' AS ESTADO, CONVERT(VARCHAR(8), GETDATE(), 112) AS FECHA FROM AUD_REFERENCIAS WHERE ID NOT IN (SELECT ID_AUD_REFERENCIAS FROM AUD_RELEVAMIENTOS WHERE CE=" & Application("unNumeroDeCE") & " AND SUCURSAL=" & Application("unNumeroDeSucursal") & " AND PERIODO='" & unPeriodoActual & "' )SELECT NRO_REFERENCIA,DESCRIPCION,STOCK_ENVIADO,ESTADO_ENVIADO,FECHA_ENVIADA,STOCK,ESTADO,FECHA FROM #TEMP_REFERENCIAS")
            unasReferencias.dataSet = unaTablaTemporal.dataSet
            unasReferencias.fillGridView(GridViewData)
            txtTitulo.Text = "TODAS LAS CATEGORÍAS"
        End If
        formatGridView()
    End Sub
End Class