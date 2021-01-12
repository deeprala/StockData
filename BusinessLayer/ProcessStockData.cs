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
        public static DataTable GetDataTableFromCSVFile(string csv_file_path, string fileDate)
        {
            DataTable csvData = new DataTable();
            List<string> fieldData;
            DateTime date = Convert.ToDateTime(fileDate);
            var dayofweek = date.DayOfWeek;


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
                        csvData.Columns.Add("DayOfWeek");
                    }

                    while (!csvReader.EndOfData)
                    {
                        try
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
                            fieldData.Add(date.ToString());
                            fieldData.Add(dayofweek.ToString());
                            string[] arrayList = fieldData.ToArray();
                            csvData.Rows.Add(arrayList);
                        }

                        catch(Exception ex)
                        {
                            Console.WriteLine("error: ", ex.Message);
                        }

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

        public static DataTable GetCADashboardDataTableFromCSVFile (string csv_file_path, string fileDate)
        {
            DataTable csvData = new DataTable();
            List<string> fieldData;
             DateTime date = Convert.ToDateTime(fileDate);
            var dayofweek = date.DayOfWeek;
            string EX = "";

            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
                {
                    string[] colFields;
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    //var line1 = csvReader.ReadLine();
                    //var line2 = csvReader.ReadLine();
                    //var line3 = csvReader.ReadLine().Trim('|');
                    var fileName = Path.GetFileName(csv_file_path)?.Substring(0,1).ToUpper();
                    switch(fileName)
                    {
                        case "A":
                        case "B":
                            EX = "CAADRTSX-412";
                        break;
                        case "C":
                            EX = "CAADRUS-177";
                            break;

                        case "D":
                            EX = "CAADR-US-176";
                            break;
                        case "E":
                            EX = "TSXCOMP-229";
                            break;
                        case "F":
                            EX = "TSXSP-234";
                            break;
                        case "G":
                        case "H":
                        case "I":
                        case "J":
                        case "K":
                            EX = "TSX-860";
                            break;
                        case "L":
                        case "M":
                        case "N":
                        case "O":
                        case "P":
                        case "Q":
                        case "R":
                        case "S":
                        case "T":
                            EX = "TSXV-1654";
                            break;
                        case "U":
                            EX = "CA-REITS";
                            break;
                        case "V":
                            EX = "CANNABIS";
                            break;
                        case "W":
                            EX = "CADRIPS-132";
                            break;
                        case "X":
                            EX = "FINANCE92";
                            break;
                        case "Y":
                            EX = "TSX-TECH-52";
                            break;
                        case "Z":
                            EX = GetExValueForZ(csv_file_path);
                            break;

                    }
                    colFields = csvReader.ReadFields();

                    foreach (string column in colFields)
                    {
                        DataColumn datacolumn = new DataColumn(column);
                        datacolumn.AllowDBNull = true;
                        csvData.Columns.Add(datacolumn);
                    }
                    if (EX != null)
                    {
                        string ex = "EX";
                        DataColumn datacolumn = new DataColumn(ex);
                        datacolumn.AllowDBNull = true;
                        csvData.Columns.Add(datacolumn);
                        csvData.Columns.Add("Date");
                        csvData.Columns.Add("DayOfWeek");
                    }

                    while (!csvReader.EndOfData)
                    {
                        try
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

                            fieldData.Add(EX);
                            fieldData.Add(date.ToString());
                            fieldData.Add(dayofweek.ToString());
                            string[] arrayList = fieldData.ToArray();
                            csvData.Rows.Add(arrayList);
                        }

                        catch (Exception ex)
                        {
                            Console.WriteLine("error: ", ex.Message);
                        }

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

        private static string GetExValueForZ(string csv_file_path)
        {
            string exchangeCode;
            var fileName = Path.GetFileName(csv_file_path)?.Substring(0, 3).ToUpper();
            switch(fileName)
            {
                case "Z26":
                    exchangeCode = "TSXV-TECH-127";
                    return exchangeCode;
                case "Z27":
                    exchangeCode = "TSX-REALESTATE-64";
                    return exchangeCode; ;
                case "Z28":
                    exchangeCode = "TSXV-REALESTATE-30";
                    return exchangeCode; ;
                case "Z29":
                    exchangeCode = "TSX-OG-80";
                    return exchangeCode; ;
                case "Z30":
                    exchangeCode = "TSXV-OG-139";
                    return exchangeCode; ;
                case "Z31":
                    exchangeCode = "TSX-MINING-228";
                    return exchangeCode; ;
                case "Z32":
                case "Z33":
                case "Z34":
                case "Z35":
                    exchangeCode = "TSXV-MINING-990";
                    return exchangeCode; ;
                case "Z36":
                case "Z37":
                    exchangeCode = "TSX-DIVERSI-IND-400";
                    return exchangeCode; ;
                case "Z38":
                    exchangeCode = "TSXV-DIVERSI-IND-198";
                    return exchangeCode; ;
                case "Z39":
                    exchangeCode = "TSX-ENEGY-SRVS-42";
                    return exchangeCode; ;
                case "Z40":
                    exchangeCode = "TSXV-ENEGY-SRVS-25";
                    return exchangeCode; ;
                case "Z41":
                    exchangeCode = "TSX-CLEAN-TECH-36";
                    return exchangeCode; ;
                case "Z42":
                    exchangeCode = "TSXV-CLEAN-TECH-56";
                    return exchangeCode; ;
                case "Z43":
                case "Z44":
                    exchangeCode = "CSE338";
                    return exchangeCode;
                default:
                    return exchangeCode = "TZXT";

            }
            
        }
    }
}
