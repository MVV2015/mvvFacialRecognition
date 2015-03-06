using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Text;

using Neurotec.Biometrics;
using Neurotec.Images;

namespace mvvFacialRecognition
{
    class dbInterface
    {
        string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source ="+ Application.StartupPath +" secureDatabase.accdb";
        bool exists = false;

        public dbInterface()
        {
            try
            {
                OleDbConnection myDbConnection = new OleDbConnection(connectionString);
                myDbConnection.Open();
                
                myDbConnection.Close();
            }
            catch (OleDbException)
            {
// How do you want to deal with no database?                
                //MessageBox.Show("Unable to find database. Exception Thrown: " + ex.ToString());
                exists = false;
            }

            if (!exists)
            {
// Should I try to create one here?                
            }
        }

        internal List<string> populateUserIdList()
        {
            List<string> userIds = new List<string>();
            try
            {
                string queryString;
                cryptography decrypt = new cryptography();
                OleDbConnection myDbConnection = new OleDbConnection(connectionString);

                queryString = String.Format("SELECT userId from myTable");
                OleDbCommand command = new OleDbCommand(queryString, myDbConnection);
                myDbConnection.Open();
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    userIds.Add(decrypt.decryptString((string)reader.GetValue(0)));
                }
                reader.Close();

                myDbConnection.Close();
                return userIds;
            }
            catch (OleDbException ex)
            {
                DisplayOleDbErrorCollection(ex);
                return userIds;
            }
        }

        public void insertEntry(string fName, string lName, string id, NImage imageBmp, NLTemplate template)
        {
            OleDbConnection myDbConnection = new OleDbConnection(connectionString);

            using (myDbConnection)
            {
                cryptography encrypt = new cryptography();
                string crypFName = encrypt.encryptString(fName);
                string cryptLName = encrypt.encryptString(lName);
                string cryptUserId = encrypt.encryptString(id);
                byte[] cryptImage = encrypt.encryptImage(imageBmp);
                //byte[] cryptTemplate = encrypt.encryptTemplate(template);

                OleDbCommand command = new OleDbCommand();
                command.CommandType = CommandType.Text;
                command.Connection = myDbConnection;
                command.CommandText = "INSERT INTO myTable (firstName, lastName, userId, picture, template) Values (@p1,@p2,@p3,@p4,@p5)";
                command.Parameters.AddWithValue("@P1", crypFName);
                command.Parameters.AddWithValue("@p2", cryptLName);
                command.Parameters.AddWithValue("@p3", cryptUserId);
                command.Parameters.AddWithValue("@p4", cryptImage);
                //command.Parameters.AddWithValue("@p5", cryptTemplate);

                try
                {
                    myDbConnection.Close();
                    myDbConnection.Open();
                    command.ExecuteNonQuery();
                }
                catch (OleDbException ex)
                {
                    DisplayOleDbErrorCollection(ex);
                    MessageBox.Show("No records were recorded");
                }
                finally
                {
                    myDbConnection.Close();
                }
            }
        }

        internal bool userIdExists(string enrolleeId)
        {
            cryptography encrypt = new cryptography();
            bool exists = false;
            string encryptedId = encrypt.encryptString(enrolleeId);

            OleDbConnection myDbConnection = new OleDbConnection(connectionString);
            OleDbCommand command = new OleDbCommand();
            command.CommandType = CommandType.Text;
            command.Connection = myDbConnection;
            command.CommandText = "SELECT COUNT(*) from myTable where userId = @p1";
            command.Parameters.AddWithValue("@p1", encryptedId);

            try
            {
                myDbConnection.Open();
                int result = (int)command.ExecuteScalar();
                if (result > 0)
                {
                    exists = true;
                }
                return exists;
            }
            catch (OleDbException ex)
            {
                DisplayOleDbErrorCollection(ex);
                return exists;
            }
            finally
            {
                myDbConnection.Close();
            }
        }

        internal bool userExists(string firstName, string lastName)
        {
            cryptography encrypt = new cryptography();
            string encryptedFName = encrypt.encryptString(firstName);
            string encryptedLName = encrypt.encryptString(lastName);
            bool exists = false;

            OleDbConnection myDbConnection = new OleDbConnection(connectionString);
            OleDbCommand command = new OleDbCommand();
            command.CommandType = CommandType.Text;
            command.Connection = myDbConnection;
            command.CommandText = "SELECT COUNT(*) from myTable where firstName = @p1 AND lastName = @p2";
            command.Parameters.AddWithValue("@p1", encryptedFName);
            command.Parameters.AddWithValue("@p2", encryptedLName);

            try
            {
                myDbConnection.Open();
                int result = (int)command.ExecuteScalar();
                if (result > 0)
                {
                    exists = true;
                }
                return exists;
            }
            catch (OleDbException ex)
            {
                DisplayOleDbErrorCollection(ex);
                return exists;
            }
            finally
            {
                myDbConnection.Close();
            }
        }

