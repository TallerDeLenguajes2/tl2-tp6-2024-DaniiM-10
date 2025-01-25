using Microsoft.Data.Sqlite;
namespace TP6.Models;

public class ClientesRepository 
{
    private string ConnectionString = @"Data Source=db/Tienda.db;Cache=Shared";
    private PresupuestosRepository presupuestosRepository;
    public ClientesRepository() {
        presupuestosRepository = new PresupuestosRepository();
    }

    public List<Clientes> GetClientes()
    {
        List<Clientes> clientes = new List<Clientes>();

        try
        {
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                string queryString = @"SELECT * FROM Clientes;";

                SqliteCommand command = new SqliteCommand(queryString, connection);
                connection.Open();
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Clientes cliente = new Clientes();
                        cliente.ClienteId = Convert.ToInt32(reader["ClienteId"]);
                        cliente.Nombre = reader["Nombre"].ToString()!;
                        cliente.Email = reader["Email"].ToString()!;
                        cliente.Telefono = reader["Telefono"].ToString()!;
                        clientes.Add(cliente);
                    }
                }
            }    
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en GetClientes: {ex.Message}");
        }

        return clientes;
    }

    public bool PostCliente(Clientes cliente)
    {
        string queryString = @"INSERT INTO Clientes (Nombre, Email, Telefono) 
        VALUES (@Nombre, @Email, @Telefono);";

        try
        {
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(queryString, connection);
                command.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                command.Parameters.AddWithValue("@Email", cliente.Email);
                command.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                command.ExecuteNonQuery();
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en PostCliente: {ex.Message}");
            return false;
        }
    }

    public bool PutCliente(Clientes cliente)
    {
        string queryString = @"UPDATE Clientes SET Nombre = @Nombre, Email = @Email, Telefono = @Telefono 
        WHERE ClienteId = @IdC;";

        try
        {
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                command.Parameters.AddWithValue("@IdC", cliente.ClienteId);
                command.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                command.Parameters.AddWithValue("@Email", cliente.Email);
                command.Parameters.AddWithValue("@Telefono", cliente.Telefono);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery(); // Obtiene el número de filas afectadas

                // Retorna true solo si se actualizó al menos una fila
                return rowsAffected > 0;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en PutCliente: {ex.Message}");
            return false;
        }
    }

    public Clientes GetCliente(int ClienteId)
    {
        try
        {
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                string queryString = @"SELECT * FROM Clientes 
                WHERE ClienteId = @IdC;";

                SqliteCommand command = new SqliteCommand(queryString, connection);
                command.Parameters.AddWithValue("@IdP", ClienteId);

                connection.Open();

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Clientes cliente = new Clientes();
                        cliente.ClienteId = (Convert.ToInt32(reader["ClienteId"]));
                        cliente.Nombre = reader["Nombre"].ToString();
                        cliente.Email = reader["Email"].ToString();
                        cliente.Telefono = reader["Telefono"].ToString();
                        return cliente;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en GetCliente: {ex.Message}");
        }

        return null;
    }

    public bool DeleteCliente(int ClienteId)
    {
        Clientes cliente = GetCliente(ClienteId);

        if (cliente != null) 
        {
            int IdPresupuesto = presupuestosRepository.GetIdPresupuesto(ClienteId);
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        if(IdPresupuesto != 0) {
                            bool deletePresupuesto = presupuestosRepository.DeletePresupuesto(IdPresupuesto);
                        }
                        string queryString = @"DELETE FROM Clientes WHERE ClienteId = @IdC;";

                        using (SqliteCommand deleteCommand1 = new SqliteCommand(queryString, connection, transaction))
                        {
                            deleteCommand1.Parameters.AddWithValue("@IdC", ClienteId);
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
                        Console.WriteLine($"Error en DeleteCliente: {ex.Message}");
                        return false;
                    }
                }
            }
        }
        return false;
    }
}