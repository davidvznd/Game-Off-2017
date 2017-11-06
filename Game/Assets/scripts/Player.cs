using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public Sprite Up;
    public Sprite Down;
    public Sprite Left;
    public Sprite Right;
    public SpriteRenderer SpriteSettings;

    public Camera GameCamera;
    Camera CameraSettings;
    RectTransform TransformSettings;

    // Use this for initialization
    void Start() {
    }

    void Movement() {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Translate(1, 0, 0);
            SpriteSettings.sprite = Right;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.Translate(-1, 0, 0);
            SpriteSettings.sprite = Left;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Translate(0, 1, 0);
            SpriteSettings.sprite = Up;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.Translate(0, -1, 0);
            SpriteSettings.sprite = Down;
        }
    }
	
    void Motion()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Translate(1, 0, 0);
            SpriteSettings.sprite = Right;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.Translate(-1, 0, 0);
            SpriteSettings.sprite = Left;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Translate(0, 1, 0);
            SpriteSettings.sprite = Up;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.Translate(0, -1, 0);
            SpriteSettings.sprite = Down;
        }
    }

	// Update is called once per frame
	void Update () {
        Movement();
    }
}
