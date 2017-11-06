using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject player;
    public Camera myCamera;
	// Use this for initialization
	void Start () {
        myCamera.orthographicSize = 6.0f;
    }

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x,player.transform.position.y, -10); // Camera follows the player with specified offset position
    }
}
