using System.IO;
using System.Data;
using FastReport;
using FastReport.Web;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Data.SqlClient;
using FastReport.Data;
using FastReport.Utils;


namespace BlazorApp2.Pages
{
    public partial class Index
    {
        readonly string directory;
        const string DEFAULT_REPORT = "Simple List.frx";
        public Report report1 { get; set; }
        public WebReport UserWebReport { get; set; }



        protected override async Task OnParametersSetAsync()
        {
            base.OnParametersSet();

            // путь до дизайна отчета
            string path =
            Path.Combine(
            directory,
            string.IsNullOrEmpty(ReportName) ? DEFAULT_REPORT : ReportName);

            report1 = Report.FromFile(path);

            // подключение к бд для отчета
            RegisteredObjects.AddConnection(typeof(MsSqlDataConnection));
            MsSqlDataConnection sqlConnection = new MsSqlDataConnection();
            sqlConnection.ConnectionString = "Data Source = .\\SQLEXPRESS; Initial Catalog = dirs; Integrated Security = True; Persist Security Info = False; ";
            sqlConnection.CreateAllTables();
            report1.Report.Dictionary.Connections.Add(sqlConnection);

            // настройка панели инструментов
            ToolbarSettings toolbarSettings1 = new ToolbarSettings()
            {
                IconColor = IconColors.White,
                Position = Positions.Left,
            };
            // создание веботчета
            UserWebReport = new WebReport
            {
                // макет отчёта
                Report = report1,
                // панель инструментов
                Toolbar = toolbarSettings1,
            };
            // получение всех линий всех заводов (id линии - названи линии - id завода)
            PlantLineList1 = await _db.GetLines();
        }

        public Index()
        {
            // путь до папки с моделями отчетов
            directory = Path.Combine(
            Directory.GetCurrentDirectory(),
            Path.Combine("Reports"));
        }
    }
}
