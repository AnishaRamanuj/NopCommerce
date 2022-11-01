using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Plugin.Widgets.MyCustomPlugin;
using Nop.Plugin.Widgets.MyCustomPlugin.Models;
using Nop.Services.Configuration;
using Nop.Services.Media;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.NivoSlider.Components
{
    [ViewComponent(Name = "WidgetsNivoSlider")]
    public class WidgetsNivoSliderViewComponent : NopViewComponent
    {
        private readonly IStoreContext _storeContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly ISettingService _settingService;
        private readonly IPictureService _pictureService;
        private readonly IWebHelper _webHelper;

        public WidgetsNivoSliderViewComponent(IStoreContext storeContext, 
            IStaticCacheManager staticCacheManager, 
            ISettingService settingService, 
            IPictureService pictureService,
            IWebHelper webHelper)
        {
            _storeContext = storeContext;
            _staticCacheManager = staticCacheManager;
            _settingService = settingService;
            _pictureService = pictureService;
            _webHelper = webHelper;
        }

        /// <returns>A task that represents the asynchronous operation</returns>
        public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
        {
            var store = await _storeContext.GetCurrentStoreAsync();
            var MyCustomPluginSettings = await _settingService.LoadSettingAsync<MyCustomPluginSettings>(store.Id);

            var model = new PublicInfoModel
            {
                Message = MyCustomPluginSettings.Message,
                UseSandbox = MyCustomPluginSettings.UseSandbox
            };
            if(!model.UseSandbox && string.IsNullOrEmpty(model.Message))
                //no Use Sandbox
                return Content("");

            return View("~/Plugins/Widgets.MyCustomPlugin/Views/PublicInfo.cshtml", model);
        }
    }
}
