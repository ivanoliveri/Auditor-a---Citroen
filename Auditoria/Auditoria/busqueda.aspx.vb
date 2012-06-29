Imports Auditoria.TableClass
Public Class busqueda
    Inherits System.Web.UI.Page
    Protected Sub hideNextOrPrevious()
        If paginaActualBusqueda = 0 Then
            btnPrevious.Visible = False
            btnNext.Visible = False
            Exit Sub
        End If
        If paginaActualBusqueda = 1 Then
            btnPrevious.Visible = False
        Else
            btnPrevious.Visible = True
        End If
        If paginaActualBusqueda = totalPaginasBusqueda Then
            btnNext.Visible = False
        Else
            btnNext.Visible = True
        End If
    End Sub
    Protected Sub getDataSetFromDescripcion()
        Dim unaTablaTemporal As TablaSQL = New TablaSQL()
        unaTablaTemporal.setConnectionString(unConnectionString)
        unaTablaTemporal.getDataSet("CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[NRO_REFERENCIA] [char](20) NULL,[DESCRIPCION] [char] (55) NOT NULL,[CATEGORIA] [char](1) NOT NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (NRO_REFERENCIA,DESCRIPCION,CATEGORIA)SELECT AUD_REFERENCIAS.NRO_REFERENCIA,AUD_REFERENCIAS.DESCRIPCION, AUD_CATEGORIAS.CODIGO FROM AUD_REFERENCIAS INNER JOIN AUD_CATEGORIAS ON AUD_REFERENCIAS.ID_CATEGORIA=AUD_CATEGORIAS.ID WHERE AUD_REFERENCIAS.DESCRIPCION LIKE'%" & txtBusqueda.Text & "%' ORDER BY AUD_REFERENCIAS.NRO_REFERENCIA ASC SELECT CATEGORIA,NRO_REFERENCIA,DESCRIPCION FROM #TEMP_REFERENCIAS WHERE FILA>" & (paginaActualBusqueda - 1) * 5 & " AND FILA<=" & paginaActualBusqueda * 5)
        unasReferencias.dataSet = unaTablaTemporal.dataSet
        unasReferencias.fillGridView(GridViewData)
    End Sub

    Protected Sub getDataSetFromNroReferencia()
        Dim unaTablaTemporal As TablaSQL = New TablaSQL()
        unaTablaTemporal.setConnectionString(unConnectionString)
        unaTablaTemporal.getDataSet("CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[NRO_REFERENCIA] [char](20) NULL,[DESCRIPCION] [char] (55) NOT NULL,[CATEGORIA] [char](1) NOT NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (NRO_REFERENCIA,DESCRIPCION,CATEGORIA)SELECT AUD_REFERENCIAS.NRO_REFERENCIA,AUD_REFERENCIAS.DESCRIPCION, AUD_CATEGORIAS.CODIGO FROM AUD_REFERENCIAS INNER JOIN AUD_CATEGORIAS ON AUD_REFERENCIAS.ID_CATEGORIA=AUD_CATEGORIAS.ID WHERE AUD_REFERENCIAS.NRO_REFERENCIA LIKE'%" & txtBusqueda.Text & "%' ORDER BY AUD_REFERENCIAS.NRO_REFERENCIA ASC SELECT CATEGORIA,NRO_REFERENCIA,DESCRIPCION FROM #TEMP_REFERENCIAS WHERE FILA>" & (paginaActualBusqueda - 1) * 5 & " AND FILA<=" & paginaActualBusqueda * 5)
        unasReferencias.dataSet = unaTablaTemporal.dataSet
        unasReferencias.fillGridView(GridViewData)
    End Sub

    Protected Sub getDataSetFromPalabraClave()
        Dim unaTablaTemporal As TablaSQL = New TablaSQL()
        unaTablaTemporal.setConnectionString(unConnectionString)
        unaTablaTemporal.getDataSet("CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[NRO_REFERENCIA] [char](20) NULL,[DESCRIPCION] [char] (55) NOT NULL,[CATEGORIA] [char](1) NOT NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (NRO_REFERENCIA,DESCRIPCION,CATEGORIA)SELECT AUD_REFERENCIAS.NRO_REFERENCIA,AUD_REFERENCIAS.DESCRIPCION, AUD_CATEGORIAS.CODIGO FROM AUD_REFERENCIAS INNER JOIN AUD_CATEGORIAS ON AUD_REFERENCIAS.ID_CATEGORIA=AUD_CATEGORIAS.ID WHERE AUD_REFERENCIAS.DESCRIPCION LIKE'%" & txtBusqueda.Text & "%' OR AUD_REFERENCIAS.DESCRIPCION LIKE '%" & txtBusqueda.Text & "%' ORDER BY AUD_REFERENCIAS.NRO_REFERENCIA ASC SELECT CATEGORIA,NRO_REFERENCIA,DESCRIPCION FROM #TEMP_REFERENCIAS WHERE FILA>" & (paginaActualBusqueda - 1) * 5 & " AND FILA<=" & paginaActualBusqueda * 5)
        unasReferencias.dataSet = unaTablaTemporal.dataSet
        unasReferencias.fillGridView(GridViewData)
    End Sub
    Protected Sub calcularPaginasDescripcion()
        Dim unaTablaTemporal As TablaSQL = New TablaSQL()
        unaTablaTemporal.setConnectionString(unConnectionString)
        Dim unaTablaIdCategoria As TablaSQL = New TablaSQL()
        unaTablaIdCategoria.setConnectionString(unConnectionString)
        unaTablaTemporal.getDataSet("CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[ID] [int] NULL,[NRO_REFERENCIA] [char](20) NULL,[DESCRIPCION] [char] (55) NOT NULL,[ID_CATEGORIA] [int] NOT NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (ID,NRO_REFERENCIA,DESCRIPCION,ID_CATEGORIA)SELECT * FROM AUD_REFERENCIAS WHERE DESCRIPCION LIKE '%" & txtBusqueda.Text & "%' SELECT COUNT(*) FROM #TEMP_REFERENCIAS")
        If CInt(unaTablaTemporal.getItem(0, 0)) Mod 5 = 0 Then
            totalPaginasBusqueda = CInt(unaTablaTemporal.getItem(0, 0)) / 5
        Else
            totalPaginasBusqueda = CInt(unaTablaTemporal.getItem(0, 0)) \ 5 + 1
        End If
    End Sub
    Protected Sub calcularPaginasNroReferencia()
        Dim unaTablaTemporal As TablaSQL = New TablaSQL()
        unaTablaTemporal.setConnectionString(unConnectionString)
        Dim unaTablaIdCategoria As TablaSQL = New TablaSQL()
        unaTablaIdCategoria.setConnectionString(unConnectionString)
        unaTablaTemporal.getDataSet("CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[ID] [int] NULL,[NRO_REFERENCIA] [char](20) NULL,[DESCRIPCION] [char] (55) NOT NULL,[ID_CATEGORIA] [int] NOT NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (ID,NRO_REFERENCIA,DESCRIPCION,ID_CATEGORIA)SELECT * FROM AUD_REFERENCIAS WHERE NRO_REFERENCIA LIKE '%" & txtBusqueda.Text & "%' SELECT COUNT(*) FROM #TEMP_REFERENCIAS")
        If CInt(unaTablaTemporal.getItem(0, 0)) Mod 5 = 0 Then
            totalPaginasBusqueda = CInt(unaTablaTemporal.getItem(0, 0)) / 5
        Else
            totalPaginasBusqueda = CInt(unaTablaTemporal.getItem(0, 0)) \ 5 + 1
        End If
    End Sub
    Protected Sub calcularPaginasPalabraClave()
        Dim unaTablaTemporal As TablaSQL = New TablaSQL()
        unaTablaTemporal.setConnectionString(unConnectionString)
        Dim unaTablaIdCategoria As TablaSQL = New TablaSQL()
        unaTablaIdCategoria.setConnectionString(unConnectionString)
        unaTablaTemporal.getDataSet("CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[ID] [int] NULL,[NRO_REFERENCIA] [char](20) NULL,[DESCRIPCION] [char] (55) NOT NULL,[ID_CATEGORIA] [int] NOT NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (ID,NRO_REFERENCIA,DESCRIPCION,ID_CATEGORIA)SELECT * FROM AUD_REFERENCIAS WHERE DESCRIPCION LIKE '%" & txtBusqueda.Text & "%' OR NRO_REFERENCIA LIKE '%" & txtBusqueda.Text & "%' SELECT COUNT(*) FROM #TEMP_REFERENCIAS")
        If CInt(unaTablaTemporal.getItem(0, 0)) Mod 5 = 0 Then
            totalPaginasBusqueda = CInt(unaTablaTemporal.getItem(0, 0)) / 5
        Else
            totalPaginasBusqueda = CInt(unaTablaTemporal.getItem(0, 0)) \ 5 + 1
        End If
    End Sub
    Protected Sub traerPrimerosRegistrosDeDescripcion()
        Dim unaTablaTemporal As TablaSQL = New TablaSQL()
        unaTablaTemporal.setConnectionString(unConnectionString)
        'CREO UNA TABLA TEMPORAL PARA OBTENER EL CORRESPONDIENTE NÚMERO DE FILAS DE CADA UNO, CUANDO LA TERMINO DE USAR SE BORRA SOLA
        unaTablaTemporal.getDataSet("CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[NRO_REFERENCIA] [char](20) NULL,[DESCRIPCION] [char] (55) NOT NULL,[CATEGORIA] [char](1) NOT NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (NRO_REFERENCIA,DESCRIPCION,CATEGORIA)SELECT AUD_REFERENCIAS.NRO_REFERENCIA,AUD_REFERENCIAS.DESCRIPCION, AUD_CATEGORIAS.CODIGO FROM AUD_REFERENCIAS INNER JOIN AUD_CATEGORIAS ON AUD_REFERENCIAS.ID_CATEGORIA=AUD_CATEGORIAS.ID WHERE AUD_REFERENCIAS.DESCRIPCION LIKE'%" & txtBusqueda.Text & "%' ORDER BY AUD_REFERENCIAS.NRO_REFERENCIA ASC SELECT CATEGORIA,NRO_REFERENCIA,DESCRIPCION FROM #TEMP_REFERENCIAS WHERE FILA>0 AND FILA<=5")
        unasReferencias.dataSet = unaTablaTemporal.dataSet
        unasReferencias.fillGridView(GridViewData)
        paginaActualBusqueda = 1
    End Sub
    Protected Sub traerPrimerosRegistrosDeNroReferencia()
        Dim unaTablaTemporal As TablaSQL = New TablaSQL()
        unaTablaTemporal.setConnectionString(unConnectionString)
        'CREO UNA TABLA TEMPORAL PARA OBTENER EL CORRESPONDIENTE NÚMERO DE FILAS DE CADA UNO, CUANDO LA TERMINO DE USAR SE BORRA SOLA
        unaTablaTemporal.getDataSet("CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[NRO_REFERENCIA] [char](20) NULL,[DESCRIPCION] [char] (55) NOT NULL,[CATEGORIA] [char](1) NOT NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (NRO_REFERENCIA,DESCRIPCION,CATEGORIA)SELECT AUD_REFERENCIAS.NRO_REFERENCIA,AUD_REFERENCIAS.DESCRIPCION, AUD_CATEGORIAS.CODIGO FROM AUD_REFERENCIAS INNER JOIN AUD_CATEGORIAS ON AUD_REFERENCIAS.ID_CATEGORIA=AUD_CATEGORIAS.ID WHERE AUD_REFERENCIAS.NRO_REFERENCIA LIKE'%" & txtBusqueda.Text & "%' ORDER BY AUD_REFERENCIAS.NRO_REFERENCIA ASC SELECT CATEGORIA,NRO_REFERENCIA,DESCRIPCION FROM #TEMP_REFERENCIAS WHERE FILA>0 AND FILA<=5")
        unasReferencias.dataSet = unaTablaTemporal.dataSet
        unasReferencias.fillGridView(GridViewData)
        paginaActualBusqueda = 1
    End Sub
    Protected Sub traerPrimerosRegistrosDePalabraClave()
        Dim unaTablaTemporal As TablaSQL = New TablaSQL()
        unaTablaTemporal.setConnectionString(unConnectionString)
        'CREO UNA TABLA TEMPORAL PARA OBTENER EL CORRESPONDIENTE NÚMERO DE FILAS DE CADA UNO, CUANDO LA TERMINO DE USAR SE BORRA SOLA
        unaTablaTemporal.getDataSet("CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[NRO_REFERENCIA] [char](20) NULL,[DESCRIPCION] [char] (55) NOT NULL,[CATEGORIA] [char](1) NOT NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (NRO_REFERENCIA,DESCRIPCION,CATEGORIA)SELECT AUD_REFERENCIAS.NRO_REFERENCIA,AUD_REFERENCIAS.DESCRIPCION, AUD_CATEGORIAS.CODIGO FROM AUD_REFERENCIAS INNER JOIN AUD_CATEGORIAS ON AUD_REFERENCIAS.ID_CATEGORIA=AUD_CATEGORIAS.ID WHERE AUD_REFERENCIAS.DESCRIPCION LIKE'%" & txtBusqueda.Text & "%' OR AUD_REFERENCIAS.DESCRIPCION LIKE '%" & txtBusqueda.Text & "%' ORDER BY AUD_REFERENCIAS.NRO_REFERENCIA ASC SELECT CATEGORIA,NRO_REFERENCIA,DESCRIPCION FROM #TEMP_REFERENCIAS WHERE FILA>0 AND FILA<=5")
        unasReferencias.dataSet = unaTablaTemporal.dataSet
        unasReferencias.fillGridView(GridViewData)
        paginaActualBusqueda = 1
    End Sub
    Protected Sub buscar()
        Dim unaTablaTemporal As TablaSQL = New TablaSQL()
        unaTablaTemporal.setConnectionString(unConnectionString)
        'CREO UNA TABLA TEMPORAL PARA OBTENER EL CORRESPONDIENTE NÚMERO DE FILAS DE CADA UNO, CUANDO LA TERMINO DE USAR SE BORRA SOLA
        If radBusqueda.SelectedValue = "DESCRIPCION" Then
            calcularPaginasDescripcion()
            If totalPaginasBusqueda = 0 Then
                GridViewData.Visible = False
                btnNext.Visible = False
                btnPrevious.Visible = False
                Exit Sub
            Else
                GridViewData.Visible = True
            End If
            Dim unQuery As String = "CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[NRO_REFERENCIA] [char](20) NULL,[DESCRIPCION] [char] (55) NOT NULL,[CATEGORIA] [char](1) NOT NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (NRO_REFERENCIA,DESCRIPCION,CATEGORIA)SELECT AUD_REFERENCIAS.NRO_REFERENCIA,AUD_REFERENCIAS.DESCRIPCION, AUD_CATEGORIAS.CODIGO FROM AUD_REFERENCIAS INNER JOIN AUD_CATEGORIAS ON AUD_REFERENCIAS.ID_CATEGORIA=AUD_CATEGORIAS.ID WHERE AUD_REFERENCIAS.DESCRIPCION LIKE'%" & txtBusqueda.Text & "%' ORDER BY AUD_REFERENCIAS.NRO_REFERENCIA ASC SELECT CATEGORIA,NRO_REFERENCIA,DESCRIPCION FROM #TEMP_REFERENCIAS WHERE FILA>0 AND FILA<=5"
            unaTablaTemporal.getDataSet("CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[NRO_REFERENCIA] [char](20) NULL,[DESCRIPCION] [char] (55) NOT NULL,[CATEGORIA] [char](1) NOT NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (NRO_REFERENCIA,DESCRIPCION,CATEGORIA)SELECT AUD_REFERENCIAS.NRO_REFERENCIA,AUD_REFERENCIAS.DESCRIPCION, AUD_CATEGORIAS.CODIGO FROM AUD_REFERENCIAS INNER JOIN AUD_CATEGORIAS ON AUD_REFERENCIAS.ID_CATEGORIA=AUD_CATEGORIAS.ID WHERE AUD_REFERENCIAS.DESCRIPCION LIKE'%" & txtBusqueda.Text & "%' ORDER BY AUD_REFERENCIAS.NRO_REFERENCIA ASC SELECT CATEGORIA,NRO_REFERENCIA,DESCRIPCION FROM #TEMP_REFERENCIAS WHERE FILA>0 AND FILA<=5")
        ElseIf radBusqueda.SelectedValue = "NRO_REFERENCIA" Then
            calcularPaginasNroReferencia()
            If totalPaginasBusqueda = 0 Then
                GridViewData.Visible = False
                Exit Sub
            Else
                GridViewData.Visible = True
            End If
            unaTablaTemporal.getDataSet("CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[NRO_REFERENCIA] [char](20) NULL,[DESCRIPCION] [char] (55) NOT NULL,[CATEGORIA] [char](1) NOT NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (NRO_REFERENCIA,DESCRIPCION,CATEGORIA)SELECT AUD_REFERENCIAS.NRO_REFERENCIA,AUD_REFERENCIAS.DESCRIPCION, AUD_CATEGORIAS.CODIGO FROM AUD_REFERENCIAS INNER JOIN AUD_CATEGORIAS ON AUD_REFERENCIAS.ID_CATEGORIA=AUD_CATEGORIAS.ID WHERE AUD_REFERENCIAS.NRO_REFERENCIA LIKE'%" & txtBusqueda.Text & "%' ORDER BY AUD_REFERENCIAS.NRO_REFERENCIA ASC SELECT CATEGORIA,NRO_REFERENCIA,DESCRIPCION FROM #TEMP_REFERENCIAS WHERE FILA>0 AND FILA<=5")
        ElseIf radBusqueda.SelectedValue = "PALABRA_CLAVE" Then
            calcularPaginasPalabraClave()
            If totalPaginasBusqueda = 0 Then
                GridViewData.Visible = False
                Exit Sub
            Else
                GridViewData.Visible = True
            End If
            unaTablaTemporal.getDataSet("CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[NRO_REFERENCIA] [char](20) NULL,[DESCRIPCION] [char] (55) NOT NULL,[CATEGORIA] [char](1) NOT NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (NRO_REFERENCIA,DESCRIPCION,CATEGORIA)SELECT AUD_REFERENCIAS.NRO_REFERENCIA,AUD_REFERENCIAS.DESCRIPCION, AUD_CATEGORIAS.CODIGO FROM AUD_REFERENCIAS INNER JOIN AUD_CATEGORIAS ON AUD_REFERENCIAS.ID_CATEGORIA=AUD_CATEGORIAS.ID WHERE AUD_REFERENCIAS.NRO_REFERENCIA LIKE '%" & txtBusqueda.Text & "%' OR AUD_REFERENCIAS.DESCRIPCION LIKE '%" & txtBusqueda.Text & "%' ORDER BY AUD_REFERENCIAS.NRO_REFERENCIA ASC SELECT CATEGORIA,NRO_REFERENCIA,DESCRIPCION FROM #TEMP_REFERENCIAS WHERE FILA>0 AND FILA<=5")
        End If
        unaTablaTemporal.fillGridView(GridViewData)
        paginaActualBusqueda = 1
        hideNextOrPrevious()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtBusqueda.Focus()
        btnCancelar.Attributes.Add("onclick", "javascript:unloadPage()")
        hideNextOrPrevious()
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnSearch.Click
        buscar()
    End Sub

    Protected Sub btnNext_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnNext.Click
        If paginaActualBusqueda < totalPaginasBusqueda Then
            paginaActualBusqueda += 1
            Select Case radBusqueda.SelectedValue
                Case "NRO_REFERENCIA"
                    getDataSetFromNroReferencia()
                Case "DESCRIPCION"
                    getDataSetFromDescripcion()
                Case "PALABRA_CLAVE"
                    getDataSetFromPalabraClave()
            End Select
        End If
        hideNextOrPrevious()
    End Sub

    Protected Sub btnPrevious_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnPrevious.Click
        If paginaActualBusqueda <> 1 Then
            paginaActualBusqueda -= 1
            If paginaActualBusqueda = 1 Then
                Select Case radBusqueda.SelectedValue
                    Case "NRO_REFERENCIA"
                        traerPrimerosRegistrosDeNroReferencia()
                    Case "DESCRIPCION"
                        traerPrimerosRegistrosDeDescripcion()
                    Case "PALABRA_CLAVE"
                        traerPrimerosRegistrosDePalabraClave()
                End Select
            Else
                'CREO UNA TABLA TEMPORAL PARA OBTENER EL CORRESPONDIENTE NÚMERO DE FILAS DE CADA UNO, CUANDO LA TERMINO DE USAR SE BORRA
                Select Case radBusqueda.SelectedValue
                    Case "NRO_REFERENCIA"
                        getDataSetFromNroReferencia()
                    Case "DESCRIPCION"
                        getDataSetFromDescripcion()
                    Case "PALABRA_CLAVE"
                        getDataSetFromPalabraClave()
                End Select
            End If
        End If
        hideNextOrPrevious()
    End Sub
End Class