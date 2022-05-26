using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MZCMobileStore.Infrastructure;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MZCMobileStore.Views.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntryOutlined : ContentView
    {
        public EntryOutlined()
        {
            InitializeComponent();
        }

        #region TextProperty
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(EntryOutlined), string.Empty);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        #endregion

        #region PlaceholderProperty
        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(EntryOutlined), string.Empty);

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }
        #endregion

        #region PlaceholderColorProperty
        public static readonly BindableProperty PlaceholderColorProperty =
            BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(EntryOutlined), Color.Black);

        public Color PlaceholderColor
        {
            get => (Color)GetValue(PlaceholderColorProperty);
            set => SetValue(PlaceholderColorProperty, value);
        }
        #endregion

        #region PlaceholderBackgroundColorProperty
        public static readonly BindableProperty PlaceholderBackgroundColorProperty =
            BindableProperty.Create(nameof(PlaceholderBackgroundColor), typeof(Color), typeof(EntryOutlined), Color.White);

        public Color PlaceholderBackgroundColor
        {
            get => (Color)GetValue(PlaceholderBackgroundColorProperty);
            set => SetValue(PlaceholderBackgroundColorProperty, value);
        }
        #endregion

        #region BorderColorProperty
        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(EntryOutlined), Color.Black);

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public Color FocusedBorderColor { get; set; }
        public Color UnFocusedBorderColor { get; set; }
        #endregion

        #region IsPasswordProperty
        public static readonly BindableProperty IsPasswordProperty =
            BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(EntryOutlined), false);

        public bool IsPassword
        {
            get => (bool)GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
        }
        #endregion

        #region PassworldLevelActionProperty
        public static readonly BindableProperty PasswordLevelActionProperty =
            BindableProperty.Create(nameof(PasswordLevelAction), typeof(Func<string, LevelsPasswordSecurityEnum>), typeof(EntryOutlined), default);

        public Func<string, LevelsPasswordSecurityEnum> PasswordLevelAction
        {
            get => (Func<string, LevelsPasswordSecurityEnum>)GetValue(PasswordLevelActionProperty);
            set => SetValue(PasswordLevelActionProperty, value);
        }
        #endregion

        #region IsVisiblePasswordSecurityLevelProperty
        public static readonly BindableProperty IsVisiblePasswordSecurityLevelProperty =
            BindableProperty.Create(nameof(IsVisiblePasswordSecurityLevel), typeof(bool), typeof(EntryOutlined), false);

        public bool IsVisiblePasswordSecurityLevel
        {
            get => (bool)GetValue(IsVisiblePasswordSecurityLevelProperty);
            set => SetValue(IsVisiblePasswordSecurityLevelProperty, value);
        }
        #endregion

        #region KeyboardProperty
        public static readonly BindableProperty KeyboardProperty =
            BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(EntryOutlined), Keyboard.Default);

        public Keyboard Keyboard
        {
            get => (Keyboard)GetValue(KeyboardProperty);
            set => SetValue(KeyboardProperty, value);
        }
        #endregion

        private async void InputEntry_OnFocused(object sender, FocusEventArgs e)
        {
            BorderColor = FocusedBorderColor;

            //Start animation
            await TranslateLabelToTitle();
        }

        private async void InputEntry_OnUnfocused(object sender, FocusEventArgs e)
        {
            BorderColor = UnFocusedBorderColor;

            //Start animation
            await TranslateLabelToPlaceholder();
        }

        private async Task TranslateLabelToTitle()
        {
            if (string.IsNullOrEmpty(Text))
            {
                await PlaceholderLabel.TranslateTo(0, -GetPlaceholderDistance(PlaceholderLabel));
            }
        }

        private async Task TranslateLabelToPlaceholder()
        {
            if (string.IsNullOrEmpty(Text))
                await PlaceholderLabel.TranslateTo(0, 0);
        }

        private double GetPlaceholderDistance(VisualElement placeholderLabel)
        {
            double distance = 0;

            if (Device.RuntimePlatform != Device.iOS)
                distance = 5;

            distance = placeholderLabel.Height + distance;

            return distance;
        }

        public event EventHandler<TextChangedEventArgs> TextChanged; 
        private async void InputEntry_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsVisiblePasswordSecurityLevel)
            {
                LevelsPasswordSecurityEnum? isValidate = PasswordLevelAction?.Invoke((sender as BorderlessEntry).Text);

                switch (isValidate)
                {
                    case LevelsPasswordSecurityEnum.Indefinite:
                        LevelPasswordSecurity.WidthRequest = LevelPasswordSecurityBackground.Width / 4;
                        LevelPasswordSecurity.Color = Color.FromRgb(255, 227, 221);
                        //await LevelPasswordSecurity.ScaleXTo(2);
                        break;

                    case LevelsPasswordSecurityEnum.Low:
                        LevelPasswordSecurity.WidthRequest = LevelPasswordSecurityBackground.Width / 3; ;
                        LevelPasswordSecurity.Color = Color.FromRgb(255, 227, 221);
                        //await LevelPasswordSecurity.ScaleXTo(3);
                        break;

                    case LevelsPasswordSecurityEnum.Medium:
                        LevelPasswordSecurity.WidthRequest = LevelPasswordSecurityBackground.Width / 2; ;
                        LevelPasswordSecurity.Color = Color.FromRgb(255, 241, 221);
                        //await LevelPasswordSecurity.ScaleXTo(4);
                        break;

                    case LevelsPasswordSecurityEnum.High:
                        LevelPasswordSecurity.WidthRequest = LevelPasswordSecurityBackground.Width / 1; ;
                        LevelPasswordSecurity.Color = Color.FromRgb(222, 255, 221);
                        //await LevelPasswordSecurity.ScaleXTo(5);
                        break;
                }
            }

            TextChanged?.Invoke(this,e);
        }
    }
}