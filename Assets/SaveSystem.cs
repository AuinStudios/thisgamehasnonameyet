
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public  static class SaveSystem
{

    public static void Savesystem()
    {
        BinaryFormatter format = new BinaryFormatter();
        string path = Application.persistentDataPath + "/savelol.banana";
        FileStream stream = new FileStream(path, FileMode.Create);

        HoldVariables data = new HoldVariables();

        format.Serialize(stream, data);
        stream.Close();
         
    }

    public static HoldVariables load()
    {
        string path = Application.persistentDataPath + "/savelol.banana";
        if (File.Exists(path))
        {
           BinaryFormatter format = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            HoldVariables Data = format.Deserialize(stream) as HoldVariables;
            stream.Close();
            return Data;
        }
        else
        {
            return null;
        }
    }

}
