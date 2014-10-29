using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace OpenClock
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            var timer = new DispatcherTimer();
            timer.Tick += Update_Clock;
            timer.Interval = new TimeSpan(0, 0, 0, 1);
            timer.Start();
            loadSettings();
        }

        private void Update_Clock(object sender, object e)
        {

            clock.Text = DateTime.Now.ToString("h:mm:ss tt");
        }

        private void clockSizeChange(object sender, RangeBaseValueChangedEventArgs e)
        {
            updateClockSize();

            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            roamingSettings.Values["sliderValue"] = clockSizeSlider.Value;
        }

        private void backgroundColorChanged(object sender, EventArgs e)
        {
            mainGrid.Background = new SolidColorBrush(backgroundColor.SelectedColor);

            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            roamingSettings.Values["backgroundColorA"] = backgroundColor.SelectedColor.A;
            roamingSettings.Values["backgroundColorB"] = backgroundColor.SelectedColor.B;
            roamingSettings.Values["backgroundColorG"] = backgroundColor.SelectedColor.G;
            roamingSettings.Values["backgroundColorR"] = backgroundColor.SelectedColor.R;
        }

        private void timeColorChanged(object sender, EventArgs e)
        {
            clock.Foreground = new SolidColorBrush(timeColor.SelectedColor);

            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            roamingSettings.Values["clockColorA"] = timeColor.SelectedColor.A;
            roamingSettings.Values["clockColorB"] = timeColor.SelectedColor.B;
            roamingSettings.Values["clockColorG"] = timeColor.SelectedColor.G;
            roamingSettings.Values["clockColorR"] = timeColor.SelectedColor.R;
        }

        private void resizeMainGrid(object sender, SizeChangedEventArgs e)
        {
            updateClockSize();
        }

        private void updateClockSize()
        {
            clock.FontSize = (clockSizeSlider.Value / clockSizeSlider.Maximum) * (0.1875 * mainGrid.ActualWidth) + 12;
        }

        private void loadSettings()
        {
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            if (roamingSettings.Values.ContainsKey("sliderValue"))
            {
                clockSizeSlider.Value = (double)roamingSettings.Values["sliderValue"];
            }

            if (roamingSettings.Values.ContainsKey("backgroundColorA") && roamingSettings.Values.ContainsKey("backgroundColorB") && roamingSettings.Values.ContainsKey("backgroundColorG") && roamingSettings.Values.ContainsKey("backgroundColorR"))
            {
                var a = (byte)roamingSettings.Values["backgroundColorA"];
                var b = (byte)roamingSettings.Values["backgroundColorB"];
                var r = (byte)roamingSettings.Values["backgroundColorR"];
                var g = (byte)roamingSettings.Values["backgroundColorG"];
                backgroundColor.SelectedColor = Windows.UI.Color.FromArgb(a, r, g, b);
                mainGrid.Background = new SolidColorBrush(backgroundColor.SelectedColor);
            }

            if (roamingSettings.Values.ContainsKey("clockColorA") && roamingSettings.Values.ContainsKey("clockColorB") && roamingSettings.Values.ContainsKey("clockColorG") && roamingSettings.Values.ContainsKey("clockColorR"))
            {
                var a = (byte)roamingSettings.Values["clockColorA"];
                var b = (byte)roamingSettings.Values["clockColorB"];
                var r = (byte)roamingSettings.Values["clockColorR"];
                var g = (byte)roamingSettings.Values["clockColorG"];
                timeColor.SelectedColor = Windows.UI.Color.FromArgb(a, r, g, b);
                clock.Foreground = new SolidColorBrush(timeColor.SelectedColor);
            }
        }
    }
}
