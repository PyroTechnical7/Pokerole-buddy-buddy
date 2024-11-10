using System.Diagnostics;

namespace PokeroleBuddyHelper
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }

        private void CurrentDomain_UnhandledException(object? sender, UnhandledExceptionEventArgs e)
        {
            // Handle the exception
            var exception = e.ExceptionObject as Exception;
            if (exception != null)
            {
                // Log or display the exception
                Debug.WriteLine($"Unhandled exception: {exception.Message}");
            }
        }

        private void TaskScheduler_UnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
        {
            // Handle the exception
            e.SetObserved();
            Debug.WriteLine($"Unobserved task exception: {e.Exception.Message}");
        }
    }
}
