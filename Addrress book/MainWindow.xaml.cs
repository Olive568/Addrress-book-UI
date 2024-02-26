using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Addrress_book
{
    public partial class MainWindow : Window
    {
        string selectedItem = "";
        bool adding = false;
        public int selected = 0;
        public List<string[]> Database = new List<string[]>();

        public MainWindow()
        {
            InitializeComponent();
            LoadDataFromCSV();
            PopulateComboBox();
        }
        private void LoadDataFromCSV()
        {
            try
            {
                using (StreamReader sr = new StreamReader("Address.csv"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] splitter = line.Split(',');
                        Database.Add(splitter);
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("Address.csv file not found: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading Address.csv: " + ex.Message);
            }
        }

        private void PopulateComboBox()
        {
            CB.Items.Clear();
            foreach (string[] entry in Database)
            {
                CB.Items.Add(entry[0]);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedItem = CB.SelectedItem?.ToString();
            for (int x = 0; x < Database.Count; x++)
            {
                if (Database[x][0] == selectedItem)
                {
                    Name.Text = Database[x][0];
                    Address.Text = Database[x][1];
                    Phone.Text = Database[x][2];
                    Email.Text = Database[x][3];
                    selected = x;
                }
            }
        }


        private void Button_Remove(object sender, RoutedEventArgs e)
        {
            if (CB.SelectedIndex != -1)
            {
                Database.RemoveAt(selected);
                CB.Items.RemoveAt(selected);
            }
            else
            {
                MessageBox.Show("Please select an item to remove.");
            }
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            adding = true;
            Submit.Margin = new Thickness(545, 346, 0, 0);
            AddBtn.Margin = new Thickness(-281, 341, 0, 0);
            RemoveBtn.Margin = new Thickness(-281, 341, 0, 0);
            UpdateBtn.Margin = new Thickness(-281, 341, 0, 0);
            Name_Copy.Margin = Name.Margin;
            Phone_Copy.Margin = Phone.Margin;
            Address_Copy.Margin = Address.Margin;
            Email_Copy.Margin = Email.Margin;
            Name.Margin = new Thickness(-281, 341, 0, 0);
            Phone.Margin = new Thickness(-281, 341, 0, 0);
            Email.Margin = new Thickness(-281, 341, 0, 0);
            Address.Margin = new Thickness(-281, 341, 0, 0);
        }
        private void Button_Update(object sender, RoutedEventArgs e)
        {
            adding = false;
            Submit.Margin = new Thickness(545, 346, 0, 0);
            AddBtn.Margin = new Thickness(-281, 341, 0, 0);
            RemoveBtn.Margin = new Thickness(-281, 341, 0, 0);
            UpdateBtn.Margin = new Thickness(-281, 341, 0, 0);
        }

        private void Button_Submit(object sender, RoutedEventArgs e)
        {
            if(adding)
            {
                Name.Margin = Name_Copy.Margin;
                Phone.Margin = Phone_Copy.Margin;
                Email.Margin = Email_Copy.Margin;
                Address.Margin = Address_Copy.Margin;
                Name_Copy.Margin = new Thickness(-281, 341, 0, 0);
                Phone_Copy.Margin = new Thickness(-281, 341, 0, 0);
                Address_Copy.Margin = new Thickness(-281, 341, 0, 0);
                Email_Copy.Margin = new Thickness(-281, 341, 0, 0);
            }
            Submit.Margin = new Thickness(-281, 341, 0, 0);
            AddBtn.Margin = new Thickness(463, 346, 0, 0);
            RemoveBtn.Margin = new Thickness(545,346,0, 0);
            UpdateBtn.Margin = new Thickness(622, 346, 0, 0);
            
            if(adding)
            {
                string[] add = new string[4];
                add[0] = Name_Copy.Text;
                add[1] = Address_Copy.Text;
                add[2] = Phone_Copy.Text;
                add[3] = Email_Copy.Text;
                Database.Add(add);
            }
            else if(!adding)
            {
                int index = 0;
                string[] add = new string[4];
                add[0] = Name.Text;
                add[1] = Address.Text;
                add[2] = Phone.Text;
                add[3] = Email.Text;
                for(int x = 0; x < Database.Count; x++)
                {
                    if (Database[x][0] == selectedItem)
                    {
                        index = x; break;
                    }
                }
                Database[index] = add;
            }
            RewriteCSVFile();
            PopulateComboBox();
          
        }
        private void RewriteCSVFile()
        {
            using (StreamWriter sw = new StreamWriter("Address.csv"))
            {
                foreach (string[] entry in Database)
                {
                    for (int y = 0; y < entry.Length; y++)
                    {
                        sw.Write(entry[y]);
                        if (y < entry.Length - 1)
                        {
                            sw.Write(",");
                        }
                    }
                    sw.WriteLine();
                }
            }
        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string newText = textBox.Text;
        }

        private void Address_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string newText = textBox.Text;
        }

        private void Phone_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string newText = textBox.Text;
        }

        private void Email_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string newText = textBox.Text;
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            string namesearch = Search.Text;
            PopulateComboBoxSearch(namesearch);
        }
        private void PopulateComboBoxSearch(string namesearch)
        {
            foreach (string[] data in Database)
            {
                if (!data[0].Contains(namesearch)) 
                {
                    CB.Items.Remove(data[0]);
                }
            }
            if(Search.Text == "")
            {
                PopulateComboBox();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Do you want to exit?", " Yes or No? ", MessageBoxButton.YesNo);
            if(result == MessageBoxResult.Yes)
            {
                Environment.Exit(0);
            }
        }
    }
}