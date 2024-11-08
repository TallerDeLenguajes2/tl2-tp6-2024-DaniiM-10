using Microsoft.Data.Sqlite;
namespace TP6.Models;

public class PresupuestosRepository 
{
    private string ConnectionString = @"Data Source=db/Tienda.db;Cache=Shared";
    private ProductosRepository productosRepository;

    public PresupuestosRepository() {
        productosRepository = new ProductosRepository();
    }

    public List<Presupuestos> GetPresupuestos() {
        List<Presupuestos> presupuestos = new List<Presupuestos>();

        try
        {
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                string queryString = @"SELECT * FROM Presupuestos;";

                SqliteCommand command = new SqliteCommand(queryString, connection);
                connection.Open();
                using(SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Presupuestos presupuesto = new Presupuestos();
                        presupuesto.setIdPresupuesto(Convert.ToInt32(reader["idPresupuesto"]));
                        presupuesto.NombreDestinatario = reader["NombreDestinatario"].ToString();
                        presupuesto.FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]);

                        presupuesto.setDetallesPresupuesto(
                            GetPresupuestosDetalles(Convert.ToInt32(reader["idPresupuesto"]))
                        );

                        presupuestos.Add(presupuesto);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener presupuestos: {ex.Message}");
            return null;
        }
        return presupuestos;
    }

    public Presupuestos GetPresupuesto(int idPr) {
        Presupuestos presupuesto = null;

        try
        {
            using (SqliteConnection connection = new SqliteConnection(ConnectionString)) {
                string queryString = @"SELECT * 
                FROM Presupuestos
                WHERE idPresupuesto = @idPr;";

                using (SqliteCommand command = new SqliteCommand(queryString, connection)) 
                {
                    command.Parameters.AddWithValue("@idPr", idPr);
                    connection.Open();
                    
                    using (SqliteDataReader reader = command.ExecuteReader()) {
                        if (reader.Read()) {
                            presupuesto = new Presupuestos();
                            presupuesto.setIdPresupuesto(Convert.ToInt32(reader["idPresupuesto"]));
                            presupuesto.NombreDestinatario = reader["NombreDestinatario"].ToString();
                            presupuesto.FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]);

                            presupuesto.setDetallesPresupuesto(
                                GetPresupuestosDetalles(Convert.ToInt32(reader["idPresupuesto"]))
                            );
                        }
                    }
                }
            }   
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener presupuesto por ID: {ex.Message}");
        }
        return presupuesto;
    }

    public bool PostPresupuesto(Presupuestos presupuesto) {
        try
        {
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    string queryString = @"INSERT INTO Presupuestos (NombreDestinatario, FechaCreacion) 
                                        VALUES (@NombreDestinatario, @FechaCreacion);
                                        SELECT last_insert_rowid();";
                    using (SqliteCommand command = new SqliteCommand(queryString, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@NombreDestinatario", presupuesto.NombreDestinatario);
                        command.Parameters.AddWithValue("@FechaCreacion", presupuesto.FechaCreacion);
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al insertar presupuesto: {ex.Message}");
            return false;
        }
    }

    public bool PostPresupuestoDetalle(int idPresupuesto, PresupuestosDetalles presupuestosDetalles) {
        try
        {
            Productos producto = productosRepository.GetProducto(presupuestosDetalles.producto.idProducto);
            Presupuestos presupuesto = GetPresupuesto(idPresupuesto);

            if (producto == null || presupuesto == null) return false;

            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    string queryString = @"INSERT INTO PresupuestosDetalle (idPresupuesto, idProducto, Cantidad) 
                    VALUES (@idPresupuesto, @idProducto, @Cantidad);";
                    using (SqliteCommand command = new SqliteCommand(queryString, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@idPresupuesto", idPresupuesto);
                        command.Parameters.AddWithValue("@idProducto", presupuestosDetalles.producto.idProducto);
                        command.Parameters.AddWithValue("@Cantidad", presupuestosDetalles.cantidad);
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al insertar detalle de presupuesto: {ex.Message}");
            return false;
        }
    }

    public bool DeletePresupuesto(int idPresupuesto)
    {
        var presupuestos = GetPresupuesto(idPresupuesto);

        if (presupuestos != null) 
        {
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string queryString = @"DELETE FROM PresupuestosDetalle WHERE idPresupuesto = @IdP;";
                        string queryString1 = @"DELETE FROM Presupuestos WHERE idPresupuesto = @IdPr;";

                        using (SqliteCommand deleteCommand = new SqliteCommand(queryString, connection, transaction))
                        {
                            deleteCommand.Parameters.AddWithValue("@IdP", idPresupuesto);
                            deleteCommand.ExecuteNonQuery();
                        }

                        using (SqliteCommand deleteCommand1 = new SqliteCommand(queryString1, connection, transaction))
                        {
                            deleteCommand1.Parameters.AddWithValue("@IdPr", idPresupuesto);
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
                        Console.WriteLine($"Error en DeletePresupuesto: {ex.Message}");
                        return false;
                    }
                }
            }
        }
        return false;
    }

    // Funciones Auxiliares
    private List<PresupuestosDetalles> GetPresupuestosDetalles(int idPresupuesto) {
        List<PresupuestosDetalles> pdList = new List<PresupuestosDetalles>();

        try
        {
            using (SqliteConnection connection = new SqliteConnection(ConnectionString)) 
            {
                string queryString = @"SELECT 
                    Productos.idProducto,
                    Productos.Descripcion,
                    Productos.Precio,
                    PresupuestosDetalle.Cantidad
                FROM 
                    Presupuestos
                LEFT JOIN 
                    PresupuestosDetalle ON Presupuestos.idPresupuesto = PresupuestosDetalle.idPresupuesto
                LEFT JOIN 
                    Productos ON PresupuestosDetalle.idProducto = Productos.idProducto
                WHERE 
                    Presupuestos.idPresupuesto = @idPresupuesto;";

                using (SqliteCommand command = new SqliteCommand(queryString, connection)) {
                    command.Parameters.AddWithValue("@idPresupuesto", idPresupuesto);
                    connection.Open();
                    using (SqliteDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            PresupuestosDetalles pd = new PresupuestosDetalles();

                            if(reader.IsDBNull(reader.GetOrdinal("idProducto"))) {
                                return pdList;
                            }

                            Productos product = new Productos();
                            product.idProducto = Convert.ToInt32(reader["idProducto"]);
                            product.Descripcion = reader["Descripcion"].ToString();
                            product.Precio = Convert.ToInt32(reader["Precio"]);

                            pd.producto = product;
                            pd.cantidad = Convert.ToInt32(reader["Cantidad"]);

                            pdList.Add(pd);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener detalles de presupuesto: {ex.Message}");
        }
        return pdList;
    }
}