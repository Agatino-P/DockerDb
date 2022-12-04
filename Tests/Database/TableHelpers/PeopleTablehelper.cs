using Npgsql;
using System;
using System.Threading.Tasks;

namespace Shared.Tests.Database.TableHelpers;

public class PeopleTableHelper
{
    private readonly NpgsqlConnection connection;

    public PeopleTableHelper(NpgsqlConnection connection)
    {
        this.connection = connection;
    }

    public async Task<uint> Feed(
        Guid id,
        int agicapId,
        string source,
        string attachmentPath,
        string fileName,
        string fileExtension,
        long fileSize,
        string senderEmail,
        DateTime dateOfCreation)
    {
        const string sqlInsert = @"
                INSERT INTO invoices_management.attachments
                (id,agicap_id,source,attachment_path,filename,file_extension,file_size,sender_email,date_of_creation)
                VALUES (@id,@agicap_id,@source,@attachment_path,@filename,@file_extension,@file_size,@sender_email,@date_of_creation)
                RETURNING xmin;
                ";

        try
        {
            await connection.OpenAsync();
            await using NpgsqlCommand command = new(sqlInsert, connection)
            {
                Parameters =
                {
                    new NpgsqlParameter("id", id),
                    new NpgsqlParameter("agicap_id", agicapId),
                    new NpgsqlParameter("source", source),
                    new NpgsqlParameter("attachment_path", attachmentPath),
                    new NpgsqlParameter("filename", fileName),
                    new NpgsqlParameter("file_extension", fileExtension),
                    new NpgsqlParameter("file_size", fileSize),
                    new NpgsqlParameter("sender_email", senderEmail),
                    new NpgsqlParameter("date_of_creation", dateOfCreation),
                }
            };

            NpgsqlDataReader reader = command.ExecuteReader();
            reader.Read();
            return Convert.ToUInt32(reader["xmin"]);
        }
        finally
        {
            await connection.CloseAsync();
        }
    }

    public async Task CleanupAsync()
    {
        try
        {
            connection.Open();
            const string sqlTruncate = "TRUNCATE test_schema.people";
            NpgsqlCommand command = new(sqlTruncate, connection);
            await command.ExecuteNonQueryAsync();
        }
        finally
        {
            connection.Close();
        }
    }
}