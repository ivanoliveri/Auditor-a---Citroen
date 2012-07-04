Imports Auditoria.TablaAccess
Module Variables
    Public unasReferencias As TablaSQL = New TablaSQL
    Public unaTablaTemporalDeBusqueda As TablaSQL = New TablaSQL
    '  Public unConnectionStringDeBasesComunes = "Provider=SQLOLEDB;Server=200.110.156.10;Database=BasesComunes;UID=GeneralManager;PWD=general34manager"
    Public unConnectionStringDeBasesComunes As String = "Data Source=200.110.156.10;Initial Catalog=BasesComunes;User ID=GeneralManager;Password=general34manager"
    Public unConnectionString As String = "Data Source=200.110.156.10;Initial Catalog=Metodologias;User ID=admmet;Password=JHD&8T63KXD8()2"
    Public lastPage, unIdSeleccionado, paginaActualMain, totalPaginasMain,paginaActualBusqueda,totalPaginasBusqueda As Integer
    Public lastCat, popupCat As String
    ' Public seleccionoCE As Boolean
    ' Public unNumeroDeCE As Integer = 0
    'Public unNumeroDeSucursal As Integer = 0
    Public idCategoriaToSearch, idReferenciaToSearch As Integer
    Public unPeriodoAnterior, unPeriodoActual, ultimoQuery, nroReferenciaToSearch, codCategoriaToSearch, descripcionToSearch As String
    Public agregoOedito, BusquedaMode, agregoDesdePopup As Boolean
End Module
