using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour {
    public int positionX;
    public int positionY;
    GameMap map;

	// Use this for initialization
	void Start () {
        map = GameObject.Find("Map").GetComponent<GameMap>();
        

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
