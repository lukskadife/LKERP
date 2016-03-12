using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;

namespace LKLibrary.DbClasses
{
    public class DBEventsLogo : IDisposable
    {
        static string _connStr;
        DataContext _cntxt;

        public string ConStr
        {
            get { return _connStr; }
        }

        public DBEventsLogo()
        {
            //_connStr = "Data Source=78.187.196.108;Initial Catalog=PSY;User ID=isd;Password=isd";
            //_connStr = "Data Source=SAMET-PC\\SQL2008;Initial Catalog=LOGO;User ID=saLogo;Password=isd";
            //_connStr = "Data Source=GCAN;Initial Catalog=LKDB;User ID=sa;Password=sapass";
            //_connStr = "Data Source=DEHASERVER1\\SQLEXPRESS;Initial Catalog=LKDB;User ID=sa;Password=isd";
            //_connStr = "Data Source=77.79.121.145\\ISDSQLSRV;Initial Catalog=LKDB;User ID=isdtest;Password=isdtest";
            //_connStr = "Data Source=SAMET-PC\\SQLEXPRESS;Initial Catalog=PSY;User ID=sa;Password=samet1986";
            //_connStr = "Data Source=SAMET-PC\\SQLEXPRESS;Initial Catalog=PSYDeha;User ID=sa;Password=samet1986";
            //_connStr = "Data Source=SAMET-PC\\SQLEXPRESS;Initial Catalog=PSYDemo;User ID=sa;Password=samet1986";
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

        public bool UpdateGeneric<T>(T entity) where T : class
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
                _cntxt.Refresh(RefreshMode.KeepCurrentValues, entity);
                _cntxt.SubmitChanges();
                sonuc = true;

                tran.Commit();
                _cntxt.Connection.Close();
            }
            catch (Exception exp)
            {
                tran.Rollback();
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

}
