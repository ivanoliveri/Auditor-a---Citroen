Imports Auditoria.TableClass
Imports Microsoft.VisualBasic
Public Class auditoria
    Inherits System.Web.UI.Page
    Protected Sub traerPrimerosRegistros(ByVal unCodigoCategoria As String)
        Dim unaTablaIdCategoria As TablaSQL = New TablaSQL()
        unaTablaIdCategoria.setConnectionString(unConnectionString)
        unaTablaIdCategoria.getDataSet("SELECT ID FROM AUD_CATEGORIAS WHERE CODIGO='" & unCodigoCategoria & "'")
        Dim unaTablaTemporal As TablaSQL = New TablaSQL()
        unaTablaTemporal.setConnectionString(unConnectionString)
        'CREO UNA TABLA TEMPORAL PARA OBTENER EL CORRESPONDIENTE NÚMERO DE FILAS DE CADA UNO, CUANDO LA TERMINO DE USAR SE BORRA SOLA
        Application("ultimoQuery") = "CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[CATEGORIA] [varchar](1) NULL,[NRO_REFERENCIA] [varchar](15) NULL,[DESCRIPCION] [varchar] (80) NULL,[STOCK_ENVIADO] [int] NULL,[ESTADO_ENVIADO] [varchar] (1) NULL,[FECHA_ENVIADA] [varchar] (15) NULL,[STOCK] [varchar] (10) NULL,[ESTADO] [varchar] (1) NULL,[FECHA] [varchar] (15) NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (CATEGORIA,NRO_REFERENCIA,DESCRIPCION,STOCK_ENVIADO,ESTADO_ENVIADO,FECHA_ENVIADA,STOCK,ESTADO,FECHA)SELECT * FROM(SELECT (SELECT CODIGO FROM AUD_CATEGORIAS WHERE AUD_CATEGORIAS.ID=AUD_REFERENCIAS.ID_CATEGORIA) AS CATEGORIA,AUD_REFERENCIAS.NRO_REFERENCIA,AUD_REFERENCIAS.DESCRIPCION,(SELECT AUD_RELEVAMIENTOS.STOCK FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & Application("unPeriodoAnterior") & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ") AS STOCK_ENVIADO, (SELECT AUD_RELEVAMIENTOS.ESTADO FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & Application("unPeriodoAnterior") & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ") AS ESTADO_ENVIADO,(SELECT AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & Application("unPeriodoAnterior") & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ") AS FECHA_ENVIADA,AUD_RELEVAMIENTOS.STOCK, AUD_RELEVAMIENTOS.ESTADO, AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS INNER JOIN AUD_REFERENCIAS ON AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID WHERE AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & " AND AUD_REFERENCIAS.ID_CATEGORIA=" & CInt(unaTablaIdCategoria.getItem(0, 0)) & " AND AUD_RELEVAMIENTOS.PERIODO='" & Application("unPeriodoActual") & "' UNION SELECT (SELECT CODIGO FROM AUD_CATEGORIAS WHERE AUD_CATEGORIAS.ID=AUD_REFERENCIAS.ID_CATEGORIA)AS CATEGORIA,NRO_REFERENCIA,DESCRIPCION,(SELECT AUD_RELEVAMIENTOS.STOCK FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND PERIODO='" & Application("unPeriodoAnterior") & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ") AS STOCK_ENVIADO,(SELECT AUD_RELEVAMIENTOS.ESTADO FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & Application("unPeriodoAnterior") & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ")AS ESTADO_ENVIADO,(SELECT AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & Application("unPeriodoAnterior") & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ")AS FECHA_ENVIADA, '' AS STOCK, 'N' AS ESTADO, '' AS FECHA FROM AUD_REFERENCIAS WHERE ID_CATEGORIA=" & CInt(unaTablaIdCategoria.getItem(0, 0)) & " AND ID NOT IN (SELECT ID_AUD_REFERENCIAS FROM AUD_RELEVAMIENTOS WHERE CE=" & Application("unNumeroDeCE") & " AND SUCURSAL=" & Application("unNumeroDeSucursal") & " AND PERIODO='" & Application("unPeriodoActual") & "' )) AS T ORDER BY LEN(T.NRO_REFERENCIA),T.NRO_REFERENCIA ASC SELECT CATEGORIA,NRO_REFERENCIA,DESCRIPCION,STOCK_ENVIADO,ESTADO_ENVIADO,FECHA_ENVIADA,STOCK,ESTADO,FECHA FROM #TEMP_REFERENCIAS WHERE FILA>0 AND FILA<=10 ORDER BY LEN(NRO_REFERENCIA),NRO_REFERENCIA ASC"
        unaTablaTemporal.getDataSet(Application("ultimoQuery"))
        unasReferencias.dataSet = unaTablaTemporal.dataSet
        Application("paginaActualMain") = 1
    End Sub
    Protected Sub setMouseOutCategoria()
        Dim todasLasCategorias As TablaSQL = New TablaSQL
        todasLasCategorias.setConnectionString(unConnectionString)
        todasLasCategorias.getDataSet("SELECT DESCRIPCION FROM AUD_CATEGORIAS WHERE CODIGO='" & Application("lastCat") & "'")
        btnA.Attributes.Add("onmouseout", "javascript:llenarLabel('CATEGORIA: " & todasLasCategorias.getItem(0, 0) & "')")
        btnB.Attributes.Add("onmouseout", "javascript:llenarLabel('CATEGORIA: " & todasLasCategorias.getItem(0, 0) & "')")
        btnC.Attributes.Add("onmouseout", "javascript:llenarLabel('CATEGORIA: " & todasLasCategorias.getItem(0, 0) & "')")
        btnD.Attributes.Add("onmouseout", "javascript:llenarLabel('CATEGORIA: " & todasLasCategorias.getItem(0, 0) & "')")
        btnE.Attributes.Add("onmouseout", "javascript:llenarLabel('CATEGORIA: " & todasLasCategorias.getItem(0, 0) & "')")
        btnF.Attributes.Add("onmouseout", "javascript:llenarLabel('CATEGORIA: " & todasLasCategorias.getItem(0, 0) & "')")
        btnG.Attributes.Add("onmouseout", "javascript:llenarLabel('CATEGORIA: " & todasLasCategorias.getItem(0, 0) & "')")
    End Sub
    Protected Sub calcularPaginas(ByVal unCodigoCategoria As String)
        Dim unaTablaTemporal As TablaSQL = New TablaSQL()
        unaTablaTemporal.setConnectionString(unConnectionString)
        Dim unaTablaIdCategoria As TablaSQL = New TablaSQL()
        unaTablaIdCategoria.setConnectionString(unConnectionString)
        unaTablaIdCategoria.getDataSet("SELECT ID FROM AUD_CATEGORIAS WHERE CODIGO='" & unCodigoCategoria & "'")
        unaTablaTemporal.getDataSet("CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[NRO_REFERENCIA] [varchar](15) NULL,[DESCRIPCION] [varchar] (80) NULL,[STOCK_ENVIADO] [int] NULL,[ESTADO_ENVIADO] [varchar] (1) NULL,[FECHA_ENVIADA] [varchar] (15) NULL,[STOCK] [varchar] (10) NULL,[ESTADO] [varchar] (1) NULL,[FECHA] [varchar] (15) NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (NRO_REFERENCIA,DESCRIPCION,STOCK_ENVIADO,ESTADO_ENVIADO,FECHA_ENVIADA,STOCK,ESTADO,FECHA)SELECT AUD_REFERENCIAS.NRO_REFERENCIA,AUD_REFERENCIAS.DESCRIPCION,(SELECT AUD_RELEVAMIENTOS.STOCK FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & Application("unPeriodoAnterior") & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ") AS STOCK_ENVIADO, (SELECT AUD_RELEVAMIENTOS.ESTADO FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & Application("unPeriodoAnterior") & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ") AS ESTADO_ENVIADO,(SELECT AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & Application("unPeriodoAnterior") & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ") AS FECHA_ENVIADA,AUD_RELEVAMIENTOS.STOCK, AUD_RELEVAMIENTOS.ESTADO, AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS INNER JOIN AUD_REFERENCIAS ON AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID WHERE AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & " AND AUD_REFERENCIAS.ID_CATEGORIA=" & CInt(unaTablaIdCategoria.getItem(0, 0)) & " AND AUD_RELEVAMIENTOS.PERIODO='" & Application("unPeriodoActual") & "' UNION SELECT NRO_REFERENCIA,DESCRIPCION,(SELECT AUD_RELEVAMIENTOS.STOCK FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND PERIODO='" & Application("unPeriodoAnterior") & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ") AS STOCK_ENVIADO,(SELECT AUD_RELEVAMIENTOS.ESTADO FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & Application("unPeriodoAnterior") & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ")AS ESTADO_ENVIADO,(SELECT AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & Application("unPeriodoAnterior") & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ")AS FECHA_ENVIADA, '' AS STOCK, 'N' AS ESTADO, '' AS FECHA FROM AUD_REFERENCIAS WHERE ID_CATEGORIA=" & CInt(unaTablaIdCategoria.getItem(0, 0)) & " AND ID NOT IN (SELECT ID_AUD_REFERENCIAS FROM AUD_RELEVAMIENTOS WHERE CE=" & Application("unNumeroDeCE") & " AND SUCURSAL=" & Application("unNumeroDeSucursal") & " AND PERIODO='" & Application("unPeriodoActual") & "' )SELECT COUNT(*) FROM #TEMP_REFERENCIAS")
        If CInt(unaTablaTemporal.getItem(0, 0)) Mod 10 = 0 Then
            Application("totalPaginasMain") = CInt(unaTablaTemporal.getItem(0, 0)) / 10
        Else
            Application("totalPaginasMain") = CInt(unaTablaTemporal.getItem(0, 0)) \ 10 + 1
        End If
    End Sub

    Protected Sub cargarCategoria()
        GridViewData.SelectedIndex = -1
        Application("paginaActualMain") = 1
        calcularPaginas(Application("lastCat"))
        traerPrimerosRegistros(Application("lastCat"))
        hideNextOrPrevious()
    End Sub

    Protected Sub formatDate(ByVal unaFecha As String, ByRef unaNuevaFecha As String)
        'Si el Label de FECHA_ENV está vacío no le tiene que dar formato a la fecha. Esto es por si es un período que NO fue relevado
        If Trim(unaFecha) = "" Then
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
        If Application("paginaActualMain") = 1 Then
            btnPrevious.Visible = False
        Else
            btnPrevious.Visible = True
        End If
        If Application("paginaActualMain") = Application("totalPaginasMain") Then
            btnNext.Visible = False
        Else
            btnNext.Visible = True
        End If
    End Sub

    Protected Sub setImageButton()
        btnA.ImageUrl = "~/images/buttons/btnA.png"
        btnB.ImageUrl = "~/images/buttons/btnB.png"
        btnC.ImageUrl = "~/images/buttons/btnC.png"
        btnD.ImageUrl = "~/images/buttons/btnD.png"
        btnE.ImageUrl = "~/images/buttons/btnE.png"
        btnF.ImageUrl = "~/images/buttons/btnF.png"
        btnG.ImageUrl = "~/images/buttons/btnG.png"
        Select Case Application("lastCat")
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
            Dim lblFecha As Label = CType(row.FindControl("Label7"), Label)
            Dim lblFechaEnviada As Label = CType(row.FindControl("Label9"), Label)
            Dim unaFechaVieja As String = lblFecha.Text
            Dim unaFecha As String
            Dim lblDescripcion As Label = CType(row.FindControl("Label2"), Label)
            Dim lblEstadoEnviado As Label = CType(row.FindControl("Label5"), Label)
            Dim unString As String = Trim(lblDescripcion.Text)
            Dim txtStock As TextBox = CType(row.FindControl("TextBox1"), TextBox)
            txtStock.MaxLength = 4
            If Trim(txtStock.Text) = "" Then txtStock.Text = ""
            Dim txtCategoria As TextBox = CType(row.FindControl("TextBox2"), TextBox)
            txtCategoria.MaxLength = 1
            Dim unNum As Integer = Len(unString)
            If lblEstadoEnviado.Text = "N" Then
                lblEstadoEnviado.Text = ""
            End If
            If Len(unString) >= 29 Then
                lblDescripcion.Text = Left(unString, 30) + ".."
            End If
            formatDate(unaFechaVieja, unaFecha)
            lblFecha.Text = unaFecha
            unaFechaVieja = lblFechaEnviada.Text
            formatDate(unaFechaVieja, unaFecha)
            lblFechaEnviada.Text = unaFecha
        Next
    End Sub
    'Sirve para eliminar los ceros antes del número. Input:00502; Output:502
    Function formatStock(ByRef unString As String)
        If unString = "" Or Trim(unString) = "0" Then Return Trim(unString)
        Dim unContador As Integer = 1
        Dim unaLetra As String = GetChar(unString, unContador)
        Do While unContador < unString.Length() And unaLetra = "0"
            unContador += 1
            If unContador <= unString.Length() Then unaLetra = GetChar(unString, unContador)
        Loop
        Return Mid(unString, unContador)
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Trim(txtCE.Text) <> "" Then
            Application("unNumeroDeSucursal") = CInt(Mid(txtSucursal.Text, 10))
            Application("unNumeroDeCE") = CInt(Mid(txtCE.Text, 4))
        End If
        If Application("agregoOEdito") = True Then
            Application("agregoOEdito") = False
            Exit Sub
        End If
        'Application("lastError") = ""
        Application("agregoOEdito") = False
        txtCE.Attributes.CssStyle.Add("TEXT-ALIGN", "right")
        txtSucursal.Attributes.CssStyle.Add("TEXT-ALIGN", "right")
        txtPeriodo.Attributes.CssStyle.Add("TEXT-ALIGN", "right")
        txtUsuario.Attributes.CssStyle.Add("TEXT-ALIGN", "right")
        txtCE.Text = "CE: " & Application("unNumeroDeCE")
        If Application("unNumeroDeSucursal") = 0 Then
            txtSucursal.Text = "Sucursal: 000"
        Else
            txtSucursal.Text = "Sucursal: " & Application("unNumeroDeSucursal")
        End If
        txtPeriodo.Text = "Período: " & Application("unPeriodoActual")
        txtUsuario.Text = "Usuario: " & Application("nombreUsuario")
        unasReferencias.setConnectionString(unConnectionString)
        Dim todasLasCategorias As TablaSQL = New TablaSQL
        todasLasCategorias.setConnectionString(unConnectionString)
        todasLasCategorias.setTableName("AUD_CATEGORIAS")
        todasLasCategorias.getAllDataSet()
        'El ViewGrid viene por defecto en categoría A
        If Application("ultimoQuery") = "" Then
            txtCategoria.Text = Trim(todasLasCategorias.getItem(0, 2))
            Application("lastCat") = "A"
            setImageButton()
            calcularPaginas(Application("lastCat"))
            traerPrimerosRegistros(Application("lastCat"))
        End If
        If Application("busquedaMode") = True Then
            Application("lastCat") = Application("codCategoriaToSearch")
            setImageButton()
            Dim unaTablaTemporal As TablaSQL = New TablaSQL
            unaTablaTemporal.setConnectionString(unconnectionstring)
            Dim unNumeroDeFila As Integer
            If Application("idCategoriaToSearch") <> 7 Then
                unaTablaTemporal.getDataSet("CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1)  NOT NULL,[NRO_REFERENCIA] [varchar] (15) NOT NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (NRO_REFERENCIA) SELECT NRO_REFERENCIA FROM AUD_REFERENCIAS WHERE ID_CATEGORIA=" & Application("idCategoriaToSearch") & " ORDER BY LEN(NRO_REFERENCIA),NRO_REFERENCIA ASC SELECT FILA FROM #TEMP_REFERENCIAS WHERE NRO_REFERENCIA='" & Application("nroReferenciaToSearch") & "'")
                unNumeroDeFila = CInt(unaTablaTemporal.getItem(0, 0))
            Else
                unaTablaTemporal.getDataSet("CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[CATEGORIA] [varchar](1) NULL,[NRO_REFERENCIA] [varchar](15) NULL,[DESCRIPCION] [varchar] (80) NULL,[STOCK_ENVIADO] [int] NULL,[ESTADO_ENVIADO] [varchar] (1) NULL,[FECHA_ENVIADA] [varchar] (15) NULL,[STOCK] [varchar] (10) NULL,[ESTADO] [varchar] (1) NULL,[FECHA] [varchar] (15) NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (CATEGORIA,NRO_REFERENCIA,DESCRIPCION,STOCK_ENVIADO,ESTADO_ENVIADO,FECHA_ENVIADA,STOCK,ESTADO,FECHA)SELECT * FROM(SELECT (SELECT CODIGO FROM AUD_CATEGORIAS WHERE AUD_CATEGORIAS.ID=AUD_REFERENCIAS.ID_CATEGORIA) AS CATEGORIA,AUD_REFERENCIAS.NRO_REFERENCIA,AUD_REFERENCIAS.DESCRIPCION,(SELECT AUD_RELEVAMIENTOS.STOCK FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & Application("unPeriodoAnterior") & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ") AS STOCK_ENVIADO, (SELECT AUD_RELEVAMIENTOS.ESTADO FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & Application("unPeriodoAnterior") & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ") AS ESTADO_ENVIADO,(SELECT AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & Application("unPeriodoAnterior") & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ") AS FECHA_ENVIADA,AUD_RELEVAMIENTOS.STOCK, AUD_RELEVAMIENTOS.ESTADO, AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS INNER JOIN AUD_REFERENCIAS ON AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID WHERE AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & " AND AUD_REFERENCIAS.ID_CATEGORIA=" & Application("idCategoriaToSearch") & " AND AUD_RELEVAMIENTOS.PERIODO='" & Application("unPeriodoActual") & "' UNION SELECT (SELECT CODIGO FROM AUD_CATEGORIAS WHERE AUD_CATEGORIAS.ID=AUD_REFERENCIAS.ID_CATEGORIA)AS CATEGORIA,NRO_REFERENCIA,DESCRIPCION,(SELECT AUD_RELEVAMIENTOS.STOCK FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND PERIODO='" & Application("unPeriodoAnterior") & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ") AS STOCK_ENVIADO,(SELECT AUD_RELEVAMIENTOS.ESTADO FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & Application("unPeriodoAnterior") & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ")AS ESTADO_ENVIADO,(SELECT AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & Application("unPeriodoAnterior") & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ")AS FECHA_ENVIADA, '' AS STOCK, 'N' AS ESTADO, '' AS FECHA FROM AUD_REFERENCIAS WHERE ID_CATEGORIA=" & Application("idCategoriaToSearch") & " AND ID NOT IN (SELECT ID_AUD_REFERENCIAS FROM AUD_RELEVAMIENTOS WHERE CE=" & Application("unNumeroDeCE") & " AND SUCURSAL=" & Application("unNumeroDeSucursal") & " AND PERIODO='" & Application("unPeriodoActual") & "' )) AS T ORDER BY LEN(T.NRO_REFERENCIA),T.NRO_REFERENCIA ASC SELECT FILA FROM #TEMP_REFERENCIAS WHERE DESCRIPCION='" & Application("descripcionToSearch") & "'")
                unNumeroDeFila = CInt(unaTablaTemporal.getItem(0, 0))
            End If
            Dim maxFila As Integer
            If unNumeroDeFila Mod 10 <> 0 Then
                Application("totalPaginasMain") = CInt(unaTablaTemporal.getItem(0, 0)) \ 10 + 1
                maxFila = unNumeroDeFila
                Do While maxFila Mod 10 <> 0
                    maxFila += 1
                Loop
            Else
                maxFila = unNumeroDeFila
            End If
            GridViewData.SelectedIndex = (unNumeroDeFila - (maxFila - 10)) - 1
            Application("paginaActualMain") = maxFila / 10
            calcularPaginas(Application("codCategoriaToSearch"))
            Application("ultimoQuery") = "CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[CATEGORIA] [varchar](1) NULL,[NRO_REFERENCIA] [varchar](15) NULL,[DESCRIPCION] [varchar] (80) NULL,[STOCK_ENVIADO] [int] NULL,[ESTADO_ENVIADO] [varchar] (1) NULL,[FECHA_ENVIADA] [varchar] (15) NULL,[STOCK] [varchar] (10) NULL,[ESTADO] [varchar] (1) NULL,[FECHA] [varchar] (15) NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (CATEGORIA,NRO_REFERENCIA,DESCRIPCION,STOCK_ENVIADO,ESTADO_ENVIADO,FECHA_ENVIADA,STOCK,ESTADO,FECHA)SELECT * FROM(SELECT (SELECT CODIGO FROM AUD_CATEGORIAS WHERE AUD_CATEGORIAS.ID=AUD_REFERENCIAS.ID_CATEGORIA) AS CATEGORIA,AUD_REFERENCIAS.NRO_REFERENCIA,AUD_REFERENCIAS.DESCRIPCION,(SELECT AUD_RELEVAMIENTOS.STOCK FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & Application("unPeriodoAnterior") & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ") AS STOCK_ENVIADO, (SELECT AUD_RELEVAMIENTOS.ESTADO FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & Application("unPeriodoAnterior") & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ") AS ESTADO_ENVIADO,(SELECT AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & Application("unPeriodoAnterior") & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ") AS FECHA_ENVIADA,AUD_RELEVAMIENTOS.STOCK, AUD_RELEVAMIENTOS.ESTADO, AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS INNER JOIN AUD_REFERENCIAS ON AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID WHERE AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & " AND AUD_REFERENCIAS.ID_CATEGORIA=" & Application("idCategoriaToSearch") & " AND AUD_RELEVAMIENTOS.PERIODO='" & Application("unPeriodoActual") & "' UNION SELECT (SELECT CODIGO FROM AUD_CATEGORIAS WHERE AUD_CATEGORIAS.ID=AUD_REFERENCIAS.ID_CATEGORIA)AS CATEGORIA,NRO_REFERENCIA,DESCRIPCION,(SELECT AUD_RELEVAMIENTOS.STOCK FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND PERIODO='" & Application("unPeriodoAnterior") & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ") AS STOCK_ENVIADO,(SELECT AUD_RELEVAMIENTOS.ESTADO FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & Application("unPeriodoAnterior") & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ")AS ESTADO_ENVIADO,(SELECT AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & Application("unPeriodoAnterior") & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ")AS FECHA_ENVIADA, '' AS STOCK, 'N' AS ESTADO, '' AS FECHA FROM AUD_REFERENCIAS WHERE ID_CATEGORIA=" & Application("idCategoriaToSearch") & " AND ID NOT IN (SELECT ID_AUD_REFERENCIAS FROM AUD_RELEVAMIENTOS WHERE CE=" & Application("unNumeroDeCE") & " AND SUCURSAL=" & Application("unNumeroDeSucursal") & " AND PERIODO='" & Application("unPeriodoActual") & "' )) AS T ORDER BY LEN(T.NRO_REFERENCIA),T.NRO_REFERENCIA ASC SELECT CATEGORIA,NRO_REFERENCIA,DESCRIPCION,STOCK_ENVIADO,ESTADO_ENVIADO,FECHA_ENVIADA,STOCK,ESTADO,FECHA FROM #TEMP_REFERENCIAS WHERE FILA>" & maxFila - 10 & " AND FILA<=" & maxFila & " ORDER BY LEN(NRO_REFERENCIA),NRO_REFERENCIA ASC"
            unasreferencias.getDataSet(Application("ultimoQuery"))
            hideNextOrPrevious()
            formatGridView()
            Application("busquedaMode") = False
        End If
        'Agrego Atributo onClick al Imprimir y Buscar
        btnSearch.Attributes.Add("onclick", "javascript:mostrarPopupBuscar('" & "busqueda.aspx" & "')")
        btnImprimir.Attributes.Add("onclick", "javascript:mostrarPopupImprimir('" & "imprimir.aspx" & "')")
        btnAgregar.Attributes.Add("onclick", "javascript:mostrarPopupAgregar('" & "agregar.aspx" & "')")
        'Agrego Atributo onMouseMove a los botones de Categoría
        btnA.Attributes.Add("onmouseover", "javascript:llenarLabel('CATEGORIA: " & todasLasCategorias.getItem(0, 2) & "')")
        btnB.Attributes.Add("onmouseover", "javascript:llenarLabel('CATEGORIA: " & todasLasCategorias.getItem(1, 2) & "')")
        btnC.Attributes.Add("onmouseover", "javascript:llenarLabel('CATEGORIA: " & todasLasCategorias.getItem(2, 2) & "')")
        btnD.Attributes.Add("onmouseover", "javascript:llenarLabel('CATEGORIA: " & todasLasCategorias.getItem(3, 2) & "')")
        btnE.Attributes.Add("onmouseover", "javascript:llenarLabel('CATEGORIA: " & todasLasCategorias.getItem(4, 2) & "')")
        btnF.Attributes.Add("onmouseover", "javascript:llenarLabel('CATEGORIA: " & todasLasCategorias.getItem(5, 2) & "')")
        btnG.Attributes.Add("onmouseover", "javascript:llenarLabel('CATEGORIA: " & todasLasCategorias.getItem(6, 2) & "')")
        hideNextOrPrevious()
    End Sub

    Protected Sub btnA_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnA.Click
        If Application("agregoOEdito") = True Then Exit Sub
        Application("lastCat") = "A"
        setImageButton()
        Dim unaTablaAuditoria As TablaSQL = New TablaSQL
        unaTablaAuditoria.setConnectionString(unConnectionString)
        unaTablaAuditoria.getDataSet("SELECT DESCRIPCION FROM AUD_CATEGORIAS WHERE CODIGO='" & Application("lastCat") & "'")
        txtCategoria.Text = "CATEGORIA: " & unaTablaAuditoria.getItem(0, 0)
        cargarCategoria()
        formatGridView()
    End Sub

    Protected Sub btnB_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnB.Click
        Application("lastCat") = "B"
        setImageButton()
        Dim unaTablaAuditoria As TablaSQL = New TablaSQL
        unaTablaAuditoria.setConnectionString(unConnectionString)
        unaTablaAuditoria.getDataSet("SELECT DESCRIPCION FROM AUD_CATEGORIAS WHERE CODIGO='" & Application("lastCat") & "'")
        txtCategoria.Text = "CATEGORIA: " & unaTablaAuditoria.getItem(0, 0)
        cargarCategoria()
        formatGridView()
    End Sub

    Protected Sub btnC_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnC.Click
        Application("lastCat") = "C"
        setImageButton()
        Dim unaTablaAuditoria As TablaSQL = New TablaSQL
        unaTablaAuditoria.setConnectionString(unConnectionString)
        unaTablaAuditoria.getDataSet("SELECT DESCRIPCION FROM AUD_CATEGORIAS WHERE CODIGO='" & Application("lastCat") & "'")
        txtCategoria.Text = "CATEGORIA: " & unaTablaAuditoria.getItem(0, 0)
        cargarCategoria()
        formatGridView()
    End Sub

    Protected Sub btnD_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnD.Click
        Application("lastCat") = "D"
        setImageButton()
        Dim unaTablaAuditoria As TablaSQL = New TablaSQL
        unaTablaAuditoria.setConnectionString(unConnectionString)
        unaTablaAuditoria.getDataSet("SELECT DESCRIPCION FROM AUD_CATEGORIAS WHERE CODIGO='" & Application("lastCat") & "'")
        txtCategoria.Text = "CATEGORIA: " & unaTablaAuditoria.getItem(0, 0)
        cargarCategoria()
        formatGridView()
    End Sub

    Protected Sub btnE_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnE.Click
        Application("lastCat") = "E"
        setImageButton()
        Dim unaTablaAuditoria As TablaSQL = New TablaSQL
        unaTablaAuditoria.setConnectionString(unConnectionString)
        unaTablaAuditoria.getDataSet("SELECT DESCRIPCION FROM AUD_CATEGORIAS WHERE CODIGO='" & Application("lastCat") & "'")
        txtCategoria.Text = "CATEGORIA: " & unaTablaAuditoria.getItem(0, 0)
        cargarCategoria()
        formatGridView()
    End Sub

    Protected Sub btnF_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnF.Click
        Application("lastCat") = "F"
        setImageButton()
        Dim unaTablaAuditoria As TablaSQL = New TablaSQL
        unaTablaAuditoria.setConnectionString(unConnectionString)
        unaTablaAuditoria.getDataSet("SELECT DESCRIPCION FROM AUD_CATEGORIAS WHERE CODIGO='" & Application("lastCat") & "'")
        txtCategoria.Text = "CATEGORIA: " & unaTablaAuditoria.getItem(0, 0)
        cargarCategoria()
        formatGridView()
    End Sub

    Protected Sub btnG_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnG.Click
        Application("lastCat") = "G"
        setImageButton()
        Dim unaTablaAuditoria As TablaSQL = New TablaSQL
        unaTablaAuditoria.setConnectionString(unConnectionString)
        unaTablaAuditoria.getDataSet("SELECT DESCRIPCION FROM AUD_CATEGORIAS WHERE CODIGO='" & Application("lastCat") & "'")
        txtCategoria.Text = "CATEGORIA: " & unaTablaAuditoria.getItem(0, 0)
        cargarCategoria()
        formatGridView()
    End Sub

    Protected Sub btnNext_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnNext.Click
        If Application("paginaActualMain") < Application("totalPaginasMain") Then
            GridViewData.SelectedIndex = -1
            Application("paginaActualMain") += 1
            Dim unaTablaIdCategoria As TablaSQL = New TablaSQL()
            unaTablaIdCategoria.setConnectionString(unConnectionString)
            unaTablaIdCategoria.getDataSet("SELECT ID FROM AUD_CATEGORIAS WHERE CODIGO='" & Application("lastCat") & "'")
            Dim unaTablaTemporal As TablaSQL = New TablaSQL()
            unaTablaTemporal.setConnectionString(unConnectionString)
            'CREO UNA TABLA TEMPORAL PARA OBTENER EL CORRESPONDIENTE NÚMERO DE FILAS DE CADA UNO, CUANDO LA TERMINO DE USAR SE BORRA SOLA
            'Application("ultimoQuery") = "CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[CATEGORIA] [varchar](1) NULL,[NRO_REFERENCIA] [varchar](15) NULL,[DESCRIPCION] [varchar] (80) NULL,[STOCK_ENVIADO] [int] NULL,[ESTADO_ENVIADO] [varchar] (1) NULL,[FECHA_ENVIADA] [varchar] (15) NULL,[STOCK] [varchar] (10) NULL,[ESTADO] [varchar] (1) NULL,[FECHA] [varchar] (15) NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (CATEGORIA,NRO_REFERENCIA,DESCRIPCION,STOCK_ENVIADO,ESTADO_ENVIADO,FECHA_ENVIADA,STOCK,ESTADO,FECHA)SELECT (SELECT CODIGO FROM AUD_CATEGORIAS WHERE AUD_CATEGORIAS.ID=AUD_REFERENCIAS.ID_CATEGORIA),AUD_REFERENCIAS.NRO_REFERENCIA,AUD_REFERENCIAS.DESCRIPCION,(SELECT AUD_RELEVAMIENTOS.STOCK FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & Application("unPeriodoAnterior") & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ") AS STOCK_ENVIADO, (SELECT AUD_RELEVAMIENTOS.ESTADO FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & Application("unPeriodoAnterior") & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ") AS ESTADO_ENVIADO,(SELECT AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & Application("unPeriodoAnterior") & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ") AS FECHA_ENVIADA,AUD_RELEVAMIENTOS.STOCK, AUD_RELEVAMIENTOS.ESTADO, AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS INNER JOIN AUD_REFERENCIAS ON AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID WHERE AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & " AND AUD_REFERENCIAS.ID_CATEGORIA=" & CInt(unaTablaIdCategoria.getItem(0, 0)) & " AND AUD_RELEVAMIENTOS.PERIODO='" & Application("unPeriodoActual") & "' UNION SELECT (SELECT CODIGO FROM AUD_CATEGORIAS WHERE AUD_CATEGORIAS.ID=AUD_REFERENCIAS.ID_CATEGORIA),NRO_REFERENCIA,DESCRIPCION,(SELECT AUD_RELEVAMIENTOS.STOCK FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND PERIODO='" & Application("unPeriodoAnterior") & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ") AS STOCK_ENVIADO,(SELECT AUD_RELEVAMIENTOS.ESTADO FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & Application("unPeriodoAnterior") & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ")AS ESTADO_ENVIADO,(SELECT AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & Application("unPeriodoAnterior") & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ")AS FECHA_ENVIADA, '' AS STOCK, 'N' AS ESTADO, '' AS FECHA FROM AUD_REFERENCIAS WHERE ID_CATEGORIA=" & CInt(unaTablaIdCategoria.getItem(0, 0)) & " AND ID NOT IN (SELECT ID_AUD_REFERENCIAS FROM AUD_RELEVAMIENTOS WHERE CE=" & Application("unNumeroDeCE") & " AND SUCURSAL=" & Application("unNumeroDeSucursal") & " AND PERIODO='" & Application("unPeriodoActual") & "' )SELECT CATEGORIA,NRO_REFERENCIA,DESCRIPCION,STOCK_ENVIADO,ESTADO_ENVIADO,FECHA_ENVIADA,STOCK,ESTADO,FECHA FROM #TEMP_REFERENCIAS WHERE FILA>" & (Application("paginaActualMain") - 1) * 10 & " AND FILA<=" & Application("paginaActualMain") * 10 & " ORDER BY LEN(NRO_REFERENCIA),NRO_REFERENCIA ASC"
            Application("ultimoQuery") = "CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[CATEGORIA] [varchar](1) NULL,[NRO_REFERENCIA] [varchar](15) NULL,[DESCRIPCION] [varchar] (80) NULL,[STOCK_ENVIADO] [int] NULL,[ESTADO_ENVIADO] [varchar] (1) NULL,[FECHA_ENVIADA] [varchar] (15) NULL,[STOCK] [varchar] (10) NULL,[ESTADO] [varchar] (1) NULL,[FECHA] [varchar] (15) NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (CATEGORIA,NRO_REFERENCIA,DESCRIPCION,STOCK_ENVIADO,ESTADO_ENVIADO,FECHA_ENVIADA,STOCK,ESTADO,FECHA)SELECT * FROM(SELECT (SELECT CODIGO FROM AUD_CATEGORIAS WHERE AUD_CATEGORIAS.ID=AUD_REFERENCIAS.ID_CATEGORIA) AS CATEGORIA,AUD_REFERENCIAS.NRO_REFERENCIA,AUD_REFERENCIAS.DESCRIPCION,(SELECT AUD_RELEVAMIENTOS.STOCK FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & Application("unPeriodoAnterior") & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ") AS STOCK_ENVIADO, (SELECT AUD_RELEVAMIENTOS.ESTADO FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & Application("unPeriodoAnterior") & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ") AS ESTADO_ENVIADO,(SELECT AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & Application("unPeriodoAnterior") & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ") AS FECHA_ENVIADA,AUD_RELEVAMIENTOS.STOCK, AUD_RELEVAMIENTOS.ESTADO, AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS INNER JOIN AUD_REFERENCIAS ON AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID WHERE AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & " AND AUD_REFERENCIAS.ID_CATEGORIA=" & CInt(unaTablaIdCategoria.getItem(0, 0)) & " AND AUD_RELEVAMIENTOS.PERIODO='" & Application("unPeriodoActual") & "' UNION SELECT (SELECT CODIGO FROM AUD_CATEGORIAS WHERE AUD_CATEGORIAS.ID=AUD_REFERENCIAS.ID_CATEGORIA)AS CATEGORIA,NRO_REFERENCIA,DESCRIPCION,(SELECT AUD_RELEVAMIENTOS.STOCK FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND PERIODO='" & Application("unPeriodoAnterior") & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ") AS STOCK_ENVIADO,(SELECT AUD_RELEVAMIENTOS.ESTADO FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & Application("unPeriodoAnterior") & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ")AS ESTADO_ENVIADO,(SELECT AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & Application("unPeriodoAnterior") & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ")AS FECHA_ENVIADA, '' AS STOCK, 'N' AS ESTADO, '' AS FECHA FROM AUD_REFERENCIAS WHERE ID_CATEGORIA=" & CInt(unaTablaIdCategoria.getItem(0, 0)) & " AND ID NOT IN (SELECT ID_AUD_REFERENCIAS FROM AUD_RELEVAMIENTOS WHERE CE=" & Application("unNumeroDeCE") & " AND SUCURSAL=" & Application("unNumeroDeSucursal") & " AND PERIODO='" & Application("unPeriodoActual") & "' )) AS T ORDER BY LEN(T.NRO_REFERENCIA),T.NRO_REFERENCIA ASC SELECT CATEGORIA,NRO_REFERENCIA,DESCRIPCION,STOCK_ENVIADO,ESTADO_ENVIADO,FECHA_ENVIADA,STOCK,ESTADO,FECHA FROM #TEMP_REFERENCIAS WHERE FILA>" & (Application("paginaActualMain") - 1) * 10 & " AND FILA<=" & Application("paginaActualMain") * 10 & " ORDER BY LEN(NRO_REFERENCIA),NRO_REFERENCIA ASC"
            unaTablaTemporal.getDataSet(Application("ultimoQuery"))
            unasReferencias.dataSet = unaTablaTemporal.dataSet
            hideNextOrPrevious()
            formatGridView()
        End If
    End Sub

    Protected Sub btnPrevious_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnPrevious.Click
        If Application("paginaActualMain") <> 1 Then
            GridViewData.SelectedIndex = -1
            Application("paginaActualMain") -= 1
            If Application("paginaActualMain") = 1 Then
                traerPrimerosRegistros(Application("lastCat"))
            Else
                Dim unaTablaIdCategoria As TablaSQL = New TablaSQL()
                unaTablaIdCategoria.setConnectionString(unConnectionString)
                unaTablaIdCategoria.getDataSet("SELECT ID FROM AUD_CATEGORIAS WHERE CODIGO='" & Application("lastCat") & "'")
                'CREO UNA TABLA TEMPORAL PARA OBTENER EL CORRESPONDIENTE NÚMERO DE FILAS DE CADA UNO, CUANDO LA TERMINO DE USAR SE BORRA
                Dim unaTablaTemporal As TablaSQL = New TablaSQL()
                unaTablaTemporal.setConnectionString(unConnectionString)
                Application("ultimoQuery") = "CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[CATEGORIA] [varchar](1) NULL,[NRO_REFERENCIA] [varchar](15) NULL,[DESCRIPCION] [varchar] (80) NULL,[STOCK_ENVIADO] [int] NULL,[ESTADO_ENVIADO] [varchar] (1) NULL,[FECHA_ENVIADA] [varchar] (15) NULL,[STOCK] [varchar] (10) NULL,[ESTADO] [varchar] (1) NULL,[FECHA] [varchar] (15) NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (CATEGORIA,NRO_REFERENCIA,DESCRIPCION,STOCK_ENVIADO,ESTADO_ENVIADO,FECHA_ENVIADA,STOCK,ESTADO,FECHA)SELECT * FROM(SELECT (SELECT CODIGO FROM AUD_CATEGORIAS WHERE AUD_CATEGORIAS.ID=AUD_REFERENCIAS.ID_CATEGORIA) AS CATEGORIA,AUD_REFERENCIAS.NRO_REFERENCIA,AUD_REFERENCIAS.DESCRIPCION,(SELECT AUD_RELEVAMIENTOS.STOCK FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & Application("unPeriodoAnterior") & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ") AS STOCK_ENVIADO, (SELECT AUD_RELEVAMIENTOS.ESTADO FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & Application("unPeriodoAnterior") & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ") AS ESTADO_ENVIADO,(SELECT AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & Application("unPeriodoAnterior") & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ") AS FECHA_ENVIADA,AUD_RELEVAMIENTOS.STOCK, AUD_RELEVAMIENTOS.ESTADO, AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS INNER JOIN AUD_REFERENCIAS ON AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID WHERE AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & " AND AUD_REFERENCIAS.ID_CATEGORIA=" & CInt(unaTablaIdCategoria.getItem(0, 0)) & " AND AUD_RELEVAMIENTOS.PERIODO='" & Application("unPeriodoActual") & "' UNION SELECT (SELECT CODIGO FROM AUD_CATEGORIAS WHERE AUD_CATEGORIAS.ID=AUD_REFERENCIAS.ID_CATEGORIA)AS CATEGORIA,NRO_REFERENCIA,DESCRIPCION,(SELECT AUD_RELEVAMIENTOS.STOCK FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND PERIODO='" & Application("unPeriodoAnterior") & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ") AS STOCK_ENVIADO,(SELECT AUD_RELEVAMIENTOS.ESTADO FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & Application("unPeriodoAnterior") & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ")AS ESTADO_ENVIADO,(SELECT AUD_RELEVAMIENTOS.FECHA FROM AUD_RELEVAMIENTOS WHERE AUD_RELEVAMIENTOS.ID_AUD_REFERENCIAS=AUD_REFERENCIAS.ID AND AUD_RELEVAMIENTOS.PERIODO='" & Application("unPeriodoAnterior") & "' AND AUD_RELEVAMIENTOS.CE=" & Application("unNumeroDeCE") & " AND AUD_RELEVAMIENTOS.SUCURSAL=" & Application("unNumeroDeSucursal") & ")AS FECHA_ENVIADA, '' AS STOCK, 'N' AS ESTADO, '' AS FECHA FROM AUD_REFERENCIAS WHERE ID_CATEGORIA=" & CInt(unaTablaIdCategoria.getItem(0, 0)) & " AND ID NOT IN (SELECT ID_AUD_REFERENCIAS FROM AUD_RELEVAMIENTOS WHERE CE=" & Application("unNumeroDeCE") & " AND SUCURSAL=" & Application("unNumeroDeSucursal") & " AND PERIODO='" & Application("unPeriodoActual") & "' )) AS T ORDER BY LEN(T.NRO_REFERENCIA),T.NRO_REFERENCIA ASC SELECT CATEGORIA,NRO_REFERENCIA,DESCRIPCION,STOCK_ENVIADO,ESTADO_ENVIADO,FECHA_ENVIADA,STOCK,ESTADO,FECHA FROM #TEMP_REFERENCIAS WHERE FILA>" & (Application("paginaActualMain") - 1) * 10 & " AND FILA<=" & Application("paginaActualMain") * 10 & " ORDER BY LEN(NRO_REFERENCIA),NRO_REFERENCIA ASC"
                unaTablaTemporal.getDataSet(Application("ultimoQuery"))
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
            txtStock.Text = formatStock(unStockTextBox)
            'Se fija que sea distinto de lo que vino cargado y que adamás haya seleccionado un estado
            If txtStock.Text.Trim() <> Trim(unStockDataTable) Then
                If IsNumeric(txtStock.Text) = False Then
                    Application("lastError") = "Error: El stock debe ser un entero positivo."
                    txtStock.Focus()
                    Application("agregoOEdito") = True
                    Exit Sub
                End If
                If CInt(txtStock.Text) < 0 Or InStr(txtStock.Text, ".", CompareMethod.Text) Or InStr(txtStock.Text, "%", CompareMethod.Text) Then
                    Application("lastError") = "Error: El stock debe ser un entero positivo."
                    txtStock.Focus()
                    Application("agregoOEdito") = True
                    Exit Sub
                End If
                Dim radOpciones As RadioButtonList = CType(row.FindControl("RadioButtonList1"), RadioButtonList)
                If radOpciones.SelectedValue = "N" And Trim(unStockTextBox) <> "0" Then
                    Application("lastError") = "Error: Debes seleccionar un estado(B/M/R)"
                    radOpciones.Focus()
                    Application("agregoOEdito") = True
                    Exit Sub
                End If
                If radOpciones.SelectedValue <> "N" And Trim(unStockTextBox) = "0" Then
                    Application("lastError") = "Error: No puedes asignarle un estado a un stock de 0."
                    radOpciones.Focus()
                    Application("agregoOEdito") = True
                    Exit Sub
                End If
                Dim unaTablaIdReferencia As TablaSQL = New TablaSQL
                unaTablaIdReferencia.setConnectionString(unConnectionString)
                If Trim(unasReferencias.getItem(unaPosicion, 1)) <> "" Then
                    unaTablaIdReferencia.getDataSet("SELECT ID FROM AUD_REFERENCIAS WHERE NRO_REFERENCIA='" & unasReferencias.getItem(unaPosicion, 1) & "'")
                Else
                    unaTablaIdReferencia.getDataSet("SELECT ID FROM AUD_REFERENCIAS WHERE DESCRIPCION='" & unasReferencias.getItem(unaPosicion, 2) & "' AND NRO_REFERENCIA=''")
                End If
                Dim unNumero As Integer = CInt(unaTablaIdReferencia.getItem(0, 0))
                Dim unaTablaNuevaDeAuditoria As TablaSQL = New TablaSQL
                unaTablaNuevaDeAuditoria.setConnectionString(unConnectionString)
                unaTablaNuevaDeAuditoria.getDataSet("SELECT COUNT(*) FROM AUD_RELEVAMIENTOS WHERE CE=" & Application("unNumeroDeCE") & " AND SUCURSAL=" & Application("unNumeroDeSucursal") & " AND PERIODO='" & Application("unPeriodoActual") & "'" & " AND ID_AUD_REFERENCIAS=" & CInt(unaTablaIdReferencia.getItem(0, 0)))
                'Si no encuentra relevamientos en el período actual para la referencia indicada INSERTA sino UPDATE
                If CInt(unaTablaNuevaDeAuditoria.getItem(0, 0)) = 0 Then
                    Dim unaFecha As String
                    Dim unAno As String = Now.Year()
                    Dim unMes As String = Now.Month()
                    Dim unDia As String = Now.Day
                    If unDia.Length = 1 Then
                        unDia = "0" & unDia
                    End If
                    If unMes.Length = 1 Then
                        unMes = "0" & unMes
                    End If
                    unaFecha = unAno & unMes & unDia
                    unaTablaNuevaDeAuditoria.execQuery("INSERT INTO AUD_RELEVAMIENTOS VALUES(" & Application("unNumeroDeCE") & "," & Application("unNumeroDeSucursal") & ",'" & Application("unPeriodoActual") & "','" & unaFecha & "'," & CInt(unaTablaIdReferencia.getItem(0, 0)) & ",'" & formatStock(unStockTextBox) & "','" & radEstado.SelectedValue & "')")
                Else
                    unaTablaNuevaDeAuditoria.execQuery("UPDATE AUD_RELEVAMIENTOS SET STOCK=" & formatStock(unStockTextBox) & ",ESTADO='" & radEstado.SelectedValue & "' WHERE CE=" & Application("unNumeroDeCE") & " AND SUCURSAL=" & Application("unNumeroDeSucursal") & " AND PERIODO='" & Application("unPeriodoActual") & "' AND ID_AUD_REFERENCIAS=" & CInt(unaTablaIdReferencia.getItem(0, 0)))
                End If
                Application("lastError") = ""
            End If
            unaPosicion += 1
        Next
        Application("agregoOEdito") = True
    End Sub

    Protected Sub RadioButtonList1_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim unaPosicion As Integer = 0
        Dim unaTablaTemporal As TablaSQL = New TablaSQL
        unaTablaTemporal.setConnectionString(unConnectionString)
        For Each row As GridViewRow In GridViewData.Rows
            Dim radEstado As RadioButtonList = CType(row.FindControl("RadioButtonList1"), RadioButtonList)
            Dim txtStock As TextBox = CType(row.FindControl("TextBox1"), TextBox)
            Dim unStockTextBox As String = Trim(txtStock.Text)
            Dim unStockDataTable As String = Trim(unasReferencias.getItem(unaPosicion, 6))
            If formatStock((unStockTextBox)) = Trim(unStockDataTable) Then
                If radEstado.SelectedValue <> unasReferencias.getItem(unaPosicion, 7) Then
                    If Application("lastCat") <> "G" Then
                        unaTablaTemporal.getDataSet("SELECT ID FROM AUD_REFERENCIAS WHERE NRO_REFERENCIA='" & unasReferencias.getItem(unaPosicion, 1) & "'")
                    Else
                        unaTablaTemporal.getDataSet("SELECT ID FROM AUD_REFERENCIAS WHERE NRO_REFERENCIA='' AND DESCRIPCION='" & unasReferencias.getItem(unaPosicion, 2) & "'")
                    End If
                    unasReferencias.execQuery("UPDATE AUD_RELEVAMIENTOS SET ESTADO='" & radEstado.SelectedValue & "' WHERE ID_AUD_REFERENCIAS=" & CInt(unaTablaTemporal.getItem(0, 0)) & " AND PERIODO='" & Application("unPeriodoActual") & "' AND CE=" & Application("unNumeroDeCE") & " AND SUCURSAL=" & Application("unNumeroDeSucursal"))
                    If unStockTextBox = "" Then
                        Application("lastError") = "Debes ingresar un stock primero"
                        txtStock.Focus()
                        Application("agregoOEdito") = True
                    End If
                End If
            End If
            unaPosicion += 1
        Next
    End Sub

    Private Sub auditoria_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        If Application("agregoDesdePopup") = True Then
            setImageButton()
            Application("agregoDesdePopup") = False
        End If
        unasReferencias.getDataSet(Application("ultimoQuery"))
        unasReferencias.fillGridView(GridViewData)
        setMouseOutCategoria()
        formatGridView()
        txtError.Text = Application("lastError")
    End Sub

    Protected Sub btnSalir_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnSalir.Click
        Application("unNumeroDeCE") = 0
        Application("unNumeroDeSucursal") = 0
        Response.Redirect("ingreso.aspx")
    End Sub

    Private Sub GridViewData_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewData.RowDataBound
        If (Application("lastCat") = "F" Or Application("lastCat") = "G") And Application("nivelUsuario") = "ADMIN" Then
            e.Row.Cells(0).Visible = True
        Else
            e.Row.Cells(0).Visible = False
        End If
    End Sub

    Protected Sub TextBox2_TextChanged(sender As Object, e As EventArgs)
        Dim unaPosicion As Integer = 0
        For Each row As GridViewRow In GridViewData.Rows
            Dim txtCategoria As TextBox = CType(row.FindControl("TextBox2"), TextBox)
            If InStr(txtCategoria.Text, "'", CompareMethod.Text) Then
                Application("lastError") = "Error: Debes ingresar una cadena de caracteres válida."
                Application("agregoOEdito") = True
            Else
                If Application("lastCat") = "F" And txtCategoria.Text = "G" Then
                    Application("lastError") = "Error: No puedes cambiar una referencia de categoría F a G."
                    Application("agregoOEdito") = True
                    Exit Sub
                End If
                Dim unaCategoriaTextBox As String = Trim(txtCategoria.Text)
                Dim unaCategoriaDataTable As String = Trim(unasReferencias.getItem(unaPosicion, 0))
                If Trim(unaCategoriaDataTable) <> Trim(unaCategoriaTextBox) Then
                    Dim unaTablaTemporal As TablaSQL = New TablaSQL
                    unaTablaTemporal.setConnectionString(unConnectionString)
                    unaTablaTemporal.getDataSet("SELECT COUNT(*) FROM AUD_CATEGORIAS WHERE CODIGO='" & Trim(unaCategoriaTextBox) & "'")
                    If unaTablaTemporal.getItem(0, 0) = "1" Then
                        unaTablaTemporal.getDataSet("SELECT ID FROM AUD_CATEGORIAS WHERE CODIGO='" & Trim(unaCategoriaTextBox) & "'")
                        If Application("lastCat") = "F" Then
                            unasReferencias.execQuery("UPDATE AUD_REFERENCIAS SET ID_CATEGORIA=" & CInt(unaTablaTemporal.getItem(0, 0)) & " WHERE NRO_REFERENCIA='" & unasReferencias.getItem(unaPosicion, 1) & "'")
                        ElseIf Application("lastCat") = "G" Then
                            unasReferencias.execQuery("UPDATE AUD_REFERENCIAS SET ID_CATEGORIA=" & CInt(unaTablaTemporal.getItem(0, 0)) & " WHERE DESCRIPCION='" & unasReferencias.getItem(unaPosicion, 2) & "' AND NRO_REFERENCIA=''")
                        End If
                    Else
                        Application("lastError") = "Error: La categoría ingresada es inválida."
                    End If
                End If
            End If
            unaPosicion += 1
        Next
        Application("agregoOEdito") = True
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnSearch.Click

    End Sub
End Class