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
        unaTablaTemporalDeBusqueda.setConnectionString(unConnectionString)
        unaTablaTemporalDeBusqueda.getDataSet("CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[NRO_REFERENCIA] [varchar](20) NULL,[DESCRIPCION] [varchar] (80) NOT NULL,[CATEGORIA] [varchar](1) NOT NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (NRO_REFERENCIA,DESCRIPCION,CATEGORIA)SELECT AUD_REFERENCIAS.NRO_REFERENCIA,AUD_REFERENCIAS.DESCRIPCION, AUD_CATEGORIAS.CODIGO FROM AUD_REFERENCIAS INNER JOIN AUD_CATEGORIAS ON AUD_REFERENCIAS.ID_CATEGORIA=AUD_CATEGORIAS.ID WHERE AUD_REFERENCIAS.DESCRIPCION LIKE'" & txtBusqueda.Text & "%' ORDER BY AUD_REFERENCIAS.NRO_REFERENCIA ASC SELECT CATEGORIA,NRO_REFERENCIA,DESCRIPCION FROM #TEMP_REFERENCIAS WHERE FILA>" & (paginaActualBusqueda - 1) * 5 & " AND FILA<=" & paginaActualBusqueda * 5 & " ORDER BY LEN(NRO_REFERENCIA),NRO_REFERENCIA ASC")
        unasReferencias.dataSet = unaTablaTemporalDeBusqueda.dataSet
        unasReferencias.fillGridView(GridViewData)
    End Sub

    Protected Sub getDataSetFromNroReferencia()
        unaTablaTemporalDeBusqueda.getDataSet("CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[NRO_REFERENCIA] [varchar](20) NULL,[DESCRIPCION] [varchar] (80) NOT NULL,[CATEGORIA] [varchar](1) NOT NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (NRO_REFERENCIA,DESCRIPCION,CATEGORIA)SELECT AUD_REFERENCIAS.NRO_REFERENCIA,AUD_REFERENCIAS.DESCRIPCION, AUD_CATEGORIAS.CODIGO FROM AUD_REFERENCIAS INNER JOIN AUD_CATEGORIAS ON AUD_REFERENCIAS.ID_CATEGORIA=AUD_CATEGORIAS.ID WHERE AUD_REFERENCIAS.NRO_REFERENCIA LIKE'" & txtBusqueda.Text & "%' ORDER BY AUD_REFERENCIAS.NRO_REFERENCIA ASC SELECT CATEGORIA,NRO_REFERENCIA,DESCRIPCION FROM #TEMP_REFERENCIAS WHERE FILA>" & (paginaActualBusqueda - 1) * 5 & " AND FILA<=" & paginaActualBusqueda * 5 & " ORDER BY LEN(NRO_REFERENCIA),NRO_REFERENCIA ASC")
        unasReferencias.dataSet = unaTablaTemporalDeBusqueda.dataSet
        unasReferencias.fillGridView(GridViewData)
    End Sub

    Protected Sub getDataSetFromPalabraClave()
        unaTablaTemporalDeBusqueda.getDataSet("CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[NRO_REFERENCIA] [varchar](20) NULL,[DESCRIPCION] [varchar] (80) NOT NULL,[CATEGORIA] [varchar](1) NOT NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (NRO_REFERENCIA,DESCRIPCION,CATEGORIA)SELECT AUD_REFERENCIAS.NRO_REFERENCIA,AUD_REFERENCIAS.DESCRIPCION, AUD_CATEGORIAS.CODIGO FROM AUD_REFERENCIAS INNER JOIN AUD_CATEGORIAS ON AUD_REFERENCIAS.ID_CATEGORIA=AUD_CATEGORIAS.ID WHERE AUD_REFERENCIAS.DESCRIPCION LIKE'%" & txtBusqueda.Text & "%' ORDER BY AUD_REFERENCIAS.NRO_REFERENCIA ASC SELECT CATEGORIA,NRO_REFERENCIA,DESCRIPCION FROM #TEMP_REFERENCIAS WHERE FILA>" & (paginaActualBusqueda - 1) * 5 & " AND FILA<=" & paginaActualBusqueda * 5 & " ORDER BY LEN(NRO_REFERENCIA),NRO_REFERENCIA ASC")
        unasReferencias.dataSet = unaTablaTemporalDeBusqueda.dataSet
        unasReferencias.fillGridView(GridViewData)
    End Sub

    Protected Sub calcularPaginasDescripcion()
        Dim unaTablaIdCategoria As TablaSQL = New TablaSQL()
        unaTablaIdCategoria.setConnectionString(unConnectionString)
        unaTablaTemporalDeBusqueda.getDataSet("CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[ID] [int] NULL,[NRO_REFERENCIA] [varchar](20) NULL,[DESCRIPCION] [varchar] (80) NOT NULL,[ID_CATEGORIA] [int] NOT NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (ID,NRO_REFERENCIA,DESCRIPCION,ID_CATEGORIA)SELECT * FROM AUD_REFERENCIAS WHERE DESCRIPCION LIKE '" & txtBusqueda.Text & "%' SELECT COUNT(*) FROM #TEMP_REFERENCIAS")
        If CInt(unaTablaTemporalDeBusqueda.getItem(0, 0)) Mod 5 = 0 Then
            totalPaginasBusqueda = CInt(unaTablaTemporalDeBusqueda.getItem(0, 0)) / 5
        Else
            totalPaginasBusqueda = CInt(unaTablaTemporalDeBusqueda.getItem(0, 0)) \ 5 + 1
        End If
    End Sub

    Protected Sub calcularPaginasNroReferencia()
        Dim unaTablaIdCategoria As TablaSQL = New TablaSQL()
        unaTablaIdCategoria.setConnectionString(unConnectionString)
        unaTablaTemporalDeBusqueda.getDataSet("CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[ID] [int] NULL,[NRO_REFERENCIA] [varchar](20) NULL,[DESCRIPCION] [varchar] (80) NOT NULL,[ID_CATEGORIA] [int] NOT NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (ID,NRO_REFERENCIA,DESCRIPCION,ID_CATEGORIA)SELECT * FROM AUD_REFERENCIAS WHERE NRO_REFERENCIA LIKE '" & txtBusqueda.Text & "%' SELECT COUNT(*) FROM #TEMP_REFERENCIAS")
        If CInt(unaTablaTemporalDeBusqueda.getItem(0, 0)) Mod 5 = 0 Then
            totalPaginasBusqueda = CInt(unaTablaTemporalDeBusqueda.getItem(0, 0)) / 5
        Else
            totalPaginasBusqueda = CInt(unaTablaTemporalDeBusqueda.getItem(0, 0)) \ 5 + 1
        End If
    End Sub

    Protected Sub calcularPaginasPalabraClave()
        Dim unaTablaIdCategoria As TablaSQL = New TablaSQL()
        unaTablaIdCategoria.setConnectionString(unConnectionString)
        unaTablaTemporalDeBusqueda.getDataSet("CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[ID] [int] NULL,[NRO_REFERENCIA] [varchar](20) NULL,[DESCRIPCION] [varchar] (80) NOT NULL,[ID_CATEGORIA] [int] NOT NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (ID,NRO_REFERENCIA,DESCRIPCION,ID_CATEGORIA)SELECT * FROM AUD_REFERENCIAS WHERE DESCRIPCION LIKE '%" & txtBusqueda.Text & "%' SELECT COUNT(*) FROM #TEMP_REFERENCIAS")
        If CInt(unaTablaTemporalDeBusqueda.getItem(0, 0)) Mod 5 = 0 Then
            totalPaginasBusqueda = CInt(unaTablaTemporalDeBusqueda.getItem(0, 0)) / 5
        Else
            totalPaginasBusqueda = CInt(unaTablaTemporalDeBusqueda.getItem(0, 0)) \ 5 + 1
        End If
    End Sub

    Protected Sub traerPrimerosRegistrosDeDescripcion()
        'CREO UNA TABLA TEMPORAL PARA OBTENER EL CORRESPONDIENTE NÚMERO DE FILAS DE CADA UNO, CUANDO LA TERMINO DE USAR SE BORRA SOLA
        unaTablaTemporalDeBusqueda.getDataSet("CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[NRO_REFERENCIA] [varchar](20) NULL,[DESCRIPCION] [varchar] (80) NOT NULL,[CATEGORIA] [varchar](1) NOT NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (NRO_REFERENCIA,DESCRIPCION,CATEGORIA)SELECT AUD_REFERENCIAS.NRO_REFERENCIA,AUD_REFERENCIAS.DESCRIPCION, AUD_CATEGORIAS.CODIGO FROM AUD_REFERENCIAS INNER JOIN AUD_CATEGORIAS ON AUD_REFERENCIAS.ID_CATEGORIA=AUD_CATEGORIAS.ID WHERE AUD_REFERENCIAS.DESCRIPCION LIKE'" & txtBusqueda.Text & "%' ORDER BY AUD_REFERENCIAS.NRO_REFERENCIA ASC SELECT CATEGORIA,NRO_REFERENCIA,DESCRIPCION FROM #TEMP_REFERENCIAS WHERE FILA>0 AND FILA<=5 ORDER BY LEN(NRO_REFERENCIA),NRO_REFERENCIA ASC")
        unasReferencias.dataSet = unaTablaTemporalDeBusqueda.dataSet
        unasReferencias.fillGridView(GridViewData)
        paginaActualBusqueda = 1
    End Sub
    Protected Sub traerPrimerosRegistrosDeNroReferencia()
        'CREO UNA TABLA TEMPORAL PARA OBTENER EL CORRESPONDIENTE NÚMERO DE FILAS DE CADA UNO, CUANDO LA TERMINO DE USAR SE BORRA SOLA
        unaTablaTemporalDeBusqueda.getDataSet("CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[NRO_REFERENCIA] [varchar](20) NULL,[DESCRIPCION] [varchar] (80) NOT NULL,[CATEGORIA] [varchar](1) NOT NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (NRO_REFERENCIA,DESCRIPCION,CATEGORIA)SELECT AUD_REFERENCIAS.NRO_REFERENCIA,AUD_REFERENCIAS.DESCRIPCION, AUD_CATEGORIAS.CODIGO FROM AUD_REFERENCIAS INNER JOIN AUD_CATEGORIAS ON AUD_REFERENCIAS.ID_CATEGORIA=AUD_CATEGORIAS.ID WHERE AUD_REFERENCIAS.NRO_REFERENCIA LIKE'" & txtBusqueda.Text & "%' ORDER BY AUD_REFERENCIAS.NRO_REFERENCIA ASC SELECT CATEGORIA,NRO_REFERENCIA,DESCRIPCION FROM #TEMP_REFERENCIAS WHERE FILA>0 AND FILA<=5 ORDER BY LEN(NRO_REFERENCIA),NRO_REFERENCIA ASC")
        unasReferencias.dataSet = unaTablaTemporalDeBusqueda.dataSet
        unasReferencias.fillGridView(GridViewData)
        paginaActualBusqueda = 1
    End Sub

    Protected Sub traerPrimerosRegistrosDePalabraClave()
        'CREO UNA TABLA TEMPORAL PARA OBTENER EL CORRESPONDIENTE NÚMERO DE FILAS DE CADA UNO, CUANDO LA TERMINO DE USAR SE BORRA SOLA
        unaTablaTemporalDeBusqueda.getDataSet("CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[NRO_REFERENCIA] [varchar](20) NULL,[DESCRIPCION] [varchar] (80) NOT NULL,[CATEGORIA] [varchar](1) NOT NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (NRO_REFERENCIA,DESCRIPCION,CATEGORIA)SELECT AUD_REFERENCIAS.NRO_REFERENCIA,AUD_REFERENCIAS.DESCRIPCION, AUD_CATEGORIAS.CODIGO FROM AUD_REFERENCIAS INNER JOIN AUD_CATEGORIAS ON AUD_REFERENCIAS.ID_CATEGORIA=AUD_CATEGORIAS.ID WHERE AUD_REFERENCIAS.DESCRIPCION LIKE'%" & txtBusqueda.Text & "%' ORDER BY AUD_REFERENCIAS.NRO_REFERENCIA ASC SELECT CATEGORIA,NRO_REFERENCIA,DESCRIPCION FROM #TEMP_REFERENCIAS WHERE FILA>0 AND FILA<=5 ORDER BY LEN(NRO_REFERENCIA),NRO_REFERENCIA ASC")
        unasReferencias.dataSet = unaTablaTemporalDeBusqueda.dataSet
        unasReferencias.fillGridView(GridViewData)
        paginaActualBusqueda = 1
    End Sub

    Protected Sub buscar()
        'CREO UNA TABLA TEMPORAL PARA OBTENER EL CORRESPONDIENTE NÚMERO DE FILAS DE CADA UNO, CUANDO LA TERMINO DE USAR SE BORRA SOLA
        If InStr(txtBusqueda.Text, "'", CompareMethod.Text) Or InStr(txtBusqueda.Text, "%", CompareMethod.Text) Then
            txtError.Text = "Error: Debes ingresar una cadena de caracteres válida."
            Exit Sub
        Else
            If radBusqueda.SelectedValue = "DESCRIPCION" Then
                calcularPaginasDescripcion()
                If totalPaginasBusqueda = 0 Then
                    GridViewData.Visible = False
                    btnNext.Visible = False
                    btnPrevious.Visible = False
                    Exit Sub
                Else
                    GridViewData.Visible = True
                    txtError.Text = ""
                End If
                unaTablaTemporalDeBusqueda.getDataSet("CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[NRO_REFERENCIA] [varchar](20) NULL,[DESCRIPCION] [varchar] (80) NOT NULL,[CATEGORIA] [varchar](1) NOT NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (NRO_REFERENCIA,DESCRIPCION,CATEGORIA)SELECT AUD_REFERENCIAS.NRO_REFERENCIA,AUD_REFERENCIAS.DESCRIPCION, AUD_CATEGORIAS.CODIGO FROM AUD_REFERENCIAS INNER JOIN AUD_CATEGORIAS ON AUD_REFERENCIAS.ID_CATEGORIA=AUD_CATEGORIAS.ID WHERE AUD_REFERENCIAS.DESCRIPCION LIKE'" & Trim(txtBusqueda.Text) & "%' ORDER BY AUD_REFERENCIAS.NRO_REFERENCIA ASC SELECT CATEGORIA,NRO_REFERENCIA,DESCRIPCION FROM #TEMP_REFERENCIAS WHERE FILA>0 AND FILA<=5")
            ElseIf radBusqueda.SelectedValue = "NRO_REFERENCIA" Then
                calcularPaginasNroReferencia()
                If totalPaginasBusqueda = 0 Then
                    GridViewData.Visible = False
                    Exit Sub
                Else
                    GridViewData.Visible = True
                    txtError.Text = ""
                End If
                unaTablaTemporalDeBusqueda.getDataSet("CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[NRO_REFERENCIA] [varchar](20) NULL,[DESCRIPCION] [varchar] (80) NOT NULL,[CATEGORIA] [varchar](1) NOT NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (NRO_REFERENCIA,DESCRIPCION,CATEGORIA)SELECT AUD_REFERENCIAS.NRO_REFERENCIA,AUD_REFERENCIAS.DESCRIPCION, AUD_CATEGORIAS.CODIGO FROM AUD_REFERENCIAS INNER JOIN AUD_CATEGORIAS ON AUD_REFERENCIAS.ID_CATEGORIA=AUD_CATEGORIAS.ID WHERE AUD_REFERENCIAS.NRO_REFERENCIA LIKE'" & Trim(txtBusqueda.Text) & "%' ORDER BY AUD_REFERENCIAS.NRO_REFERENCIA ASC SELECT CATEGORIA,NRO_REFERENCIA,DESCRIPCION FROM #TEMP_REFERENCIAS WHERE FILA>0 AND FILA<=5")
            ElseIf radBusqueda.SelectedValue = "PALABRA_CLAVE" Then
                calcularPaginasPalabraClave()
                If totalPaginasBusqueda = 0 Then
                    GridViewData.Visible = False
                    Exit Sub
                Else
                    GridViewData.Visible = True
                    txtError.Text = ""
                End If
                unaTablaTemporalDeBusqueda.getDataSet("CREATE TABLE [dbo].[#TEMP_REFERENCIAS]([FILA] [int] IDENTITY(1,1) NOT NULL,[NRO_REFERENCIA] [varchar](20) NULL,[DESCRIPCION] [varchar] (80) NOT NULL,[CATEGORIA] [varchar](1) NOT NULL) ON [PRIMARY] INSERT INTO #TEMP_REFERENCIAS (NRO_REFERENCIA,DESCRIPCION,CATEGORIA)SELECT AUD_REFERENCIAS.NRO_REFERENCIA,AUD_REFERENCIAS.DESCRIPCION, AUD_CATEGORIAS.CODIGO FROM AUD_REFERENCIAS INNER JOIN AUD_CATEGORIAS ON AUD_REFERENCIAS.ID_CATEGORIA=AUD_CATEGORIAS.ID WHERE AUD_REFERENCIAS.DESCRIPCION LIKE '%" & txtBusqueda.Text & "%' ORDER BY AUD_REFERENCIAS.NRO_REFERENCIA ASC SELECT CATEGORIA,NRO_REFERENCIA,DESCRIPCION FROM #TEMP_REFERENCIAS WHERE FILA>0 AND FILA<=5 ORDER BY LEN(NRO_REFERENCIA),NRO_REFERENCIA ASC")
            End If
            unaTablaTemporalDeBusqueda.fillGridView(GridViewData)
            paginaActualBusqueda = 1
            hideNextOrPrevious()
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        unaTablaTemporalDeBusqueda.setConnectionString(unConnectionString)
        txtBusqueda.Focus()
        btnCancelar.Attributes.Add("onclick", "javascript:unloadPage()")
        hideNextOrPrevious()
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnSearch.Click
        buscar()
        GridViewData.SelectedIndex = -1
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

    Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)
        For Each row As GridViewRow In GridViewData.Rows
            Dim lblDescripcion As Label = CType(row.FindControl("lblDescripcion"), Label)
            Dim unString As String = Trim(lblDescripcion.Text)
            If Len(unString) >= 29 Then
                lblDescripcion.Text = Left(unString, 30) + ".."
            End If
            If row.RowType = DataControlRowType.DataRow Then
                row.Attributes.Add("onmouseover", "this.style.cursor='hand'; tempColour = this.style.backgroundColor;fontColor= this.style.color; this.style.backgroundColor='#DC002E';this.style.color='white'")
                row.Attributes.Add("onmouseout", "this.style.backgroundColor=tempColour; this.style.color=fontColor;")
                row.Attributes("onclick") = ClientScript.GetPostBackClientHyperlink(GridViewData, "Select$" & row.DataItemIndex, True)
            End If
        Next
        MyBase.Render(writer)
    End Sub

    Private Sub GridViewData_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewData.RowDataBound
        If Application("nivelUsuario") = "ADMIN" Then
            e.Row.Cells(3).Visible = True
        Else
            e.Row.Cells(3).Visible = False
        End If
    End Sub

    Private Sub GridViewData_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles GridViewData.SelectedIndexChanged
        nroReferenciaToSearch = unaTablaTemporalDeBusqueda.getItem(GridViewData.SelectedIndex, 1)
        codCategoriaToSearch = unaTablaTemporalDeBusqueda.getItem(GridViewData.SelectedIndex, 0)
        descripcionToSearch = Trim(unaTablaTemporalDeBusqueda.getItem(GridViewData.SelectedIndex, 2))
        Dim unaTablaTemp As TablaSQL = New TablaSQL
        unaTablaTemp.setConnectionString(unConnectionString)
        unaTablaTemp.getDataSet("SELECT ID FROM AUD_CATEGORIAS WHERE CODIGO='" & codCategoriaToSearch & "'")
        idCategoriaToSearch = CInt(unaTablaTemp.getItem(0, 0))
        unaTablaTemp.getDataSet("SELECT ID FROM AUD_REFERENCIAS WHERE NRO_REFERENCIA='" & nroReferenciaToSearch & "'")
        idReferenciaToSearch = CInt(unaTablaTemp.getItem(0, 0))
        BusquedaMode = True
        Response.Write("<script>opener.location.href='http://normasymetodos.com/citroen.ar/Auditoria/auditoria.aspx';</script>")
        'Response.Write("<script>opener.location.reload();</script>")
        Response.Write("<script>window.close();</script>")
    End Sub
End Class