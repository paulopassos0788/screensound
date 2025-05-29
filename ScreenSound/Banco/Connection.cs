using Microsoft.Data.SqlClient;
using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.Banco;

internal class Connection
{
    private static readonly string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=screensound;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

    public SqlConnection ObterConexao()
    {
        return new SqlConnection(connectionString);
    }
}
