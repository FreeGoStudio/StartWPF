using System.Windows;

namespace ExceptionCatch;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void ThrowExceptionButton_Click(object sender, RoutedEventArgs e)
    {
        Console.WriteLine("测试-未捕获异常按钮");

        throw new System.Exception("这是一个测试异常");
    }
}