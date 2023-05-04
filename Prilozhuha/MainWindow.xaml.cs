using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Prilozhuha
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += Window_Loaded;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lb_disc.Items.Clear();
            DriveInfo[] di = DriveInfo.GetDrives();
            foreach (DriveInfo d in di)
                lb_disc.Items.Add(d.Name);
        }

        private void lb_disc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lb_folder.Items.Clear();
            lb_file.Items.Clear();
            var select = lb_disc.SelectedItem as string;
            var directories = Directory.GetDirectories(select);

            foreach (var d in directories)
                lb_folder.Items.Add(d);

            foreach (var d in Directory.GetFiles(select))
                lb_file.Items.Add(d);
        }

        private void lb_folder_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var select = lb_folder.SelectedItem as string;
                if (select != null)
                {
                    RunContentDir(select);
                    RunContentFile(select);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void mi_remove_Click(object sender, RoutedEventArgs e)
        {
            var select = lb_file.SelectedItem as string;
            var directory = System.IO.Path.GetDirectoryName(select);

            var r = MessageBox.Show($"Вы действительно хотите удалить файл {select}?", "Удалить файл", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (r == MessageBoxResult.Yes)
            {
                File.Delete(select);
                MessageBox.Show($"Файл {select} удалён.");
                RunContentFile(directory);
            }
        }

        #region Обновление списков
        private void RunContentDir(string select)
        {
            lb_folder.Items.Clear();
            var directories = Directory.GetDirectories(select);

            foreach (var d in directories)
                lb_folder.Items.Add(d);
        }

        private void RunContentFile(string? directory)
        {
            try
            {
                lb_file.Items.Clear();
                foreach (var d in Directory.GetFiles(directory))
                    lb_file.Items.Add(d);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            #endregion
        }
    }
}
