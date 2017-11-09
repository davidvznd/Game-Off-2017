using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    // Player Position
    public int positionX;
    public int positionY;

    //Sprites
    public Sprite Up;
    public Sprite Down;
    public Sprite Left;
    public Sprite Right;
    public SpriteRenderer SpriteSettings;

    //Reference to map
    public GameMap map;
    int DirectionValue;

    // Use this for initialization
    void Start() {

    }

    void Movement() {
        // Right equals value 1
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (CheckMove(1))
            {
                transform.Translate(1, 0, 0);
                map.maze[positionY, positionX + 1] = 'P';
                map.maze[positionY, positionX] = ' ';
                positionX += 1;
                SpriteSettings.sprite = Right;
            }
            else
            {
                Debug.Log("Can't move");
            }
        }

        // Left equals value 2
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (CheckMove(2))
            {
                transform.Translate(-1, 0, 0);
                map.maze[positionY, positionX - 1] = 'P';
                map.maze[positionY, positionX] = ' ';
                positionX -= 1;
                SpriteSettings.sprite = Left;
            }
            else
            {
                Debug.Log("Can't move");
            }
        }

        // Up equals value 3
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (CheckMove(3))
            {
                transform.Translate(0, 1, 0);
                map.maze[positionY + 1, positionX] = 'P';
                map.maze[positionY, positionX] = ' ';
                positionY += 1;
                SpriteSettings.sprite = Up;
            }
            else
            {
                Debug.Log("Can't move");
            }
        }

        // Down equals value 4
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (CheckMove(4))
            {
                transform.Translate(0, -1, 0);
                map.maze[positionY - 1, positionX] = 'P';
                map.maze[positionY, positionX] = ' ';
                positionY -= 1;
                SpriteSettings.sprite = Down;
            }
            else
            {
                Debug.Log("Can't move");
            }
        }
    }

    bool CheckMove(int value)
    {
        if (value == 1)
        {
            if(map.maze[positionY,positionX + 1] == '#')
            {
                return false;
            }
            else
            {
                //Movement code
                return true;
            }
        }
        else if (value == 2)
        {
            if (map.maze[positionY, positionX - 1] == '#')
            {
                return false;
            }
            else
            {
                //Movement code
                return true;
            }
        }
        else if (value == 3)
        {
            if (map.maze[positionY + 1, positionX] == '#')
            {
                return false;
            }
            else
            {
                //Movement code
                return true;
            }
        }
        else
        {
            if (map.maze[positionY -1, positionX] == '#')
            {
                return false;
            }
            else
            {
                //Movement code
                return true;
            }
        }
        
    }

	// Update is called once per frame
	void Update () {
        Debug.Log("X: " + positionX + ", " + positionY);
        Movement();
    }
}
