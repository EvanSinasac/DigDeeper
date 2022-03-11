using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary; 

public static class SaveSystem
{
    public static void SaveItem(Item item) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/item.bin";

        FileStream stream = new FileStream(path, FileMode.Create);

        Item data = new Item();
    }
}
