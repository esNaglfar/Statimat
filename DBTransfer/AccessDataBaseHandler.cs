using System.Data.OleDb;
using System.Data;

namespace DBTransfer
{

    class AccessDataBaseHandler
    {

        #region Singleton
        private static AccessDataBaseHandler Instance;

        private AccessDataBaseHandler()
        {
            IsWorking = false;
            Connection = new OleDbConnection();
            Command = new OleDbCommand();
            Command.Connection = Connection;
        }
        public static AccessDataBaseHandler GetInstance()
        {
            if (Instance == null)
                Instance = new AccessDataBaseHandler();
            return Instance;
        }
        #endregion

        public bool IsWorking { get { return TestConnection(); } private set { IsWorking = value; } }
        public OleDbConnection Connection { get; private set; }
        public OleDbCommand Command { get; private set; }
        public string DBPath { get; set; }

        private bool TestConnection()
        {
            Connection.ConnectionString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = {DBPath}";

            try
            {
                Connection.Open();
                Connection.Close();
                IsWorking = true;
                return true;
            }
            catch
            {
                IsWorking = false;
                return false;
            }
        }
        private void Open()
        {
            Connection.Open();
        }
        private void Close()
        {
            Connection.Close();
        }


        public void Query()
        {
            OleDbConnection connection = new OleDbConnection
            {
                ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = C://kek/Data.mdb"
            };
            connection.Open();

            OleDbCommand command = new OleDbCommand
            {
                Connection = connection
            };

            string query = $"SELECT * FROM {comboBox1.Text}";

            command.CommandText = query;

            OleDbDataAdapter da = new OleDbDataAdapter(command);

            DataTable dt = new DataTable();

            da.Fill(dt);

            dataGridView1.DataSource = dt;

            connection.Close();
        }
    }
}
