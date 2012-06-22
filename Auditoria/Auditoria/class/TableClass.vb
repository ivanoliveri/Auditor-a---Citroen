Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class TableClass
    Public connectionString As String
    Public dataSet As DataSet
    Public tableName As String
    Public Sub setConnectionString(ByVal unConnectionString As String)
        connectionString = unConnectionString
    End Sub
    Function getRowsCount()
        Return dataSet.Tables(0).Rows.Count
    End Function
    'Devuelve el valor de un Item determinado
    Function getItem(ByVal unaFila As Integer, ByVal unaColumna As Integer)
        Return dataSet.Tables(0).Rows(unaFila).Item(unaColumna).ToString()
    End Function
    Public Sub setTableName(ByVal unNombre As String)
        tableName = unNombre
    End Sub
    Public Sub fillGridView(ByRef unGridView As Object)
        unGridView.DataSource = dataSet.Tables(0)
        unGridView.DataBind()
    End Sub
    Public Sub getDataSetTop()

    End Sub
End Class

Public Class TablaAccess
    Inherits TableClass
    Private fileName As String
    Public Sub getData(ByVal unQuery As String)
        Dim unaConeccion As OleDbConnection = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & CurDir() & "\" & fileName)
        unaConeccion.Open()
        Dim unDataSet As DataSet = New Data.DataSet()
        Dim unDataAdapter As OleDbDataAdapter = New OleDbDataAdapter(unQuery, unaConeccion)
        Dim commandBuilder As New OleDbCommandBuilder(unDataAdapter)
        unDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey
        Try
            unDataAdapter.Fill(unDataSet, Me.tableName)
        Catch ex As Exception
            MsgBox("La base de datos seleccionada no contiene una tabla con el nombre " & Me.tableName & " o no respeta la estructura de datos por default.", , "Error al llenar el DataAdapter")
        End Try
    End Sub
    Public Sub setFileName(ByVal unFileName As String)
        fileName = unFileName
    End Sub
End Class

Public Class TablaExcel
    Inherits TableClass
    Private fileName As String
    Public Sub getDataSet(ByVal unQuery As String)
        Dim unaConeccion As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties = 'Excel 8.0'; Data Source= " & CurDir() & "\" & fileName & ";")
        Dim MiAdaptador As New OleDbDataAdapter(unQuery, unaConeccion)
        Dim unDataSet As New DataSet()
        MiAdaptador.Fill(unDataSet)
        dataSet = unDataSet
    End Sub
    Public Sub setFileName(ByVal unFileName As String)
        fileName = unFileName
    End Sub
    Public Sub execQuery(ByVal unQuery As String)
        Dim unaConeccion As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties = 'Excel 8.0'; Data Source= " & CurDir() & "\" & fileName & ";")
        unaConeccion.Open()
        Dim unaSentencia As New OleDbCommand(unQuery, unaConeccion)
        unaSentencia.ExecuteNonQuery()
        unaConeccion.Close()
    End Sub
End Class

Public Class TablaSQL
    Inherits TableClass
    'Devuelve todos los registros de la tabla en cuestión. Primero debe settiar el tableName
    Public Sub getAllDataSet()
        Me.getDataSet("SELECT * FROM " & tableName)
    End Sub
    'Ejecuta un query y guarda el resultado en el dataset
    Public Sub getDataSet(unQuery As String)
        Dim unDataSet As New DataSet
        Try
            Dim objConnection As New SqlConnection(connectionString)
            objConnection.Open()
            Dim objCommand As New SqlCommand(unQuery, objConnection)
            Dim objDataAdapter As New SqlDataAdapter(objCommand)
            objDataAdapter.Fill(unDataSet, "tabla")
            objDataAdapter = Nothing
            objCommand = Nothing
            objConnection.Close()
            objConnection = Nothing
        Catch ex As Exception

        End Try
        dataSet = unDataSet
    End Sub
    'Sirve para ejecutar un query directamente en la base de datos provocando cambios (INSERT/UPDATE/DELETE)
    Public Sub execQuery(ByVal unQuery As String)
        Dim unaConeccion As New SqlConnection(connectionString)
        unaConeccion.Open()
        Dim unaSentencia As New SqlCommand(unQuery, unaConeccion)
        unaSentencia.ExecuteNonQuery()
        unaConeccion.Close()
    End Sub
    Public Sub deleteById(ByVal unId As Integer)
        Me.execQuery("DELETE FROM " & tableName & " WHERE ID=" & unId)
    End Sub
End Class

