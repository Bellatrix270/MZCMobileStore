using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MZCMobileStore.Models;
using MZCMobileStore.Models.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MZCMobileStore.Views.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PcCardView : ContentView 
    {
        public PcCardView()
        {
            InitializeComponent();
        }

        #region ItemDescriptionProperty
        public static readonly BindableProperty ItemDescriptionProperty = 
            BindableProperty.Create(nameof(ItemDescription), typeof(string), typeof(PcCardView), string.Empty);

        public string ItemDescription
        {
            get => (string)GetValue(ItemDescriptionProperty);
            set => SetValue(ItemDescriptionProperty, value);
        }
        #endregion

        #region ItemImageProperty
        public static readonly BindableProperty ItemImageProperty =
            BindableProperty.Create(nameof(ItemImage), typeof(ImageSource), typeof(PcCardView), default);

        public ImageSource ItemImage
        {
            get => (ImageSource)GetValue(ItemImageProperty);
            set => SetValue(ItemImageProperty, value);
        }
        #endregion

        #region ItemNameProperty
        public static readonly BindableProperty ItemNameProperty =
            BindableProperty.Create(nameof(ItemName), typeof(string), typeof(PcCardView), string.Empty);

        public string ItemName
        {
            get => (string)GetValue(ItemNameProperty);
            set => SetValue(ItemNameProperty, value);
        }
        #endregion

        #region ItemPriceProperty
        public static readonly BindableProperty ItemPriceProperty =
            BindableProperty.Create(nameof(ItemPrice), typeof(double), typeof(PcCardView), double.MinValue);

        public double ItemPrice
        {
            get => (double)GetValue(ItemPriceProperty);
            set => SetValue(ItemPriceProperty, value);
        }
        #endregion

        #region ItemCountProperty
        public static readonly BindableProperty ItemCountProperty =
            BindableProperty.Create(nameof(ItemCount), typeof(int), typeof(PcCardView), 1);

        public int ItemCount
        {
            get => (int)GetValue(ItemCountProperty);
            set => SetValue(ItemCountProperty, value);
        }
        #endregion

        #region ItemCountChangedCommandProperty
        public static readonly BindableProperty ItemCountChangedCommandProperty =
            BindableProperty.Create(nameof(ItemCountChangedCommand), typeof(ICommand), typeof(PcCardView), default);

        public ICommand ItemCountChangedCommand
        {
            get => (ICommand)GetValue(ItemCountChangedCommandProperty);
            set => SetValue(ItemCountChangedCommandProperty, value);
        }
        #endregion

        #region ItemCountChangedCommandParameterProperty
        public static readonly BindableProperty ItemCountChangedCommandParameterProperty =
            BindableProperty.Create(nameof(ItemCountChangedCommandParameter), typeof(object), typeof(PcCardView), default);

        public object ItemCountChangedCommandParameter
        {
            get => (object)GetValue(ItemCountChangedCommandParameterProperty);
            set => SetValue(ItemCountChangedCommandParameterProperty, value);
        }
        #endregion

        #region LoadingFrameProperty
        public static readonly BindableProperty LoadingFrameProperty =
            BindableProperty.Create(nameof(LoadingFrame), typeof(Style), typeof(PcCardView), default);

        public Style LoadingFrame
        {
            get => (Style)GetValue(LoadingFrameProperty);
            set => SetValue(LoadingFrameProperty, value);
        }
        #endregion

        private void StepperOutlined_OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            ItemPrice *= e.NewValue;
        }
    }
}