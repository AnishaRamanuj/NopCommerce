using Nop.Core.Domain.Documents;
using Nop.Web.Areas.Admin.Models.Documents;
using System.Threading.Tasks;

namespace Nop.Web.Areas.Admin.Factories
{
    public partial interface IDocumentFactory
    {
        Task<DocumentModel> PrepareDocumentModelAsync(DocumentModel model, Document document, bool excludeProperties = false);
    }
}
