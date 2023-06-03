 using ModelAgency_Api.Models;
using Microsoft.Data.Sqlite;

namespace ModelAgency_Api.Repositories
{
    public interface IEventRepository
    {
        Task<List<Event>> GetEvents();

        Task<List<ModelEvent>> GetEventsForModel(int modelId);

        Task<List<Event>> GetAvailableEventsForSpecificModel(int modelId);

        Task AddEvent(Event modelEvent);

        Task AddEventManaging(Event modelEvent);

        Task AddModelToTheEvent(ModelEventCoordinates modelEventCoordinates);

        Task UpdateEvent(Event modelEvent);

        Task UpdateModelEventResponce(ModelEventCoordinates modelEventCoordinates);

        Task DeleteEvent(int Id);
    }

    public class EventRepository : IEventRepository
    {
        private readonly IConfiguration _configuration;
        private string connectionString;

        public EventRepository(IConfiguration configuration)
        {
            _configuration = configuration;

            string? conStr = _configuration["ConnectionString"];
            if(!string.IsNullOrEmpty(conStr))
            {
                connectionString = conStr;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public async Task AddEvent(Event modelEvent)
        {
            string insertEvent = @"INSERT INTO Event (Details, Type, TargetDate, Address, CreatedAt) 
                                        VALUES (@Details, @Type, @TargetDate, @Address, @CreatedAt); 
                                        SELECT last_insert_rowid();"; 


            using (var connection = new SqliteConnection(connectionString))
            {
               connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = insertEvent;

                int eventType = 0;

                switch (modelEvent.EventType)
                {
                    case "Показ":
                        eventType = (int)EventType.Show;
                        break;
                    case "Фотосесія":
                        eventType = (int)EventType.Photoshoot;
                        break;
                    default: return;
                }

                command.Parameters.Add("@Details", SqliteType.Text).Value = modelEvent.Details;
                command.Parameters.Add("@Type", SqliteType.Integer).Value = eventType;
                command.Parameters.Add("@TargetDate", SqliteType.Text).Value = modelEvent.TargetDate;
                command.Parameters.Add("@Address", SqliteType.Text).Value = modelEvent.Address;
                command.Parameters.Add("@CreatedAt", SqliteType.Text).Value = modelEvent.CreatedAt;

               modelEvent.Id = Convert.ToInt32(await command.ExecuteScalarAsync());
            }
        }

        public async Task DeleteEvent(int Id)
        {
            string deleteEvent = @"DELETE FROM Event
                                         Where Id = @Id";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = deleteEvent;

                command.Parameters.Add("@Id", SqliteType.Integer).Value = Id;

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<List<Event>> GetEvents()
        {
            const string getAllEvents = @"SELECT Event.Id, Event.Details, EventType.Name, Event.TargetDate, Event.Address, Event.createdAt
                                            FROM Event
                                            JOIN EventType ON Event.Type = EventType.Id";

            List<Event> events = new List<Event>();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = getAllEvents;

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        var modelEvent = new Event();
                        modelEvent.Id = reader.GetInt32(0);
                        modelEvent.Details = reader.GetString(1);
                        modelEvent.EventType = reader.GetString(2);
                        modelEvent.TargetDate = DateTime.Parse(reader.GetString(3));
                        modelEvent.Address = reader.GetString(4);
                        modelEvent.CreatedAt = DateTime.Parse(reader.GetString(5));

                        events.Add(modelEvent);
                    }
                }
            }

            return events;
        }

