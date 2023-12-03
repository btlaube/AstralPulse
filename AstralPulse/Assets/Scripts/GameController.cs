using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField] private float sequenceDuration;

    void Start()
    {
        StartCoroutine("StartSequence");
    }

    private IEnumerator StartSequence()
    {
        // Disable scripts

        // Do player start stuff

        // Wait
        yield return new WaitForSeconds(sequenceDuration);

        // Activate scripts
    }
}
