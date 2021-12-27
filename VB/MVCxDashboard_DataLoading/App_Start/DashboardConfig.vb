Imports System.Threading
Imports System.Web.Routing
Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardWeb
Imports DevExpress.DashboardWeb.Mvc

Public Class DashboardConfig
    Public Shared Sub RegisterService(ByVal routes As RouteCollection)
        routes.MapDashboardRoute("dashboardControl", "DefaultDashboard")

        DashboardConfigurator.Default.SetDashboardStorage(New DashboardFileStorage("~/App_Data/"))

        Dim dataSourceStorage = New DataSourceInMemoryStorage()
        DashboardConfigurator.Default.SetDataSourceStorage(dataSourceStorage)
        Dim objDataSource As New DashboardObjectDataSource("Object Data Source")
        objDataSource.DataId = "odsSales"
        objDataSource.DataSource = GetType(SalesPersonData)
        dataSourceStorage.RegisterDataSource("objDataSource", objDataSource.SaveToXml())

        AddHandler DashboardConfigurator.Default.DataLoading, AddressOf Default_DataLoading
    End Sub

    Private Shared Sub Default_DataLoading(ByVal sender As Object, ByVal e As DataLoadingWebEventArgs)
        If e.DataId = "odsSales" Then
            e.Data = CreateData()
        End If
    End Sub

    Public Shared Function CreateData() As List(Of SalesPersonData)
        Dim data As New List(Of SalesPersonData)()
        Dim salesPersons() As String = {"Andrew Fuller", "Michael Suyama",
                                            "Robert King", "Nancy Davolio",
                                            "Margaret Peacock", "Laura Callahan",
                                            "Steven Buchanan", "Janet Leverling"}

        For i As Integer = 0 To 99
            Dim record As New SalesPersonData()
            Dim seed As Long = CLng(Date.Now.Ticks) And &HFFFF
            record.SalesPerson = salesPersons((New Random(CInt(seed))).Next(0, salesPersons.Length))
            record.Quantity = (New Random(CInt(seed))).Next(0, 100)
            data.Add(record)
            Thread.Sleep(3)
        Next i
        Return data
    End Function
End Class

Public Class SalesPersonData
    Public Property SalesPerson() As String
    Public Property Quantity() As Integer
End Class