        public async Task<List<ModelEvent>> GetEventsForModel(int modelId)
        {
            const string getAllEvents = @"SELECT Event.Id, Event.Details, EventType.Name, Event.TargetDate, Event.Address, AcceptingType.Name
                                          FROM Event
                                          JOIN EventType ON Event.Type = EventType.Id
                                          JOIN ModelEvent ON Event.Id = ModelEvent.EventId
                                          JOIN AcceptingType ON ModelEvent.IsAccepted = AcceptingType.Id
                                          WHERE ModelEvent.ModelId == @ModelId";

            List<ModelEvent> events = new List<ModelEvent>();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.Parameters.Add("@ModelId", SqliteType.Integer).Value = modelId;
                command.CommandText = getAllEvents;

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        var modelEvent = new ModelEvent();
                        modelEvent.Id = reader.GetInt32(0);
                        modelEvent.Details = reader.GetString(1);
                        modelEvent.EventType = reader.GetString(2);
                        modelEvent.TargetDate = DateTime.Parse(reader.GetString(3));
                        modelEvent.Address = reader.GetString(4);
                        modelEvent.AcceptingType = reader.GetString(5);

                        events.Add(modelEvent);
                    }
                }
            }

            return events;
        }

        public async Task<List<Event>> GetAvailableEventsForSpecificModel(int modelId)
        {
            const string getAllEvents = @"SELECT Event.Id, Event.Details, EventType.Name, Event.TargetDate, Event.Address 
                                          FROM Event join EventType on Event.Type = EventType.Id
                                          WHERE Event.Id NOT IN (SELECT EventId FROM ModelEvent JOIN Event ON Event.Id = ModelEvent.EventId WHERE ModelId = @ModelId)
                                          AND Event.TargetDate NOT IN (SELECT TargetDate FROM Event JOIN ModelEvent ON Event.Id = ModelEvent.EventId WHERE ModelId = @ModelId)";

            List<Event> events = new List<Event>();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.Parameters.Add("@ModelId", SqliteType.Integer).Value = modelId;
                command.CommandText = getAllEvents;

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        var modelEvent = new Event();
                        modelEvent.Id = reader.GetInt32(0);
                        modelEvent.Details = reader.GetString(1);
                        modelEvent.EventType = reader.GetString(2);
                        modelEvent.TargetDate = DateTime.Parse(reader.GetString(3));
                        modelEvent.Address = reader.GetString(4);


                        events.Add(modelEvent);
                    }
                }
            }

            return events;
        }

        public async Task UpdateEvent(Event modelEvent)
        {
            const string insertEvent = @"UPDATE Event
                                         SET Details = @Details,
                                             Type = @Type,
                                             TargetDate = @TargetDate,
                                             Address = @Address,
                                             CreatedAt = @CreatedAt
                                         Where Id = @Id";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = insertEvent;

                int eventType = 0;

                switch (modelEvent.EventType)
                {
                    case "Показ":
                        eventType = (int)EventType.Show;
                        break;
                    case "Фотосесія":
                        eventType = (int)EventType.Photoshoot;
                        break;
                    default: return;
                }

                command.Parameters.Add("@Id", SqliteType.Integer).Value = modelEvent.Id;
                command.Parameters.Add("@Details", SqliteType.Text).Value = modelEvent.Details;
                command.Parameters.Add("@Type", SqliteType.Integer).Value = eventType;
                command.Parameters.Add("@TargetDate", SqliteType.Text).Value = modelEvent.TargetDate;
                command.Parameters.Add("@Address", SqliteType.Text).Value = modelEvent.Address;
                command.Parameters.Add("@CreatedAt", SqliteType.Text).Value = modelEvent.CreatedAt;

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateModelEventResponce(ModelEventCoordinates modelEventCoordinates)
        {
            const string updateModelEventResponce = @"UPDATE ModelEvent
                                                      SET EventId = @EventId,
                                                          ModelId = @ModelId,
                                                          IsAccepted = @IsAccepted
                                                      Where EventId = @EventId AND ModelId = @ModelId";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = updateModelEventResponce;

                command.Parameters.Add("@EventId", SqliteType.Integer).Value = modelEventCoordinates.EventId;
                command.Parameters.Add("@ModelId", SqliteType.Integer).Value = modelEventCoordinates.ModelId;
                command.Parameters.Add("@IsAccepted", SqliteType.Text).Value = (int)modelEventCoordinates.ModelEventResponceType;

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task AddEventManaging(Event modelEvent)
        {
            const string insertEvent = @"INSERT INTO EventManaging (EventId, CreatedBy) 
                                        VALUES (@EventId, @CreatedBy)";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = insertEvent;

                command.Parameters.Add("@EventId", SqliteType.Integer).Value = modelEvent.Id;
                command.Parameters.Add("@CreatedBy", SqliteType.Integer).Value = modelEvent.CreatedBy;

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task AddModelToTheEvent(ModelEventCoordinates modelEventCoordinates)
        {
            const string insertEvent = @"INSERT INTO ModelEvent (EventId, ModelId, IsAccepted) 
                                        VALUES (@EventId, @ModelId, @IsAccepted)";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = insertEvent;

                command.Parameters.Add("@EventId", SqliteType.Integer).Value = modelEventCoordinates.EventId;
                command.Parameters.Add("@ModelId", SqliteType.Integer).Value = modelEventCoordinates.ModelId;
                command.Parameters.Add("@IsAccepted", SqliteType.Integer).Value = (int)modelEventCoordinates.ModelEventResponceType;

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
