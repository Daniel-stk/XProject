using Caroto.Services;
using Caroto.Tools;
using Gateway;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Caroto.RecurringTasks.Tasks
{
    public class GenerateProgrammingTask : RecurringTask
    {
        private static readonly Lazy<GenerateProgrammingTask> _instance = new Lazy<GenerateProgrammingTask>(() => new GenerateProgrammingTask());
        private ProgrammingManagerService _service;
        private GenerateProgrammingTask() : base()
        {
            _service = ProgrammingManagerService.Instance;
        }

        public static GenerateProgrammingTask Instance { get { return _instance.Value; } }

        public async Task CreateProgrammingFile(CancellationToken token)
        {
            try
            {
                Console.WriteLine("Descargando programación"); 
                var programming = await _service.CreateProgrammingInformation(Properties.Settings.Default.ApiKey, Properties.Settings.Default.Identidad);
                JsonFileHandler.WriteJsonFile(CarotoSettings.Default.ProgrammingFolder + @"\Programming.json",programming);
                await _bufferBlock.SendAsync("Programming Downloaded");
                Console.WriteLine("Descarga de programación finalizada");
            }
            catch(Exception ex)
            {
#if DEBUG
                FileLogger.Instance.Log("Origen -" + GetType().ToString() + " Tipo - " + ex.GetType().ToString() + " Mensaje - " + ex.Message + " Fecha - " + DateTime.Now.ToString(), LogType.Error);
#endif
            }
        }
    }
}
