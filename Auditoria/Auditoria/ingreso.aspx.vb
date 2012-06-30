Imports Auditoria.TableClass
Public Class ingreso
    Inherits System.Web.UI.Page
    Private unContadorPrimerCE As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        unPeriodoAnterior = "2011-2"
        unPeriodoActual = "2012-1"
        BusquedaMode = False
        If unNumeroDeCE = 0 Then cargarConcesionarias()
        If dropConcesionaria.Text = "Seleccione CE" Or dropConcesionaria.Text = "" Then Exit Sub
        unNumeroDeCE = CInt(dropConcesionaria.Text)
        If dropSucursal.Text <> "Seleccione SUC" Then
            If Trim(dropSucursal.Text) = "" Then
                unNumeroDeSucursal = 0
            Else
                unNumeroDeSucursal = CInt(dropSucursal.Text)
            End If
        End If

    End Sub

    Protected Sub dropConcesionaria_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dropConcesionaria.SelectedIndexChanged
        cargarSucursales()
    End Sub

    Protected Sub cargarSucursales()
        Dim unaTablaDeRelevamientos As TablaSQL = New TablaSQL
        unaTablaDeRelevamientos.setConnectionString(unConnectionStringDeBasesComunes)
        unaTablaDeRelevamientos.getDataSet("SELECT Suc FROM CONCESIONARIAS WHERE (CE=" & unNumeroDeCE & ") AND (TS IN (7,2,9,4)) ORDER BY CE")
        dropSucursal.DataSource = unaTablaDeRelevamientos.dataSet.Tables(0)
        dropSucursal.DataTextField = "Suc"
        dropSucursal.DataValueField = "Suc"
        dropSucursal.DataBind()
        dropSucursal.Items.Insert(0, "Seleccione SUC")
        If Trim(dropSucursal.Items.Item(1).Text) = "" Then dropSucursal.Items.Item(1).Text = "0"
        unNumeroDeSucursal = -1
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
        unNumeroDeCE = -1
    End Sub

    Private Sub dropSucursal_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles dropSucursal.SelectedIndexChanged
        Response.Redirect("auditoria.aspx")
    End Sub
End Class