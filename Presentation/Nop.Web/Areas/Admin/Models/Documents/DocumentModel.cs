using System;
using System.ComponentModel.DataAnnotations;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Models;
using Nop.Web.Areas.Admin.Models.Orders;
using System.Collections.Generic;

namespace Nop.Web.Areas.Admin.Models.Documents
{
    /// <summary>
    /// Represents a Document model
    /// </summary>
    public partial class DocumentModel 
    {
        #region Properties

        public int VendorId { get; set; }

        [NopResourceDisplayName("Admin.Documents.Fields.Note")]
        public string Name { get; set; }

        [NopResourceDisplayName("Admin.Documents.Fields.CreatedOn")]
        public string Description { get; set; }
        public IEnumerable<object> Locales { get;  set; }

        #endregion
    }
}
