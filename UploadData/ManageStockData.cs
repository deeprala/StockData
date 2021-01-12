using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.VisualStyles;
using SearchOption = System.IO.SearchOption;
using DAL;
using BusinessLayer;


namespace UploadData
{
    public partial class ManageStockData : Form
    {
        public ManageStockData()
        {
            InitializeComponent();
            success.Visible = false;
        }
        

        public static string strDataserver;
        public static string strDatabase;
        public static string dataConnectionString;
        public static string fileDate;


        private void btnUploadData_Click(object sender, EventArgs e)
        {

            success.Visible = false;
            Cursor = Cursors.WaitCursor;
            bool isFolderDate = false;

            dataConnectionString = "Data Source=" + strDataserver + ";Initial Catalog=" + strDatabase + "; Integrated Security=SSPI;";
            var fileDir = new DirectoryInfo(folderPath.Text);
            DirectoryInfo[] diArr = fileDir.GetDirectories();
            for (int i = 0; i < diArr.Length; i++)
            {
                isFolderDate = false;
                DirectoryInfo dri = diArr[i];
                Console.WriteLine(dri.Name);
                var date = dri.Name.Substring(0, 10);
                DateTime myDate;
                if (DateTime.TryParse(date, out myDate))
                {
                    fileDate = date;
                    isFolderDate = true;
                }
                


                try
                {
                    foreach (var file in diArr[i].EnumerateFiles("*.csv", SearchOption.TopDirectoryOnly))
                    {
                        string stockFile = file.FullName.ToLower();
                        if (!isFolderDate)
                        {
                            fileDate = Path.GetFileName(stockFile).Substring(0, 10);
                        }
                        DataTable csvData = ProcessStockData.GetDataTableFromCSVFile(stockFile, fileDate);
                        DatabaseLayer.InsertDataIntoSQLServerUsingSQLBulkCopy(csvData, dataConnectionString, stockFile);
                    }
                    var outputPath = textOutputPath.Text.ToLower() + @"\AllDataRun_" + fileDate + @".csv";
                    var query = @"SELECT [Description],[EX],[Date],[DayOfWeek],[Symbol],[%Change],[Last],[EPS],[PE],[Volume],[Market Cap],
                                [Shares],[Net Chng],[52Low],[Low],[Last],[Close],[Open],[High],[52High],[Bid],[Ask] ,[RSI],[Div. Payout Per Share (% of EPS) - Current],[Open.Int],[MarketCapNum],
                                [Beta],[Delta],[Gamma],[Theta],[Vega],[Rho],[Div],[Div.Freq],[Div. Payout (% of Earnings) - Current (Annual)],[Div. Per Share - Current],
                                [Div. Per Share - TTM - Current (Annual)],[Div. Yield - Current],[Earnings Per Share - Current],[Earnings Per Share - TTM - Current (Annual)],
                                [Sector],[Industry],[Sub-Industry],[Cash Flow Per Share - Current (Annual)],
                                [Free Cash Flow Per Share - Current (Annual)],[Price / Cash Flow Ratio - Current],
                                [Book Value Per Share - Current (Annual)],[Book Value Per Share Growth - Current],[Price / Book Value Ratio - Current],
                                [Price / Earnings Ratio - Current], [Return on Assets (ROA) - Current (Annual)], [ROC], [Return on Equity (ROE) - Current (Annual)], [ROR], 
                                [Revenue - Current], [MoneyFlow], [MoneyFlowIndex], [MoneyFlowIndexCrossover], [MoneyFlowOscillator], [Back Vol], [DailySMA], 
                                [Financial Leverage (Assets/Equity) - Current (Annual)],[Front Vol], [Market Cap / Common Equity Ratio - Current], [Market Maker Move], [Momentum], [MomentumCrossover], [MomentumPercent], [MomentumPercentDiff],
                                [MomentumSMA],[Sizzle Index],[Spreads],[Weighted Back Vol]
                                 FROM [dbo].[WatchList]";
                    DatabaseLayer.SaveResultToFile(dataConnectionString, outputPath, query);
                    SaveSelectedData();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error has occured", ex.StackTrace);
                }
            }
            Cursor = Cursors.Default;
            var t = Task.Delay(2000);//1 second/1000 ms
            t.Wait();
            success.Visible = true;

        }
    
        private void BtnCalc_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("calc.exe");       
        }

