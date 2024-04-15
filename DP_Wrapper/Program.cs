using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;
using DPFP.Capture;
using DPFP;
using static DP_Wrapper.Wrapper.DPWrapper;
using DP_Wrapper.Wrapper;
using DP_Wrapper.Class_s;

namespace DP_Wrapper
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

   
            var services = new ServiceCollection();
            ConfigureServices(services);

            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                var mainForm = serviceProvider.GetRequiredService<Form1>();
                Application.Run(mainForm);
            }
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Registro de la fábrica para crear CaptureWrapper
            services.AddScoped<Func<ProcessWrapperDP, CaptureWrapper>>(provider => (proceso) => DPWrapper.CreateCaptureWrapper(proceso));
            // Registro de las clases que usaran la captura de huella
            services.AddScoped<CapturadorDP>();
            services.AddScoped<ValidadorDP>();
            services.AddScoped<Form1>();
        }

    }
}
