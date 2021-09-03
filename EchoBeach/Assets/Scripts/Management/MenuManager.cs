using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SaveManager.instance.ActiveSave.CurrentTargets.Clear();
    }

    public void StartNewGame()
    {
        var Save = SaveManager.instance.ActiveSave;
        Save.SongTracklist.Clear();
        Save.MTaskNumber = TaskNumber.Tutorial;
        Save.GameCompleted = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadMainGameScene()
    {
        SceneManager.LoadScene("MainGameScene");
    }


}
