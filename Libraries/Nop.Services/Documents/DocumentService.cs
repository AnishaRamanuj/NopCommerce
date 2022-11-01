using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Spreadsheet;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Documents;
using Nop.Core.Domain.Vendors;
using Nop.Data;
using Nop.Services.Html;

namespace Nop.Services.Documents
{
    public partial class DocumentService : IDocumentService
    {
        #region Fields

        private readonly IHtmlFormatter _htmlFormatter;
        private readonly IRepository<Document> _documentRepository;
        #endregion

        #region Ctor

        public DocumentService(IHtmlFormatter htmlFormatter,
           
            IRepository<Document> documentRepository)
           
        {
            _htmlFormatter = htmlFormatter;
            _documentRepository = documentRepository;
        }

        public Task DeleteDocumentAsync(Document document)
        {
            throw new NotImplementedException();
        }

        #endregion


        public virtual async Task DeleteVendorAsync(Document document)
        {
            await _documentRepository.DeleteAsync(document);
        }

        public virtual async Task<IPagedList<Document>> GetAllDocumentsAsync(string name = "", string description = "")
        {
            var documents = await _documentRepository.GetAllPagedAsync(query =>
            {
                if (!string.IsNullOrWhiteSpace(name))
                    query = query.Where(v => v.Name.Contains(name));

                if (!string.IsNullOrWhiteSpace(description))
                    query = query.Where(v => v.Description.Contains(description));

                query = query.OrderBy(v => v.Description);

                return query;
            });

            return documents;
        }


        public virtual async Task<Document> GetDocumentByIdAsync(int docId)
        {
            return await _documentRepository.GetByIdAsync(docId, cache => default);
        }

        public virtual async Task InsertDocumentAsync(Document document)
        {
            await _documentRepository.InsertAsync(document);
        }


        public virtual async Task UpdateDocumentAsync(Document document)
        {
            await _documentRepository.UpdateAsync(document);
        }

    }
}
