using Dapper;
using Microsoft.Data.SqlClient;
using tiendapersonal.Models;

namespace tiendapersonal.Servicios
{

    public interface IRepositorioTiposCuentas
    {
        Task Crear(TipoCuenta tipoCuenta);
        Task<bool> Existe(string nombre, int usuarioId);
    }

    public class RepositorioTipoCuentas:IRepositorioTiposCuentas
    {
        private readonly string connectionString;

        public RepositorioTipoCuentas(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        public  async Task Crear(TipoCuenta tipoCuenta)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>
                                                ($@"INSERT INTO TiposCuentas (Nombre,UsuarioId,Orden) 
                                                Values (@Nombre,@UsuarioId,0);
                                                SELECT SCOPE_IDENTITY();", tipoCuenta);
            tipoCuenta.Id = id;

        }



        //METODO QUE VERIFICA QUE TIPO CUENTAS NO SEA DUPLICADO
        public async Task<bool> Existe(string nombre, int usuarioId)
        {
            using var connection= new SqlConnection(connectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>(
                                                                    @"SELECT 1
                                                                        FROM TiposCuentas
                                                                            WHERE Nombre=@Nombre AND UsuarioId=@usuarioId;",
                                                                    new { nombre, usuarioId});
            return existe == 1;
        }
    }
}
