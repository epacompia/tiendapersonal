using Dapper;
using Microsoft.Data.SqlClient;
using tiendapersonal.Models;

namespace tiendapersonal.Servicios
{

    public interface IRepositorioTiposCuentas
    {
        Task Actualizar(TipoCuenta tipoCuenta);
        Task Borrar(int id);
        Task Crear(TipoCuenta tipoCuenta);
        Task<bool> Existe(string nombre, int usuarioId);
        Task<IEnumerable<TipoCuenta>> Obtener(int usuarioId);
        Task<TipoCuenta> ObtenerPorId(int id, int usuarioId);
        Task Ordenar(IEnumerable<TipoCuenta> tipoCuentasOrdenados);
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
                                                ("TiposCuentas_Insertar",
                                                new {usuarioId=tipoCuenta.UsuarioId,
                                                nombre=tipoCuenta.Nombre},
                                                commandType:System.Data.CommandType.StoredProcedure);
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


        //METODO PARA LISTADO DE TIPO CUENTAS
        public async Task<IEnumerable<TipoCuenta>> Obtener (int usuarioId)
        {
            using var connection= new SqlConnection(connectionString);
            return await connection.QueryAsync<TipoCuenta>(@"select Id, Nombre,Orden
                                    from TiposCuentas where UsuarioId=@usuarioId ORDER BY Orden",new {usuarioId });
        }



        //ACTUALIZANDO TIPOCUENTAS
        public async Task Actualizar(TipoCuenta tipoCuenta)
        {
            using var connection=new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"update TiposCuentas
                                            set Nombre=@Nombre
                                            where Id=@Id",tipoCuenta);
        } 
        //ACTUALIZANDO TIPO CUENTAS 2
        public async Task<TipoCuenta> ObtenerPorId (int id,int usuarioId)
        {
            using var connection= new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<TipoCuenta>(@"select Id,Nombre,Orden from TiposCuentas
                                                                where Id=@Id and UsuarioId=@usuarioId", new { id, usuarioId });
        }

        //METODO PARA BORRAR REGISTRO
        public async Task Borrar(int id)
        {    
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("DELETE TiposCuentas where Id=@Id", new {id});
        }


        //METODO PARA ORDENAR LOS REGISTROS
        public async Task Ordenar(IEnumerable<TipoCuenta> tipoCuentasOrdenados)
        {
            var query = "update TiposCuentas set Orden=@Orden where Id=@Id;";
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(query,tipoCuentasOrdenados);
        }
    }
}
