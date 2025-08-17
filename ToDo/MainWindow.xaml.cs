using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ToDo;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void TaskTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            var textBox = sender as TextBox;
            if (textBox == null)
            {
                return;
            }

            if (Keyboard.Modifiers == ModifierKeys.Shift)
            {
                Console.WriteLine("Shift换行");
                // Shift+Enter：插入换行
                int caret = textBox.CaretIndex;
                textBox.Text = textBox.Text.Insert(caret, "\n");
                textBox.CaretIndex = caret + 1;
                e.Handled = true;
            }
            else
            {
                Console.WriteLine("提交内容");
                // Enter：提交
                MessageBox.Show("提交内容：" + textBox.Text);
                e.Handled = true; // 阻止默认换行
            }
        }
    }
}