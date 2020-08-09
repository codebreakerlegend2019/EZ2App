using Rg.Plugins.Popup.Services;
using EasyTwoJuetengApp.Helpers.LoadingView;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EasyTwoJuetengApp.Helpers.JdsHelpers;
using Plugin.Toast;
using System.Net;

namespace EasyTwoJuetengApp.Helpers
{
    public static class UIHelper
    {
        [Obsolete]
        public static async Task Load(Task task)
        {
            var loadingPage = new PopUpLoadingPage("defaultLoading.json",string.Empty);
            await PopupNavigation.PushAsync(loadingPage);
            await task;
            await PopupNavigation.PopAllAsync();
        }
        [Obsolete]
        public static async Task Load(Task task, string loadingCaption)
        {
            var loadingPage = new PopUpLoadingPage("defaultLoading.json", loadingCaption);
            await PopupNavigation.PushAsync(loadingPage);
            await task;
            await PopupNavigation.PopAllAsync();
        }
        [Obsolete]
        public static async Task Load(Task task, string lottieAnimationFileName, string loadingCaption)
        {
            var loadingPage = new PopUpLoadingPage(lottieAnimationFileName, loadingCaption);
            await PopupNavigation.PushAsync(loadingPage);
            await task;
            await PopupNavigation.PopAllAsync();
        }

        public static void ErrorRequestHandler<T>(JdsMultiReponse<T> jdsMultiResponse,string message)
        {
            if (jdsMultiResponse.StatusCode == HttpStatusCode.BadRequest)
                CrossToastPopUp.Current.ShowToastError(jdsMultiResponse.RequestContent.Trim(new char[] { '"' }));
            if (jdsMultiResponse.StatusCode == HttpStatusCode.BadGateway)
                CrossToastPopUp.Current.ShowToastError($"{message}, Please Internet Connection");
            else
                CrossToastPopUp.Current.ShowToastError($"{message}, Please Try Again");
        }
        public static void ErrorRequestHandler(JdsResponse jdsMultiResponse, string message)
        {
            if (jdsMultiResponse.StatusCode == HttpStatusCode.BadRequest)
                CrossToastPopUp.Current.ShowToastError(jdsMultiResponse.RequestContent.Trim(new char[] { '"' }));
            if (jdsMultiResponse.StatusCode == HttpStatusCode.BadGateway)
                CrossToastPopUp.Current.ShowToastError($"{message}, Please Internet Connection");
            else
                CrossToastPopUp.Current.ShowToastError($"{message}, Please Try Again");
        }
        public static List<T> GenerateDummyDataFor1DModel<T>(int count) where T : new()
        {
            var result = new List<T>();

            var sampleData = new T();
            foreach (var props in sampleData.GetType().GetProperties())
            {
                if (props.PropertyType == typeof(string))
                    props.SetValue(sampleData, "Sample Data", null);
                if (props.PropertyType == typeof(int))
                    props.SetValue(sampleData, 100, null);
                if (props.PropertyType == typeof(bool))
                    props.SetValue(sampleData, false, null);
                if (props.PropertyType == typeof(decimal))
                    props.SetValue(sampleData, "Sample Data", null);
                if (props.PropertyType == typeof(DateTime))
                    props.SetValue(sampleData, "Sample Data", null);
            }
            if (count == 0)
            {
                result.Add(sampleData);
                return result;
            }
            for (var i = 1; i <= count; i++)
                result.Add(sampleData);
            return result;
        }
    }
    
}
