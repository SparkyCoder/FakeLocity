namespace FakeLocity.Models.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using DapperExtensions;

    public interface IDapperHub
    {
        void BeginTransaction();
        void CommitTransaction();
        int Insert<T>(T objectToInsert) where T : class;
        IEnumerable<T> GetAll<T>(object predicate) where T : class;
        void Delete<T>(T toBeDeleted) where T : class;
    }

    public class DapperHub : IDapperHub, IDisposable
    {
        private readonly string databaseConnection;
        private SqlConnection sqlConnection;
        private SqlTransaction sqlTransaction;
        private const int Timeout = 180;

        public DapperHub(string databaseConnection)
        {
            this.databaseConnection = databaseConnection;
        }

        public void BeginTransaction()
        {
            sqlConnection = new SqlConnection(databaseConnection);
            sqlConnection.Open();
            sqlTransaction = sqlConnection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (sqlConnection != null && sqlTransaction != null) sqlTransaction.Commit();
            if (sqlConnection != null) sqlConnection.Close();
        }

        public int Insert<T>(T objectToInsert) where T : class
        {
            return sqlConnection.Insert(objectToInsert, sqlTransaction, Timeout);
        }

        public IEnumerable<T> GetAll<T>(object predicate) where T : class
        {
            var result = sqlConnection.GetList<T>(predicate, transaction: sqlTransaction, commandTimeout: Timeout);

            return result;
        }

        public void Delete<T>(T toBeDeleted) where T : class
        {
            sqlConnection.Delete(toBeDeleted, sqlTransaction, Timeout);
        }

        public void Dispose()
        {
            if (sqlConnection == null) return;
            if (sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
            sqlConnection.Dispose();
            sqlConnection = null;
        }
    }
}