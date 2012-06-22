Imports Auditoria.TableClass
Public Class ingreso
    Inherits System.Web.UI.Page
    Private unContadorPrimerCE As Integer = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        unPeriodoAnterior = "2011-2"
        unPeriodoActual = "2012-2"
        If unNumeroDeCE = 0 Then cargarConcesionarias()
        If dropConcesionaria.Text = "Seleccione CE" Then Exit Sub
        unNumeroDeCE = CInt(dropConcesionaria.Text)
        If dropSucursal.Text <> "Seleccione SUC" Then unNumeroDeSucursal = CInt(dropSucursal.Text)
    End Sub

    Protected Sub dropConcesionaria_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dropConcesionaria.SelectedIndexChanged
        cargarSucursales()
    End Sub
    Protected Sub cargarSucursales()
        Dim unaTablaDeRelevamientos As TablaSQL = New TablaSQL
        unaTablaDeRelevamientos.setConnectionString(unConnectionString)
        unaTablaDeRelevamientos.getDataSet("SELECT DISTINCT(SUCURSAL) FROM AUD_RELEVAMIENTOS WHERE CE=" & unNumeroDeCE & " ORDER BY SUCURSAL ASC")
        dropSucursal.DataSource = unaTablaDeRelevamientos.dataSet.Tables(0)
        dropSucursal.DataTextField = "SUCURSAL"
        dropSucursal.DataValueField = "SUCURSAL"
        dropSucursal.DataBind()
        dropSucursal.Items.Insert(0, "Seleccione SUC")
        unNumeroDeSucursal = -1
    End Sub
    Protected Sub cargarConcesionarias()
        Dim unaTablaDeRelevamientos As TablaSQL = New TablaSQL
        unaTablaDeRelevamientos.setConnectionString(unConnectionString)
        unaTablaDeRelevamientos.getDataSet("SELECT DISTINCT(CE) FROM AUD_RELEVAMIENTOS ORDER BY CE ASC")
        dropConcesionaria.DataSource = unaTablaDeRelevamientos.dataSet.Tables(0)
        dropConcesionaria.DataTextField = "CE"
        dropConcesionaria.DataValueField = "CE"
        dropConcesionaria.DataBind()
        dropConcesionaria.Items.Insert(0, "Seleccione CE")
        dropSucursal.Items.Insert(0, "Seleccione SUC")
        unNumeroDeCE = -1
    End Sub

    Private Sub dropSucursal_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles dropSucursal.SelectedIndexChanged
        ' Dim unaTablaDeAuditoria As TablaSQL = New TablaSQL
        ' unaTablaDeAuditoria.setConnectionString(unConnectionString)
        '  unaTablaDeAuditoria.getDataSet("SELECT COUNT(*) FROM AUD_RELEVAMIENTOS WHERE CE=" & unNumeroDeCE & " AND SUCURSAL=" & unNumeroDeSucursal & " AND PERIODO='" & unPeriodoActual & "'")
        '  If unaTablaDeAuditoria.getItem(0, 0) = "0" Then
        '        Dim unContador As Integer = 1
        '  Do While unContador <= unaTablaDeAuditoria.getRowsCount()
        '       'unaTablaDeAuditoria.execQuery("INSERT INTO AUD_RELEVAMIENTOS VALUES()")
        'unContador += 1
        '   Loop
        '    End If
        Response.Redirect("auditoria.aspx")
    End Sub
End Class