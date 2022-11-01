using System.Threading.Tasks;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Documents;
using Nop.Core.Domain.Vendors;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Documents;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Seo;
using Nop.Services.Vendors;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Areas.Admin.Models.Documents;
using Nop.Web.Framework.Factories;

namespace Nop.Web.Areas.Admin.Factories
{
    public partial class DocumentFactory : IDocumentFactory
    {
        #region Fields
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly ILocalizationService _localizationService;
        private readonly ILocalizedModelFactory _localizedModelFactory;
    
        private readonly IDocumentService _documentService;


        #endregion

        #region Ctor

        public DocumentFactory(
           
            IDocumentService documentService
           )
        {
           
            _documentService = documentService;
          
        }

        #endregion
        public virtual async Task<DocumentModel> PrepareDocumentModelAsync(DocumentModel model, Document document, bool excludeProperties = false)
        {
            if (document != null)
            {
                //fill in model values from the entity
                if (model == null)
                {
                 //   model = document.ToModel<DocumentModel>();
                  //  model.SeName = await _urlRecordService.GetSeNameAsync(document, 0, true, false);
                }
                
            }
            return model;
        }
    }
}
