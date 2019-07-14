﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Logger;

namespace DAL
{
    public class DatabaseLayer
    {
       public static void InsertDataIntoSQLServerUsingSQLBulkCopy(DataTable csvFileData, string DataConnection, string filename)
        {
            // change the data source value as needed LN443397
            using (SqlConnection dbConnection =
                   new SqlConnection(DataConnection))
                // using (SqlConnection dbConnection =
                // new SqlConnection("Data Source=RAMESHI7;Initial Catalog=RAMTEST;Integrated Security=SSPI;"))
                //RAMESHhrd2in1
                //rameshdi5
                try
                {
                    {
                        dbConnection.Open();
                        using (SqlBulkCopy s = new SqlBulkCopy(dbConnection))
                        {
                            s.DestinationTableName = "watchlist";
                            foreach (var column in csvFileData.Columns)
                                s.ColumnMappings.Add(column.ToString(), column.ToString());
                            s.WriteToServer(csvFileData);

                        }
                        var updateCommandText = @"update WatchList set [Market Cap]= replace([Market Cap], '|', ','), [Shares] = REPLACE(Shares,'|',','),
                                          [Volume] = REPLACE(Volume,'|',','), [Description] = REPLACE(Description,'|',','),[Open.Int] = REPLACE([Open.Int],'|',',')";
                        SqlCommand updateData = new SqlCommand(updateCommandText, dbConnection);
                        updateData.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Logging.Logger("SQL exception with file " + filename + " ; " + ex.Message);
                }

        }

        public static void SaveResultToFile(string ConnectionString, string OutputPath)
        {
            var command = @"SELECT [Description],[EX],[Date],[Symbol],[%Change],[EPS],[PE],[Volume],[Market Cap]
      ,[Shares],[Net Chng],[52Low],[Low],[Last],[Close],[Open],[High],[52High],[Bid],[Ask] ,[RSI],[Div. Payout Per Share (% of EPS) - Current]      ,[Open.Int]
      FROM [dbo].[WatchList]";

            using (var sqlConnection = new SqlConnection(ConnectionString))
            using (var sqlCommand = new SqlCommand(command, sqlConnection))
            {
                sqlConnection.Open();
                var sqlDataReader = sqlCommand.ExecuteReader();


                string Delimiter = "|";
                string Separator = ",";
                string fileName = OutputPath;
                StreamWriter writer = new StreamWriter(fileName);

                // write header number row - start
                for (int columnCounter = 0; columnCounter < sqlDataReader.FieldCount; columnCounter++)
                {
                    //add numbers to columns
                    writer.Write((columnCounter + 1) + Delimiter);

                }
                writer.WriteLine(string.Empty);
                //write header number row - end

                //loop to add column header description
                for (int columnCounter = 0; columnCounter < sqlDataReader.FieldCount; columnCounter++)
                {

                    writer.Write(sqlDataReader.GetName(columnCounter) + Delimiter);
                }
                writer.WriteLine(string.Empty);

                // data loop
                while (sqlDataReader.Read())
                {
                    // column loop
                    for (int columnCounter = 0; columnCounter < sqlDataReader.FieldCount; columnCounter++)
                    {

                        writer.Write(sqlDataReader.GetValue(columnCounter).ToString().Replace('"', '\'') + Delimiter);
                    }   // end of column loop
                    writer.WriteLine(string.Empty);
                }   // data loop

                writer.Flush();
            }

        }
    }
}
