using System;
using System.Collections.Generic;
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

namespace Demo.Main.Contorls
{
	/// <summary>
	/// Interaction logic for UserControl1.xaml
	/// </summary>
	public partial class DropdownShadow : UserControl
	{
		public DropdownShadow()
		{
			this.InitializeComponent();
		}

    public double TopOffset
    {
      get { return (double)GetValue(TopOffsetProperty); }
      set { SetValue(TopOffsetProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Offset.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty TopOffsetProperty =
        DependencyProperty.Register("TopOffset",
                                  typeof(double),
                                  typeof(DropdownShadow),
                                  new FrameworkPropertyMetadata((double)20));

    public double RightOffset
    {
      get { return (double)GetValue(RightOffsetProperty); }
      set { SetValue(RightOffsetProperty, value); }
    }

    // Using a DependencyProperty as the backing store for RightOffset.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty RightOffsetProperty =
        DependencyProperty.Register("RightOffset", typeof(double), typeof(DropdownShadow), new UIPropertyMetadata((double)20));



    public double LeftOffset
    {
      get { return (double)GetValue(LeftOffsetProperty); }
      set { SetValue(LeftOffsetProperty, value); }
    }

    // Using a DependencyProperty as the backing store for LeftOffset.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty LeftOffsetProperty =
        DependencyProperty.Register("LeftOffset", typeof(double), typeof(DropdownShadow), new UIPropertyMetadata((double)20));



    public double BottomOffset
    {
      get { return (double)GetValue(BottomOffsetProperty); }
      set { SetValue(BottomOffsetProperty, value); }
    }

    // Using a DependencyProperty as the backing store for BottomOffset.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty BottomOffsetProperty =
        DependencyProperty.Register("BottomOffset", typeof(double), typeof(DropdownShadow), new UIPropertyMetadata((double)20));


    public double RoundMargin
    {
      get { return (double)GetValue(RoundMarginProperty); }
      set { SetValue(RoundMarginProperty, value); }
    }

    // Using a DependencyProperty as the backing store for RoundMargin.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty RoundMarginProperty =
        DependencyProperty.Register("RoundMargin",
                                  typeof(double),
                                  typeof(DropdownShadow),
                                  new UIPropertyMetadata((double)-7));



    public Brush BottomRightBrush
    {
      get { return (Brush)GetValue(BottomRightBrushProperty); }
      set { SetValue(BottomRightBrushProperty, value); }
    }

    // Using a DependencyProperty as the backing store for BottomRightBrush.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty BottomRightBrushProperty =
        DependencyProperty.Register("BottomRightBrush", typeof(Brush), typeof(DropdownShadow), new UIPropertyMetadata(null));



    public Brush RightMiddleBrush
    {
      get { return (Brush)GetValue(RightMiddleBrushProperty); }
      set { SetValue(RightMiddleBrushProperty, value); }
    }

    // Using a DependencyProperty as the backing store for RightMiddleBrush.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty RightMiddleBrushProperty =
        DependencyProperty.Register("RightMiddleBrush", typeof(Brush), typeof(DropdownShadow), new UIPropertyMetadata(null));




    public Brush TopRightBrush
    {
      get { return (Brush)GetValue(TopRightBrushProperty); }
      set { SetValue(TopRightBrushProperty, value); }
    }

    // Using a DependencyProperty as the backing store for TopRightBrush.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty TopRightBrushProperty =
        DependencyProperty.Register("TopRightBrush", typeof(Brush), typeof(DropdownShadow), new UIPropertyMetadata(null));



    public Brush TopMiddleBrush
    {
      get { return (Brush)GetValue(TopMiddleBrushProperty); }
      set { SetValue(TopMiddleBrushProperty, value); }
    }

    // Using a DependencyProperty as the backing store for TopMiddleBrush.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty TopMiddleBrushProperty =
        DependencyProperty.Register("TopMiddleBrush", typeof(Brush), typeof(DropdownShadow), new UIPropertyMetadata(null));




    public Brush BottomMiddleBrush
    {
      get { return (Brush)GetValue(BottomMiddleBrushProperty); }
      set { SetValue(BottomMiddleBrushProperty, value); }
    }

    // Using a DependencyProperty as the backing store for BottomMiddleBrush.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty BottomMiddleBrushProperty =
        DependencyProperty.Register("BottomMiddleBrush", typeof(Brush), typeof(DropdownShadow), new UIPropertyMetadata(null));




    public Brush TopLeftBrush
    {
      get { return (Brush)GetValue(TopLeftBrushProperty); }
      set { SetValue(TopLeftBrushProperty, value); }
    }

    // Using a DependencyProperty as the backing store for TopLeftBrush.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty TopLeftBrushProperty =
        DependencyProperty.Register("TopLeftBrush", typeof(Brush), typeof(DropdownShadow), new UIPropertyMetadata(null));



    public Brush LeftBottomBrush
    {
      get { return (Brush)GetValue(LeftBottomBrushProperty); }
      set { SetValue(LeftBottomBrushProperty, value); }
    }

    // Using a DependencyProperty as the backing store for LeftBottomBrush.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty LeftBottomBrushProperty =
        DependencyProperty.Register("LeftBottomBrush", typeof(Brush), typeof(DropdownShadow), new UIPropertyMetadata(null));




    public Brush LeftMiddleBrush
    {
      get { return (Brush)GetValue(LeftMiddleBrushProperty); }
      set { SetValue(LeftMiddleBrushProperty, value); }
    }

    // Using a DependencyProperty as the backing store for LeftMiddleBrush.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty LeftMiddleBrushProperty =
        DependencyProperty.Register("LeftMiddleBrush", typeof(Brush), typeof(DropdownShadow), new UIPropertyMetadata(null));

   
    
	}
}