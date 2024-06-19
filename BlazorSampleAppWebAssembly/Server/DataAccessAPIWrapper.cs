using DatabaseAccessLib;
using UtilityFunctions;

namespace BlazorSampleAppWebAssembly
{
    public class DataAccessAPIWrapper : DataAccessAPIInMemory
    {
        string NorthwindsDBBackupName = "NorthwindsDB.xml";
        public DataAccessAPIWrapper() : base()
        {
            try
            {
                if (!System.IO.File.Exists(NorthwindsDBBackupName))
                {
                    throw new Exception(string.Format("The database XML file {0} is not found!", NorthwindsDBBackupName));
                }
                else
                {
                    string XmlDB = null;
                    DatabaseBackup databaseBackup = new DatabaseBackup();
                    try
                    {
                        XmlDB = System.IO.File.ReadAllText(NorthwindsDBBackupName);
                    }
                    catch (Exception)
                    {
                        throw new Exception(string.Format("Found, but unable to read the database XML file {0}!", NorthwindsDBBackupName));
                    }

                    if (XmlDB != null)
                    {
                        try
                        {
                            databaseBackup = Utility.DeserializeXml<DatabaseBackup>(XmlDB);
                        }
                        catch (Exception)
                        {
                            throw new Exception(string.Format("Found, but unable to DESERIALIZE the database XML file {0}!", NorthwindsDBBackupName));
                        }
                    }

                    if (databaseBackup != null)
                    {
                        base.SetDatabase(databaseBackup);
                    }
                }
            }
            finally
            {

            }

        }


    }
}
