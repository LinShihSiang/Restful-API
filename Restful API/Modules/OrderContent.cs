namespace Restful_API.Modules
{
    public class OrderContent
    {
        public string Title { get; set; }
        public string ReporterDescription { get; set; }
        public string MaintenanceDescription { get; set; }

        public override string ToString()
        {
            return $"Title={Title}, ReporterDesc={ReporterDescription}, MaintenanceDesc={MaintenanceDescription}";
        }
    }
}