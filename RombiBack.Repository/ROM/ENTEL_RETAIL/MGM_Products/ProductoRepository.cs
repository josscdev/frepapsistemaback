using RombiBack.Abstraction;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Producto;
using Microsoft.Extensions.Configuration;

using System.Data.SqlClient;

namespace RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_Products
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly DataAcces _dbConnection;
        public ProductoRepository(DataAcces dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<Producto> Add(Producto entity)
        {
            using (SqlConnection connection =new SqlConnection(_dbConnection.GetConnectionENTEL_RETAIL()))
            {
                await connection.OpenAsync();

                string insertQuery = "INSERT INTO RETAIL_ModeloEquipo (intModeloEquipoID, strModeloEquipoDesc, " +
                                     "strModeloEquipoEstado, strModeloEquipoUsuCre, " +
                                     "dteModeloEquipoFeCre, strModeloEquipoUsuModi, " +
                                     "dteModeloEquipoFeModi, strModeloEquipoUsuAnul, " +
                                     "dteModeloEquipoFeAnul) " +
                                     "VALUES (@intModeloEquipoID, @strModeloEquipoDesc, " +
                                     "@strModeloEquipoEstado, @strModeloEquipoUsuCre, " +
                                     "@dteModeloEquipoFeCre, @strModeloEquipoUsuModi, " +
                                     "@dteModeloEquipoFeModi, @strModeloEquipoUsuAnul, " +
                                     "@dteModeloEquipoFeAnul); " +
                                     "SELECT SCOPE_IDENTITY()";

                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@intModeloEquipoID", entity.intModeloEquipoID);
                    command.Parameters.AddWithValue("@strModeloEquipoDesc", entity.strModeloEquipoDesc);
                    command.Parameters.AddWithValue("@strModeloEquipoEstado", entity.strModeloEquipoEstado);
                    command.Parameters.AddWithValue("@strModeloEquipoUsuCre", entity.strModeloEquipoUsuCre);
                    command.Parameters.AddWithValue("@dteModeloEquipoFeCre", entity.dteModeloEquipoFeCre);
                    command.Parameters.AddWithValue("@strModeloEquipoUsuModi", entity.strModeloEquipoUsuModi);
                    command.Parameters.AddWithValue("@dteModeloEquipoFeModi", entity.dteModeloEquipoFeModi);
                    command.Parameters.AddWithValue("@strModeloEquipoUsuAnul", entity.strModeloEquipoUsuAnul);
                    command.Parameters.AddWithValue("@dteModeloEquipoFeAnul", entity.dteModeloEquipoFeAnul);

                    // Ejecutar la consulta y obtener el ID generado automáticamente
                    int generatedId = Convert.ToInt32(await command.ExecuteScalarAsync());
                    entity.intModeloEquipoID = generatedId;

                    return entity;
                }
            }
        }

      

     

       



        public async Task<List<Producto>> ObtenerTodo()
        {
            using (SqlConnection dbConnection = new SqlConnection(_dbConnection.GetConnectionENTEL_RETAIL()))
            {
                dbConnection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM RETAIL_ModeloEquipo", (SqlConnection)dbConnection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        List<Producto> result = new List<Producto>();

                        while (await reader.ReadAsync())
                        {
                            Producto producto = new Producto
                            {
                                // Asigna directamente los valores desde el SqlDataReader a las propiedades del objeto Producto
                                intModeloEquipoID = Convert.ToInt32(reader["intModeloEquipoID"]),
                                strModeloEquipoDesc = reader["strModeloEquipoDesc"].ToString(),
                                //Precio = Convert.ToDecimal(reader["Precio"]),
                                // Agrega más asignaciones según las propiedades de tu clase Producto
                            };

                            result.Add(producto);
                        }

                        return result;
                    }
                }
            }
        }

       
    }
}