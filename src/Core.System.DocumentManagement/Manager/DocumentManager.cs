using System;
using System.Collections.Generic;
using System.Common.Exceptions;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.System.DocumentManagement.Extensions;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Extensions.Logging;
using Royalty.Insurance.Settings;

namespace Core.System.DocumentManagement.Manager
{
    public class DocumentManager : IDocumentManager
    {
        private readonly ILogger<DataViewManager> _logger;

        public DocumentManager(ILogger<DataViewManager> logger)
        {
            _logger = logger;
        }

        public async Task<Stream> GenerateDocumentFromTemplateAsync(Stream template, Func<Dictionary<string, string>> getProperties, CancellationToken cancellationToken)
        {
            try
            {
                await using var memoryStream = new MemoryStream();
                await template.CopyToAsync(memoryStream, cancellationToken);
                using var document = WordprocessingDocument.Open(memoryStream, true);
                document.ChangeDocumentType(WordprocessingDocumentType.Document);
                var mergeFields = document.MainDocumentPart.RootElement.Descendants<FieldCode>().ToList();
                //TODO: use this code to get table and brainstorm how to fill it
                //foreach (var item in document.MainDocumentPart.Document.Body.Elements<Table>())
                //{

                //}
                foreach (KeyValuePair<string, string> keys in getProperties())
                {
                    ReplaceMergeFieldWithText(mergeFields, keys.Key, keys.Value);
                }
                document.MainDocumentPart.Document.Save();
                document.Close();
                memoryStream.Seek(0, SeekOrigin.Begin);

                return new MemoryStream(memoryStream.ToArray(), false);
            }
            finally
            {
                await template.DisposeAsync();
            }
        }

        #region Private Methods

        private void ReplaceMergeFieldWithText(IEnumerable<FieldCode> fields, string mergeFieldName, string replacementText)
        {
            try
            {
                var field = fields
                    .Where(f => f.InnerText.Contains(mergeFieldName))
                    .FirstOrDefault();

                if (field == null)
                {
                    throw new RestApiResponseException(ResourceCommonMessage.GivenPropertyDoesNotExists);
                }

                // Get the Run that contains our FieldCode
                // Then get the parent container of this Run
                Run rFldCode = (Run)field.Parent;

                // Get the three (3) other Runs that make up our merge field
                Run rBegin = rFldCode.PreviousSibling<Run>();
                Run rSep = rFldCode.NextSibling<Run>();
                Run rText = rSep.NextSibling<Run>();
                Run rEnd = rText.NextSibling<Run>();
                Text t = rText.GetFirstChild<Text>();

                // Get the Run that holds the Text element for our merge field
                // Get the Text element and replace the text content 
                t.Text = replacementText;

                // Remove all the four (4) Runs for our merge field
                rFldCode.Remove();
                rBegin.Remove();
                rSep.Remove();
                rEnd.Remove();
            }
            catch (RestApiResponseException)
            {
                throw;
            }
            catch (Exception e)
            {
                _logger.LogError("An error occured", e);
            }
        }

        //TODO: this is template how to use patten, will be refatored based on brainstorm
        private Table CreateTable()
        {
            return new TableBuilder()
                .BasicBorder(8)
                .NewRow()
                .AppendCellToRow(new Text("First name"), "1600")
                .AppendCellToRow(new Text("Last name"), "1600")

                .AppendCellToRow(new Text("John"), "1600")
                .AppendCellToRow(new Text("Smith"), "1600")

                .AppendCellToRow(new Text("John"), "1600")
                .AppendCellToRow(new Text("Smith"), "1600")

                .AppendCellToRow(new Text("John"), "1600")
                .AppendCellToRow(new Text("Smith"), "1600")

                .AppendCellToRow(new Text("John"), "1600")
                .AppendCellToRow(new Text("Smith"), "1600")
                .AppendCellToRow(new Text("John"), "1600")
                .AppendCellToRow(new Text("Smith"), "1600")
                .AppendCellToRow(new Text("John"), "1600")
                .AppendCellToRow(new Text("Smith"), "1600")
                .BuildTable();
        }

        #endregion
    }
}
