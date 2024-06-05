namespace Restful_API.Modules
{
    public class OrderConfirmed
    {
        public bool ReporterConfimed { get; set; }
        public bool MaintenanceConfirmed { get; set; }
        public bool HeadquartersConfirmed { get; set; }

        public bool CanCompleted()
        {
            return ReporterConfimed && MaintenanceConfirmed && HeadquartersConfirmed;
        }

        public override string ToString()
        {
            return $"R|M|H={ReporterConfimed}|{MaintenanceConfirmed}|{HeadquartersConfirmed}";
        }
    }
}
