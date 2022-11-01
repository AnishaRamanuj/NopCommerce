using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Core.Domain.Documents;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Documents;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Services.Seo;
using Nop.Services.Vendors;
using Nop.Web.Areas.Admin.Factories;
using Nop.Web.Areas.Admin.Models.Documents;

namespace Nop.Web.Areas.Admin.Controllers
{
    public class DocumentController1 : Controller
    {
        #region Fields

        private readonly IDocumentFactory _documentFactory;
        private readonly IDocumentService _documentService;
        private readonly ILocalizedEntityService _localizedEntityService;
         private readonly IUrlRecordService _urlRecordService;

        #endregion

        #region Ctor

        public DocumentController1(IDocumentFactory documentFactory, IDocumentService documentService)
        {
            _documentFactory = documentFactory;
            _documentService = documentService;
        }

        #endregion
        protected virtual async Task UpdateLocalesAsync(Document document, DocumentModel model)
        {
            //foreach (var localized in model.Locales)
            //{
            //    await _localizedEntityService.SaveLocalizedValueAsync(document,
            //        x => x.Name,
            //        localized.Name,
            //        localized.LanguageId);

            //    await _localizedEntityService.SaveLocalizedValueAsync(document,
            //        x => x.Description,
            //        localized.Description,
            //        localized.LanguageId);

            //    //search engine name
            //    var seName = await _urlRecordService.ValidateSeNameAsync(document, localized.SeName, localized.Name, false);
            //    await _urlRecordService.SaveSlugAsync(vendor, seName, localized.LanguageId);
            //}
        }
    }
}
