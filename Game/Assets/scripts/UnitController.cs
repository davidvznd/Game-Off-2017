using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour {
    int playerX;
    int playerY;
    int oplayerX;
    int oplayerY;
    GameObject CurrentPlayer;
    GameObject EnemyToDelete;
    GameMap map;

    int x;
    int y;

    //How you construct the list.
    public List<GameObject> Enemies = new List<GameObject>();
    public List<GameObject> EnemiesToDelete = new List<GameObject>();
    Player PlayerController;
    Monster EnemyController;

    // Use this for initialization
    void Start () {
        map = GameObject.Find("Map").GetComponent<GameMap>();
        CurrentPlayer = GameObject.FindGameObjectWithTag("Player");
        PlayerController = CurrentPlayer.GetComponent<Player>();
        playerX = PlayerController.GetPositionX();
        playerY = PlayerController.GetPositionY();
        foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Enemies.Add(unit);
        }
    }
	
	// Update is called once per frame. THIS CONTROLS EVERY MOVABLE OBJECT
    // Does array remove things that no longer exist?
	void Update () {
        oplayerX = playerX;
        oplayerY = playerY;
        playerX = PlayerController.GetPositionX();
        playerY = PlayerController.GetPositionY();
        //Note you have to check if player even moved before moving units.
        PlayerController.UpdatePlayer();

        //Delete enemy here - Essentially deletes units with zero health
        Debug.Log(Enemies.Count);
        foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (unit.GetComponent<Monster>().health == 0)
            {
                EnemyToDelete = unit;
                Enemies.Remove(unit);
                x = unit.GetComponent<Monster>().positionX;
                y = unit.GetComponent<Monster>().positionY;
                Debug.Log(x + "," + y);
            }
        }
        //Scenario usually when player kills monster. Might have to change this down the line.
        map.maze[y, x] = 'P';
        Destroy(EnemyToDelete, 0.5f);

        if (oplayerX == playerX && oplayerY == playerY)
        {
            //Debug.Log("Do nothing");
        }
        else
        {
            foreach (GameObject unit in Enemies)
            {
                EnemyController = unit.GetComponent<Monster>();
                EnemyController.UpdateMonster();
            }
        }
	}
}
