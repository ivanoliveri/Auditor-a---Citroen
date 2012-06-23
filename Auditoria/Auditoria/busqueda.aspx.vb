Imports Auditoria.TableClass
Public Class busqueda
    Inherits System.Web.UI.Page
    Protected Sub btnSearch_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnSearch.Click
        calcularPaginas()
        Dim unaTablaTemporal As TablaSQL = New TablaSQL()
        unaTablaTemporal.setConnectionString(unConnectionString)
        'CREO UNA TABLA TEMPORAL PARA OBTENER EL CORRESPONDIENTE NÚMERO DE FILAS DE CADA UNO, CUANDO LA TERMINO DE USAR SE BORRA SOLA
        If radBusqueda.SelectedValue = "DESCRIPCIÓN" Then
            unaTablaTemporal.getDataSet("CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[NRO_REFERENCIA] [char](20) NULL,[DESCRIPCION] [char] (75) NOT NULL,[CATEGORIA] [char](1) NOT NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (NRO_REFERENCIA,DESCRIPCION,CATEGORIA)SELECT AUD_REFERENCIAS.NRO_REFERENCIA,AUD_REFERENCIAS.DESCRIPCION, AUD_CATEGORIAS.CODIGO FROM AUD_REFERENCIAS INNER JOIN AUD_CATEGORIAS ON AUD_REFERENCIAS.ID_CATEGORIA=AUD_CATEGORIAS.ID WHERE AUD_REFERENCIAS.DESCRIPCION LIKE'%" & txtBusqueda.Text & "%' ORDER BY AUD_REFERENCIAS.NRO_REFERENCIA ASC SELECT CATEGORIA,NRO_REFERENCIA,DESCRIPCION FROM #TEMP_REFERENCIAS WHERE FILA>0 AND FILA<=5")
            unaTablaTemporal.fillGridView(GridViewData)
        ElseIf radBusqueda.SelectedValue = "NRO_REFERNCIA" Then
            unaTablaTemporal.getDataSet("CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[NRO_REFERENCIA] [char](20) NULL,[DESCRIPCION] [char] (75) NOT NULL,[CATEGORIA] [char](1) NOT NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (NRO_REFERENCIA,DESCRIPCION,CATEGORIA)SELECT AUD_REFERENCIAS.NRO_REFERENCIA,AUD_REFERENCIAS.DESCRIPCION, AUD_CATEGORIAS.CODIGO FROM AUD_REFERENCIAS INNER JOIN AUD_CATEGORIAS ON AUD_REFERENCIAS.ID_CATEGORIA=AUD_CATEGORIAS.ID WHERE AUD_REFERENCIAS.NRO_REFERENCIA LIKE'%" & txtBusqueda.Text & "%' ORDER BY AUD_REFERENCIAS.NRO_REFERENCIA ASC SELECT CATEGORIA,NRO_REFERENCIA,DESCRIPCION FROM #TEMP_REFERENCIAS WHERE FILA>0 AND FILA<=5")
            unaTablaTemporal.fillGridView(GridViewData)
        End If
        paginaActualBusqueda = 1
    End Sub

    Protected Sub btnNext_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnNext.Click
        If paginaActualBusqueda < totalPaginasBusqueda Then
            paginaActualBusqueda += 1
            Dim unaTablaTemporal As TablaSQL = New TablaSQL()
            unaTablaTemporal.setConnectionString(unConnectionString)
            'CREO UNA TABLA TEMPORAL PARA OBTENER EL CORRESPONDIENTE NÚMERO DE FILAS DE CADA UNO, CUANDO LA TERMINO DE USAR SE BORRA SOLA
            unaTablaTemporal.getDataSet("CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[NRO_REFERENCIA] [char](20) NULL,[DESCRIPCION] [char] (75) NOT NULL,[CATEGORIA] [char](1) NOT NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (NRO_REFERENCIA,DESCRIPCION,CATEGORIA)SELECT AUD_REFERENCIAS.NRO_REFERENCIA,AUD_REFERENCIAS.DESCRIPCION, AUD_CATEGORIAS.CODIGO FROM AUD_REFERENCIAS INNER JOIN AUD_CATEGORIAS ON AUD_REFERENCIAS.ID_CATEGORIA=AUD_CATEGORIAS.ID WHERE AUD_REFERENCIAS.DESCRIPCION LIKE'%" & txtBusqueda.Text & "%' ORDER BY AUD_REFERENCIAS.NRO_REFERENCIA ASC SELECT CATEGORIA,NRO_REFERENCIA,DESCRIPCION FROM #TEMP_REFERENCIAS WHERE FILA>" & (paginaActualBusqueda - 1) * 5 & " AND FILA<=" & paginaActualBusqueda * 5)
            unasReferencias.dataSet = unaTablaTemporal.dataSet
            unasReferencias.fillGridView(GridViewData)
        End If
    End Sub

    Protected Sub btnPrevious_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnPrevious.Click
        If paginaActualBusqueda <> 1 Then
            paginaActualBusqueda -= 1
            If paginaActualBusqueda = 1 Then
                traerPrimerosRegistros()
            Else
                'CREO UNA TABLA TEMPORAL PARA OBTENER EL CORRESPONDIENTE NÚMERO DE FILAS DE CADA UNO, CUANDO LA TERMINO DE USAR SE BORRA
                Dim unaTablaTemporal As TablaSQL = New TablaSQL()
                unaTablaTemporal.setConnectionString(unConnectionString)
                unaTablaTemporal.getDataSet("CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[NRO_REFERENCIA] [char](20) NULL,[DESCRIPCION] [char] (75) NOT NULL,[CATEGORIA] [char](1) NOT NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (NRO_REFERENCIA,DESCRIPCION,CATEGORIA)SELECT AUD_REFERENCIAS.NRO_REFERENCIA,AUD_REFERENCIAS.DESCRIPCION, AUD_CATEGORIAS.CODIGO FROM AUD_REFERENCIAS INNER JOIN AUD_CATEGORIAS ON AUD_REFERENCIAS.ID_CATEGORIA=AUD_CATEGORIAS.ID WHERE AUD_REFERENCIAS.DESCRIPCION LIKE'%" & txtBusqueda.Text & "%' ORDER BY AUD_REFERENCIAS.NRO_REFERENCIA ASC SELECT CATEGORIA,NRO_REFERENCIA,DESCRIPCION FROM #TEMP_REFERENCIAS WHERE FILA>" & (paginaActualBusqueda - 1) * 5 & " AND FILA<=" & paginaActualBusqueda * 5)
                unasReferencias.dataSet = unaTablaTemporal.dataSet
                unasReferencias.fillGridView(GridViewData)
            End If
        End If
    End Sub
    Protected Sub calcularPaginas()
        Dim unaTablaTemporal As TablaSQL = New TablaSQL()
        unaTablaTemporal.setConnectionString(unConnectionString)
        Dim unaTablaIdCategoria As TablaSQL = New TablaSQL()
        unaTablaIdCategoria.setConnectionString(unConnectionString)
        unaTablaTemporal.getDataSet("CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[ID] [int] NULL,[NRO_REFERENCIA] [char](20) NULL,[DESCRIPCION] [char] (75) NOT NULL,[ID_CATEGORIA] [int] NOT NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (ID,NRO_REFERENCIA,DESCRIPCION,ID_CATEGORIA)SELECT * FROM AUD_REFERENCIAS WHERE DESCRIPCION LIKE '%" & txtBusqueda.Text & "%' SELECT COUNT(*) FROM #TEMP_REFERENCIAS")
        If CInt(unaTablaTemporal.getItem(0, 0)) Mod 10 = 0 Then
            totalPaginasBusqueda = CInt(unaTablaTemporal.getItem(0, 0)) / 5
        Else
            totalPaginasBusqueda = CInt(unaTablaTemporal.getItem(0, 0)) \ 5 + 1
        End If
    End Sub
    Protected Sub traerPrimerosRegistros()
        Dim unaTablaTemporal As TablaSQL = New TablaSQL()
        unaTablaTemporal.setConnectionString(unConnectionString)
        'CREO UNA TABLA TEMPORAL PARA OBTENER EL CORRESPONDIENTE NÚMERO DE FILAS DE CADA UNO, CUANDO LA TERMINO DE USAR SE BORRA SOLA
        unaTablaTemporal.getDataSet("CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[NRO_REFERENCIA] [char](20) NULL,[DESCRIPCION] [char] (75) NOT NULL,[CATEGORIA] [char](1) NOT NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (NRO_REFERENCIA,DESCRIPCION,CATEGORIA)SELECT AUD_REFERENCIAS.NRO_REFERENCIA,AUD_REFERENCIAS.DESCRIPCION, AUD_CATEGORIAS.CODIGO FROM AUD_REFERENCIAS INNER JOIN AUD_CATEGORIAS ON AUD_REFERENCIAS.ID_CATEGORIA=AUD_CATEGORIAS.ID WHERE AUD_REFERENCIAS.DESCRIPCION LIKE'%" & txtBusqueda.Text & "%' ORDER BY AUD_REFERENCIAS.NRO_REFERENCIA ASC SELECT CATEGORIA,NRO_REFERENCIA,DESCRIPCION FROM #TEMP_REFERENCIAS WHERE FILA>0 AND FILA<=5")
        unasReferencias.dataSet = unaTablaTemporal.dataSet
        unasReferencias.fillGridView(GridViewData)
        paginaActualBusqueda = 1
    End Sub

    Private Sub WebForm2_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If primerIngresoBusqueda = True Then
            txtBusqueda.Text = ""
            calcularPaginas()
            primerIngresoBusqueda = False
        End If
    End Sub
End Class