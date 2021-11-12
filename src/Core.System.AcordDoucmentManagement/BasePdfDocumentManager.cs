using System;
using System.Collections;
using System.Collections.Generic;
using System.Common.Exceptions;
using System.IO;
using System.Net;
using iTextSharp.text.pdf;

namespace Core.System.AcordDocumentManagement
{
    public class BasePdfDocumentManager
    {

        public byte[] GenerateAcordForm(Stream acordFormStream)
        {
            var fields = new List<string>();
            byte[] result;
            try
            {
                using (var mem = new MemoryStream())
                {
                    var pdfReader = new PdfReader(acordFormStream);
                    ICollection fieldNames = GetFormFields(pdfReader);
                    foreach (var fieldName in fieldNames)
                    {
                        fields.Add(fieldName.ToString());
                    }

                    var stamper = new PdfStamper(pdfReader, mem);

                    //iText is completely done and disposed of at this point
                    //so we can now grab the raw bytes that represent a PDF
                    AcroFields form = stamper.AcroFields;
                    form.SetField(fields[0], "John Smith"); // TODO: this is code  example to reuse, should be replaceed with actual field name and value

                    mem.Seek(0, SeekOrigin.Begin);
                    //form.SetField(SampleFormFieldNames.IAmAwesomeCheck, model.AwesomeCheck ? "Yes" : "Off"); //TODO:, code example, set checkbox value
                    // set this if you want the result PDF to not be editable. 
                    stamper.FormFlattening = true;
                    var firstBytes = mem.ToArray();// DO NOT REMOVE THIS LINe, read first bytes before closing pdfReader, that's how it works
                    pdfReader.Close();
                    stamper.Close();
                    var lastBytes = mem.ToArray();// DO NOT REMOVE THIS LINe, read last bytes after closing pdfReader, that's how it works

                    result = new byte[firstBytes.Length + lastBytes.Length];
                    firstBytes.CopyTo(result, 0);
                    lastBytes.CopyTo(result, firstBytes.Length);
                }

                var cReportName = "generated.pdf";
                //TODO - return either byte arrays 
                if (File.Exists(cReportName))
                {
                    File.Delete(cReportName);
                }

                using (FileStream file = new FileStream(cReportName, FileMode.Create, FileAccess.Write))
                {
                    file.Write(result, 0, result.Length);
                }
            }
            catch(Exception exception)
            {
                throw new RestApiResponseException((int)HttpStatusCode.InternalServerError, $"Error occured can not generate the document, {exception.Message}");
            }

            return result;
        }

        public ICollection GetFormFields(PdfReader pdfReader)
        {
            try
            {
                AcroFields acroFields = pdfReader.AcroFields;

                return acroFields.Fields.Keys;
            }
            catch (Exception exception)
            {
                throw new RestApiResponseException((int)HttpStatusCode.InternalServerError, $"Error occured can not read PDF file fields, {exception.Message}");
            }
        }
    }
}
