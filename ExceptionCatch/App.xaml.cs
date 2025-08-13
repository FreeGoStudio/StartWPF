using System.IO;
using System.Windows;
using System.Windows.Threading;

namespace ExceptionCatch;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        // 捕获UI线程未处理异常
        this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        // 捕获非UI线程未处理异常
        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        // 捕获Task未观察到的异常
        TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

        base.OnStartup(e);
    }

    private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        LogException(e.Exception, "UI线程异常");
        e.Handled = true; // 防止程序直接崩溃
    }

    private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        LogException(e.ExceptionObject as Exception, "非UI线程异常");
    }

    private void TaskScheduler_UnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
    {
        LogException(e.Exception, "Task异常");
        e.SetObserved();
    }

    private void LogException(Exception? ex, string type)
    {
        try
        {
            string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "error.log");
            File.AppendAllText(logPath,
                $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {type}: {ex}\n");
        }
        catch
        {
            // 忽略日志写入异常，防止递归崩溃
        }
    }
}

