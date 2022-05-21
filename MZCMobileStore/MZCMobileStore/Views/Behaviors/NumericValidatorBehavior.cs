using System.Linq;
using MZCMobileStore.Views.Controls;
using Xamarin.Forms;

namespace MZCMobileStore.Views.Behaviors
{
    public class NumericValidationBehavior : Behavior<EntryOutlined>
    {
        public static readonly BindableProperty MaxCountNumbersProperty =
            BindableProperty.Create(nameof(MaxCountNumbers), typeof(int), typeof(NumericValidationBehavior), 255);

        public int MaxCountNumbers
        {
            get => (int)GetValue(MaxCountNumbersProperty);
            set => SetValue(MaxCountNumbersProperty, value);
        }

        protected override void OnAttachedTo(EntryOutlined entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(EntryOutlined entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            if (string.IsNullOrWhiteSpace(args.NewTextValue))
            {
                ((EntryOutlined)sender).Text = string.Empty;
                return;
            }

            var isValid = args.NewTextValue.ToCharArray().All(char.IsDigit) && args.NewTextValue.ToCharArray().Length <= MaxCountNumbers;

            var current = args.NewTextValue;

            ((EntryOutlined)sender).Text = isValid ? current : current.Remove(current.Length - 1);
        }
    }
}