Public Class TablaClientes
    Inherits TablaSQL
    Public Sub New()
        Me.setConnectionString("Server=IVAN-PC;Database=Db_GeoConnect;Trusted_Connection=True;")
        Me.setTableName("db_Geo.CLIENTES")
    End Sub
    Public Sub insertClient(ByVal unCuit As String, ByVal unDoc As String, ByVal unTitular As String, ByVal unaProvincia As String, ByVal unaLocalidad As String, ByVal unDepartamento As String)
        Me.getDataSet("SELECT ID FROM db_geo.PROVINCIAS WHERE NOM_PROVINCIA = '" & Trim(unaProvincia) & "'")
        Dim idProvincia As Integer = CInt(Me.getItem(0, 0))
        Me.getDataSet("SELECT ID FROM db_geo.LOCALIDADES WHERE NOM_LOCALIDAD LIKE '%" & Trim(unaLocalidad) & "%'")
        Dim idLocalidad As Integer = CInt(Me.getItem(0, 0))
        Me.getDataSet("SELECT ID FROM db_geo.DEPARTAMENTOS WHERE NOM_DEPARTAMENTO ='" & Trim(unDepartamento) & "'")
        Dim idDepartamento As Integer = CInt(Me.getItem(0, 0))
        Me.execQuery("INSERT INTO db_geo.CLIENTES VALUES('" & unCuit & "','" & unDoc & "','" & Trim(unTitular) & "'," & idProvincia & "," & idLocalidad & "," & idDepartamento & ")")
    End Sub
End Class

Public Class TablaVins
    Inherits TablaSQL
    Public Sub New()
        Me.setConnectionString("Server=IVAN-PC;Database=Db_GeoConnect;Trusted_Connection=True;")
        Me.setTableName("db_Geo.VINS")
    End Sub
    Public Sub insertVin(ByVal unIdCliente As Integer, ByVal unDominio As String, ByVal unVin As String, ByVal unCodigoDeConcesionaria As String, ByVal unNumMotor As String, ByVal unModelo As String, ByVal unaDescripcionDeModelo As String)
        Me.getDataSet("SELECT COUNT(*) FROM db_geo.VINS WHERE VIN='" & unVin & "' OR DOMINIO='" & unDominio & "'")
        If CInt(Me.getItem(0, 0)) = 0 Then
            Me.execQuery("INSERT INTO db_geo.VINS VALUES(" & unIdCliente & ",'" & unVin & "','" & unDominio & "','','','" & unCodigoDeConcesionaria & "','" & unNumMotor & "',NULL,NULL,NULL,NULL,'" & unaDescripcionDeModelo & "')")
        End If
    End Sub
End Class

Public Class TablaClienteContacto
    Inherits TablaSQL
    Public Sub New()
        Me.setConnectionString("Server=IVAN-PC;Database=Db_GeoConnect;Trusted_Connection=True;")
        Me.setTableName("db_Geo.CLIENTE_CONTACTO")
    End Sub
    Public Sub insertContact_Client(ByVal unId As Integer, ByVal unaDireccion As String, ByVal unEmail As String, ByVal unTelefono As String)
        If Trim(unaDireccion) <> "" Then
            'Verifico que la dirección no contenga el caracter ', porque traería un error cuando haga LIKE '%'%'
            If InStr(Trim(unaDireccion), "'", CompareMethod.Binary) = 0 Then
                Me.getDataSet("SELECT ID FROM db_geo.TIPO_CONTACTO WHERE DESCRIPCION LIKE '%DIRECCION%'")
                Me.execQuery("INSERT INTO db_geo.CLIENTE_CONTACTO VALUES(" & unId & ",'" & unaDireccion & "','" & Me.getItem(0, 0) & "')")
            End If
        End If
        If Trim(unEmail) <> "" Then
            Me.getDataSet("SELECT ID FROM db_geo.TIPO_CONTACTO WHERE DESCRIPCION LIKE '%EMAIL%'")
            Me.execQuery("INSERT INTO db_geo.CLIENTE_CONTACTO VALUES(" & unId & ",'" & unaDireccion & "','" & Me.getItem(0, 0) & "')")
        End If
        If Trim(unTelefono) <> "" Then
            Me.getDataSet("SELECT ID FROM db_geo.TIPO_CONTACTO WHERE DESCRIPCION LIKE '%TELEFONO%'")
            Me.execQuery("INSERT INTO db_geo.CLIENTE_CONTACTO VALUES(" & unId & ",'" & unaDireccion & "','" & Me.getItem(0, 0) & "')")
        End If
    End Sub
End Class