using System;
using System.Data.Common;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;
using System.Data;

namespace RuTrackerDBLoader
{
    public interface IDataSet: IDisposable
    {
        FbParameterCollection Parameters { get; }
        string Query { get; set; }
        DbDataReader Reader { get; }

        void Close();
        void CloseReader();
        void Commit(bool Retaining = false);
        void ExecQueryNoData(string ExtQuery = null);
        Task ExecQueryNoDataAsync(string ExtQuery = null);
        IDataReader ExecuteQuery(string ExtQuery = null);
        Task<IDataReader> ExecuteQueryAsync(string ExtQuery = null);
        bool IsInTransaction();
        void RollBack(bool Retaining = false);
        void StartTransaction();
        Task StartTransactionAsync();
    }
}