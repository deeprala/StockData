using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Logger;

namespace BusinessLayer
{
    public class ProcessStockData
    {

        //Read CSV file and create a datatable to mimic db table.
        public static DataTable GetDataTableFromCSVFile(string csv_file_path)
        {
            DataTable csvData = new DataTable();
            List<string> fieldData;
            var date = Path.GetFileName(csv_file_path).Substring(0, 10);


            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
                {
                    string[] colFields;
                    csvReader.SetDelimiters(new string[] { "|" });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    var line1 = csvReader.ReadLine();
                    var line2 = csvReader.ReadLine();
                    var line3 = csvReader.ReadLine().Trim('|');
                    colFields = csvReader.ReadFields();

                    foreach (string column in colFields)
                    {
                        DataColumn datacolumn = new DataColumn(column);
                        datacolumn.AllowDBNull = true;
                        csvData.Columns.Add(datacolumn);
                    }
                    if (line3 != null)
                    {
                        string ex = "EX";
                        DataColumn datacolumn = new DataColumn(ex);
                        datacolumn.AllowDBNull = true;
                        csvData.Columns.Add(datacolumn);
                        csvData.Columns.Add("Date");
                    }

                    while (!csvReader.EndOfData)
                    {

                        fieldData = csvReader.ReadFields().ToList();

                        //Making empty value as null
                        for (int i = 0; i < fieldData.Count; i++)
                        {
                            if (fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                        }

                        fieldData.Add(line3);
                        fieldData.Add(date);
                        string[] arrayList = fieldData.ToArray();
                        csvData.Rows.Add(arrayList);

                    }
                }

                return csvData;
            }
            catch (Exception ex)
            {
                Logging.Logger("File exception: " + csv_file_path + " ;" + ex.Message);
                return null;
            }


        }

    }
}
