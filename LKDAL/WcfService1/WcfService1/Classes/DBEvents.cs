using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Collections;

namespace WcfService1.Classes
{
    public class DBEvents : IDisposable
    {
        static string _connStr;
        internal DataContext _cntxt;

        public string ConStr
        {
            get { return _connStr; }
        }

        public DBEvents()
        {
            //_connStr = "Data Source=78.187.196.108;Initial Catalog=PSY;User ID=isd;Password=isd";
            //_connStr = "Data Source=MEHMETPC\\SQLMEHMET;Initial Catalog=LKDB;User ID=sa;Password=isd";
            //_connStr = "Data Source=SAMET-PC\\SQL2008;Initial Catalog=LKDB;User ID=sa;Password=isd";
            //_connStr = "Data Source=10.10.10.144\\MSSQLSERVERR2;Initial Catalog=LKDB;User ID=Sa;Password=Sa123";
            _connStr = "Data Source=localhost;Initial Catalog=LKDB;User ID=Sa;Password=SaLuks123";
            //_connStr = "Data Source=GCAN;Initial Catalog=LKDB;User ID=sa;Password=sapass";
            //_connStr = "Data Source=77.79.121.145\\ISDSQLSRV;Initial Catalog=LKDB;User ID=isdtest;Password=isdtest";
        }

        #region Generic

        public List<T> GetGenericWithSQLQuery<T>(string sqlStr, object[] parametres) where T : class
        {
            _cntxt = new DataContext(_connStr);
            try
            {
                if (_cntxt.Connection.State == System.Data.ConnectionState.Closed)
                    _cntxt.Connection.Open();

                List<T> list = (from s in _cntxt.ExecuteQuery<T>(sqlStr, parametres) select s).ToList<T>();
                return list;
            }
            catch (Exception exp)
            {
                _cntxt.Connection.Close();
                LogException(exp, "GetGenericWithSQLQuery<T>()", 0);
                return null;
            }
        }

        public List<T> GetGeneric<T>() where T : class
        {
            _cntxt = new DataContext(_connStr);
            try
            {
                if (_cntxt.Connection.State == System.Data.ConnectionState.Closed)
                    _cntxt.Connection.Open();
                
                
                return (from s in _cntxt.GetTable<T>() select s).ToList<T>();

            } 
            catch (Exception exp)
            {
                _cntxt.Connection.Close();
                LogException(exp, "GetGeneric<T>()", 0);
                return null;
            }

        }

        public List<T> GetGeneric<T>(Expression<Func<T, bool>> query) where T : class
        {
            _cntxt = new DataContext(_connStr);
            List<T> result = new List<T>();
            try
            {
                if (_cntxt.Connection.State == System.Data.ConnectionState.Closed)
                    _cntxt.Connection.Open();

                
                result = (_cntxt.GetTable<T>().Where(query).Select(s => s)).ToList<T>();

            }
            catch (Exception exp)
            {
                _cntxt.Connection.Close();
                LogException(exp, "GetGeneric<T>(Expression<Func<T, bool>> query)", 0);

            }
            return result;
        }

        public bool SaveGeneric<T>(T value) where T : class
        {
            _cntxt = new DataContext(_connStr);
            try
            {
                if (_cntxt.Connection.State == System.Data.ConnectionState.Closed)
                    _cntxt.Connection.Open();


                _cntxt.GetTable<T>().InsertOnSubmit(value);
                _cntxt.SubmitChanges();
                return true;

            }
            catch (Exception exp)
            {
                _cntxt.Connection.Close();
                LogException(exp, "SaveGeneric<T>(T value)", 0);
                return false;
            }

        }

        public bool SaveGeneric<T>(ref T value) where T : class
        {
            _cntxt = new DataContext(_connStr);
            try
            {
                if (_cntxt.Connection.State == System.Data.ConnectionState.Closed)
                    _cntxt.Connection.Open();

                
                _cntxt.GetTable<T>().InsertOnSubmit(value);
                _cntxt.SubmitChanges();
                return true;

            }
            catch (Exception exp)
            {
                _cntxt.Connection.Close();
                LogException(exp, "SaveGeneric<T>(T value)", 0);
                return false;
            }

        }
        
        public bool SaveGeneric<T>(List<T> values) where T : class
        {
            _cntxt = new DataContext(_connStr);
            try
            {
                if (_cntxt.Connection.State == System.Data.ConnectionState.Closed)
                    _cntxt.Connection.Open();

                _cntxt.GetTable<T>().InsertAllOnSubmit(values);
                _cntxt.SubmitChanges();
                return true;
            }
            catch (Exception exp)
            {
                _cntxt.Connection.Close();
                LogException(exp, "SaveGeneric<T>(List<T> values)", 0);
                return false;
            }

        }

        public bool SaveGeneric<T>(ref List<T> values) where T : class
        {
            _cntxt = new DataContext(_connStr);
            try
            {
                if (_cntxt.Connection.State == System.Data.ConnectionState.Closed)
                    _cntxt.Connection.Open();

                _cntxt.GetTable<T>().InsertAllOnSubmit(values);
                _cntxt.SubmitChanges();
                return true;
            }
            catch (Exception exp)
            {
                _cntxt.Connection.Close();
                LogException(exp, "SaveGeneric<T>(List<T> values)", 0);
                return false;
            }

        }

