using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Domain.Documents;

namespace Nop.Services.Documents
{
    public partial interface IDocumentService
    {
        Task<Document> GetDocumentByIdAsync(int docId);
        Task<IPagedList<Document>> GetAllDocumentsAsync(string name = "", string description = "");
        Task InsertDocumentAsync(Document document);
        Task UpdateDocumentAsync(Document document);
        Task DeleteDocumentAsync(Document document);
    }
}
