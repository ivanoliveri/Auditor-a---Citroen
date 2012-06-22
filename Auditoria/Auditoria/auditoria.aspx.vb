Imports Auditoria.TableClass
Public Class auditoria
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtCE.Attributes.CssStyle.Add("TEXT-ALIGN", "right")
        txtSucursal.Attributes.CssStyle.Add("TEXT-ALIGN", "right")
        txtCE.Text = "CE: " & unNumeroDeCE
        txtSucursal.Text = "Sucursal: " & unNumeroDeSucursal
        unasReferencias.setConnectionString(unConnectionString)
        'Setteo este atributo para que cuando ingrese al popup, no esté vacio el gridView
        primerIngresoBusqueda = True 'Ver esto si lo pongo en el click, se ejecuta mucho mas rapido el JS
        'El ViewGrid viene por defecto en categoría A
        If primerIngresoMain = True Then
            lastCat = "A"
            calcularPaginas(lastCat)
            traerPrimerosRegistros(lastCat)
            setBorderOfButton(btnA)
            primerIngresoMain = False
        End If
        'Agrego Atributo onClick al Imprimir y Buscar
        btnSearch.Attributes.Add("onclick", "javascript:mostrarPopupBuscar('" & "busqueda.aspx" & "')")
        btnImprimir.Attributes.Add("onclick", "javascript:mostrarPopupImprimir('" & "imprimir.aspx" & "')")
        'Agrego validación de isNumeric al textBox de Stock
        'For Each row As GridViewRow In GridViewData.Rows
        ' Dim txtStock As TextBox = CType(row.FindControl("TextBox1"), TextBox)
        ' txtStock.Attributes.Add("onkeypress", "javascript:return isNumeric(event);")
        ' Next
        Dim todasLasCategorias As TablaSQL = New TablaSQL
        todasLasCategorias.setConnectionString(unConnectionString)
        todasLasCategorias.setTableName("AUD_CATEGORIAS")
        todasLasCategorias.getAllDataSet()
        'Agrego Atributo onMouseMove a los botones de Categoría
        btnA.Attributes.Add("onmouseover", "javascript:llenarLabel('CATEGORIA: " & todasLasCategorias.getItem(0, 2) & "')")
        btnB.Attributes.Add("onmouseover", "javascript:llenarLabel('CATEGORIA: " & todasLasCategorias.getItem(1, 2) & "')")
        btnC.Attributes.Add("onmouseover", "javascript:llenarLabel('CATEGORIA: " & todasLasCategorias.getItem(2, 2) & "')")
        btnD.Attributes.Add("onmouseover", "javascript:llenarLabel('CATEGORIA: " & todasLasCategorias.getItem(3, 2) & "')")
        btnE.Attributes.Add("onmouseover", "javascript:llenarLabel('CATEGORIA: " & todasLasCategorias.getItem(4, 2) & "')")
        btnF.Attributes.Add("onmouseover", "javascript:llenarLabel('CATEGORIA: " & todasLasCategorias.getItem(5, 2) & "')")
        btnG.Attributes.Add("onmouseover", "javascript:llenarLabel('CATEGORIA: " & todasLasCategorias.getItem(6, 2) & "')")
        btnA.Attributes.Add("onmouseout", "javascript:llenarLabel('')")
        btnB.Attributes.Add("onmouseout", "javascript:llenarLabel('')")
        btnC.Attributes.Add("onmouseout", "javascript:llenarLabel('')")
        btnD.Attributes.Add("onmouseout", "javascript:llenarLabel('')")
        btnE.Attributes.Add("onmouseout", "javascript:llenarLabel('')")
        btnF.Attributes.Add("onmouseout", "javascript:llenarLabel('')")
        btnG.Attributes.Add("onmouseout", "javascript:llenarLabel('')")
        'Seteo que los TextBox estén en align CENTER
        For Each row As GridViewRow In GridViewData.Rows

            ' MsgBox(lblFecha.Text)
            '    If unaFecha <> "" Then
            '        Dim unaNuevaFecha As String
            '        formatDate(unaFecha, unaNuevaFecha)
            '   lblFecha.Text = unaNuevaFecha
            Dim txtStock As TextBox = CType(row.FindControl("TextBox1"), TextBox)
            txtStock.Attributes.CssStyle.Add("TEXT-ALIGN", "center")
            '      End If
        Next

        hideNextOrPrevious()
    End Sub

    Protected Sub btnA_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnA.Click
        lastCat = "A"
        cargarCategoria()
        setBorderOfButton(btnA)
    End Sub

    Protected Sub btnB_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnB.Click
        lastCat = "B"
        cargarCategoria()
        setBorderOfButton(btnB)
    End Sub

    Protected Sub btnC_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnC.Click
        lastCat = "C"
        cargarCategoria()
        setBorderOfButton(btnC)
    End Sub

    Protected Sub btnD_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnD.Click
        lastCat = "D"
        cargarCategoria()
        setBorderOfButton(btnD)
    End Sub

    Protected Sub btnE_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnE.Click
        lastCat = "E"
        cargarCategoria()
        setBorderOfButton(btnE)
    End Sub

    Protected Sub btnF_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnF.Click
        lastCat = "F"
        cargarCategoria()
        setBorderOfButton(btnF)
    End Sub

    Protected Sub btnG_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnG.Click
        lastCat = "G"
        cargarCategoria()
        setBorderOfButton(btnG)
    End Sub

    Protected Sub btnNext_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnNext.Click
        If paginaActualMain < totalPaginasMain Then
            paginaActualMain += 1
            Dim unaTablaIdCategoria As TablaSQL = New TablaSQL()
            unaTablaIdCategoria.setConnectionString(unConnectionString)
            unaTablaIdCategoria.getDataSet("SELECT ID FROM AUD_CATEGORIAS WHERE CODIGO='" & lastCat & "'")
            Dim unaTablaTemporal As TablaSQL = New TablaSQL()
            unaTablaTemporal.setConnectionString(unConnectionString)
            'CREO UNA TABLA TEMPORAL PARA OBTENER EL CORRESPONDIENTE NÚMERO DE FILAS DE CADA UNO, CUANDO LA TERMINO DE USAR SE BORRA SOLA
            unaTablaTemporal.getDataSet("CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[NRO_REFERENCIA] [char](20) NULL,[DESCRIPCION] [char] (75) NULL,[STOCK_ENVIADO] [int] NULL,[ESTADO_ENVIADO] [char] (1) NULL,[FECHA_ENVIADA] [char] (20) NULL,[STOCK] [int] NULL,[ESTADO] [char] (1) NULL,[FECHA] [char] (20) NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (NRO_REFERENCIA,DESCRIPCION,STOCK_ENVIADO,ESTADO_ENVIADO,FECHA_ENVIADA,STOCK,ESTADO,FECHA)SELECT AUD_REFERENCIAS.NRO_REFERENCIA,AUD_REFERENCIAS.DESCRIPCION,(SELECT AUD_RELEVAMIENTOS.STOCK FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ") AS STOCK_ENVIADO, (SELECT AUD_RELEVAMIENTOS.ESTADO FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ") AS ESTADO_ENVIADO,(SELECT AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ") AS FECHA_ENVIADA,AUD_RELEVAMIENTOS.STOCK, AUD_RELEVAMIENTOS.ESTADO, AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS INNER JOIN AUD_REFERENCIAS ON AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID WHERE AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & " AND AUD_REFERENCIAS.ID_CATEGORIA=" & CInt(unaTablaIdCategoria.getItem(0, 0)) & " AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoActual & "' UNION SELECT NRO_REFERENCIA,DESCRIPCION,(SELECT AUD_RELEVAMIENTOS.STOCK FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ") AS STOCK_ENVIADO,(SELECT AUD_RELEVAMIENTOS.ESTADO FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ")AS ESTADO_ENVIADO,(SELECT AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ")AS FECHA_ENVIADA, '' AS STOCK, '' AS ESTADO, CONVERT(VARCHAR(8), GETDATE(), 112) AS FECHA FROM AUD_REFERENCIAS WHERE ID_CATEGORIA=" & CInt(unaTablaIdCategoria.getItem(0, 0)) & " AND ID NOT IN (SELECT ID_AUD_REFERENCIAS FROM AUD_RELEVAMIENTOS WHERE CE=" & unNumeroDeCE & " AND SUCURSAL=" & unNumeroDeSucursal & " AND PERIODO='" & unPeriodoActual & "' )SELECT NRO_REFERENCIA,DESCRIPCION,STOCK_ENVIADO,ESTADO_ENVIADO,FECHA_ENVIADA,STOCK,ESTADO,FECHA FROM #TEMP_REFERENCIAS WHERE FILA>" & (paginaActualMain - 1) * 10 & " AND FILA<=" & paginaActualMain * 10)
            unasReferencias.dataSet = unaTablaTemporal.dataSet
            unasReferencias.fillGridView(GridViewData)
            hideNextOrPrevious()
        End If
    End Sub

    Protected Sub btnPrevious_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnPrevious.Click
        If paginaActualMain <> 1 Then
            paginaActualMain -= 1
            If paginaActualMain = 1 Then
                traerPrimerosRegistros(lastCat)
            Else
                Dim unaTablaIdCategoria As TablaSQL = New TablaSQL()
                unaTablaIdCategoria.setConnectionString(unConnectionString)
                unaTablaIdCategoria.getDataSet("SELECT ID FROM AUD_CATEGORIAS WHERE CODIGO='" & lastCat & "'")
                'CREO UNA TABLA TEMPORAL PARA OBTENER EL CORRESPONDIENTE NÚMERO DE FILAS DE CADA UNO, CUANDO LA TERMINO DE USAR SE BORRA
                Dim unaTablaTemporal As TablaSQL = New TablaSQL()
                unaTablaTemporal.setConnectionString(unConnectionString)
                unaTablaTemporal.getDataSet("CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[NRO_REFERENCIA] [char](20) NULL,[DESCRIPCION] [char] (75) NULL,[STOCK_ENVIADO] [int] NULL,[ESTADO_ENVIADO] [char] (1) NULL,[FECHA_ENVIADA] [char] (20) NULL,[STOCK] [int] NULL,[ESTADO] [char] (1) NULL,[FECHA] [char] (20) NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (NRO_REFERENCIA,DESCRIPCION,STOCK_ENVIADO,ESTADO_ENVIADO,FECHA_ENVIADA,STOCK,ESTADO,FECHA)SELECT AUD_REFERENCIAS.NRO_REFERENCIA,AUD_REFERENCIAS.DESCRIPCION,(SELECT AUD_RELEVAMIENTOS.STOCK FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ") AS STOCK_ENVIADO, (SELECT AUD_RELEVAMIENTOS.ESTADO FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ") AS ESTADO_ENVIADO,(SELECT AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ") AS FECHA_ENVIADA,AUD_RELEVAMIENTOS.STOCK, AUD_RELEVAMIENTOS.ESTADO, AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS INNER JOIN AUD_REFERENCIAS ON AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID WHERE AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & " AND AUD_REFERENCIAS.ID_CATEGORIA=" & CInt(unaTablaIdCategoria.getItem(0, 0)) & " AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoActual & "' UNION SELECT NRO_REFERENCIA,DESCRIPCION,(SELECT AUD_RELEVAMIENTOS.STOCK FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ") AS STOCK_ENVIADO,(SELECT AUD_RELEVAMIENTOS.ESTADO FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ")AS ESTADO_ENVIADO,(SELECT AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ")AS FECHA_ENVIADA, '' AS STOCK, '' AS ESTADO, CONVERT(VARCHAR(8), GETDATE(), 112) AS FECHA FROM AUD_REFERENCIAS WHERE ID_CATEGORIA=" & CInt(unaTablaIdCategoria.getItem(0, 0)) & " AND ID NOT IN (SELECT ID_AUD_REFERENCIAS FROM AUD_RELEVAMIENTOS WHERE CE=" & unNumeroDeCE & " AND SUCURSAL=" & unNumeroDeSucursal & " AND PERIODO='" & unPeriodoActual & "' )SELECT NRO_REFERENCIA,DESCRIPCION,STOCK_ENVIADO,ESTADO_ENVIADO,FECHA_ENVIADA,STOCK,ESTADO,FECHA FROM #TEMP_REFERENCIAS WHERE FILA>" & (paginaActualMain - 1) * 10 & " AND FILA<=" & paginaActualMain * 10)
                unasReferencias.dataSet = unaTablaTemporal.dataSet
                unasReferencias.fillGridView(GridViewData)
            End If
            hideNextOrPrevious()
        End If
    End Sub

    Protected Sub traerPrimerosRegistros(ByVal lastCat As String)
        Dim unaTablaIdCategoria As TablaSQL = New TablaSQL()
        unaTablaIdCategoria.setConnectionString(unConnectionString)
        unaTablaIdCategoria.getDataSet("SELECT ID FROM AUD_CATEGORIAS WHERE CODIGO='" & lastCat & "'")
        Dim unaTablaTemporal As TablaSQL = New TablaSQL()
        unaTablaTemporal.setConnectionString(unConnectionString)
        'CREO UNA TABLA TEMPORAL PARA OBTENER EL CORRESPONDIENTE NÚMERO DE FILAS DE CADA UNO, CUANDO LA TERMINO DE USAR SE BORRA SOLA
        unaTablaTemporal.getDataSet("CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[NRO_REFERENCIA] [char](20) NULL,[DESCRIPCION] [char] (75) NULL,[STOCK_ENVIADO] [int] NULL,[ESTADO_ENVIADO] [char] (1) NULL,[FECHA_ENVIADA] [char] (20) NULL,[STOCK] [int] NULL,[ESTADO] [char] (1) NULL,[FECHA] [char] (20) NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (NRO_REFERENCIA,DESCRIPCION,STOCK_ENVIADO,ESTADO_ENVIADO,FECHA_ENVIADA,STOCK,ESTADO,FECHA)SELECT AUD_REFERENCIAS.NRO_REFERENCIA,AUD_REFERENCIAS.DESCRIPCION,(SELECT AUD_RELEVAMIENTOS.STOCK FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ") AS STOCK_ENVIADO, (SELECT AUD_RELEVAMIENTOS.ESTADO FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ") AS ESTADO_ENVIADO,(SELECT AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ") AS FECHA_ENVIADA,AUD_RELEVAMIENTOS.STOCK, AUD_RELEVAMIENTOS.ESTADO, AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS INNER JOIN AUD_REFERENCIAS ON AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID WHERE AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & " AND AUD_REFERENCIAS.ID_CATEGORIA=" & CInt(unaTablaIdCategoria.getItem(0, 0)) & " AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoActual & "' UNION SELECT NRO_REFERENCIA,DESCRIPCION,(SELECT AUD_RELEVAMIENTOS.STOCK FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ") AS STOCK_ENVIADO,(SELECT AUD_RELEVAMIENTOS.ESTADO FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ")AS ESTADO_ENVIADO,(SELECT AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ")AS FECHA_ENVIADA, '' AS STOCK, '' AS ESTADO, CONVERT(VARCHAR(8), GETDATE(), 112) AS FECHA FROM AUD_REFERENCIAS WHERE ID_CATEGORIA=" & CInt(unaTablaIdCategoria.getItem(0, 0)) & " AND ID NOT IN (SELECT ID_AUD_REFERENCIAS FROM AUD_RELEVAMIENTOS WHERE CE=" & unNumeroDeCE & " AND SUCURSAL=" & unNumeroDeSucursal & " AND PERIODO='" & unPeriodoActual & "' )SELECT NRO_REFERENCIA,DESCRIPCION,STOCK_ENVIADO,ESTADO_ENVIADO,FECHA_ENVIADA,STOCK,ESTADO,FECHA FROM #TEMP_REFERENCIAS WHERE FILA>0 AND FILA<=10")
        unasReferencias.dataSet = unaTablaTemporal.dataSet
        unasReferencias.fillGridView(GridViewData)
        paginaActualMain = 1
    End Sub
    Protected Sub calcularPaginas(ByVal lastCat As String)
        Dim unaTablaTemporal As TablaSQL = New TablaSQL()
        unaTablaTemporal.setConnectionString(unConnectionString)
        Dim unaTablaIdCategoria As TablaSQL = New TablaSQL()
        unaTablaIdCategoria.setConnectionString(unConnectionString)
        unaTablaIdCategoria.getDataSet("SELECT ID FROM AUD_CATEGORIAS WHERE CODIGO='" & lastCat & "'")
        unaTablaTemporal.getDataSet("CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[NRO_REFERENCIA] [char](20) NULL,[DESCRIPCION] [char] (75) NULL,[STOCK_ENVIADO] [int] NULL,[ESTADO_ENVIADO] [char] (1) NULL,[FECHA_ENVIADA] [char] (20) NULL,[STOCK] [int] NULL,[ESTADO] [char] (1) NULL,[FECHA] [char] (20) NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (NRO_REFERENCIA,DESCRIPCION,STOCK_ENVIADO,ESTADO_ENVIADO,FECHA_ENVIADA,STOCK,ESTADO,FECHA)SELECT AUD_REFERENCIAS.NRO_REFERENCIA,AUD_REFERENCIAS.DESCRIPCION,(SELECT AUD_RELEVAMIENTOS.STOCK FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ") AS STOCK_ENVIADO, (SELECT AUD_RELEVAMIENTOS.ESTADO FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ") AS ESTADO_ENVIADO,(SELECT AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ") AS FECHA_ENVIADA,AUD_RELEVAMIENTOS.STOCK, AUD_RELEVAMIENTOS.ESTADO, AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS INNER JOIN AUD_REFERENCIAS ON AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID WHERE AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & " AND AUD_REFERENCIAS.ID_CATEGORIA=" & CInt(unaTablaIdCategoria.getItem(0, 0)) & " AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoActual & "' UNION SELECT NRO_REFERENCIA,DESCRIPCION,(SELECT AUD_RELEVAMIENTOS.STOCK FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ") AS STOCK_ENVIADO,(SELECT AUD_RELEVAMIENTOS.ESTADO FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ")AS ESTADO_ENVIADO,(SELECT AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ")AS FECHA_ENVIADA, '' AS STOCK, '' AS ESTADO, CONVERT(VARCHAR(8), GETDATE(), 112) AS FECHA FROM AUD_REFERENCIAS WHERE ID_CATEGORIA=" & CInt(unaTablaIdCategoria.getItem(0, 0)) & " AND ID NOT IN (SELECT ID_AUD_REFERENCIAS FROM AUD_RELEVAMIENTOS WHERE CE=" & unNumeroDeCE & " AND SUCURSAL=" & unNumeroDeSucursal & " AND PERIODO='" & unPeriodoActual & "' )SELECT COUNT(*) FROM #TEMP_REFERENCIAS")
        If CInt(unaTablaTemporal.getItem(0, 0)) Mod 10 = 0 Then
            totalPaginasMain = CInt(unaTablaTemporal.getItem(0, 0)) / 10
        Else
            totalPaginasMain = CInt(unaTablaTemporal.getItem(0, 0)) \ 10 + 1
        End If
    End Sub
    Protected Sub cargarCategoria()
        GridViewData.SelectedIndex = -1
        paginaActualMain = 1
        calcularPaginas(lastCat)
        traerPrimerosRegistros(lastCat)
    End Sub

    Protected Sub TextBox1_TextChanged(sender As Object, e As EventArgs)
        Dim unaPosicion As Integer = 0
        For Each row As GridViewRow In GridViewData.Rows
            Dim txtStock As TextBox = CType(row.FindControl("TextBox1"), TextBox)
            Dim unStockTextBox As String = Trim(txtStock.Text)
            'MsgBox(unStockTextBox)
            Dim unStockDataTable As String = Trim(unasReferencias.getItem(unaPosicion, 2))
            If unStockTextBox <> unStockDataTable Then
                ' MsgBox(unaPosicion)
                Dim unaTablaIdReferencia As TablaSQL = New TablaSQL
                unaTablaIdReferencia.setConnectionString(unConnectionString)
                unaTablaIdReferencia.getDataSet("SELECT ID FROM AUD_REFERENCIAS WHERE NRO_REFERENCIA='" & unasReferencias.getItem(unaPosicion, 0) & "'")
                'MsgBox("UPDATE AUD_RELEVAMIENTOS SET STOCK_REAL='" & unStockTextBox & "' WHERE ID_AUD_REFERENCIAS=" & CInt(unaTablaIdReferencia.getItem(0, 0)) & " AND CE=" & unNumeroDeCE & " AND SUCURSAL=" & unNumeroDeSucursal)
                unasReferencias.execQuery("UPDATE AUD_RELEVAMIENTOS SET STOCK='" & unStockTextBox & "' WHERE ID_AUD_REFERENCIAS=" & CInt(unaTablaIdReferencia.getItem(0, 0)) & " AND CE=" & unNumeroDeCE & " AND SUCURSAL=" & unNumeroDeSucursal)
            End If
            unaPosicion += 1
        Next
    End Sub

    Private Sub GridViewData_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewData.RowDataBound
        ' Try
        'Tengo que considerar la opción de AutoGenerateColums=False. En el caso de que una categoría tenga 2 columnas más que otra NO SIRVE
        'For i = 5 To 10
        'e.Row.Cells(i).Visible = False
        '  Next i
        ' Catch ex As Exception

        '   End Try
    End Sub

    Protected Sub RadioButtonList1_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim unaPosicion As Integer = 0
        For Each row As GridViewRow In GridViewData.Rows
            Dim radOpciones As RadioButtonList = CType(row.FindControl("RadioButtonList1"), RadioButtonList)
            Dim valorRadOpciones As String = radOpciones.SelectedValue
            'MsgBox(valorRadOpciones)
            Dim unValorTabla As String = Trim(unasReferencias.dataSet.Tables(0).Rows(unaPosicion).Item(4).ToString())
            If valorRadOpciones <> unValorTabla Then
                Dim unaTablaIdReferencia As TablaSQL = New TablaSQL
                unaTablaIdReferencia.setConnectionString(unConnectionString)
                unaTablaIdReferencia.getDataSet("SELECT ID FROM AUD_REFERENCIAS WHERE NRO_REFERENCIA='" & unasReferencias.getItem(unaPosicion, 0) & "'")
                unasReferencias.execQuery("UPDATE AUD_RELEVAMIENTOS SET ESTADO='" & valorRadOpciones & "' WHERE ID_AUD_REFERENCIAS=" & CInt(unaTablaIdReferencia.getItem(0, 0)) & " AND CE=" & unNumeroDeCE & " AND SUCURSAL=" & unNumeroDeSucursal)
            End If
            unaPosicion += 1
        Next
    End Sub
    Protected Sub formatDate(ByVal unaFecha As String, ByRef unaNuevaFecha As String)
        Dim unAno As String = Left(unaFecha, 4)
        Dim unMes As String = Mid(unaFecha, 5, 2)
        Dim unDia As String = Mid(unaFecha, 7, 2)
        unaNuevaFecha = unDia & "/" & unMes & "/" & unAno
    End Sub

    Protected Sub hideNextOrPrevious()
        If paginaActualMain = 1 Then
            btnPrevious.Visible = False
        Else
            btnPrevious.Visible = True
        End If
        If paginaActualMain = totalPaginasMain Then
            btnNext.Visible = False
        Else
            btnNext.Visible = True
        End If
    End Sub
    Protected Sub setBorderOfButton(ByRef unButton As ImageButton)
        btnA.BorderWidth = 0
        btnB.BorderWidth = 0
        btnC.BorderWidth = 0
        btnD.BorderWidth = 0
        btnE.BorderWidth = 0
        btnF.BorderWidth = 0
        btnG.BorderWidth = 0
        unButton.BorderWidth = 2
        unButton.BorderColor = Drawing.Color.Red
    End Sub
End Class