﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Camera myCamera;
	// Use this for initialization
	void Start () {
        myCamera.orthographicSize = 5.5f;
    }

    void Update()
    {
        //transform.position = new Vector3(player.transform.position.x,player.transform.position.y, -10); // Camera follows the player with specified offset position
    }
}
