using ModelAgency_Api.Models;
using System.Data;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;
using Microsoft.Data.Sqlite;
using System;
using System.Formats.Asn1;
using System.Data.Common;
using System.Xml;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Collections.Generic;
using ModelAgency_Api.Controllers;

namespace ModelAgency_Api.Repositories
{
    public interface IEventRepository
    {
        Task<List<Event>> GetEvents();

        Task AddEvent(Event modelEvent);

        Task AddEventManaging(Event modelEvent);

        Task UpdateEvent(Event modelEvent);

        Task DeleteEvent(int Id);
    }

    public class EventRepository : IEventRepository
    {
        private readonly string connectionString = "Data Source=C:\\Users\\Julia\\Desktop\\projects\\Coursework-ModelAgency\\ModelAgency.db;Mode=ReadWrite;";
        public EventRepository()
        {

        }

        public async Task AddEvent(Event modelEvent)
        {
            string insertEvent = @"INSERT INTO Event (Id, Type, TargetDate, Address, CreatedAt) 
                                        VALUES (@Id, @Type, @TargetDate, @Address, @CreatedAt)";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = insertEvent;

                command.Parameters.Add("@Id", SqliteType.Integer).Value = modelEvent.Id;
                command.Parameters.Add("@Type", SqliteType.Integer).Value = modelEvent.EventType;
                command.Parameters.Add("@TargetDate", SqliteType.Text).Value = modelEvent.TargetDate;
                command.Parameters.Add("@Address", SqliteType.Text).Value = modelEvent.Address;
                command.Parameters.Add("@CreatedAt", SqliteType.Text).Value = modelEvent.CreatedAt;

                await command.ExecuteNonQueryAsync();
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
            const string getAllEvents = @" SELECT Id, Type, TargetDate, Address, CreatedAt, CreatedBy
                                           FROM Event JOIN EventManaging ON Event.Id == EventManaging.EventId";

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
                        modelEvent.EventType = (EventType)reader.GetInt32(1);
                        modelEvent.TargetDate = DateTime.Parse(reader.GetString(2));
                        modelEvent.Address = reader.GetString(3);

                        if (!reader.IsDBNull(4))
                        {
                            modelEvent.CreatedAt = reader.GetValue(4) == null ? null : DateTime.Parse(reader.GetString(4));
                        }

                        modelEvent.CreatedBy = reader.GetInt32(5);

                        events.Add(modelEvent);
                    }
                }
            }

            return events;
        }

        public async Task UpdateEvent(Event modelEvent)
        {
            const string insertEvent = @"UPDATE Event
                                         SET Type = @Type,
                                             TargetDate = @TargetDate,
                                             Address = @Address,
                                             CreatedAt = @CreatedAt
                                         Where Id = @Id";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = insertEvent;

                command.Parameters.Add("@Id", SqliteType.Integer).Value = modelEvent.Id;
                command.Parameters.Add("@Type", SqliteType.Integer).Value = modelEvent.EventType;
                command.Parameters.Add("@TargetDate", SqliteType.Text).Value = modelEvent.TargetDate;
                command.Parameters.Add("@Address", SqliteType.Text).Value = modelEvent.Address;
                command.Parameters.Add("@CreatedAt", SqliteType.Text).Value = modelEvent.CreatedAt;

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
    }
}
