using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel.Composition;


namespace Demo.Main
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  [Export]
  public partial class Shell : Window
  {
    public Shell()
    {
      InitializeComponent();
    }


    private void Close_Click(object sender, RoutedEventArgs e)
    {
      this.Close();
    }

    private void Minimum_Click(object sender, RoutedEventArgs e)
    {
      this.WindowState = WindowState.Minimized;
    }

    private void Maximum_Click(object sender, RoutedEventArgs e)
    {
      this.Maximum.Visibility = System.Windows.Visibility.Collapsed;
      this.Normalize.Visibility = System.Windows.Visibility.Visible;
      //this.dropdownShadow.Visibility = System.Windows.Visibility.Collapsed;
      this.LayoutRoot.Margin = new Thickness(0);
      this.WindowState = System.Windows.WindowState.Maximized;
    }

    private void Normalize_Click(object sender, RoutedEventArgs e)
    {
      this.Maximum.Visibility = System.Windows.Visibility.Visible;
      this.Normalize.Visibility = System.Windows.Visibility.Collapsed;
      //this.dropdownShadow.Visibility = System.Windows.Visibility.Visible;
      this.LayoutRoot.Margin = new Thickness(7);
      this.WindowState = System.Windows.WindowState.Normal;
    }

    private void ContentContainer_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      if (this.ContentBorderOuter.IsMouseDirectlyOver)
        this.DragMove();
    }
  }
}
