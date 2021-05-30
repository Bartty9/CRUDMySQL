using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_MySQL
{
    class DbMeetings
    {
        public static MySqlConnection GetConnection()
        {
            string sql = "datasource=localhost;port=3306;username=root;password=;database=meetings";
            MySqlConnection connection = new MySqlConnection(sql);
            try
            {
                connection.Open();
            }
            catch (MySqlException exception)
            {
                MessageBox.Show("MySqlConnection \n" + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return connection;
        }

        public static void NewMeeting(Meeting meeting)
        {
            string sql = "INSERT INTO meetings_table VALUES (NULL, @MeetingName, @MeetingPlace, @MeetingDate, @MeetingTime, NULL)";
            MySqlConnection connection = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@MeetingName", MySqlDbType.VarChar).Value = meeting.Name;
            cmd.Parameters.Add("@MeetingPlace", MySqlDbType.VarChar).Value = meeting.Place;
            cmd.Parameters.Add("@MeetingDate", MySqlDbType.VarChar).Value = meeting.Date;
            cmd.Parameters.Add("@MeetingTime", MySqlDbType.VarChar).Value = meeting.Time;
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Added Successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException exception)
            {
                MessageBox.Show("Meeting not created. \n" + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            connection.Close();
        }

        public static void UpdateMeeting(Meeting meeting, string id)
        {
            string sql = "UPDATE meetings_table SET Name = @MeetingName, Place = @MeetingPlace, Date = @MeetingDate, Time = @MeetingTime WHERE ID = @MeetingID";
            MySqlConnection connection = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@MeetingID", MySqlDbType.VarChar).Value = id;
            cmd.Parameters.Add("@MeetingName", MySqlDbType.VarChar).Value = meeting.Name;
            cmd.Parameters.Add("@MeetingPlace", MySqlDbType.VarChar).Value = meeting.Place;
            cmd.Parameters.Add("@MeetingDate", MySqlDbType.VarChar).Value = meeting.Date;
            cmd.Parameters.Add("@MeetingTime", MySqlDbType.VarChar).Value = meeting.Time;
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Updated Successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException exception)
            {
                MessageBox.Show("Meeting not updated. \n" + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            connection.Close();
        }

        public static void SignUpParticipant(Participant participant, string id)
        {
            string sql = "INSERT INTO " + id + "table" + " VALUES (NULL, @Participant)";
            MySqlConnection connection = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@Participant", MySqlDbType.VarChar).Value = participant.Name + " " + participant.Email;
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Signed up Successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException exception)
            {
                MessageBox.Show("Can not sign up. \n" + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            connection.Close();
        }
        public static void CreateParticipantsTable(string id)
        {
            string sql = "CREATE TABLE " + id + "table" + " (ParticipantID int NOT NULL AUTO_INCREMENT, Participant varchar(50), PRIMARY KEY (ParticipantID))";

            MySqlConnection connection = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            cmd.CommandType = CommandType.Text;
            try
            {
                cmd.ExecuteNonQuery();
                //MessageBox.Show("Table created.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException exception)
            {
                MessageBox.Show("Can not create table. \n" + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            connection.Close();
        }

        public static bool CheckIfTableExists(string id)
        {
            string sql = "SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = 'meetings' AND table_name = \'" + id + "table\'";

            MySqlConnection connection = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            cmd.CommandType = CommandType.Text;
            MySqlDataReader reader = cmd.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    int count = reader.GetInt32(0);
                    if (count == 0)
                    {
                        return false;
                    }
                    else if (count == 1)
                    {
                        return true;
                    }
                }
                MessageBox.Show("Something went wrong.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            catch (MySqlException exception)
            {
                MessageBox.Show("Table Exists Error. \n" + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            connection.Close();
        }

        public static bool CheckNumberOfParticipants(string id)
        {
            string sql = "SELECT COUNT(*) FROM " + id + "table";

            MySqlConnection connection = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            cmd.CommandType = CommandType.Text;
            MySqlDataReader reader = cmd.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    int count = reader.GetInt32(0);
                    return count < 25;                   
                }
                MessageBox.Show("Something went wrong.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            catch (MySqlException exception)
            {
                MessageBox.Show("Number of Participants Error. \n" + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            connection.Close();
        }

        public static void DeleteMeeting(string id)
        {
            string sql = "DELETE FROM meetings_table WHERE ID = @MeetingID";
            MySqlConnection connection = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@MeetingID", MySqlDbType.VarChar).Value = id;
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Deleted Successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException exception)
            {
                MessageBox.Show("Meeting not deleted. \n" + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            connection.Close();
        }

        public static void DisplaySearch(string query, DataGridView dataGridView)
        {
            string sql = query;
            MySqlConnection connection = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGridView.DataSource = dataTable;
            connection.Close();
        }
    }
}
