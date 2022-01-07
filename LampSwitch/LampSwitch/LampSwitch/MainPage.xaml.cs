using Newtonsoft.Json.Linq;
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

        private void UpdateStatus(JObject res)
        {
            if (res["level"] != null) ViewModel.CurrentLevel = int.Parse(res["level"].ToString());
            if (res["position"] != null) ViewModel.CurrentPosition = int.Parse(res["position"].ToString());
        }

        public MainPage()
        {
            InitializeComponent();
            ViewModel = BindingContext as LampStatusViewModel;
            try
            {
                UpdateStatus(WebUtil.GET("get-status", new Dictionary<string, string>()));
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
                    UpdateStatus(WebUtil.GET("set-lamp-level", new Dictionary<string, string> { { "level", ViewModel.CurrentLevel + "" } }));
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
                    UpdateStatus(WebUtil.GET("set-lamp-level", new Dictionary<string, string> { { "level", ViewModel.CurrentLevel + "" } }));
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
                    UpdateStatus(WebUtil.GET("set-current-level", new Dictionary<string, string> { { "level", ViewModel.CurrentLevel + "" } }));
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
                    UpdateStatus(WebUtil.GET("set-current-level", new Dictionary<string, string> { { "level", ViewModel.CurrentLevel + "" } }));
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
                UpdateStatus(WebUtil.GET("set-current-position", new Dictionary<string, string> { { "position", ViewModel.CurrentPosition + "" } }));
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
                UpdateStatus(WebUtil.GET("set-lamp-level", new Dictionary<string, string> { { "level", ViewModel.CurrentLevel + "" } }));
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
                UpdateStatus(WebUtil.GET("set-lamp-level", new Dictionary<string, string> { { "level", ViewModel.CurrentLevel + "" } }));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message + "\n" + ex.StackTrace, "OK");
            }
        }

        private async void RefreshView_Refreshing(object sender, EventArgs e)
        {
            try
            {
                UpdateStatus(WebUtil.GET("get-status", new Dictionary<string, string>()));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message + "\n" + ex.StackTrace, "OK");
            }
            finally
            {
                ViewModel.IsRefreshing = false;
            }
        }
    }
}
