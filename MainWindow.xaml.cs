using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Npgsql;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {

        private readonly Server _server = new Server("Server=localhost; port=5432; user id = postgres; password = testserver; database = Test;");

        private readonly DataTable _dataTable = new DataTable();
        public MainWindow()
        {
            InitializeComponent();
            this.Title = "PostgresSQL App";
        }
        
        public void ButtonClick(object sender, RoutedEventArgs e)
        {
            MainData.ItemsSource = _server.GetData("select * from users").DefaultView;
        }
        public void ButtonClickAddUser(object sender, RoutedEventArgs e)
        {
            AddUserWindow addUserWindow = new AddUserWindow();
            addUserWindow.Show();
        }


    }
    public class Server
    {
        private readonly NpgsqlConnection _serverConnection;
        private readonly NpgsqlCommand _serverCommand;
        public Server(string serverConnectionString)
        {
            _serverConnection = new NpgsqlConnection(serverConnectionString);
            _serverConnection.Open();
            _serverCommand = new NpgsqlCommand()
            {
                Connection = _serverConnection
            };

        }
        ~Server()
        {
            _serverConnection.Close();
        }
        public NpgsqlDataReader GetDataReader()
        {
            if (_serverConnection.State != ConnectionState.Open)
            {
                _serverConnection.Open();
            }
            return _serverCommand.ExecuteReader();
        }
        public DataTable GetData(string sqlQuery)
        {
            var dataTable = new DataTable();
            _serverCommand.CommandText = sqlQuery;

            dataTable.Load(GetDataReader());

            return dataTable;
        }

        public void AddUserToTable(User user)
        {
            if (_serverConnection.State != ConnectionState.Open) 
            {
                _serverConnection.Open();
            }

            _serverCommand.CommandText = "insert into users values(@user_id,@first_name,@last_name,@nickname,@balance)";
            _serverCommand.Parameters.Add("@user_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = user.GetUserID();
            _serverCommand.Parameters.Add("@first_name", NpgsqlTypes.NpgsqlDbType.Varchar, 30).Value = user.GetFirstName();
            _serverCommand.Parameters.Add("@last_name", NpgsqlTypes.NpgsqlDbType.Varchar, 30).Value = user.GetLastName();
            _serverCommand.Parameters.Add("@nickname", NpgsqlTypes.NpgsqlDbType.Varchar, 30).Value = user.GetGameName();
            _serverCommand.Parameters.Add("@balance", NpgsqlTypes.NpgsqlDbType.Real).Value = user.GetBalance();
            _serverCommand.CommandType = CommandType.Text;
            _serverCommand.ExecuteNonQuery();
        }

    }
    public class User
    {
        private int _userID = -1;
        private string _firstName = "";
        private string _lastName = "";
        private string _gameName = "";
        private float _balance = -1;

        public User() { }
        public void SetUserID(int UserID)
        {
            this._userID = UserID;
        }
        public void SetFirstName(string FirstName)
        {
            this._firstName = FirstName;
        }
        public void SetLastName(string LastName)
        {
            this._lastName = LastName;
        }
        public void SetGameName(string GameName)
        {
            this._gameName = GameName;
        }
        public void SetBalance(float Balance)
        {
            this._balance = Balance;
        }

        public int GetUserID() { return  this._userID; }
        public string GetFirstName()
        {
            return this._firstName;
        }
        public string GetLastName()
        {
            return this._lastName;
        }
        public string GetGameName()
        {
            return this._gameName;
        }
        public float GetBalance()
        {
            return this._balance;
        }


    }
}
