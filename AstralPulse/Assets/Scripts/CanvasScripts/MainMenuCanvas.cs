using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCanvas : MonoBehaviour
{
    public void FreeMoveSceneButton()
    {
        LevelLoader.instance.LoadScene(1);
    }

    public void RicochetSceneButton()
    {
        LevelLoader.instance.LoadScene(2);
    }
}