        public bool UpdateGeneric<T>(T entity) where T : class
        {
            bool sonuc = false;
            _cntxt = new DataContext(_connStr);

            try
            {
                if (_cntxt.Connection.State == System.Data.ConnectionState.Closed)
                    _cntxt.Connection.Open();

                var Table = _cntxt.GetTable<T>();
                Table.Attach(entity);
                _cntxt.Refresh(RefreshMode.KeepCurrentValues, entity);
                _cntxt.SubmitChanges();
                sonuc = true;

                _cntxt.Connection.Close();
            }
            catch (Exception exp)
            {
                _cntxt.Connection.Close();
                LogException(exp, "UpdateGeneric", -1);
                sonuc = false;
            }
            finally
            {
                if (_cntxt.Connection.State == System.Data.ConnectionState.Open)
                    _cntxt.Connection.Close();
            }
            return sonuc;

        }

        public bool UpdateGeneric<T>(List<T> entity) where T : class
        {
            _cntxt = new DataContext(_connStr);
            System.Data.Common.DbTransaction tran = null;

            try
            {
                if (_cntxt.Connection.State == System.Data.ConnectionState.Closed)
                    _cntxt.Connection.Open();

                tran = _cntxt.Connection.BeginTransaction();
                _cntxt.Transaction = tran;

                _cntxt.GetTable<T>().AttachAll<T>(entity);
                _cntxt.Refresh(RefreshMode.KeepCurrentValues, entity);
                _cntxt.SubmitChanges();

                tran.Commit();
                _cntxt.Connection.Close();
                return true;
            }
            catch (Exception e)
            {
                tran.Rollback();
                _cntxt.Connection.Close();
                DBEvents.LogException(e, "UpdateGeneric<T>(List<T> entity)", 0);
                return false;
            }
        }

        public bool DeleteGeneric<T>(T entity) where T : class
        {
            bool sonuc = false;
            _cntxt = new DataContext(_connStr);
            System.Data.Common.DbTransaction tran = null;

            try
            {
                if (_cntxt.Connection.State == System.Data.ConnectionState.Closed)
                    _cntxt.Connection.Open();

                

                tran = _cntxt.Connection.BeginTransaction();
                _cntxt.Transaction = tran;

                var Table = _cntxt.GetTable<T>();
                Table.Attach(entity);
                Table.DeleteOnSubmit(entity);
                _cntxt.SubmitChanges();
                sonuc = true;

                tran.Commit();
                _cntxt.Connection.Close();
            }
            catch (Exception exp)
            {
                tran.Rollback();
                _cntxt.Connection.Close();
                LogException(exp, "DeleteGeneric", -1);
                sonuc = false;
            }
            finally
            {
                if (_cntxt.Connection.State == System.Data.ConnectionState.Open)
                    _cntxt.Connection.Close();
            }
            return sonuc;

        }

        public bool DeleteGeneric<T>(List<T> entity) where T : class
        {
            bool sonuc = false;
            _cntxt = new DataContext(_connStr);
            System.Data.Common.DbTransaction tran = null;

            try
            {
                if (_cntxt.Connection.State == System.Data.ConnectionState.Closed)
                    _cntxt.Connection.Open();

                tran = _cntxt.Connection.BeginTransaction();
                _cntxt.Transaction = tran;

                var Table = _cntxt.GetTable<T>();

                foreach (T kayit in entity)
                {
                    Table.Attach(kayit);
                    Table.DeleteOnSubmit(kayit);
                    _cntxt.SubmitChanges();
                }
                sonuc = true;

                tran.Commit();
                _cntxt.Connection.Close();
            }
            catch (Exception exp)
            {
                tran.Rollback();
                _cntxt.Connection.Close();
                LogException(exp, "DeleteGeneric", -1);
                sonuc = false;
            }
            finally
            {
                if (_cntxt.Connection.State == System.Data.ConnectionState.Open)
                    _cntxt.Connection.Close();
            }
            return sonuc;

        }

        public static void LogException(Exception _exp, string _function, int _companyId)
        {
            tblExceptionLog Exlog = new tblExceptionLog();
            Exlog.CompanyId = _companyId;
            Exlog.FunctionName = _function;
            Exlog.InnerMessage = "";
            Exlog.Message = _exp.Message;
            Exlog.RecordDate = DateTime.Now;
            Exlog.Source = _exp.Source;

            DBEvents db = new DBEvents();
            DataContext cntxt = new DataContext(db.ConStr);
            try
            {
                if (cntxt.Connection.State == System.Data.ConnectionState.Closed)
                    cntxt.Connection.Open();

                cntxt.GetTable<tblExceptionLog>().InsertOnSubmit(Exlog);
                cntxt.SubmitChanges();
                cntxt.Connection.Close();
            }
            catch (Exception exp)
            {
                string str = exp.Message;
                cntxt.Connection.Close();
            }

        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
       
    }

    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
                                                            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                    (Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                                Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                    (Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }
    }
}
