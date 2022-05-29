using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MZCMobileStore.Views.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StepperOutlined : ContentView
    {
        public StepperOutlined()
        {
            InitializeComponent();
        }

        #region BorderColorProperty
        public static readonly BindableProperty BorderColorProperty
            = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(StepperOutlined), Color.Accent);

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }
        #endregion

        #region ButtonTextColorProperty
        public static readonly BindableProperty ButtonTextColorProperty
            = BindableProperty.Create(nameof(ButtonTextColor), typeof(Color), typeof(StepperOutlined), Color.Black);

        public Color ButtonTextColor
        {
            get => (Color)GetValue(ButtonTextColorProperty);
            set => SetValue(ButtonTextColorProperty, value);
        }
        #endregion

        #region ValueTextColorProperty
        public static readonly BindableProperty ValueTextColorProperty
            = BindableProperty.Create(nameof(ValueTextColor), typeof(Color), typeof(StepperOutlined), Color.Accent);

        public Color ValueTextColor
        {
            get => (Color)GetValue(ValueTextColorProperty);
            set => SetValue(ValueTextColorProperty, value);
        }
        #endregion

        #region ValueProperty
        public static readonly BindableProperty ValueProperty
            = BindableProperty.Create(nameof(Value), typeof(float), typeof(StepperOutlined), 1f);

        public float Value
        {
            get => (float)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }
        #endregion

        #region MaxValueProperty
        public static readonly BindableProperty MaxValueProperty
            = BindableProperty.Create(nameof(MaxValue), typeof(float), typeof(StepperOutlined), float.MaxValue);

        public float MaxValue
        {
            get => (float)GetValue(MaxValueProperty);
            set => SetValue(MaxValueProperty, value);
        }
        #endregion

        #region MinValueProperty
        public static readonly BindableProperty MinValueProperty
            = BindableProperty.Create(nameof(MinValue), typeof(float), typeof(StepperOutlined), float.MinValue);

        public float MinValue
        {
            get => (float)GetValue(MinValueProperty);
            set => SetValue(MinValueProperty, value);
        }
        #endregion

        #region IncrementProperty
        public static readonly BindableProperty IncrementProperty
            = BindableProperty.Create(nameof(BorderColor), typeof(float), typeof(StepperOutlined), 1f);

        public float Increment
        {
            get => (float)GetValue(IncrementProperty);
            set => SetValue(IncrementProperty, value);
        }
        #endregion

        #region ValueChangeCommandProperty
        public static readonly BindableProperty ValueChangeCommandProperty
            = BindableProperty.Create(nameof(ValueChangeCommand), typeof(ICommand), typeof(StepperOutlined), default);

        public ICommand ValueChangeCommand
        {
            get => (ICommand)GetValue(ValueChangeCommandProperty);
            set => SetValue(ValueChangeCommandProperty, value);
        }

        public void ExecuteValueChangeCommand(ICommand command)
        {
            if (command == null)
                return;

            object[] commandParameters = new[] { ValueChangeCommandParameter, Value };

            if (command?.CanExecute(commandParameters) == false)
                return;

            command?.Execute(commandParameters);
        }

        public Command OnExecuteValueChangeCommand =>
            new Command(() => ExecuteValueChangeCommand(ValueChangeCommand));
        #endregion

        #region ValueChangeCommandParameterProperty //Cart item
        public static readonly BindableProperty ValueChangeCommandParameterProperty
            = BindableProperty.Create(nameof(ValueChangeCommandParameter), typeof(object), typeof(StepperOutlined), default);

        public object ValueChangeCommandParameter
        {
            get => (object)GetValue(ValueChangeCommandParameterProperty);
            set => SetValue(ValueChangeCommandParameterProperty, value);
        }
        #endregion

        private void ButtonMinus_OnClicked(object sender, EventArgs e)
        {
            Value -= Increment;
            ValueChanged?.Invoke(this, new ValueChangedEventArgs(Value + Increment, Value));
        }

        private void ButtonPlus_OnClicked(object sender, EventArgs e)
        {
            Value += Increment;
            ValueChanged?.Invoke(this, new ValueChangedEventArgs(Value - Increment, Value));
        }

        public event EventHandler<ValueChangedEventArgs> ValueChanged;

        private void InputEntry_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            //TODO: Check input text
            ExecuteValueChangeCommand(ValueChangeCommand);
        }
    }
}