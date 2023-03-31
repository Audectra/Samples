using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using SampleApp.Localization;

namespace SampleApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Obviously there are better and cleaner ways to implement this, but this sample focuses on localization only. 
        private void LanguageSelector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var localizationService = LocalizationService.Current;
            if (LanguageSelector.SelectedItem is not ComboBoxItem selectedItem)
                return;

            var selectedCulture = selectedItem.Content?.ToString();
            if (selectedCulture is null)
                return;

            // Update the localization service to the selected culture. 
            localizationService.CurrentCulture = CultureInfo.GetCultureInfo(selectedCulture);
        }
    }
}
