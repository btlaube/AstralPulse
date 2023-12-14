using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathCanvasScript : MonoBehaviour
{
    public void MainMenuButton()
    {
        LevelLoader.instance.LoadScene(0);
    }

    public void RespawnButton()
    {
        LevelLoader.instance.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
