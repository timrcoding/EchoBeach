using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class SaveManager : MonoBehaviour
{

    public static SaveManager instance;

    public SaveData ActiveSave;

    public bool hasLoaded;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        Load();
        StartCoroutine(SaveOnRepeat());
    }

    IEnumerator SaveOnRepeat()
    {
        Save();
        yield return new WaitForSeconds(1);
        StartCoroutine(SaveOnRepeat());
    }

    public void SetTaskNumber(TaskNumber Task)
    {
        ActiveSave.MTaskNumber = Task;
    }

    public void Save()
    {
        string dataPath = Application.persistentDataPath;
        var serializer = new XmlSerializer(typeof(SaveData));
        var stream = new FileStream(dataPath + "/" + ActiveSave.saveName + ".save", FileMode.Create);
        serializer.Serialize(stream, ActiveSave);
        stream.Close();
    }
    public void Load()
    {
        string dataPath = Application.persistentDataPath;
        if (System.IO.File.Exists(dataPath + "/" + ActiveSave.saveName + ".save"))
        {
            var serializer = new XmlSerializer(typeof(SaveData));
            var stream = new FileStream(dataPath + "/" + ActiveSave.saveName + ".save", FileMode.Open);
            ActiveSave = serializer.Deserialize(stream) as SaveData;
            stream.Close();

            hasLoaded = true;
        }
    }

    public void deleteSaveData()
    {
        string dataPath = Application.persistentDataPath;
        if (System.IO.File.Exists(dataPath + "/" + ActiveSave.saveName + ".save"))
        {
            File.Delete(dataPath + "/" + ActiveSave.saveName + ".save");
            Debug.Log("Deleted");
        }
    }

    public void deleteData()
    {

        if (SaveManager.instance != null)
        {
            ActiveSave.MTaskNumber = TaskNumber.Tutorial;
        }



    }
}

[System.Serializable]
public class SaveData
{
    public bool GameCompleted;
    public string saveName;
    public TaskNumber MTaskNumber;
    public DialogueScene CurrentDialogueScene;
    public List<LinkAndSong> SongTracklist;
    public List<CharName> CurrentTargets;
    public List<DeepNetLinkName> MusiciansEncountered;
}
[System.Serializable]
public class LinkAndSong
{
    public LinkAndSong(){}
    public LinkAndSong(DeepNetLinkName Link, Song song)
    {
        LinkName = Link;
        Song = song;
    }

    public Song Song;
    public DeepNetLinkName LinkName;
}
