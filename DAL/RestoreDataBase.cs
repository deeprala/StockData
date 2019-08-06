//using System;
//using Microsoft.SqlServer.Serv

//namespace DAL
//{
//    class RestoreDataBase
//    {

//        public void RestoreDatabase(String databaseName, String backUpFile, String serverName, String userName, String password)
//        {
//            ServerConnection connection = new ServerConnection(serverName, userName, password);
//            Server sqlServer = new Server(connection);
//            Restore rstDatabase = new Restore();
//            rstDatabase.Action = RestoreActionType.Database;
//            rstDatabase.Database = databaseName;
//            BackupDeviceItem bkpDevice = new BackupDeviceItem(backUpFile, DeviceType.File);
//            rstDatabase.Devices.Add(bkpDevice);
//            rstDatabase.ReplaceDatabase = true;
//            rstDatabase.SqlRestore(sqlServer);
//        }
//    }
//}
