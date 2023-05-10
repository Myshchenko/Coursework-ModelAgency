using Microsoft.Data.Sqlite;
using ModelAgency_Api.Models;

namespace ModelAgency_Api.Repositories
{
    public interface IModelRepository
    {
        Task<List<Model>> GetModels();
    }

    public class ModelRepository : IModelRepository
    {
        private readonly string connectionString = "Data Source=C:\\Users\\Julia\\Desktop\\projects\\Coursework-ModelAgency\\ModelAgency.db;Mode=ReadWrite;";

        public async Task<List<Model>> GetModels()
        {
            const string getAllModels = @" SELECT *
                                           FROM Model";

            List<Model> models = new List<Model>();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = getAllModels;

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        var model = new Model();

                        model.Id = reader.GetInt32(0);
                        model.Name = reader.GetString(1);
                        model.Surname = reader.GetString(2);
                        model.DateOfBith = DateTime.Parse(reader.GetString(3));
                        model.Height = reader.GetInt32(4);
                        model.Weight = reader.GetInt32(5);
                        model.Chest = reader.GetInt32(6);
                        model.Waist = reader.GetInt32(7);
                        model.Hips = reader.GetInt32(8);
                        model.Email = reader.GetString(9);

                        models.Add(model);
                    }
                }
            }

            return models;
        }
    }
}
