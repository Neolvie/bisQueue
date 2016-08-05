using System.Data.SqlClient;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace Domain.Core.Database
{
  public class Environment
  {
    private const string ConnectionStringParamName = "FoodDataBase";
    //private const string DefaultConnectionString = @"workstation id=bisQueue.mssql.somee.com;packet size=4096;user id=techus_SQLLogin_1;pwd=az36955hgg;data source=bisQueue.mssql.somee.com;persist security info=False;initial catalog=bisQueue";
    private const string DefaultConnectionString = @"Data Source=NASTYA-NB\SQLEXPRESS;Initial Catalog=BISQUEUE;User ID=admin;Password=11111";
    private static string ConnectionString;

    private static ISessionFactory _sessionFactory;
    private static ISession _session;

    public static ISession Session
    {
      get
      {
        if (!Initialized)
          Initialize();

        return _session;
      }
      set { _session = value; }
    }

    public static ISession OpenSession()
    {
      var session = _sessionFactory.OpenSession();
      session.FlushMode = FlushMode.Auto;
      return session;
    }

    public static bool Initialized { get; set; }

    public static void Initialize(string connectionString = null)
    {
      Initialized = false;

      if (string.IsNullOrEmpty(connectionString))
      {
        var connectionStringParam = System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionStringParamName];

        ConnectionString = connectionStringParam != null ? connectionStringParam.ConnectionString : DefaultConnectionString;
      }
      else
      {
        ConnectionString = connectionString;
      }

      CreateDatabaseIfNotExist(ConnectionString);

      _sessionFactory = CreateSessionFactory();
      Session = OpenSession();

      Initialized = true;
    }

    public static void Close()
    {
      if (_sessionFactory == null || _sessionFactory.IsClosed)
        return;

      _sessionFactory.Close();
    }

    private static ISessionFactory CreateSessionFactory()
    {
      return Fluently
        .Configure()
        .Database(MsSqlConfiguration.MsSql2008.ConnectionString(ConnectionString))
        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Environment>())
        .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
        .BuildSessionFactory();
    }

    private static void CreateDatabaseIfNotExist(string connectionString)
    {
      var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
      var databaseName = connectionStringBuilder.InitialCatalog;

      connectionStringBuilder.InitialCatalog = "master";

      using (var connection = new SqlConnection(connectionStringBuilder.ToString()))
      {
        connection.Open();

        using (var command = connection.CreateCommand())
        {
          command.CommandText = string.Format("select * from master.dbo.sysdatabases where name = '{0}'", databaseName);
          using (var reader = command.ExecuteReader())
          {
            if (reader.HasRows)
              return;
          }

          command.CommandText = string.Format("CREATE DATABASE {0}", databaseName);
          command.ExecuteNonQuery();
        }
      }
    }
  }
}