using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;
using DevExpress.DashboardWeb.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.Routing;

public class DashboardConfig {
    public static void RegisterService(RouteCollection routes) {
        routes.MapDashboardRoute("dashboardControl", "DefaultDashboard");

        DashboardConfigurator.Default.SetDashboardStorage(new DashboardFileStorage(@"~/App_Data/"));

        var dataSourceStorage = new DataSourceInMemoryStorage();
        DashboardConfigurator.Default.SetDataSourceStorage(dataSourceStorage);
        DashboardObjectDataSource objDataSource = new DashboardObjectDataSource("Object Data Source");
        objDataSource.DataSource = typeof(SalesPersonData);
        dataSourceStorage.RegisterDataSource("objDataSource", objDataSource.SaveToXml());

        DashboardConfigurator.Default.DataLoading += Default_DataLoading;
    }

    private static void Default_DataLoading(object sender, DataLoadingWebEventArgs e) {
        if (e.DataSourceName == "Object Data Source") {
            e.Data = CreateData();
        }
    }

    public static List<SalesPersonData> CreateData() {
        List<SalesPersonData> data = new List<SalesPersonData>();
        string[] salesPersons = { "Andrew Fuller", "Michael Suyama",
                                    "Robert King", "Nancy Davolio",
                                    "Margaret Peacock", "Laura Callahan",
                                    "Steven Buchanan", "Janet Leverling" };

        for (int i = 0; i < 100; i++) {
            SalesPersonData record = new SalesPersonData();
            int seed = (int)DateTime.Now.Ticks & 0x0000FFFF;
            record.SalesPerson = salesPersons[new Random(seed).Next(0, salesPersons.Length)];
            record.Quantity = new Random(seed).Next(0, 100);
            data.Add(record);
            Thread.Sleep(3);
        }
        return data;
    }
}

public class SalesPersonData {
    public string SalesPerson { get; set; }
    public int Quantity { get; set; }
}