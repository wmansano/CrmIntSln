using System;


namespace CrmCore
{
    public partial class CrmCoreLogic : IDisposable
    {
        //public bool ImportFile(string filename)
        //{
        //    bool success = false;
        //    string ErrorMsg = string.Empty;

        //    try
        //    {
        //        FileInfo fi = new FileInfo("filename");

        //        var path = @"C:\Person.csv"; // Habeeb, "Dubai Media City, Dubai"


        //        using (CsvTextFieldParser csvParser = new CsvTextFieldParser(path))
        //        {
        //            //csvParser.Delimiters = new string[] { "#" };
        //            //csvParser.SetDelimiters(new string[] { "," });
        //            //csvParser.HasFieldsEnclosedInQuotes = true;

        //            // Skip the row with the column names
        //            //csvParser.

        //            //while (!csvParser.EndOfData)
        //            //{
        //            //    // Read current line fields, pointer moves to the next line.
        //            //    string[] fields = csvParser.ReadFields();
        //            //    string Name = fields[0];
        //            //    string Address = fields[1];
        //            //}
        //        }

        //        //re_imp reif = new re_import_funds();

        //        //reif.re_import_fund_category = "";

        //        //int? id = null;

        //        //var matches = db_ctx.re_import_funds.Where(x => x.re_import_fund_srid == id);


        //        //if (matches.Any())
        //        //{
        //        //    // update
        //        //    reif = matches.OrderByDescending(y => y.re_import_fund_modified_datetime).FirstOrDefault();
        //        //}
        //        //else {
        //        //    // insert
        //        //    db_ctx.re_import_funds.Add(reif);
        //        //}

        //        //using (crmdb_entities db_ctx = new crmdb_entities()) {                 
        //        //    db_ctx.SaveChanges();
        //        //}

        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMsg = ex.ToString();
        //    }

        //    return success;
        //}
    }
}
