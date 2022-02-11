using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Primitives;

namespace SoundChoice.Models
{
    public class SqlModel 
    {
        public SqlConnection Connection { get; set; }
        public SqlCommand Command { get; set; }
        public string Query { get; set; }
    }
}
