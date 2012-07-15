Imports Auditoria.TableClass
Public Class ingreso
    Inherits System.Web.UI.Page
    Private unContadorPrimerCE As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Application("unPeriodoAnterior") = "2011-2"
        Application("unPeriodoActual") = "2012-1"
        Application("busquedaMode") = False
        If Trim(dropConcesionaria.Text) = "" Then
            Application.Lock()
            Application("unNumeroDeCE") = 0
            Application("unNumeroDeSucursal") = 0
            Application("ultimoQuery") = ""
            Application.UnLock()
            cargarConcesionarias()
            dropSucursal.Visible = False
            Label1.Visible = False
        End If
        If dropConcesionaria.Text = "Seleccione CE" Or dropConcesionaria.Text = "" Then Exit Sub
        Application("unNumeroDeCE") = CInt(dropConcesionaria.Text)
        If dropSucursal.Text <> "Seleccione SUC" Then
            If Trim(dropSucursal.Text) = "" Then
                Application("unNumeroDeSucursal") = 0
            Else
                Application("unNumeroDeSucursal") = CInt(dropSucursal.Text)
            End If
        End If
    End Sub

    Protected Sub dropConcesionaria_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dropConcesionaria.SelectedIndexChanged
        cargarSucursales()
    End Sub

    Protected Sub cargarSucursales()
        Dim unaTablaDeRelevamientos As TablaSQL = New TablaSQL
        unaTablaDeRelevamientos.setConnectionString(unConnectionStringDeBasesComunes)
        unaTablaDeRelevamientos.getDataSet("SELECT Suc FROM CONCESIONARIAS WHERE (CE=" & Application("unNumeroDeCE") & ") AND (TS IN (7,2,9,4)) ORDER BY Suc")
        If Trim(unaTablaDeRelevamientos.getItem(0, 0)) = "" And unaTablaDeRelevamientos.getRowsCount = 1 Then
            Application("unNumeroDeSucursal") = 0
            Response.Redirect("auditoria.aspx")
        Else
            dropSucursal.Visible = True
            Label1.Visible = True
            dropSucursal.DataSource = unaTablaDeRelevamientos.dataSet.Tables(0)
            dropSucursal.DataTextField = "Suc"
            dropSucursal.DataValueField = "Suc"
            dropSucursal.DataBind()
            dropSucursal.Items.Insert(0, "Seleccione SUC")
            If Trim(dropSucursal.Items.Item(1).Text) = "" Then dropSucursal.Items.Item(1).Text = "000"
            Application("unNumeroDeSucursal") = -1
        End If
    End Sub

    Protected Sub cargarConcesionarias()
        Dim unaTablaDeRelevamientos As TablaSQL = New TablaSQL
        unaTablaDeRelevamientos.setConnectionString(unConnectionStringDeBasesComunes)
        unaTablaDeRelevamientos.getDataSet("SELECT DISTINCT(CE) FROM CONCESIONARIAS WHERE (CE<3800) AND (TS IN (7,2,9,4)) ORDER BY CE")
        dropConcesionaria.DataSource = unaTablaDeRelevamientos.dataSet.Tables(0)
        dropConcesionaria.DataTextField = "CE"
        dropConcesionaria.DataValueField = "CE"
        dropConcesionaria.DataBind()
        dropConcesionaria.Items.Insert(0, "Seleccione CE")
        dropSucursal.Items.Insert(0, "Seleccione SUC")
        Application("unNumeroDeCE") = -1
    End Sub

    Private Sub dropSucursal_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles dropSucursal.SelectedIndexChanged
        Application("ultimoQuery") = ""
        Response.Redirect("auditoria.aspx")
    End Sub
End Class