using FirebirdSql.Data.FirebirdClient;
using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace RuTrackerDBLoader
{
    public class FBDataSet : IDataSet
    {
        private readonly string sysdba = "sysdba";
        private readonly string password = "masterkey";
        private readonly string codePage = "UTF8";
        private const FbServerType serverType = FbServerType.Default;

        private FbConnection connection;
        private FbTransaction transaction;
        private DbDataReader dataReader;
        private FbCommand command;

        public string Query { get; set; } = "";
        public FbParameterCollection Parameters { get { return command.Parameters; } }
        public DbDataReader Reader { get { return dataReader; } }

        public FBDataSet(string DataBase, string Sysdba, string Password, string Codepage)
        {
            sysdba = Sysdba;
            password = Password;
            codePage = Codepage;
            InternalInit(DataBase);
        }

        public FBDataSet(string DataBase)
        {
            InternalInit(DataBase);
        }

        private void InternalInit(string DataBase)
        {
            FbConnectionStringBuilder connectionString = new FbConnectionStringBuilder
            {
                Charset = codePage,
                UserID = sysdba,
                Password = password,
                Database = DataBase,
                ServerType = serverType
            };
            connection = new FbConnection(connectionString.ToString());
            command = new FbCommand("", connection);
        }

        #region Sync
        private void Open()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public IDataReader ExecuteQuery(string ExtQuery = null)
        {
            command.CommandText = string.IsNullOrEmpty(ExtQuery) ? Query : ExtQuery;
            Open();
            CloseReader();
            if (IsInTransaction())
            {
                command.Transaction = transaction;
            }
            dataReader = command.ExecuteReader();
            return dataReader;
        }

        public void ExecQueryNoData(string ExtQuery = null)
        {
            command.CommandText = string.IsNullOrEmpty(ExtQuery) ? Query : ExtQuery;
            Open();
            if (IsInTransaction())
            {
                command.Transaction = transaction;
            }
            command.ExecuteNonQuery();
        }

        public void StartTransaction()
        {
            if (transaction == null)
            {
                Open();
                transaction = connection.BeginTransaction();
            }
        }
        #endregion

        #region Async
        private async Task OpenAsync()
        {
            if (connection.State == ConnectionState.Closed)
            {
                await connection.OpenAsync();
            }
        }

        public async Task<IDataReader> ExecuteQueryAsync(string ExtQuery = null)
        {
            command.CommandText = string.IsNullOrEmpty(ExtQuery) ? Query : ExtQuery;
            await OpenAsync();
            CloseReader();
            if (IsInTransaction())
            {
                command.Transaction = transaction;
            }
            dataReader = await command.ExecuteReaderAsync();
            return dataReader;
        }

        public async Task ExecQueryNoDataAsync(string ExtQuery = null)
        {
            command.CommandText = string.IsNullOrEmpty(ExtQuery) ? Query : ExtQuery;
            await OpenAsync();
            if (IsInTransaction())
            {
                command.Transaction = transaction;
            }
            await command.ExecuteNonQueryAsync();
        }

        public async Task StartTransactionAsync()
        {
            if (transaction == null)
            {
                await OpenAsync();
                transaction = connection.BeginTransaction();
            }
        }
        #endregion

        public void Commit(bool Retaining = false)
        {
            if (transaction != null)
            {
                if (Retaining)
                    transaction.CommitRetaining();
                else
                    transaction.Commit();

                transaction.Dispose();
                transaction = null;
            }
        }

        public void RollBack(bool Retaining = false)
        {
            if (transaction != null)
            {
                if (Retaining)
                    transaction.RollbackRetaining();
                else
                    transaction.Rollback();

                transaction.Dispose();
                transaction = null;
            }
        }

        public bool IsInTransaction()
        {
            return transaction != null;
        }

        public void CloseReader()
        {
            if (dataReader != null && !dataReader.IsClosed)
            {
                dataReader.Close();
            }
        }

        public void Close()
        {
            CloseReader();
            if (connection.State != ConnectionState.Closed)
            {
                if (IsInTransaction())
                    Commit();
                connection.Close();
            }
        }

        public void Dispose()
        {
            CloseReader();
            if (dataReader != null)
                dataReader.Dispose();
            if (connection != null)
                connection.Dispose();
        }
    }
}
