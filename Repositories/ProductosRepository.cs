using Microsoft.Data.Sqlite;
namespace TP6.Models;

public class ProductosRepository
{
    private string ConnectionString = @"Data Source=db/Tienda.db;Cache=Shared";

    public ProductosRepository() {}

    public List<Productos> GetProductos()
    {
        List<Productos> productos = new List<Productos>();

        try
        {
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                string queryString = @"SELECT * FROM Productos;";

                SqliteCommand command = new SqliteCommand(queryString, connection);
                connection.Open();
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Productos producto = new Productos();
                        producto.IdProducto = (Convert.ToInt32(reader["idProducto"]));
                        producto.Descripcion = reader["Descripcion"].ToString();
                        producto.Precio = Convert.ToInt32(reader["Precio"]);
                        productos.Add(producto);
                    }
                }
            }    
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en GetProductos: {ex.Message}");
        }

        return productos;
    }

    public bool PostProducto(Productos producto)
    {
        string queryString = @"INSERT INTO Productos (Descripcion, Precio) 
        VALUES (@Descripcion, @Precio);";

        try
        {
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(queryString, connection);
                command.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                command.Parameters.AddWithValue("@Precio", producto.Precio);
                command.ExecuteNonQuery();
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en PostProducto: {ex.Message}");
            return false;
        }
    }

    public bool PutProducto(int IdProducto, Productos producto)
    {
        string queryString = @"UPDATE Productos SET Descripcion = @Descripcion, Precio = @Precio 
        WHERE idProducto = @IdP;";

        try
        {
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                command.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                command.Parameters.AddWithValue("@Precio", producto.Precio);
                command.Parameters.AddWithValue("@IdP", IdProducto);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery(); // Obtiene el número de filas afectadas

                // Retorna true solo si se actualizó al menos una fila
                return rowsAffected > 0;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en PutProducto: {ex.Message}");
            return false;
        }
    }

    public Productos GetProducto(int IdProducto)
    {
        try
        {
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                string queryString = @"SELECT * FROM Productos 
                WHERE idProducto = @IdP;";

                SqliteCommand command = new SqliteCommand(queryString, connection);
                command.Parameters.AddWithValue("@IdP", IdProducto);

                connection.Open();

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Productos producto = new Productos();
                        producto.IdProducto = (Convert.ToInt32(reader["idProducto"]));
                        producto.Descripcion = reader["Descripcion"].ToString();
                        producto.Precio = Convert.ToInt32(reader["Precio"]);
                        return producto;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en GetProducto: {ex.Message}");
        }

        return null;
    }

    public bool DeleteProducto(int IdProducto)
    {
        var producto = GetProducto(IdProducto);

        if (producto != null) 
        {
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string queryString = @"DELETE FROM PresupuestosDetalle WHERE idProducto = @IdP;";
                        string queryString1 = @"DELETE FROM Productos WHERE idProducto = @IdPr;";

                        using (SqliteCommand deleteCommand = new SqliteCommand(queryString, connection, transaction))
                        {
                            deleteCommand.Parameters.AddWithValue("@IdP", IdProducto);
                            deleteCommand.ExecuteNonQuery();
                        }

                        using (SqliteCommand deleteCommand1 = new SqliteCommand(queryString1, connection, transaction))
                        {
                            deleteCommand1.Parameters.AddWithValue("@IdPr", IdProducto);
                            int rowsAffected = deleteCommand1.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                transaction.Commit();
                                return true;
                            }
                            else
                            {
                                transaction.Rollback();
                                return false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine($"Error en DeleteProducto: {ex.Message}");
                        return false;
                    }
                }
            }
        }
        return false;
    }
}