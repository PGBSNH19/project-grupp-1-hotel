using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Client.Toast
{
    public class ToastBase : ComponentBase, IDisposable
    {
        protected string Heading { get; set; }
        protected string Message { get; set; }
        protected bool IsVisible { get; set; }
        protected string BackgroundCss { get; set; }
        protected string IconCss { get; set; }
        [Inject] ToastService ToastService { get; set; }
        protected override void OnInitialized()
        {
            ToastService.OnShow += ShowToast;
            ToastService.OnHide += HideToast;
        }

        private void ShowToast(string message, ToastLevel level)
        {
            buildToastSettings(message, level);
            IsVisible = true;
            StateHasChanged();
        }

        private void buildToastSettings(string message, ToastLevel level)
        {
            switch (level)
            {
                case ToastLevel.Info:
                    BackgroundCss = "bg-info";
                    IconCss = "info";
                    Heading = "Info";
                    break;
                case ToastLevel.Success:
                    BackgroundCss = "bg-success";
                    IconCss = "check";
                    Heading = "Success";
                    break;
                case ToastLevel.Warning:
                    BackgroundCss = "bg-warning";
                    IconCss = "exclamation";
                    Heading = "Warning";
                    break;
                case ToastLevel.Error:
                    BackgroundCss = "bg-danger";
                    IconCss = "times";
                    Heading = "Error";
                    break;
            }

            this.Message = message;
        }

        private void HideToast()
        {
            IsVisible = false;
            StateHasChanged();
        }

        public void Dispose()
        {
            ToastService.OnShow -= ShowToast;
        }
    }
}
