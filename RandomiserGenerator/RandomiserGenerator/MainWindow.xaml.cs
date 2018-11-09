using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace RandomiserGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random Rand = new Random();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Input_TextChanged(object sender, TextChangedEventArgs e)
        {



            TextBox TxtBx = sender as TextBox;
            string InputText = TxtBx.Text;
            IEnumerable<TextChange> changes = e.Changes;
            foreach (var change in changes)
            {
                updateResult(change,InputText);
            }
            //char[] splitarg = {' '};
            //string[] Words = InputText.Split(splitarg, StringSplitOptions.RemoveEmptyEntries);
            //Outpt.Children.Clear();
            //foreach (string word in Words)
            //{
            //    foreach (char C in word)
            //    {
            //        Label n = new Label();
            //        n.Content = C;
            //        n.Padding = new Thickness(1);
            //        n.FontFamily = RandomFont();
            //        //n.Foreground = RandomFontColor();
            //        n.FontSize = Rand.Next(10, 20);
            //        Console.WriteLine(n.FontFamily.ToString()+" " +n.FontSize);
            //        Outpt.Children.Add(n);
            //    }
            //    Outpt.Children.Add(SpaceBarLabel());
            //}
        }

        private void updateResult(TextChange change,string l)
        {
            AddCreatedData(change, l);
            RemoveDeletedData(change, l);

        }

        private void RemoveDeletedData(TextChange change, string l)
        {
            Outpt.Children.RemoveRange(change.Offset, change.RemovedLength);
        }

        private void AddCreatedData(TextChange change, string l)
        {
            int index = change.Offset;
            int length = change.AddedLength;
            string a = l.Substring(index, length);
            char[] ExplodedString = a.ToCharArray();
            List<Label> NewLabels = new List<Label>();
            foreach (var item in ExplodedString)
            {
                NewLabels.Add(CreateLabel(item));
            }
            NewLabels.Reverse();
            foreach (var item in NewLabels)
            {
                Outpt.Children.Insert(index, item);
            }
        }

        private Label CreateLabel(char item)
        {
            Label NewLabel = new Label
            {
                Content = item,
                Padding = new Thickness(1),
                FontFamily = RandomFont(),
                Foreground = RandomFontColor(),
                FontSize = Rand.Next(10, 20)
            };
            return NewLabel;
        }

        int[] prevcolor = { 1, 0, 0 };
        private Brush RandomFontColor()
        {
            int a = Rand.Next(3);
            int b = Rand.Next(3);
            int c = 4 - a - b;
            byte R = (byte)((a / 2) * 125);
            byte G = (byte)((b / 2) * 125);
            byte B = (byte)((c / 2) * 125);
            return new SolidColorBrush(Color.FromRgb(R,G,B));
        }

        private bool samevalue(int[] newcolor, int[] prevcolor)
        {
            for (int i = 0; i < newcolor.Length; i++)
            {
                if(newcolor[i]!= prevcolor[i])
                {
                    return false;
                }
            }
            return true;
        }

        private UIElement SpaceBarLabel()
        {
            Label f = new Label();
            f.Content = " ";
            return f;
        }

        int lastresult = 0;
        private FontFamily RandomFont()
        {

            ICollection<FontFamily> fonts = Fonts.SystemFontFamilies;

            int total = fonts.Count;
            int result = lastresult;
            
            do
            {
                result = Rand.Next(0, total);
            } while (result == lastresult);
            lastresult = result;
            return fonts.ToList()[result];
        }
    }
}
