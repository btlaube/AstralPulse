using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCanvas : MonoBehaviour
{
    public void GameSceneButton()
    {
        LevelLoader.instance.LoadScene(1);
    }
}
