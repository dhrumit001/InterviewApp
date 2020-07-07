using App.Core.Domain.Categorize;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Services.ExportImport
{
    /// <summary>
    /// Export manager interface
    /// </summary>
    public partial interface IExportManager
    {
        /// <summary>
        /// Export questions to XLSX
        /// </summary>
        /// <param name="questions">Questions</param>
        byte[] ExportQuestionsToXlsx(IEnumerable<Question> questions);

    }
}
