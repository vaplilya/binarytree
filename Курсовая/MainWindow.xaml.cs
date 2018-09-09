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
using System.Threading;
using System.Windows.Shapes;

namespace Курсовая
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Canvas canva2 = new Canvas();
        public static Grid grid2 = new Grid();
        static int u = 1, size,n;
        Tree bin = new Tree();
        Tree bin_copy = new Tree();
        public static List<int> nodes = new List<int>(); 
        public static Button Find2 = new Button();
        static double width, height, dx, dy;

        public MainWindow()
        {
            InitializeComponent();
            canva2 = grid;
            Find2 = Find;
            width = 920;
            height = 0;
            //draw(0);
        }


        public partial class Tree
        {
            double x = 0, y = 1,klp=1;
            // подкласс "элемент дерева"
            public class TreeNode
            {
                public int value; // численное значение
                                  // public int Count = 0; // сколько раз было добавлено данное численное значение
                public TreeNode left; // левое поддерево
                public TreeNode right; // правое поддерево
                public TreeNode(int value)
                {
                    this.value = value;
                }
            }
            public TreeNode root;

            public Tree()
            {
                root = null;
            }

            public void draw(double x, double y, int val, int size)
            {
                Ellipse el = new Ellipse();
                el.Width = size;
                el.Height = size;
                el.Fill = Brushes.GreenYellow;
                el.Stretch = Stretch.Uniform;
                TextBox text = new TextBox();
                text.BorderBrush = Brushes.GreenYellow;
                text.Background = Brushes.GreenYellow;
                text.Foreground = Brushes.Black;
                text.FontWeight = FontWeights.Bold;
                text.FontSize = size / 3;
                text.Text = val.ToString();
                Panel.SetZIndex(el, 2);
                Canvas.SetLeft(el, x);
                Canvas.SetTop(el, y);
                Panel.SetZIndex(text, 3);
                Canvas.SetLeft(text, x + size / 4);
                Canvas.SetTop(text, y + size / 4);
                canva2.Children.Add(el);
                canva2.Children.Add(text);
                return;
            }
            private void timerTick(object sender, EventArgs e)
            {
                Find2.Visibility = Visibility.Visible;
            }
            public void drawnew(double x, double y, int val, int size)
            {
                Ellipse eli = new Ellipse();
                eli.Width = 7 * size / 6;
                eli.Height = 7 * size / 6;
                eli.Fill = Brushes.Red;
                eli.Stretch = Stretch.Uniform;
                Canvas.SetLeft(eli, x - size / 7);
                Canvas.SetTop(eli, y - size / 7);
                Panel.SetZIndex(eli, 1);
                canva2.Children.Add(eli);


                Thread thread = new Thread(() =>
                {
                    Thread.Sleep(2000);
                    Application.Current.Dispatcher.Invoke((Action)(() => { canva2.Children.Remove(eli); }));

                });

                thread.Start();

                return;
            }
            public void drawline2(double x1, double y1, double x2, double y2)
            {
                Line line = new Line();
                line.X1 = x1;
                line.X2 = x2;
                line.Y1 = y1;
                line.Y2 = y2;
                line.Stroke = Brushes.Red;
                line.StrokeThickness = 3;
                canva2.Children.Add(line);
                Canvas.SetLeft(line, x);
                Canvas.SetTop(line, y);
                Thread thread2 = new Thread(() =>
                {
                    Thread.Sleep(2000);
                    Application.Current.Dispatcher.Invoke((Action)(() => { canva2.Children.Remove(line); }));

                });

                thread2.Start();
            }

            public void drawline(double x1, double y1, double x2, double y2)
            {
                Line line = new Line();
                line.X1 = x1;
                line.X2 = x2;
                line.Y1 = y1;
                line.Y2 = y2;
                line.Stroke = Brushes.Black;
                canva2.Children.Add(line);
                Canvas.SetLeft(line, x);
                Canvas.SetTop(line, y);
            }

            //      public TreeNode Node; // экземпляр класса "элемент дерева"
            public void Add(int val) // добавление элемента
            {
                if (root == null)
                {
                    root = new TreeNode(val);
                    draw(width, height, val, size);
                    return;
                }
                AddNode(root, val);
            }
            private void AddNode(TreeNode p, int val)
            {
                dx /= 2.0;
                if (val <= p.value)
                {
                    height += dy;
                    width -= dx;
                    if (p.left == null)
                    {
                        p.left = new TreeNode(val);
                        drawline(width + dx, height - dy + size / 2, width + size / 2, height);
                        draw(width, height, val, size);
                    }
                    else
                    {
                        AddNode(p.left, val);
                    }
                }
                else
                {
                    height += dy;
                    width += dx;
                    if (p.right == null)
                    {
                        p.right = new TreeNode(val);
                        drawline(width - dx + size, height - dy + size / 2, width + size / 2, height);
                        draw(width, height, val, size);
                    }
                    else
                    {
                        AddNode(p.right, val);
                    }
                }
            }

            public void FindNode(TreeNode p, int val)
            {
                dx /= 2.0;
                if (val == p.value)
                {
                    drawnew(width, height, val, size + 6);
                    return;
                }
                if (val < p.value)
                {
                    height += dy;
                    width -= dx;
                    if (p.left != null)
                    {
                        drawline2(width + dx, height - dy + size / 2, width + size / 2, height);
                        FindNode(p.left, val);
                    }
                }
                else
                {
                    height += dy;
                    width += dx;
                    if (p.right != null)
                    {
                        drawline2(width - dx + size, height - dy + size / 2, width + size / 2, height);
                        FindNode(p.right, val);
                    }
                }
            }

            private int Minvalue(TreeNode p)
            {
                if (p.left == null) return p.value;
                else
                {
                    n = Minvalue(p.left);
                    return n;
                }
            }

            public TreeNode DeleteNode(TreeNode p, int val)
            {
                if (val == p.value)
                {
                    if ((p.left == null) && p.right == null)
                    {
                        p = null;
                        return p;
                    }
                    if ((p.left == null) && (p.right != null))
                    {
                        p = p.right;
                        return p;
                    }
                    if ((p.right == null) && (p.left != null))
                    {
                        p = p.left;
                        p.left = null;
                        return p;
                    }
                    if ((p.right != null) && (p.left != null))
                    {
                        p.value = Minvalue(p.right);
                        p.right=DeleteNode(p.right, p.value);
                        return p;
                    }
                }
                    if (val < p.value)
                    {
                        if (p.left == null) MessageBox.Show("wrong");
                        else
                        {
                            p.left = DeleteNode(p.left, val);
                            return p;
                        }
                    }
                    if (p.right == null) throw new Exception("Невозможно удалить элемент из пустого дерева");
                    else
                    {
                        p.right = DeleteNode(p.right, val);
                        return p;
                    }
                    
            }

            public void PrintNode(TreeNode p, int val)
            {
                dx /= 2.0;
                if (p != null)
                {
                    if (val <= p.value)
                    {
                        height += dy;
                        width -= dx;
                        drawline(width + dx, height - dy + size / 2, width + size / 2, height);
                        draw(width, height, val, size);

                        PrintNode(p.left, val);
                    }
                    else
                    {
                        height += dy;
                        width += dx;
                        drawline(width - dx + size, height - dy + size / 2, width + size / 2, height);
                        draw(width, height, val, size);

                        PrintNode(p.right, val);
                    }
                }
                else return;
            }

            public void Print(TreeNode p)
            {
                if (p != null)
                {
                    nodes.Add(p.value);
                    Print(p.left);
                    Print(p.right);
                }
            }

        }

        private void Button_Click6(object sender, RoutedEventArgs e)
        {
            Help help = new Help();
            help.Show();
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            size = 40;
            height = 10;
            width = 435;
            dx = width;
            dy = 80;
            try
            {
                int test = Convert.ToInt32(AddBox.Text);
                AddBox.Clear();
                bin.Add(test);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Введите корректное значение!");
            }
        }


        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            size = 40;
            width = 435;
            dx = width;
            dy = 80;
            height = 10;
            try
            {
                int test = Convert.ToInt32(AddBox.Text);
                AddBox.Clear();
                bin.FindNode(bin.root, test);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Введите корректное значение!");
            }
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            try {
                int node = Convert.ToInt32(AddBox.Text);
                if (int.TryParse(AddBox.Text, out node))
                {
                    size = 40;
                    width = 435;
                    dx = width;
                    dy = 80;
                    height = 10;
                    AddBox.Clear();
                    canva2.Children.Clear();
                    bin.DeleteNode(bin.root, node);
                    bin.Print(bin.root);
                    for (int h = 0; h < nodes.Count(); h++)
                    {
                        size = 40;
                        width = 435;
                        dx = width;
                        dy = 80;
                        height = 10;
                        bin_copy.Add(nodes[h]);
                    }
                    nodes.Clear();
                    bin_copy.root = null;
                }
                else MessageBox.Show("Введите значение!");
            }
            catch(Exception ex)
            {

            }
            }

        private void Button_Click4(object sender, RoutedEventArgs e)
        {
            canva2.Children.Clear();
            String str;
            Microsoft.Win32.OpenFileDialog file = new Microsoft.Win32.OpenFileDialog();
            if (file.ShowDialog() == false) return;
            str = file.FileName;
            using (BinaryReader reader = new BinaryReader(File.Open(str, FileMode.Open)))
            {
                // пока не достигнут конец файла
                // считываем каждое значение из файла
                while (reader.PeekChar() > -1)
                {
                    size = 40;
                    width = 435;
                    dx = width;
                    dy = 80;
                    height = 10;
                    int area = reader.ReadInt32();
                    bin.Add(area);
                }
            }
        }

        private void Button_Click5(object sender, RoutedEventArgs e)
        {
            bin.root = null;
            canva2.Children.Clear();
        }
    }
}
