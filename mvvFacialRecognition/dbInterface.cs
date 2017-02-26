using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using Neurotec.Biometrics;

namespace mvvFacialRecognition
{
	class dbInterface
    {
        string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source =" + Application.StartupPath + "\\secureDatabase.accdb";
        bool exists = true;

        public dbInterface()
        {
            try
            {
                OleDbConnection myDbConnection = new OleDbConnection(connectionString);
                myDbConnection.Open();
                myDbConnection.Close();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("Unable to find database 'secureDatabase.accdb'. Please insure it is present and try again.");
                exists = false;
            }

            if (!exists)
            {
                if (System.Windows.Forms.Application.MessageLoop)
                {
                    System.Windows.Forms.Application.Exit();
                }
                else
                {
                    System.Environment.Exit(1);
                }
            }
        }

        internal List<string> populateUserIdList()
        {
            List<string> userNames = new List<string>();
            userNames.Add("Select User");
            try
            {
                string fName;
                string lName;
                cryptography decrypt = new cryptography();
                OleDbConnection myDbConnection = new OleDbConnection(connectionString);
                OleDbCommand command = new OleDbCommand();
                command.CommandType = CommandType.Text;
                command.Connection = myDbConnection;
                command.CommandText = "SELECT firstName, lastName from myTable";
                myDbConnection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        fName = decrypt.decryptString(reader.GetString(reader.GetOrdinal("firstName")));
                        lName = decrypt.decryptString(reader.GetString(reader.GetOrdinal("lastName")));
                        userNames.Add(fName + " " + lName);
                    }
                    reader.Close();
                }
                myDbConnection.Close();
                return userNames;
            }
            catch (OleDbException ex)
            {
                DisplayOleDbErrorCollection(ex);
                return userNames;
            }
        }

        public void insertEntry(string fName, string lName,string permissionsLevel, string id, Bitmap imageBmp, NLTemplate template, string vidLoc)
        {
            OleDbConnection myDbConnection = new OleDbConnection(connectionString);
            using (myDbConnection)
            {
                cryptography encrypt = new cryptography();
                string crypFName = encrypt.encryptString(fName);
                string cryptLName = encrypt.encryptString(lName);
                string cryptAccessLevel = encrypt.encryptString(permissionsLevel);
                string cryptUserId = encrypt.encryptString(id);
                string cryptVidLoc = encrypt.encryptString(vidLoc);
                byte[] cryptImage = encrypt.encryptImage(imageBmp);
                byte[] cryptTemplate = encrypt.encryptTemplate(template);

                OleDbCommand command = new OleDbCommand();
                command.CommandType = CommandType.Text;
                command.Connection = myDbConnection;
                command.CommandText = "INSERT INTO myTable (firstName, lastName, userId, permission, picture, template, videoLink) Values (@p1,@p2,@p3,@p4,@p5,@p6, @p7)";
                command.Parameters.AddWithValue("@P1", crypFName);
                command.Parameters.AddWithValue("@p2", cryptLName);
                command.Parameters.AddWithValue("@p3", cryptUserId);
                command.Parameters.AddWithValue("@p4", cryptAccessLevel);
                command.Parameters.AddWithValue("@p5", cryptImage);
                command.Parameters.AddWithValue("@p6", cryptTemplate);
                command.Parameters.AddWithValue("p7", cryptVidLoc);
                try
                {
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

        internal void updateEntry(int primeKey, string fName, string lName, string accessLevel, string enrolleeId, string videoFileLoc)
        {
            OleDbConnection myDbConnection = new OleDbConnection(connectionString);
            using (myDbConnection)
            {
                cryptography encrypt = new cryptography();
                string crypFName = encrypt.encryptString(fName);
                string cryptLName = encrypt.encryptString(lName);
                string cryptAccessLevel = encrypt.encryptString(accessLevel);
                string cryptUserId = encrypt.encryptString(enrolleeId);
                string cryptVidLoc = encrypt.encryptString(videoFileLoc);

                OleDbCommand command = new OleDbCommand();
                command.CommandType = CommandType.Text;
                command.Connection = myDbConnection;
                command.CommandText = "UPDATE myTable SET firstName = @p1, lastName = @p2, userId = @p3, permission = @p4, videoLink = @p5 WHERE PrimaryKey = @p6";
                command.Parameters.AddWithValue("@P1", crypFName);
                command.Parameters.AddWithValue("@p2", cryptLName);
                command.Parameters.AddWithValue("@p3", cryptUserId);
                command.Parameters.AddWithValue("@p4", cryptAccessLevel);
                command.Parameters.AddWithValue("@p5", cryptVidLoc);
                command.Parameters.AddWithValue("@p6", primeKey);
                try
                {
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

        internal bool deleteUser(int primeKey)
        {
            cryptography encrypt = new cryptography();
            OleDbConnection myDbConnection = new OleDbConnection(connectionString);
            OleDbCommand command = new OleDbCommand();
            command.CommandType = CommandType.Text;
            command.Connection = myDbConnection;
            command.CommandText = "DELETE * FROM myTable WHERE PrimaryKey = p1";
            command.Parameters.AddWithValue("p1", primeKey);

            try
            {
                myDbConnection.Open();
                command.ExecuteNonQuery();
                return true;
            }
            catch (OleDbException ex)
            {
                DisplayOleDbErrorCollection(ex);
                return false;
            }
            finally
            {
                myDbConnection.Close();
            }
        }

        internal int getKeyFromName(string userFName, string userLName)
        {
            int primeKey = 0;
            cryptography decrypt = new cryptography();
            OleDbConnection myDbConnection = new OleDbConnection(connectionString);
            OleDbCommand command = new OleDbCommand();
            command.CommandType = CommandType.Text;
            command.Connection = myDbConnection;
            command.CommandText = "SELECT PrimaryKey from myTable where firstName = p1 AND lastName = p2";
            command.Parameters.AddWithValue("p1", decrypt.encryptString(userFName));
            command.Parameters.AddWithValue("p2", decrypt.encryptString(userLName));
            try
            {
                myDbConnection.Close();
                myDbConnection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    primeKey = (int)reader.GetValue(0);
                }
                reader.Close();

                myDbConnection.Close();
                return primeKey;
            }
            catch (OleDbException ex)
            {
                DisplayOleDbErrorCollection(ex);
                return primeKey;
            }
            finally
            {
                myDbConnection.Close();
            }
        }

        internal bool userIdExists(string enrolleeId)
        {
            bool exists = false;

            cryptography encrypt = new cryptography();
            OleDbConnection myDbConnection = new OleDbConnection(connectionString);
            OleDbCommand command = new OleDbCommand();
            command.CommandType = CommandType.Text;
            command.Connection = myDbConnection;
            command.CommandText = "SELECT COUNT(*) from myTable where userId = @p1";
            command.Parameters.AddWithValue("@p1", encrypt.encryptString(enrolleeId));

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
            bool exists = false;
            cryptography encrypt = new cryptography();
            OleDbConnection myDbConnection = new OleDbConnection(connectionString);
            OleDbCommand command = new OleDbCommand();
            command.CommandType = CommandType.Text;
            command.Connection = myDbConnection;
            command.CommandText = "SELECT COUNT(*) from myTable where firstName = @p1 AND lastName = @p2";
            command.Parameters.AddWithValue("@p1", encrypt.encryptString(firstName));
            command.Parameters.AddWithValue("@p2", encrypt.encryptString(lastName));

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
            cryptography deCrypt = new cryptography();
            OleDbConnection myDbConnection = new OleDbConnection(connectionString);
            byte[] encryptedTemplateArray = null;
            byte[] templateArray = null;
            string queryString = String.Format("SELECT template from myTable where userId = '{0}'", deCrypt.encryptString(userId));
            OleDbCommand command = new OleDbCommand(queryString, myDbConnection);

            try
            {
                myDbConnection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        encryptedTemplateArray = (byte[])reader.GetValue(0);
                        templateArray = deCrypt.decryptBytes(encryptedTemplateArray);
                    }
                }
                reader.Close();
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

        internal string getFName(int primeKey)
        {
            string encryptedFName = null;
            string firstName = null;
            cryptography decrypt = new cryptography();
            OleDbConnection myDbConnection = new OleDbConnection(connectionString);
            OleDbCommand command = new OleDbCommand();
            command.CommandType = CommandType.Text;
            command.Connection = myDbConnection;
            command.CommandText = "SELECT firstName from myTable where PrimaryKey = p1";
            command.Parameters.AddWithValue("p1", primeKey);
            try
            {
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

        internal string getLName(int primeKey)
        {
            string encryptedLName;
            string lastName = null;
            cryptography decrypt = new cryptography();
            OleDbConnection myDbConnection = new OleDbConnection(connectionString);
            OleDbCommand command = new OleDbCommand();
            command.CommandType = CommandType.Text;
            command.Connection = myDbConnection;
            command.CommandText = "SELECT lastName from myTable where PrimaryKey = p1";
            command.Parameters.AddWithValue("p1", primeKey);
            try
            {
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

        internal string getVideoLoc(int keyNum)
        {
            string encryptedFileLoc = null;
            string fileLoc = null;
            cryptography decrypt = new cryptography();
            OleDbConnection myDbConnection = new OleDbConnection(connectionString);
            try
            {
                string queryString;

                queryString = String.Format("SELECT videoLink from myTable where PrimaryKey = {0}", keyNum);
                OleDbCommand command = new OleDbCommand(queryString, myDbConnection);
                myDbConnection.Open();
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    encryptedFileLoc = (string)reader.GetValue(0);
                    fileLoc = decrypt.decryptString(encryptedFileLoc);
                }
                reader.Close();
                return fileLoc;
            }
            catch (OleDbException ex)
            {
                DisplayOleDbErrorCollection(ex);
                return fileLoc;
            }
            finally
            {
                myDbConnection.Close();
            }
        }

        internal int maxPrimaryKey()
        {
            int maxKey = 0;
            OleDbConnection myDbConnection = new OleDbConnection(connectionString);
            OleDbCommand command = new OleDbCommand();
            command.CommandType = CommandType.Text;
            command.Connection = myDbConnection;
            command.CommandText = "SELECT max(PrimaryKey) as PrimaryKey from myTable";

            try
            {
                myDbConnection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (!reader.IsDBNull(reader.GetOrdinal("PrimaryKey")))
                    {
                        maxKey = (int)reader.GetValue(reader.GetOrdinal("PrimaryKey"));
                    }
                }
                reader.Close();
                return maxKey;
            }
            catch (OleDbException ex)
            {
                DisplayOleDbErrorCollection(ex);
                return maxKey;
            }
            finally
            {
                myDbConnection.Close();
            }
        }

        internal void setNewFileLoc(int primeKey, string newLocation)
        {
            OleDbConnection myDbConnection = new OleDbConnection(connectionString);

            using (myDbConnection)
            {
                cryptography encrypt = new cryptography();
                string cryptVidLoc = encrypt.encryptString(newLocation);

                OleDbCommand command = new OleDbCommand();
                command.CommandType = CommandType.Text;
                command.Connection = myDbConnection;
                command.CommandText = "UPDATE myTable SET videoLink = p1 WHERE PrimaryKey = p2 ";
                command.Parameters.AddWithValue("p1", cryptVidLoc);
                command.Parameters.AddWithValue("p2", primeKey);
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

        internal bool isAdmin(int myPrimeKey)
        {
            try
            {
                string cryptPermissionLevel = null;
                cryptography decrypt = new cryptography();
                OleDbConnection myDbConnection = new OleDbConnection(connectionString);
                OleDbCommand command = new OleDbCommand();
                command.CommandType = CommandType.Text;
                command.Connection = myDbConnection;
                command.CommandText = "SELECT permission from myTable where PrimaryKey = p1";
                command.Parameters.AddWithValue("p1", myPrimeKey);
                myDbConnection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        cryptPermissionLevel = reader.GetString(reader.GetOrdinal("permission"));
                    }
                    reader.Close();
                }
                myDbConnection.Close();
                string permissionLevel = decrypt.decryptString(cryptPermissionLevel);
                if (permissionLevel == "Admin")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (OleDbException ex)
            {
                DisplayOleDbErrorCollection(ex);
                return false;
            }
        }

        public bool adminExists()
        {
            bool result = true;            
            try
            {
                cryptography decrypt = new cryptography();
                OleDbConnection myDbConnection = new OleDbConnection(connectionString);
                OleDbCommand command = new OleDbCommand();
                command.CommandType = CommandType.Text;
                command.Connection = myDbConnection;
                command.CommandText = "SELECT * FROM myTable WHERE permission = p1";
                command.Parameters.AddWithValue("p1", decrypt.encryptString("Admin"));
                myDbConnection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        result = false;// Admin does not exist
                    }
                    reader.Close();
                    myDbConnection.Close();
                    return result;
                }
                catch (OleDbException ex)
                {
                    DisplayOleDbErrorCollection(ex);
                    return result; // So the user is not enrolled as an Admin by default
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
