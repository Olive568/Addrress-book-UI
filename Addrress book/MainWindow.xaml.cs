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

namespace Addrress_book
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<string[]> Database = new List<string[]>();
        public MainWindow()
        {
            InitializeComponent();
            using (StreamReader sr = new StreamReader("Address.csv"))
            {
                string[] splitter = new string[4];
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    splitter = line.Split(',');
                    Database.Add(splitter);
                }
            }
            for(int x = 0; x < Database.Count; x++) 
            {
                CB.Items.Add(Database[x][0]);
            }

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Your event handling code here
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Your event handling code here
        }
    }
}
