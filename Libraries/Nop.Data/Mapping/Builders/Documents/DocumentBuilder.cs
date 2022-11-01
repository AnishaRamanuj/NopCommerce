using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Documents;
using Nop.Core.Domain.Vendors;

namespace Nop.Data.Mapping.Builders.Documents
{
    /// <summary>
    /// Represents a Document entity builder
    /// </summary>
    public partial class DocumentBuilder : NopEntityBuilder<Document>
    {
        #region Methods

        /// <summary>
        /// Apply entity configuration
        /// </summary>
        /// <param name="table">Create table expression builder</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
               .WithColumn(nameof(Document.Name)).AsString(400).NotNullable()
               .WithColumn(nameof(Document.Description)).AsString(400).Nullable();
               
        }
        #endregion
    }
}
