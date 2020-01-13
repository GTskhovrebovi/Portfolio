using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SavingSystem 
{
    public static void Save(Stats stats)
    {
        Debug.Log("Saving : "+ Application.persistentDataPath);
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/save.cx";
        FileStream stream = new FileStream(path, FileMode.Create);
        //Stats stats = new Stats()
        formatter.Serialize(stream, stats);
        stream.Close();
    }

    public static Stats Load ()
    {
        string path = Application.persistentDataPath + "/save.cx";

        Debug.Log("Loading : " + Application.persistentDataPath);
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            Stats stats = formatter.Deserialize(stream) as Stats;
            stream.Close();
            return stats;
        }
        else
        {
            Debug.Log("No Save File");
            return null;
        }
    }

}
