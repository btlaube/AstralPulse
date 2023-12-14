using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsScroll : MonoBehaviour {

    [SerializeField] private RawImage image;
    [SerializeField] private float speed;
    private Vector2 inputVector;
    private Camera mainCamera;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    void Update() {

        // inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        // // inputVector = new Vector2.one;
        // Debug.Log(mainCamera.velocity.normalized);
        inputVector = mainCamera.velocity.normalized;
        image.uvRect = new Rect(image.uvRect.position + new Vector2(inputVector.x * (speed / 100.0f), inputVector.y * (speed  / 100.0f)) * Time.deltaTime, image.uvRect.size);
    }

}
