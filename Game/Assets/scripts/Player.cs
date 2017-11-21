using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    // Player Position
    int positionX;
    int positionY;

    //Sprites
    public Sprite Up;
    public Sprite Down;
    public Sprite Left;
    public Sprite Right;
    public SpriteRenderer SpriteSettings;

    //Reference to map
    public GameMap map;
    public UnitController unitcont;

    // Use this for initialization
    void Start() {
        map = GameObject.Find("Map").GetComponent<GameMap>();
        positionX = map.playerX;
        positionY = map.playerY; 
    }

    public int GetPositionX() { return positionX; }
    public int GetPositionY() { return positionY; }

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
                Debug.Log("X: " + positionX + ", Y:" + positionY);
            }
            else if (map.maze[positionY, positionX + 1] == 'M')
            {
                //attack boi
                Debug.Log("ATTACK");
                foreach (GameObject unit in unitcont.Enemies)
                {
                    if (unit.GetComponent<Monster>().positionX == positionX + 1 && unit.GetComponent<Monster>().positionY == positionY)
                    {
                        Attack(unit);
                    }
                }
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
                Debug.Log("X: " + positionX + ", Y:" + positionY);
            }
            else if (map.maze[positionY, positionX - 1] == 'M')
            {
                //attack boi
                Debug.Log("ATTACK");
                foreach(GameObject unit in unitcont.Enemies)
                {
                    if (unit.GetComponent<Monster>().positionX == positionX - 1 && unit.GetComponent<Monster>().positionY == positionY)
                    {
                        Attack(unit);
                    }
                }
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
                Debug.Log("X: " + positionX + ", Y:" + positionY);
            }
            else if (map.maze[positionY + 1, positionX] == 'M')
            {
                //attack boi
                Debug.Log("ATTACK");
                foreach (GameObject unit in unitcont.Enemies)
                {
                    if (unit.GetComponent<Monster>().positionX == positionX && unit.GetComponent<Monster>().positionY == positionY + 1)
                    {
                        Attack(unit);
                    }
                }
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
                Debug.Log("X: " + positionX + ", Y:" + positionY);
            }
            else if (map.maze[positionY - 1, positionX] == 'M')
            {
                //attack boi
                Debug.Log("ATTACK");
                foreach (GameObject unit in unitcont.Enemies)
                {
                    if (unit.GetComponent<Monster>().positionX == positionX && unit.GetComponent<Monster>().positionY == positionY - 1)
                    {
                        Attack(unit);
                    }
                }
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
            if(map.maze[positionY,positionX + 1] == '#' || map.maze[positionY, positionX + 1] == 'M')
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
            if (map.maze[positionY, positionX - 1] == '#' || map.maze[positionY, positionX - 1] == 'M')
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
            if (map.maze[positionY + 1, positionX] == '#' || map.maze[positionY + 1, positionX] == 'M')
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
            if (map.maze[positionY -1, positionX] == '#' || map.maze[positionY - 1, positionX] == 'M')
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

    void Attack(GameObject unit)
    {
        unit.GetComponent<Monster>().health -= 1;
    }

	// Update is called once per frame
	public void UpdatePlayer () {
        Movement();
    }
}