        private void FolderPath_Click(object sender, EventArgs e)
        {
            SelectInputFolderPath();
        }

        private void FolderPath_Enter(object sender, EventArgs e)
        {
            SelectInputFolderPath();
        }

        private void SelectInputFolderPath()
        {
            FolderBrowserDialog dialog;
            dialog = new FolderBrowserDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                folderPath.Text = dialog.SelectedPath;

            }
        }

        private void TextOutputPath_Click(object sender, EventArgs e)
        {
            SelectOutputFolderPath();
        }

        private void TextOutputPath_Enter(object sender, EventArgs e)
        {
            SelectOutputFolderPath();
        }

        private void SelectOutputFolderPath()
        {
            FolderBrowserDialog dialog;
            dialog = new FolderBrowserDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textOutputPath.Text = dialog.SelectedPath;

            }
        }

        private void btnBreakEven_Click(object sender, EventArgs e)
        {
            BreakEven be = new BreakEven();

            dataConnectionString = "Data Source=" + strDataserver + ";Initial Catalog=" + strDatabase + "; Integrated Security=SSPI;";
            be.ShowDialog();
        }

        private void btnBackUpDatabase_Click(object sender, EventArgs e)
        {
            try
            {
                var DBBackUpPath = txtPathtoBackUpDB.Text;
                dataConnectionString = "Data Source=" + strDataserver + ";Initial Catalog=" + strDatabase + "; Integrated Security=SSPI;";
                BackupService bkBackupService = new BackupService(dataConnectionString, DBBackUpPath);
                bkBackupService.BackupAllUserDatabases();

            }
            catch (Exception exception)
            {
                MessageBox.Show("Failed to backup database: " + exception.Message);
            }
           
        }

        private void DBBackUpPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog;
            dialog = new FolderBrowserDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtPathtoBackUpDB.Text = dialog.SelectedPath;

            }
        }

        private void btnRestoreDB_Click(object sender, EventArgs e)
        {
            try
            {
                var targetDirectory = txtRestoreFilePath.Text; //@"C:\Latha\Personal\UploadData\UploadData";
                var restorePath = txtRestoreDBPath.Text; //@"C:\Latha\Personal\UploadData\UploadData\DBRestore\";
                string[] fileEntries = Directory.GetFiles(targetDirectory, "*.bak");
                strDataserver = textDataServer.Text.ToLower();
                foreach (var fe in fileEntries)
                {
                    var filename = Path.GetFileName(fe);
                    restorePath += filename;
                    string dbname = null;
                    int index = filename.IndexOf('-');
                    if (index > 0)
                    {
                        dbname = filename.Substring(0, index) + "_B";
                    }

                    RestoreDataBase restoreDataBase = new RestoreDataBase();
                    restoreDataBase.RestoreDatabase(dbname, fe, strDataserver, restorePath);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Failed to restore database : " + ex.Message);
            }
            


        }

        private void btnlnkWeb_Click(object sender, EventArgs e)
        {
            Links weblinks = new Links();
            weblinks.ShowDialog();
        }

        private void CAData_Click(object sender, EventArgs e)
        {
            // Just a place holder            
            
        }

        private void btnOutputData_Click(object sender, EventArgs e)
        {
            var outputdataToDate = dateTimeOutputData_From.Text;
            var outputdataFromDate = dateTimeOutputData_To.Text;
            lblFileOutput.Text = "";
            dataConnectionString = "Data Source=" + strDataserver + ";Initial Catalog=" + strDatabase + "; Integrated Security=SSPI;";
            var outputPath = textOutputPath.Text.ToLower() + @"\AllDataPrint_"+DateTime.Today.Date.ToString("yyyy-MM-dd") +".csv";
            var query = @"SELECT [Description],[EX],[Date],[DayOfWeek],[Symbol],[%Change],[Last],[EPS],[PE],[Volume],[Market Cap]
                        ,[Shares],[Net Chng],[52Low],[Low],[Last],[Close],[Open],[High],[52High],[Bid],[Ask] ,[RSI],[Div. Payout Per Share (% of EPS) - Current],[Open.Int],[MarketCapNum]
                        FROM [dbo].[WatchList] where [Date] between '"+ outputdataToDate +"' and  '"+ outputdataFromDate + "'";

            DatabaseLayer.SaveResultToFile(dataConnectionString, outputPath, query);
            lblFileOutput.Text += @": "+ outputPath;
        }

        private void btnSelectedOutputFile_Click(object sender, EventArgs e)
        {
            var selecteddataFromDate = dateTimeSelectedData_From.Text;
            var selecteddataToDate = dateTimeSelectedData_To.Text; 
            SaveSelectedData(selecteddataFromDate, selecteddataToDate);
            
        }

        private void textDatabase_TextChanged(object sender, EventArgs e)
        {
            strDatabase = textDatabase.Text.ToLower();
        }

        private void textDataServer_TextChanged(object sender, EventArgs e)
        {
            strDataserver = textDataServer.Text.ToLower();
        }

        public void SaveSelectedData()
        {
            dataConnectionString = "Data Source=" + strDataserver + ";Initial Catalog=" + strDatabase + "; Integrated Security=SSPI;";
            var outputPath = textOutputPath.Text.ToLower() + @"\SelectedDataRun_" + DateTime.Today.Date.ToString("yyyy-MM-dd") + ".csv";
            var query = @"select [Description],[EX],[Date],[DayOfWeek],[Symbol],[%Change],[Last],[EPS],[PE],[Volume],[Market Cap]
                        ,[Shares],[Net Chng],[52Low],[Low],[Last],[Close],[Open],[High],[52High],[Bid],[Ask] ,[RSI],[Div. Payout Per Share (% of EPS) - Current],[Open.Int],[MarketCapNum] from watchlist where EX in (
                            'MP',
                             --10
                            '% Change Gainers',
                            '% Change Losers', 
                            'Cash-Settled Futures',
                            'Company Profile',
                            'Economic Indicators', 
                            'Full Session Options',
                            'Futures',
                            'FX',
                            'FX Commission Pairs',
                            'FX Non-Commission Pairs',

                             -- 10
                            'Gap Down'	,
                            'Gap Up	 '		 ,
                            'Interest Rates'	 , 
                            'Market Maker Move Stocks'  , 
                            'MICRO FUTURES',
                            'New Yearly Highs '	 , 
                            'New Yearly Lows'	    , 
                            'Penny Increment Options',
                            'Physicallt Settled Futures',
                            'Preferred Stocks', 

                            --4
                            'Upcoming Dividends',
                            'Upcoming Earnings'	    , 
                            'Upcoming Splits'	 ,
                            'Weeklys'		 , 


                            --4
                            'Analyst Downgrades', 
                            'Analyst Upgrades', 
                            'Post-market Movers', 
                            'Pre-market Movers', 
                             --12
                            'Top10 % Gain NASDAQ ',
                            'Top10 % Gain NYSE'	 , 
                            'Top10 % Loss NASDAQ',	
                            'Top10 % Loss NYSE',
                            'Top10 Active NASDAQ'	 ,
                            'Top10 Active NYSE'	 ,
                            'Top10 Net Gain NASDAQ'	 ,
                            'Top10 Net Gain NYSE'	 ,
                            'Top10 Net Loss NASDAQ'	    ,
                            'Top10 Net Loss NYSE'	    , 
                            'Top10 Sizzling Stocks'	 , 
                            'Top10 Volatility Increase',

                             --- 10
                            '-3To-100M'		 , 
                            '3To100M'		 ,
                            'AAPCGR', 
                            'AAPCLR'		 , 
                            'PCLR', 
                            'PCGR'	,
                            'PCG'			 ,  
                            'PCL',
                            'INV'			 , 
                            'AINV'		 )";

            DatabaseLayer.SaveResultToFile(dataConnectionString, outputPath, query);
            lblSelectedData.Text += @": " + outputPath;
        }

        public void SaveSelectedData(string selecteddataFromDate, string selecteddataToDate)
        {
            dataConnectionString = "Data Source=" + strDataserver + ";Initial Catalog=" + strDatabase + "; Integrated Security=SSPI;";
            var outputPath = textOutputPath.Text.ToLower() + @"\SelectedDataPrint_" + DateTime.Today.Date.ToString("yyyy-MM-dd") + ".csv";
            var query = @"select [Description],[EX],[Date],[DayOfWeek],[Symbol],[%Change],[Last],[EPS],[PE],[Volume],[Market Cap]
                        ,[Shares],[Net Chng],[52Low],[Low],[Last],[Close],[Open],[High],[52High],[Bid],[Ask] ,[RSI],[Div. Payout Per Share (% of EPS) - Current],[Open.Int],[MarketCapNum] 
                        from watchlist where EX in (
                            'MP',
                             --10
                            '% Change Gainers',
                            '% Change Losers', 
                            'Cash-Settled Futures',
                            'Company Profile',
                            'Economic Indicators', 
                            'Full Session Options',
                            'Futures',
                            'FX',
                            'FX Commission Pairs',
                            'FX Non-Commission Pairs',

                             -- 10
                            'Gap Down'	,
                            'Gap Up	 '		 ,
                            'Interest Rates'	 , 
                            'Market Maker Move Stocks'  , 
                            'MICRO FUTURES',
                            'New Yearly Highs '	 , 
                            'New Yearly Lows'	    , 
                            'Penny Increment Options',
                            'Physicallt Settled Futures',
                            'Preferred Stocks', 

                            --4
                            'Upcoming Dividends',
                            'Upcoming Earnings'	    , 
                            'Upcoming Splits'	 ,
                            'Weeklys'		 , 


                            --4
                            'Analyst Downgrades', 
                            'Analyst Upgrades', 
                            'Post-market Movers', 
                            'Pre-market Movers', 
                             --12
                            'Top10 % Gain NASDAQ ',
                            'Top10 % Gain NYSE'	 , 
                            'Top10 % Loss NASDAQ',	
                            'Top10 % Loss NYSE',
                            'Top10 Active NASDAQ'	 ,
                            'Top10 Active NYSE'	 ,
                            'Top10 Net Gain NASDAQ'	 ,
                            'Top10 Net Gain NYSE'	 ,
                            'Top10 Net Loss NASDAQ'	    ,
                            'Top10 Net Loss NYSE'	    , 
                            'Top10 Sizzling Stocks'	 , 
                            'Top10 Volatility Increase',

                             --- 10
                            '-3To-100M'		 , 
                            '3To100M'		 ,
                            'AAPCGR', 
                            'AAPCLR'		 , 
                            'PCLR', 
                            'PCGR'	,
                            'PCG'			 ,  
                            'PCL',
                            'INV'			 , 
                            'AINV'		 )  and 
                            [Date] between '" + selecteddataFromDate + "' and  '" + selecteddataToDate + "'";

            DatabaseLayer.SaveResultToFile(dataConnectionString, outputPath, query);
            lblSelectedData.Text += @": " + outputPath;
        }

        public void SaveSelectedData(string selectedFromDate, string selectedToDate, string filterSymbol)
        {
            dataConnectionString = "Data Source=" + strDataserver + ";Initial Catalog=" + strDatabase + "; Integrated Security=SSPI;";
            var outputPath = textOutputPath.Text.ToLower() + @"\SelectedSymbolDataPrint_" + DateTime.Today.Date.ToString("yyyy-MM-dd") + ".csv";
            var query = @"select [Description],[EX],[Date],[DayOfWeek],[Symbol],[%Change],[Last],[EPS],[PE],[Volume],[Market Cap]
                        ,[Shares],[Net Chng],[52Low],[Low],[Last],[Close],[Open],[High],[52High],[Bid],[Ask] ,[RSI],[Div. Payout Per Share (% of EPS) - Current],[Open.Int],[MarketCapNum] 
                        from watchlist where 
                            [Date] between '" + selectedFromDate + "' and  '" + selectedToDate + "' and" +
                            "[Symbol] like '%" + filterSymbol + "%'  ";

            DatabaseLayer.SaveResultToFile(dataConnectionString, outputPath, query);
            lblSelectedData.Text += @": " + outputPath;
        }
        private void BtnEXSelect_Click(object sender, EventArgs e)
        {
            EXSelect selectEX = new EXSelect();

            dataConnectionString = "Data Source=" + strDataserver + ";Initial Catalog=" + strDatabase + "; Integrated Security=SSPI;";
            selectEX.ShowDialog();

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            var selectedFromDate = dateTimeFrom.Text;
            var selectedToDate = dateTimeTo.Text;
            var filterSymbol = textBoxSelectSymbol.Text;
            SaveSelectedData(selectedFromDate, selectedToDate, filterSymbol);
        }

        private void LinkLabelGoogleFinance_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void LinkLabelGoogleFinance_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData as string);
        }

        private void ManageStockData_Load(object sender, EventArgs e)
        {
            LinkLabel.Link link1 = new LinkLabel.Link();
            link1.LinkData = "https://www.google.ca/finance";
            linkLabelGoogleFinance.Links.Add(link1);
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void txtRestoreFilePath_TextChanged(object sender, EventArgs e)
        {

        }

        private void success_Click(object sender, EventArgs e)
        {

        }

        private void dateTimeOutputData_From_ValueChanged(object sender, EventArgs e)
        {

        }

        private void lblAllDataOutput_From_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void lblRestoreDBPath_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label81_Click(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {

        }

        private void btn_CADashboard_Click(object sender, EventArgs e)
        {
            success.Visible = false;
            Cursor = Cursors.WaitCursor;
            bool isFolderDate = false;
            dataConnectionString = "Data Source=" + strDataserver + ";Initial Catalog=" + strDatabase + "; Integrated Security=SSPI;";
            var fileDir = new DirectoryInfo(folderPath.Text);
            DirectoryInfo[] diArr = fileDir.GetDirectories();
            for (int i = 0; i < diArr.Length; i++)
            {
                isFolderDate = false;
                DirectoryInfo dri = diArr[i];
                Console.WriteLine(dri.Name);
                var date = dri.Name.Substring(0, 10);
                DateTime myDate;
                if (DateTime.TryParse(date, out myDate))
                {
                    fileDate = date;
                    isFolderDate = true;
                }


                try
                {
                    foreach (var file in diArr[i].EnumerateFiles("*.csv", SearchOption.AllDirectories))
                    {
                        string stockFile = file.FullName.ToLower();
                        //fileDate = Path.GetFileName(stockFile).Substring(0, 10);
                        DataTable csvData = ProcessStockData.GetCADashboardDataTableFromCSVFile(stockFile, fileDate);
                        DatabaseLayer.InsertCADashboardDataIntoSQL(csvData, dataConnectionString, stockFile);
                    }
                    var outputPath = textOutputPath.Text.ToLower() + @"\AllDataRun_" + fileDate + @".csv";
                    var query = @"SELECT [Description],[EX],[Date],[DayOfWeek],[Symbol],[%Change],[Last],[EPS],[PE],[Volume],[Market Cap],
                                [Shares],[Net Chng],[52Low],[Low],[Last],[Close],[Open],[High],[52High],[Bid],[Ask] ,[RSI],[Div. Payout Per Share (% of EPS) - Current],[Open.Int],[MarketCapNum],
                                [Beta],[Delta],[Gamma],[Theta],[Vega],[Rho],[Div],[Div.Freq],[Div. Payout (% of Earnings) - Current (Annual)],[Div. Per Share - Current],
                                [Div. Per Share - TTM - Current (Annual)],[Div. Yield - Current],[Earnings Per Share - Current],[Earnings Per Share - TTM - Current (Annual)],
                                [Sector],[Industry],[Sub-Industry],[Cash Flow Per Share - Current (Annual)],
                                [Free Cash Flow Per Share - Current (Annual)],[Price / Cash Flow Ratio - Current],
                                [Book Value Per Share - Current (Annual)],[Book Value Per Share Growth - Current],[Price / Book Value Ratio - Current],
                                [Price / Earnings Ratio - Current], [Return on Assets (ROA) - Current (Annual)], [ROC], [Return on Equity (ROE) - Current (Annual)], [ROR], 
                                [Revenue - Current], [MoneyFlow], [MoneyFlowIndex], [MoneyFlowIndexCrossover], [MoneyFlowOscillator], [Back Vol], [DailySMA], 
                                [Financial Leverage (Assets/Equity) - Current (Annual)],[Front Vol], [Market Cap / Common Equity Ratio - Current], [Market Maker Move], [Momentum], [MomentumCrossover], [MomentumPercent], [MomentumPercentDiff],
                                [MomentumSMA],[Sizzle Index],[Spreads],[Weighted Back Vol]
                                 FROM [dbo].[WatchList]";
                    DatabaseLayer.SaveResultToFile(dataConnectionString, outputPath, query);
                    SaveSelectedData();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error has occured", ex.StackTrace);
                }

            }
            Cursor = Cursors.Default;
            var t = Task.Delay(2000);//1 second/1000 ms
            t.Wait();
            success.Visible = true;
        }
    }
}
