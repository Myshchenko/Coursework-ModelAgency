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
        private readonly IConfiguration _configuration;
        private string connectionString;

        public ModelRepository(IConfiguration configuration)
        {
            _configuration = configuration;

            string? conStr = _configuration["ConnectionString"];
            if (!string.IsNullOrEmpty(conStr))
            {
                connectionString = conStr;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public async Task<List<Model>> GetModels()
        {
            const string getAllModels = @"SELECT User.Id, User.Name, User.Surname, Model.Height, Model.Weight, Model.Chest, Model.Waist, Model.Hips, Model.Shoes, Model.Hair, User.Email, User.Gender_Id
                                          FROM User join Model ON User.Id == Model.ModelId";

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
                        model.Height = reader.GetDouble(3);
                        model.Weight = reader.GetDouble(4);
                        model.Chest = reader.GetDouble(5);
                        model.Waist = reader.GetDouble(6);
                        model.Hips = reader.GetDouble(7);
                        model.Shoes = reader.GetDouble(8);
                        model.Hair = reader.GetString(9);
                        model.Email = reader.GetString(10);
                        model.GenderId = reader.GetInt32(11);

                        models.Add(model);
                    }
                }
            }

            return models;
        }
    }
}
