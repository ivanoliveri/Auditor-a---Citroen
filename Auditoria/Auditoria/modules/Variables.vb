Imports Auditoria.TablaAccess
Module Variables
    Public unasReferencias As TablaSQL = New TablaSQL
    Public unConnectionString As String = "Data Source=200.110.156.10;Initial Catalog=Metodologias;User ID=admmet;Password=JHD&8T63KXD8()2"
    Public lastPage, unIdSeleccionado, paginaActualMain, totalPaginasMain,paginaActualBusqueda,totalPaginasBusqueda As Integer
    Public lastCat As String
    Public primerIngresoMain As Boolean = True
    Public unNumeroDeCE As Integer = 0
    Public unNumeroDeSucursal As Integer = 0
    Public unPeriodoAnterior, unPeriodoActual As String
    Public agregoOedito, primerIngresoBusqueda As Boolean
End Module
