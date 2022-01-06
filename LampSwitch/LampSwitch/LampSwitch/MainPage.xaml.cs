using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LampSwitch
{
    public partial class MainPage : ContentPage
    {
        private LampStatusViewModel ViewModel;

        public MainPage()
        {
            InitializeComponent();
            ViewModel = BindingContext as LampStatusViewModel;
            try
            {
                var res = WebUtil.GETAsync("get-status", new Dictionary<string, string>());
                if (res["level"] != null) ViewModel.CurrentLevel = int.Parse(res["level"].ToString());
                if (res["position"] != null) ViewModel.CurrentPosition = int.Parse(res["position"].ToString());
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message + "\n" + ex.StackTrace, "OK");
            }
        }

        private async void AddLevel(object sender, EventArgs e)
        {
            if (ViewModel.CurrentLevel != Settings.MaxLevel)
            {
                ViewModel.CurrentLevel ++;
                try
                {
                    var res = WebUtil.GETAsync("set-lamp-level", new Dictionary<string, string> { { "level", ViewModel.CurrentLevel + "" } });
                    if (res["level"] != null) ViewModel.CurrentLevel = int.Parse(res["level"].ToString());
                    if (res["position"] != null) ViewModel.CurrentPosition = int.Parse(res["position"].ToString());
                } catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message + "\n" + ex.StackTrace, "OK");
                }
            }
        }

        private async void MinusLevel(object sender, EventArgs e)
        {
            if (ViewModel.CurrentLevel != 0)
            {
                ViewModel.CurrentLevel --;
                try
                {
                    var res = WebUtil.GETAsync("set-lamp-level", new Dictionary<string, string> { { "level", ViewModel.CurrentLevel + "" } });
                    if (res["level"] != null) ViewModel.CurrentLevel = int.Parse(res["level"].ToString());
                    if (res["position"] != null) ViewModel.CurrentPosition = int.Parse(res["position"].ToString());
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message + "\n" + ex.StackTrace, "OK");
                }
            }
        }

        private async void AddCurrentLevel(object sender, EventArgs e)
        {
            if (ViewModel.CurrentLevel != Settings.MaxLevel)
            {
                ViewModel.CurrentLevel ++;
                try
                {
                    var res = WebUtil.GETAsync("set-current-level", new Dictionary<string, string> { { "level", ViewModel.CurrentLevel + "" } });
                    if (res["level"] != null) ViewModel.CurrentLevel = int.Parse(res["level"].ToString());
                    if (res["position"] != null) ViewModel.CurrentPosition = int.Parse(res["position"].ToString());
                } catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message + "\n" + ex.StackTrace, "OK");
                }
            }
        }

        private async void MinusCurrentLevel(object sender, EventArgs e)
        {
            if (ViewModel.CurrentLevel != 0)
            {
                ViewModel.CurrentLevel --;
                try
                {
                    var res = WebUtil.GETAsync("set-current-level", new Dictionary<string, string> { { "level", ViewModel.CurrentLevel + "" } });
                    if (res["level"] != null) ViewModel.CurrentLevel = int.Parse(res["level"].ToString());
                    if (res["position"] != null) ViewModel.CurrentPosition = int.Parse(res["position"].ToString());
                } catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message + "\n" + ex.StackTrace, "OK");
                }
            }
        }
        private async void SwitchPosition(object sender, EventArgs e)
        {
            ViewModel.CurrentPosition ^= 1;
            try
            {
                var res = WebUtil.GETAsync("set-current-position", new Dictionary<string, string> { { "position", ViewModel.CurrentPosition + "" } });
                if (res["level"] != null) ViewModel.CurrentLevel = int.Parse(res["level"].ToString());
                if (res["position"] != null) ViewModel.CurrentPosition = int.Parse(res["position"].ToString());
            } catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message + "\n" + ex.StackTrace, "OK");
            }
        }

        private async void TurnOnTheLight(object sender, EventArgs e)
        {
            ViewModel.CurrentLevel = Settings.MaxLevel;
            try
            {
                var res = WebUtil.GETAsync("set-lamp-level", new Dictionary<string, string> { { "level", ViewModel.CurrentLevel + "" } });
                if (res["level"] != null) ViewModel.CurrentLevel = int.Parse(res["level"].ToString());
                if (res["position"] != null) ViewModel.CurrentPosition = int.Parse(res["position"].ToString());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message + "\n" + ex.StackTrace, "OK");
            }
        }

        private async void TurnOffTheLight(object sender, EventArgs e)
        {
            ViewModel.CurrentLevel = 0;
            try
            {
                var res = WebUtil.GETAsync("set-lamp-level", new Dictionary<string, string> { { "level", ViewModel.CurrentLevel + "" } });
                if (res["level"] != null) ViewModel.CurrentLevel = int.Parse(res["level"].ToString());
                if (res["position"] != null) ViewModel.CurrentPosition = int.Parse(res["position"].ToString());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message + "\n" + ex.StackTrace, "OK");
            }
        }
    }
}
