namespace Comun.Tareas
{
    public class CriteriosTareas
    {
        public bool ConsultarSoloMisTareas { get; set; } =  false;
        public bool ConsultarTareasPendientes { get; set; } =  false;
        public bool ConsultarTareasFinalizadas { get; set; } = false;
        public bool OrdenarConsultaPorFechaVencimiento { get; set; } = false;
    }
}
