using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;
using Nop.Web.Framework.Menu;

namespace Nop.Plugin.Widgets.MyCustomPlugin
{
    public class MyCustomPlugin : BasePlugin, IWidgetPlugin, IAdminMenuPlugin
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly IWebHelper _webHelper;
        private readonly ISettingService _settingService;

        #endregion

        #region Ctor

        public MyCustomPlugin(ILocalizationService localizationService,
            IWebHelper webHelper,
            ISettingService settingService)
        {
            _localizationService = localizationService;
            _webHelper = webHelper;
            _settingService = settingService;
        }

        #endregion
        public override string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/MyCustomPlugin/Configure";
        }
        public bool HideInWidgetList => false;

        public string GetWidgetViewComponentName(string widgetZone)
        {
            return "WidgetsMyCustomPlugin";
        }

        public Task<IList<string>> GetWidgetZonesAsync()
        {
            return Task.FromResult<IList<string>>(new List<string> { PublicWidgetZones.HeadHtmlTag });
        }

        public override async Task InstallAsync()
        {
            var settings = new MyCustomPluginSettings()
            {
                UseSandbox = true,
                Message = "Hello World"
            };
            await _settingService.SaveSettingAsync(settings);

            await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
            {
                ["Plugin.Widgets.MyCustomPlugin.UseSandbox"]= "UseSandbox",
                ["Plugin.Widgets.MyCustomPlugin.Message"]= "Message"
             });

            await base.InstallAsync();
        }

        public override async Task UninstallAsync()
        {
            //settings
            await _settingService.DeleteSettingAsync<MyCustomPluginSettings>();

            //locales
            await _localizationService.DeleteLocaleResourcesAsync("Plugin.Widgets.MyCustomPlugin.UseSandbox");
            await _localizationService.DeleteLocaleResourcesAsync("Plugin.Widgets.MyCustomPlugin.Message");
          
            await base.UninstallAsync();
        }

        Task IAdminMenuPlugin.ManageSiteMapAsync(SiteMapNode rootNode)
        {
            var menuItem = new SiteMapNode()
            {
                SystemName = "MyCustomPlugin",
                Title = "MyCustomPlugin Title",
                ControllerName = "MyCustomPlugin",
                ActionName = "Configure",
                Visible = true,
                // RouteValues = new RouteValueDictionary() { { "area", null } },
            };
            var pluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "Third party plugins");
            if (pluginNode != null)
                pluginNode.ChildNodes.Add(menuItem);
            else
                rootNode.ChildNodes.Add(menuItem);
            return Task.FromResult<IList<string>>(new List<string> { PublicWidgetZones.HeadHtmlTag });
        }
    }
}
