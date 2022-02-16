
using Newtonsoft.Json;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Labor01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Feljegyzes> feljegyzesek;
        public MainWindow()
        {
            InitializeComponent();
            feljegyzesek = new List<Feljegyzes>();
            if (File.Exists("data.json"))
            {
                feljegyzesek = JsonConvert.DeserializeObject<Feljegyzes[]>(File.ReadAllText("data.json")).ToList();
                feljegyzesek.ForEach(x => lsbox.Items.Add(x.feljegyzesneve));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var p = new Feljegyzes("", tb_feljegyzes.Text);
            lsbox.Items.Add(tb_feljegyzes.Text);
            feljegyzesek.Add(p);
        }

        private void lsbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lsbox.SelectedItem != null)
            {
                var f = feljegyzesek.Where(x => x.feljegyzesneve == lsbox.SelectedItem.ToString()).FirstOrDefault();
                tb_wraptext.Text = f.text;
            }

        }

        private void tb_wraptext_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (lsbox.SelectedItem != null)
            {
                var f = feljegyzesek.Where(x => x.feljegyzesneve == lsbox.SelectedItem.ToString()).FirstOrDefault();
                f.text = tb_wraptext.Text;
                string jsonData = JsonConvert.SerializeObject(feljegyzesek);
                File.WriteAllText("data.json", jsonData);
            }
        }


        private void lsbox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lsbox.SelectedItem != null)
            {
                var f = feljegyzesek.Where(x => x.feljegyzesneve == lsbox.SelectedItem.ToString()).FirstOrDefault();
                lsbox.Items.Remove(f.feljegyzesneve);
                feljegyzesek.Remove(f);
                string jsonData = JsonConvert.SerializeObject(feljegyzesek);
                File.WriteAllText("data.json", jsonData);
            }
        }
    }

    internal class Feljegyzes
    {
        public string feljegyzesneve;
        public string text;

        public Feljegyzes(string text, string feljegyzesneve)
        {
            this.text = text;
            this.feljegyzesneve = feljegyzesneve;
        }
    }
}
