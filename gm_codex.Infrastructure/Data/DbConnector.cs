using System.Data;
using System.IO;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace gm_codex.Infrastructure.Data;

public class DbConnector
{
    private readonly string? _connectionString;

    public DbConnector(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public IDbConnection CreateConnection()
    {
        if (string.IsNullOrWhiteSpace(_connectionString))
        {
            throw new InvalidOperationException("Missing connection string: DefaultConnection.");
        }

        var builder = new SqliteConnectionStringBuilder(_connectionString);
        var dataSource = builder.DataSource;
        if (!string.IsNullOrWhiteSpace(dataSource))
        {
            var fullPath = Path.IsPathRooted(dataSource)
                ? dataSource
                : Path.Combine(AppContext.BaseDirectory, dataSource);

            var directory = Path.GetDirectoryName(fullPath);
            if (!string.IsNullOrWhiteSpace(directory))
            {
                Directory.CreateDirectory(directory);
            }

            builder.DataSource = fullPath;
        }

        return new SqliteConnection(builder.ToString());
    }
}
