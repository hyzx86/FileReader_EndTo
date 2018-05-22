using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
namespace FileReader_EndTo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<string, string> envDic = new Dictionary<string, string>();
        public MainWindow()
        {
            InitializeComponent();
            envDic.Add("PROD", "\\\\k70svbpmapp\\bpmlog$");
            envDic.Add("UAT", "\\\\k70svbpmuat\\bpmlog$");
            envDic.Add("DEV", "\\\\CCNSV-BPMDEV\\bpmlog$");
        }
        private string remotePath = "";

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int contentLength = 0;
            if (!int.TryParse(txtlenth.Text, out contentLength))
            {
                txtContent.Text = "Wrong length !";
                return;
            }
            remotePath = envDic[((ComboBoxItem)cb1.SelectedValue).Content.ToString()];
            string filePath = $"{remotePath}\\server.log";
             using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                byte[] buffer = new byte[1024 * 100];
                fs.Position = fs.Length - buffer.Length;
              
                fs.Read(buffer, 0, buffer.Length);
                var content = Encoding.Default.GetString(buffer);
                txtContent.Text = content;

            }

        }
    }
}