        internal byte[] getTemplateFromId(string userId)
        {
            OleDbConnection myDbConnection = new OleDbConnection(connectionString);
            byte[] templateArray = null;

            try
            {
                string queryString;
                byte[] encryptedTemplateArray = null;
                cryptography deCrypt = new cryptography();

                queryString = String.Format("SELECT template from myTable where userId = '{0}'", deCrypt.encryptString(userId));
                OleDbCommand command = new OleDbCommand(queryString, myDbConnection);
                myDbConnection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    encryptedTemplateArray = (byte[])reader.GetValue(0);
                }
                reader.Close();
                templateArray = deCrypt.decryptBytes(encryptedTemplateArray);
                return templateArray;
            }
            catch (OleDbException ex)
            {
                DisplayOleDbErrorCollection(ex);
                return templateArray;
            }
            finally
            {
                myDbConnection.Close();
            }
        }

        internal byte[] getTemplateFromKey(int primaryKey)
        {
            OleDbConnection myDbConnection = new OleDbConnection(connectionString);
            byte[] decryptedTemplate = null;

            try
            {
                string queryString;
                byte[] encryptedTemplate = null;
                cryptography decrypt = new cryptography();

                queryString = String.Format("SELECT template from myTable where PrimaryKey = {0}", primaryKey);
                OleDbCommand command = new OleDbCommand(queryString, myDbConnection);
                myDbConnection.Open();
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    encryptedTemplate = (byte[])reader.GetValue(0);
                    decryptedTemplate = decrypt.decryptBytes(encryptedTemplate);
                }
                reader.Close();
                return decryptedTemplate;
            }
            catch (OleDbException ex)
            {
                DisplayOleDbErrorCollection(ex);
                return decryptedTemplate;
            }
            finally
            {
                myDbConnection.Close();
            }
        }

        internal Bitmap getImageFromId(string userId)
        {
            Bitmap dbaseImage = null;
            OleDbConnection myDbConnection = new OleDbConnection(connectionString);
            try
            {
                string queryString;
                byte[] encryptedImageArray = null;
                byte[] decryptedImageArray = null;
                cryptography decrypt = new cryptography();
                queryString = String.Format("SELECT picture from myTable where userId = '{0}'", decrypt.encryptString(userId));
                OleDbCommand command = new OleDbCommand(queryString, myDbConnection);
                myDbConnection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    encryptedImageArray = (byte[])reader.GetValue(0);
                    decryptedImageArray = decrypt.decryptBytes(encryptedImageArray);
                    MemoryStream stream = new MemoryStream(decryptedImageArray);
                    dbaseImage = (Bitmap)Image.FromStream(stream);
                }
                reader.Close();
                return dbaseImage;
            }
            catch (OleDbException ex)
            {
                DisplayOleDbErrorCollection(ex);
                return dbaseImage;
            }
            finally
            {
                myDbConnection.Close();
            }
        }

        internal string getFName(string userId)
        {
            string encryptedFName = null;
            string firstName = null;
            cryptography decrypt = new cryptography();
            OleDbConnection myDbConnection = new OleDbConnection(connectionString);
            try
            {
                string queryString;

                queryString = String.Format("SELECT firstName from myTable where userId = '{0}'", decrypt.encryptString(userId));
                OleDbCommand command = new OleDbCommand(queryString, myDbConnection);
                myDbConnection.Open();
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    encryptedFName = (string)reader.GetValue(0);
                    firstName = decrypt.decryptString(encryptedFName);
                }
                reader.Close();
                return firstName;
            }
            catch (OleDbException ex)
            {
                DisplayOleDbErrorCollection(ex);
                return firstName;
            }
            finally
            {
                myDbConnection.Close();
            }
        }

        internal string getLName(string userId)
        {
            string encryptedLName;
            string lastName = null;
            cryptography decrypt = new cryptography();
            OleDbConnection myDbConnection = new OleDbConnection(connectionString);

            try
            {
                string queryString;
                queryString = String.Format("SELECT lastName from myTable where userId = '{0}'", decrypt.encryptString(userId));
                OleDbCommand command = new OleDbCommand(queryString, myDbConnection);
                myDbConnection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    encryptedLName = (string)reader.GetValue(0);
                    lastName = decrypt.decryptString(encryptedLName);
                }
                reader.Close();
                return lastName;
            }
            catch (OleDbException ex)
            {
                DisplayOleDbErrorCollection(ex);
                return lastName;
            }
            finally
            {
                myDbConnection.Close();
            }
        }

        internal string userIdFromKey(int keyNum)
        {
            string userId = null;
            string encryptedUserId = null;
            OleDbConnection myDbConnection = new OleDbConnection(connectionString);
            try
            {
                string queryString;
                cryptography decrypt = new cryptography();
                queryString = String.Format("SELECT userId from myTable where PrimaryKey = {0}", keyNum);
                OleDbCommand command = new OleDbCommand(queryString, myDbConnection);
                myDbConnection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    encryptedUserId = (string)reader.GetValue(0);
                    userId = decrypt.decryptString(encryptedUserId);
                }
                reader.Close();
                return userId;
            }
            catch (OleDbException ex)
            {
                DisplayOleDbErrorCollection(ex);
                return encryptedUserId;
            }
            finally
            {
                myDbConnection.Close();
            }
        }

        internal int numOfRecords()
        {
            int recordCount = 0;
            OleDbConnection myDbConnection = new OleDbConnection(connectionString);
            OleDbCommand command = new OleDbCommand();
            command.CommandType = CommandType.Text;
            command.Connection = myDbConnection;
            command.CommandText = "SELECT COUNT(*) from myTable";

            try
            {
                myDbConnection.Open();
                recordCount = (int)command.ExecuteScalar();
                return recordCount;
            }
            catch (OleDbException ex)
            {
                DisplayOleDbErrorCollection(ex);
                return recordCount;
            }
            finally
            {
                myDbConnection.Close();
            }
        }

        public void DisplayOleDbErrorCollection(OleDbException exception)
        {
            for (int i = 0; i < exception.Errors.Count; i++)
            {
                MessageBox.Show("Index #" + i + "\n" +
                       "Message: " + exception.Errors[i].Message + "\n" +
                       "Native: " + exception.Errors[i].NativeError.ToString() + "\n" +
                       "Source: " + exception.Errors[i].Source + "\n" +
                       "SQL: " + exception.Errors[i].SQLState + "\n");
            }
        }
    }
}
