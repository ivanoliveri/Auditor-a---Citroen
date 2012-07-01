Imports Auditoria.TableClass
Public Class auditoria
    Inherits System.Web.UI.Page

    Protected Sub traerPrimerosRegistros(ByVal lastCat As String)
        Dim unaTablaIdCategoria As TablaSQL = New TablaSQL()
        unaTablaIdCategoria.setConnectionString(unConnectionString)
        unaTablaIdCategoria.getDataSet("SELECT ID FROM AUD_CATEGORIAS WHERE CODIGO='" & lastCat & "'")
        Dim unaTablaTemporal As TablaSQL = New TablaSQL()
        unaTablaTemporal.setConnectionString(unConnectionString)
        'CREO UNA TABLA TEMPORAL PARA OBTENER EL CORRESPONDIENTE NÚMERO DE FILAS DE CADA UNO, CUANDO LA TERMINO DE USAR SE BORRA SOLA
        ultimoQuery = "CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[CATEGORIA] [char](1) NULL,[NRO_REFERENCIA] [char](15) NULL,[DESCRIPCION] [char] (55) NULL,[STOCK_ENVIADO] [int] NULL,[ESTADO_ENVIADO] [char] (1) NULL,[FECHA_ENVIADA] [char] (15) NULL,[STOCK] [char] (10) NULL,[ESTADO] [char] (1) NULL,[FECHA] [char] (15) NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (CATEGORIA,NRO_REFERENCIA,DESCRIPCION,STOCK_ENVIADO,ESTADO_ENVIADO,FECHA_ENVIADA,STOCK,ESTADO,FECHA)SELECT (SELECT CODIGO FROM AUD_CATEGORIAS WHERE AUD_CATEGORIAS.ID=AUD_REFERENCIAS.ID_CATEGORIA),AUD_REFERENCIAS.NRO_REFERENCIA,AUD_REFERENCIAS.DESCRIPCION,(SELECT AUD_RELEVAMIENTOS.STOCK FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ") AS STOCK_ENVIADO, (SELECT AUD_RELEVAMIENTOS.ESTADO FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ") AS ESTADO_ENVIADO,(SELECT AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ") AS FECHA_ENVIADA,AUD_RELEVAMIENTOS.STOCK, AUD_RELEVAMIENTOS.ESTADO, AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS INNER JOIN AUD_REFERENCIAS ON AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID WHERE AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & " AND AUD_REFERENCIAS.ID_CATEGORIA=" & CInt(unaTablaIdCategoria.getItem(0, 0)) & " AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoActual & "' UNION SELECT (SELECT CODIGO FROM AUD_CATEGORIAS WHERE AUD_CATEGORIAS.ID=AUD_REFERENCIAS.ID_CATEGORIA),NRO_REFERENCIA,DESCRIPCION,(SELECT AUD_RELEVAMIENTOS.STOCK FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ") AS STOCK_ENVIADO,(SELECT AUD_RELEVAMIENTOS.ESTADO FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ")AS ESTADO_ENVIADO,(SELECT AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ")AS FECHA_ENVIADA, '' AS STOCK, 'N' AS ESTADO, CONVERT(VARCHAR(8), GETDATE(), 112) AS FECHA FROM AUD_REFERENCIAS WHERE ID_CATEGORIA=" & CInt(unaTablaIdCategoria.getItem(0, 0)) & " AND ID NOT IN (SELECT ID_AUD_REFERENCIAS FROM AUD_RELEVAMIENTOS WHERE CE=" & unNumeroDeCE & " AND SUCURSAL=" & unNumeroDeSucursal & " AND PERIODO='" & unPeriodoActual & "' )SELECT CATEGORIA,NRO_REFERENCIA,DESCRIPCION,STOCK_ENVIADO,ESTADO_ENVIADO,FECHA_ENVIADA,STOCK,ESTADO,FECHA FROM #TEMP_REFERENCIAS WHERE FILA>0 AND FILA<=10"
        unaTablaTemporal.getDataSet(ultimoQuery)
        unasReferencias.dataSet = unaTablaTemporal.dataSet
        paginaActualMain = 1
    End Sub

    Protected Sub calcularPaginas(ByVal lastCat As String)
        Dim unaTablaTemporal As TablaSQL = New TablaSQL()
        unaTablaTemporal.setConnectionString(unConnectionString)
        Dim unaTablaIdCategoria As TablaSQL = New TablaSQL()
        unaTablaIdCategoria.setConnectionString(unConnectionString)
        unaTablaIdCategoria.getDataSet("SELECT ID FROM AUD_CATEGORIAS WHERE CODIGO='" & lastCat & "'")
        unaTablaTemporal.getDataSet("CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[NRO_REFERENCIA] [char](15) NULL,[DESCRIPCION] [char] (55) NULL,[STOCK_ENVIADO] [int] NULL,[ESTADO_ENVIADO] [char] (1) NULL,[FECHA_ENVIADA] [char] (15) NULL,[STOCK] [char] (10) NULL,[ESTADO] [char] (1) NULL,[FECHA] [char] (15) NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (NRO_REFERENCIA,DESCRIPCION,STOCK_ENVIADO,ESTADO_ENVIADO,FECHA_ENVIADA,STOCK,ESTADO,FECHA)SELECT AUD_REFERENCIAS.NRO_REFERENCIA,AUD_REFERENCIAS.DESCRIPCION,(SELECT AUD_RELEVAMIENTOS.STOCK FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ") AS STOCK_ENVIADO, (SELECT AUD_RELEVAMIENTOS.ESTADO FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ") AS ESTADO_ENVIADO,(SELECT AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ") AS FECHA_ENVIADA,AUD_RELEVAMIENTOS.STOCK, AUD_RELEVAMIENTOS.ESTADO, AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS INNER JOIN AUD_REFERENCIAS ON AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID WHERE AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & " AND AUD_REFERENCIAS.ID_CATEGORIA=" & CInt(unaTablaIdCategoria.getItem(0, 0)) & " AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoActual & "' UNION SELECT NRO_REFERENCIA,DESCRIPCION,(SELECT AUD_RELEVAMIENTOS.STOCK FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ") AS STOCK_ENVIADO,(SELECT AUD_RELEVAMIENTOS.ESTADO FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ")AS ESTADO_ENVIADO,(SELECT AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ")AS FECHA_ENVIADA, '' AS STOCK, 'N' AS ESTADO, CONVERT(VARCHAR(8), GETDATE(), 112) AS FECHA FROM AUD_REFERENCIAS WHERE ID_CATEGORIA=" & CInt(unaTablaIdCategoria.getItem(0, 0)) & " AND ID NOT IN (SELECT ID_AUD_REFERENCIAS FROM AUD_RELEVAMIENTOS WHERE CE=" & unNumeroDeCE & " AND SUCURSAL=" & unNumeroDeSucursal & " AND PERIODO='" & unPeriodoActual & "' )SELECT COUNT(*) FROM #TEMP_REFERENCIAS")
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
        hideNextOrPrevious()
    End Sub

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

    Protected Sub setImageButton(ByVal unaCategoriaPasada As String, ByVal unaCategoriaActual As String)
        ' If unaCategoriaActual = unaCategoriaPasada Then Exit Sub
        Select Case unaCategoriaPasada
            Case "A"
                btnA.ImageUrl = "~/images/buttons/btnA.png"
            Case "B"
                btnB.ImageUrl = "~/images/buttons/btnB.png"
            Case "C"
                btnC.ImageUrl = "~/images/buttons/btnC.png"
            Case "D"
                btnD.ImageUrl = "~/images/buttons/btnD.png"
            Case "E"
                btnE.ImageUrl = "~/images/buttons/btnE.png"
            Case "F"
                btnF.ImageUrl = "~/images/buttons/btnF.png"
            Case "G"
                btnG.ImageUrl = "~/images/buttons/btnG.png"
        End Select

        Select Case unaCategoriaActual
            Case "A"
                btnA.ImageUrl = "~/images/buttons/btnA1.png"
            Case "B"
                btnB.ImageUrl = "~/images/buttons/btnB1.png"
            Case "C"
                btnC.ImageUrl = "~/images/buttons/btnC1.png"
            Case "D"
                btnD.ImageUrl = "~/images/buttons/btnD1.png"
            Case "E"
                btnE.ImageUrl = "~/images/buttons/btnE1.png"
            Case "F"
                btnF.ImageUrl = "~/images/buttons/btnF1.png"
            Case "G"
                btnG.ImageUrl = "~/images/buttons/btnG1.png"
        End Select
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
            ' txtStock.Attributes.CssStyle.Add("TEXT-ALIGN", "center")
        Next
    End Sub
    'Sirve para eliminar los ceros antes del número. Input:00502; Output:502
    Function formatStock(ByRef unString As String)
        If unString = "" Then Return unString
        Dim unContador As Integer = 1
        Dim unaLetra As String = GetChar(unString, unContador)
        Do While unContador < unString.Length() And unaLetra = "0"
            unContador += 1
            If unContador <= unString.Length() Then unaLetra = GetChar(unString, unContador)
        Loop
        Return Mid(unString, unContador)
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        agregoOedito = False
        txtCE.Attributes.CssStyle.Add("TEXT-ALIGN", "right")
        txtSucursal.Attributes.CssStyle.Add("TEXT-ALIGN", "right")
        txtPeriodo.Attributes.CssStyle.Add("TEXT-ALIGN", "right")
        txtCE.Text = "CE: " & unNumeroDeCE
        txtSucursal.Text = "Sucursal: " & unNumeroDeSucursal
        txtPeriodo.Text = "Período: " & unPeriodoActual
        unasReferencias.setConnectionString(unConnectionString)
        'Setteo este atributo para que cuando ingrese al popup, no esté vacio el gridView
        'El ViewGrid viene por defecto en categoría A
        If primerIngresoMain = True Then
            setImageButton(lastCat, "A")
            lastCat = "A"
            calcularPaginas(lastCat)
            traerPrimerosRegistros(lastCat)
            primerIngresoMain = False
        End If
        If BusquedaMode = True Then
            setImageButton(lastCat, codCategoriaToSearch)
            lastCat = codCategoriaToSearch
            'MsgBox("ID_CATEGORIA: " & idCategoriaToSearch & " ; ID_REF: " & idReferenciaToSearch & " ; NRO_REF: " & nroReferenciaToSearch)
            'setImageButton(lastCat, codCategoriaToSearch)
            Dim unaTablaTemporal As TablaSQL = New TablaSQL
            unaTablaTemporal.setConnectionString(unConnectionString)
            unaTablaTemporal.getDataSet("CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1)  NOT NULL,[NRO_REFERENCIA] [char] (15) NOT NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (NRO_REFERENCIA) SELECT NRO_REFERENCIA FROM AUD_REFERENCIAS WHERE ID_CATEGORIA=" & idCategoriaToSearch & " ORDER BY NRO_REFERENCIA ASC SELECT FILA FROM #TEMP_REFERENCIAS WHERE NRO_REFERENCIA='" & nroReferenciaToSearch & "'")
            Dim unNumeroDeFila As Integer = CInt(unaTablaTemporal.getItem(0, 0))
            Dim maxFila As Integer
            If unNumeroDeFila Mod 10 <> 0 Then
                totalPaginasMain = CInt(unaTablaTemporal.getItem(0, 0)) \ 10 + 1
                maxFila = unNumeroDeFila
                Do While maxFila Mod 10 <> 0
                    maxFila += 1
                Loop
            Else
                maxFila = unNumeroDeFila
            End If
            'unNumFila = 222 y maxFila=230
            GridViewData.SelectedIndex = (unNumeroDeFila - (maxFila - 10)) - 1
            paginaActualMain = maxFila / 10
            calcularPaginas(codCategoriaToSearch)
            'MsgBox("TOTAL PAGINAS: " & totalPaginasMain & " ; MAX FILA: " & maxFila & " ; NRO FILA: " & unNumeroDeFila)
            ultimoQuery = "CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[CATEGORIA] [char](1) NULL,[NRO_REFERENCIA] [char](15) NULL,[DESCRIPCION] [char] (55) NULL,[STOCK_ENVIADO] [int] NULL,[ESTADO_ENVIADO] [char] (1) NULL,[FECHA_ENVIADA] [char] (15) NULL,[STOCK] [char] (10) NULL,[ESTADO] [char] (1) NULL,[FECHA] [char] (15) NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (CATEGORIA,NRO_REFERENCIA,DESCRIPCION,STOCK_ENVIADO,ESTADO_ENVIADO,FECHA_ENVIADA,STOCK,ESTADO,FECHA)SELECT (SELECT CODIGO FROM AUD_CATEGORIAS WHERE AUD_CATEGORIAS.ID=AUD_REFERENCIAS.ID_CATEGORIA),AUD_REFERENCIAS.NRO_REFERENCIA,AUD_REFERENCIAS.DESCRIPCION,(SELECT AUD_RELEVAMIENTOS.STOCK FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ") AS STOCK_ENVIADO, (SELECT AUD_RELEVAMIENTOS.ESTADO FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ") AS ESTADO_ENVIADO,(SELECT AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ") AS FECHA_ENVIADA,AUD_RELEVAMIENTOS.STOCK, AUD_RELEVAMIENTOS.ESTADO, AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS INNER JOIN AUD_REFERENCIAS ON AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID WHERE AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & " AND AUD_REFERENCIAS.ID_CATEGORIA=" & idCategoriaToSearch & " AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoActual & "' UNION SELECT (SELECT CODIGO FROM AUD_CATEGORIAS WHERE AUD_CATEGORIAS.ID=AUD_REFERENCIAS.ID_CATEGORIA),NRO_REFERENCIA,DESCRIPCION,(SELECT AUD_RELEVAMIENTOS.STOCK FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ") AS STOCK_ENVIADO,(SELECT AUD_RELEVAMIENTOS.ESTADO FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ")AS ESTADO_ENVIADO,(SELECT AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ")AS FECHA_ENVIADA, '' AS STOCK, 'N' AS ESTADO, CONVERT(VARCHAR(8), GETDATE(), 112) AS FECHA FROM AUD_REFERENCIAS WHERE ID_CATEGORIA=" & idCategoriaToSearch & " AND ID NOT IN (SELECT ID_AUD_REFERENCIAS FROM AUD_RELEVAMIENTOS WHERE CE=" & unNumeroDeCE & " AND SUCURSAL=" & unNumeroDeSucursal & " AND PERIODO='" & unPeriodoActual & "' )SELECT CATEGORIA,NRO_REFERENCIA,DESCRIPCION,STOCK_ENVIADO,ESTADO_ENVIADO,FECHA_ENVIADA,STOCK,ESTADO,FECHA FROM #TEMP_REFERENCIAS WHERE FILA>" & maxFila - 10 & " AND FILA<=" & maxFila
            unasReferencias.getDataSet(ultimoQuery)
            hideNextOrPrevious()
            formatGridView()
            BusquedaMode = False
        End If
        'Agrego Atributo onClick al Imprimir y Buscar
        btnSearch.Attributes.Add("onclick", "javascript:mostrarPopupBuscar('" & "busqueda.aspx" & "')")
        btnImprimir.Attributes.Add("onclick", "javascript:mostrarPopupImprimir('" & "imprimir.aspx" & "')")
        btnAgregar.Attributes.Add("onclick", "javascript:mostrarPopupAgregar('" & "agregar.aspx" & "')")
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
        hideNextOrPrevious()
    End Sub

    Protected Sub btnA_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnA.Click
        If agregoOedito = True Then Exit Sub
        setImageButton(lastCat, "A")
        lastCat = "A"
        cargarCategoria()
        formatGridView()
    End Sub

    Protected Sub btnB_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnB.Click
        setImageButton(lastCat, "B")
        lastCat = "B"
        cargarCategoria()
        formatGridView()
    End Sub

    Protected Sub btnC_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnC.Click
        setImageButton(lastCat, "C")
        lastCat = "C"
        cargarCategoria()
        formatGridView()
    End Sub

    Protected Sub btnD_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnD.Click
        setImageButton(lastCat, "D")
        lastCat = "D"
        cargarCategoria()
        formatGridView()
    End Sub

    Protected Sub btnE_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnE.Click
        setImageButton(lastCat, "E")
        lastCat = "E"
        cargarCategoria()
        formatGridView()
    End Sub

    Protected Sub btnF_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnF.Click
        setImageButton(lastCat, "F")
        lastCat = "F"
        cargarCategoria()
        formatGridView()
    End Sub

    Protected Sub btnG_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnG.Click
        setImageButton(lastCat, "G")
        lastCat = "G"
        cargarCategoria()
        formatGridView()
    End Sub

    Protected Sub btnNext_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnNext.Click
        If paginaActualMain < totalPaginasMain Then
            GridViewData.SelectedIndex = -1
            paginaActualMain += 1
            Dim unaTablaIdCategoria As TablaSQL = New TablaSQL()
            unaTablaIdCategoria.setConnectionString(unConnectionString)
            unaTablaIdCategoria.getDataSet("SELECT ID FROM AUD_CATEGORIAS WHERE CODIGO='" & lastCat & "'")
            Dim unaTablaTemporal As TablaSQL = New TablaSQL()
            unaTablaTemporal.setConnectionString(unConnectionString)
            'CREO UNA TABLA TEMPORAL PARA OBTENER EL CORRESPONDIENTE NÚMERO DE FILAS DE CADA UNO, CUANDO LA TERMINO DE USAR SE BORRA SOLA
            ultimoQuery = "CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[CATEGORIA] [char](1) NULL,[NRO_REFERENCIA] [char](15) NULL,[DESCRIPCION] [char] (55) NULL,[STOCK_ENVIADO] [int] NULL,[ESTADO_ENVIADO] [char] (1) NULL,[FECHA_ENVIADA] [char] (15) NULL,[STOCK] [char] (10) NULL,[ESTADO] [char] (1) NULL,[FECHA] [char] (15) NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (CATEGORIA,NRO_REFERENCIA,DESCRIPCION,STOCK_ENVIADO,ESTADO_ENVIADO,FECHA_ENVIADA,STOCK,ESTADO,FECHA)SELECT (SELECT CODIGO FROM AUD_CATEGORIAS WHERE AUD_CATEGORIAS.ID=AUD_REFERENCIAS.ID_CATEGORIA),AUD_REFERENCIAS.NRO_REFERENCIA,AUD_REFERENCIAS.DESCRIPCION,(SELECT AUD_RELEVAMIENTOS.STOCK FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ") AS STOCK_ENVIADO, (SELECT AUD_RELEVAMIENTOS.ESTADO FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ") AS ESTADO_ENVIADO,(SELECT AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ") AS FECHA_ENVIADA,AUD_RELEVAMIENTOS.STOCK, AUD_RELEVAMIENTOS.ESTADO, AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS INNER JOIN AUD_REFERENCIAS ON AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID WHERE AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & " AND AUD_REFERENCIAS.ID_CATEGORIA=" & CInt(unaTablaIdCategoria.getItem(0, 0)) & " AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoActual & "' UNION SELECT (SELECT CODIGO FROM AUD_CATEGORIAS WHERE AUD_CATEGORIAS.ID=AUD_REFERENCIAS.ID_CATEGORIA),NRO_REFERENCIA,DESCRIPCION,(SELECT AUD_RELEVAMIENTOS.STOCK FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ") AS STOCK_ENVIADO,(SELECT AUD_RELEVAMIENTOS.ESTADO FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ")AS ESTADO_ENVIADO,(SELECT AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ")AS FECHA_ENVIADA, '' AS STOCK, 'N' AS ESTADO, CONVERT(VARCHAR(8), GETDATE(), 112) AS FECHA FROM AUD_REFERENCIAS WHERE ID_CATEGORIA=" & CInt(unaTablaIdCategoria.getItem(0, 0)) & " AND ID NOT IN (SELECT ID_AUD_REFERENCIAS FROM AUD_RELEVAMIENTOS WHERE CE=" & unNumeroDeCE & " AND SUCURSAL=" & unNumeroDeSucursal & " AND PERIODO='" & unPeriodoActual & "' )SELECT CATEGORIA,NRO_REFERENCIA,DESCRIPCION,STOCK_ENVIADO,ESTADO_ENVIADO,FECHA_ENVIADA,STOCK,ESTADO,FECHA FROM #TEMP_REFERENCIAS WHERE FILA>" & (paginaActualMain - 1) * 10 & " AND FILA<=" & paginaActualMain * 10
            unaTablaTemporal.getDataSet(ultimoQuery)
            unasReferencias.dataSet = unaTablaTemporal.dataSet
            hideNextOrPrevious()
            formatGridView()
        End If
    End Sub

    Protected Sub btnPrevious_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnPrevious.Click
        If paginaActualMain <> 1 Then
            GridViewData.SelectedIndex = -1
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
                ultimoQuery = "CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[CATEGORIA] [char](1) NULL,[NRO_REFERENCIA] [char](15) NULL,[DESCRIPCION] [char] (55) NULL,[STOCK_ENVIADO] [int] NULL,[ESTADO_ENVIADO] [char] (1) NULL,[FECHA_ENVIADA] [char] (15) NULL,[STOCK] [char] (10) NULL,[ESTADO] [char] (1) NULL,[FECHA] [char] (15) NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (CATEGORIA,NRO_REFERENCIA,DESCRIPCION,STOCK_ENVIADO,ESTADO_ENVIADO,FECHA_ENVIADA,STOCK,ESTADO,FECHA)SELECT (SELECT CODIGO FROM AUD_CATEGORIAS WHERE AUD_CATEGORIAS.ID=AUD_REFERENCIAS.ID_CATEGORIA),AUD_REFERENCIAS.NRO_REFERENCIA,AUD_REFERENCIAS.DESCRIPCION,(SELECT AUD_RELEVAMIENTOS.STOCK FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ") AS STOCK_ENVIADO, (SELECT AUD_RELEVAMIENTOS.ESTADO FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ") AS ESTADO_ENVIADO,(SELECT AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ") AS FECHA_ENVIADA,AUD_RELEVAMIENTOS.STOCK, AUD_RELEVAMIENTOS.ESTADO, AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS INNER JOIN AUD_REFERENCIAS ON AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID WHERE AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & " AND AUD_REFERENCIAS.ID_CATEGORIA=" & CInt(unaTablaIdCategoria.getItem(0, 0)) & " AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoActual & "' UNION SELECT (SELECT CODIGO FROM AUD_CATEGORIAS WHERE AUD_CATEGORIAS.ID=AUD_REFERENCIAS.ID_CATEGORIA),NRO_REFERENCIA,DESCRIPCION,(SELECT AUD_RELEVAMIENTOS.STOCK FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ") AS STOCK_ENVIADO,(SELECT AUD_RELEVAMIENTOS.ESTADO FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ")AS ESTADO_ENVIADO,(SELECT AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & unPeriodoAnterior & "' AND AUD_RELEVAMIENTOS.CE=" & unNumeroDeCE & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & unNumeroDeSucursal & ")AS FECHA_ENVIADA, '' AS STOCK, 'N' AS ESTADO, CONVERT(VARCHAR(8), GETDATE(), 112) AS FECHA FROM AUD_REFERENCIAS WHERE ID_CATEGORIA=" & CInt(unaTablaIdCategoria.getItem(0, 0)) & " AND ID NOT IN (SELECT ID_AUD_REFERENCIAS FROM AUD_RELEVAMIENTOS WHERE CE=" & unNumeroDeCE & " AND SUCURSAL=" & unNumeroDeSucursal & " AND PERIODO='" & unPeriodoActual & "' )SELECT CATEGORIA,NRO_REFERENCIA,DESCRIPCION,STOCK_ENVIADO,ESTADO_ENVIADO,FECHA_ENVIADA,STOCK,ESTADO,FECHA FROM #TEMP_REFERENCIAS WHERE FILA>" & (paginaActualMain - 1) * 10 & " AND FILA<=" & paginaActualMain * 10
                unaTablaTemporal.getDataSet(ultimoQuery)
                unasReferencias.dataSet = unaTablaTemporal.dataSet
            End If
            hideNextOrPrevious()
            formatGridView()
        End If
    End Sub

    Protected Sub TextBox1_TextChanged(sender As Object, e As EventArgs)
        Dim unaPosicion As Integer = 0
        For Each row As GridViewRow In GridViewData.Rows
            Dim radEstado As RadioButtonList = CType(row.FindControl("RadioButtonList1"), RadioButtonList)
            Dim txtStock As TextBox = CType(row.FindControl("TextBox1"), TextBox)
            Dim unStockTextBox As String = Trim(txtStock.Text)
            Dim unStockDataTable As String = Trim(unasReferencias.getItem(unaPosicion, 6))
            Dim unaFechaDataTable As String = Trim(unasReferencias.getItem(unaPosicion, 8))
            txtStock.Text = formatStock(unStockTextBox)
            'Se fija que sea distinto de lo que vino cargado y que adamás haya seleccionado un estado
            If formatStock((unStockTextBox)) <> Trim(unStockDataTable) Then
                If IsNumeric(txtStock.Text) = False Then
                    Response.Write("<script>alert('El stock debe ser un entero positivo.');</script>")
                    txtStock.Text = ""
                    agregoOedito = True
                    Exit Sub
                End If
                If CInt(txtStock.Text) < 0 Then
                    Response.Write("<script>alert('El stock debe ser un entero positivo.');</script>")
                    txtStock.Text = ""
                    agregoOedito = True
                    Exit Sub
                End If
                If InStr(txtStock.Text, ".", CompareMethod.Text) Then
                    Response.Write("<script>alert('El stock debe ser un entero positivo.');</script>")
                    txtStock.Text = ""
                    agregoOedito = True
                    Exit Sub
                End If
                Dim radOpciones As RadioButtonList = CType(row.FindControl("RadioButtonList1"), RadioButtonList)
                If radOpciones.SelectedValue = "N" Then
                    Response.Write("<script>alert('Debes seleccionar un estado(B/M/R).');</script>")
                    agregoOedito = True
                    Exit Sub
                End If
                Dim unaTablaIdReferencia As TablaSQL = New TablaSQL
                unaTablaIdReferencia.setConnectionString(unConnectionString)
                unaTablaIdReferencia.getDataSet("SELECT ID FROM AUD_REFERENCIAS WHERE NRO_REFERENCIA='" & unasReferencias.getItem(unaPosicion, 1) & "'")
                Dim unaTablaNuevaDeAuditoria As TablaSQL = New TablaSQL
                unaTablaNuevaDeAuditoria.setConnectionString(unConnectionString)
                unaTablaNuevaDeAuditoria.getDataSet("SELECT COUNT(*) FROM AUD_RELEVAMIENTOS WHERE CE=" & unNumeroDeCE & " AND SUCURSAL=" & unNumeroDeSucursal & " AND PERIODO='" & unPeriodoActual & "'" & " AND ID_AUD_REFERENCIAS=" & CInt(unaTablaIdReferencia.getItem(0, 0)))
                'Si no encuentra relevamientos en el período actual para la referencia indicada INSERTA sino UPDATE
                If CInt(unaTablaNuevaDeAuditoria.getItem(0, 0)) = 0 Then
                    unaTablaNuevaDeAuditoria.execQuery("INSERT INTO AUD_RELEVAMIENTOS VALUES(" & unNumeroDeCE & "," & unNumeroDeSucursal & ",'" & unPeriodoActual & "'," & unaFechaDataTable & "," & CInt(unaTablaIdReferencia.getItem(0, 0)) & ",'" & formatStock(unStockTextBox) & "','" & radEstado.SelectedValue & "')")
                Else
                    unaTablaNuevaDeAuditoria.execQuery("UPDATE AUD_RELEVAMIENTOS SET STOCK=" & formatStock(unStockTextBox) & ",ESTADO='" & radEstado.SelectedValue & "' WHERE CE=" & unNumeroDeCE & " AND SUCURSAL=" & unNumeroDeSucursal & " AND PERIODO='" & unPeriodoActual & "' AND ID_AUD_REFERENCIAS=" & CInt(unaTablaIdReferencia.getItem(0, 0)))
                End If
               ' unasReferencias.getDataSet(ultimoQuery)
                '  unasReferencias.fillGridView(GridViewData)
                '  formatGridView()
            End If
            unaPosicion += 1
        Next
        agregoOedito = True
    End Sub

    Protected Sub RadioButtonList1_SelectedIndexChanged(sender As Object, e As EventArgs)

        Dim unaPosicion As Integer = 0
        For Each row As GridViewRow In GridViewData.Rows
            Dim txtStock As TextBox = CType(row.FindControl("TextBox1"), TextBox)
            Dim radOpciones As RadioButtonList = CType(row.FindControl("RadioButtonList1"), RadioButtonList)
            Dim unValorTabla As String = Trim(unasReferencias.dataSet.Tables(0).Rows(unaPosicion).Item(6).ToString())
            If radOpciones.SelectedValue = "N" And txtStock.Text <> unValorTabla Then
                Response.Write("<script>alert('Debes ingresar el stock antes de ingresar el estado.');</script>")
                agregoOedito = True
                Exit Sub
            End If
            'End If
            unaPosicion += 1
        Next
    End Sub

    Private Sub auditoria_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        unasReferencias.getDataSet(ultimoQuery)
        unasReferencias.fillGridView(GridViewData)
        formatGridView()
    End Sub

    Protected Sub btnSalir_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnSalir.Click
        unNumeroDeCE = 0
        unNumeroDeSucursal = 0
        primerIngresoMain = True
        Response.Redirect("ingreso.aspx")
    End Sub

    Private Sub GridViewData_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewData.RowDataBound
        If lastCat = "F" Or lastCat = "G" Then
            e.Row.Cells(0).Visible = True
        Else
            e.Row.Cells(0).Visible = False
        End If
    End Sub

    Protected Sub TextBox2_TextChanged(sender As Object, e As EventArgs)
        Dim unaPosicion As Integer = 0
        For Each row As GridViewRow In GridViewData.Rows
            Dim txtCategoria As TextBox = CType(row.FindControl("TextBox2"), TextBox)
            Dim unaCategoriaTextBox As String = Trim(txtCategoria.Text)
            Dim unaCategoriaDataTable As String = Trim(unasReferencias.getItem(unaPosicion, 0))
            If Trim(unaCategoriaDataTable) <> Trim(unaCategoriaTextBox) Then
                Dim unaTablaTemporal As TablaSQL = New TablaSQL
                unaTablaTemporal.setConnectionString(unConnectionString)
                unaTablaTemporal.getDataSet("SELECT COUNT(*) FROM AUD_CATEGORIAS WHERE CODIGO='" & Trim(unaCategoriaTextBox) & "'")
                If unaTablaTemporal.getItem(0, 0) = "1" Then
                    unaTablaTemporal.getDataSet("SELECT ID FROM AUD_CATEGORIAS WHERE CODIGO='" & Trim(unaCategoriaTextBox) & "'")
                    unasReferencias.execQuery("UPDATE AUD_REFERENCIAS SET ID_CATEGORIA=" & CInt(unaTablaTemporal.getItem(0, 0)) & " WHERE NRO_REFERENCIA='" & unasReferencias.getItem(unaPosicion, 1) & "'")
                Else
                    Response.Write("<script>alert('La categoría ingresada es inválida.');</script>")
                End If
            End If
            unaPosicion += 1
        Next
        agregoOedito = True
    End Sub

End Class