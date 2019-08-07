using System;
using System.Data;
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


        private void btnUploadData_Click(object sender, EventArgs e)
        {

            success.Visible = false;
            Cursor = Cursors.WaitCursor;

             strDataserver = textDataServer.Text.ToLower();
             strDatabase = textDatabase.Text.ToLower();
             dataConnectionString = "Data Source=" + strDataserver + ";Initial Catalog=" + strDatabase + "; Integrated Security=SSPI;";
            var fileDir = new DirectoryInfo(folderPath.Text);
            var outputPath = textOutputPath.Text.ToLower()+ @"\output.csv";

            try
            {
                foreach (var file in fileDir.EnumerateFiles("*.csv", SearchOption.AllDirectories))
                {
                    string stockFile = file.FullName.ToLower();
                    DataTable csvData = ProcessStockData.GetDataTableFromCSVFile(stockFile);
                    DatabaseLayer.InsertDataIntoSQLServerUsingSQLBulkCopy(csvData, dataConnectionString, stockFile);
                }
                DatabaseLayer.SaveResultToFile(dataConnectionString, outputPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error has occured", ex.StackTrace);
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
            strDataserver = textDataServer.Text.ToLower();
            strDatabase = textDatabase.Text.ToLower();
            dataConnectionString = "Data Source=" + strDataserver + ";Initial Catalog=" + strDatabase + "; Integrated Security=SSPI;";
            be.ShowDialog();
        }

        private void btnBackUpDatabase_Click(object sender, EventArgs e)
        {
            try
            {
                var DBBackUpPath = txtPathtoBackUpDB.Text;
                strDataserver = textDataServer.Text.ToLower();
                strDatabase = textDatabase.Text.ToLower();
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
                var targetDirectory = @"C:\Latha\Personal\UploadData\UploadData";
                var restorePath = @"C:\Latha\Personal\UploadData\UploadData\DBRestore\";
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
    }
}
