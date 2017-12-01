using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour {
    // Player Position
    public int positionX;
    public int positionY;

    bool FeedbackActive = true;

    //Sprites
    public Sprite Up;
    public Sprite Down;
    public Sprite Left;
    public Sprite Right;
    public SpriteRenderer SpriteSettings;

    public int health = 5;
    public int attack = 1;

    public Text playerfeedback;

    //Reference to map
    public GameMap map;
    public UnitController unitcont;

    // Use this for initialization
    void Start() {
        map = GameObject.Find("Map").GetComponent<GameMap>();
        playerfeedback = GameObject.Find("PlayerFeedback").GetComponent<Text>();
        positionX = map.playerX;
        positionY = map.playerY;
    }

    public int GetPositionX() { return positionX; }
    public int GetPositionY() { return positionY; }
    public void SetPositionX(int x) { positionX = x; }
    public void SetPositionY(int y) { positionY = y; }

    void Movement() {
        // Right equals value 1
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (CheckMove(1))
            {
                Debug.Log("Pass");
                if (map.maze[positionY, positionX + 1] == 'E')
                {
                    Debug.Log("Exit right");
                    map.CallMapChange = true;
                    Debug.Log(unitcont.Enemies.Count);
                }
                else if (map.maze[positionY, positionX + 1] == 'I')
                {
                    //Repeat with other directions
                    foreach(GameObject item in unitcont.Items)
                    {
                        if (item.GetComponent<Item>().positionX == positionX + 1 && item.GetComponent<Item>().positionY == positionY)
                        {
                            item.GetComponent<Item>().ItemPickedUp();
                            FeedbackActive = true;
                            map.maze[positionY, positionX + 1] = ' ';
                            break;
                        }
                    }
                }
                else
                {
                    Debug.Log("Move right");
                    transform.Translate(1, 0, 0);
                    map.maze[positionY, positionX + 1] = 'P';
                    map.maze[positionY, positionX] = ' ';
                    positionX += 1;
                    SpriteSettings.sprite = Right;
                }
            }
            else if (map.maze[positionY, positionX + 1] == 'M')
            {
                //attack boi
                Debug.Log("ATTACK right");
                foreach (GameObject unit in unitcont.Enemies)
                {
                    if (unit.GetComponent<Monster>().positionX == positionX + 1 && unit.GetComponent<Monster>().positionY == positionY)
                    {
                        Attack(unit);
                        playerfeedback.text = "Dealt an attack";
                        FeedbackActive = true;
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("Can't move right");
            }
        }

        // Left equals value 2
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (CheckMove(2))
            {
                Debug.Log("Pass");
                if (map.maze[positionY, positionX - 1] == 'E')
                {
                    Debug.Log("Exit left");
                    map.CallMapChange = true;
                    Debug.Log(unitcont.Enemies.Count);
                }
                else if (map.maze[positionY, positionX - 1] == 'I')
                {
                    //Repeat with other directions
                    foreach (GameObject item in unitcont.Items)
                    {
                        if (item.GetComponent<Item>().positionX == positionX - 1 && item.GetComponent<Item>().positionY == positionY)
                        {
                            item.GetComponent<Item>().ItemPickedUp();
                            FeedbackActive = true;
                            map.maze[positionY, positionX - 1] = ' ';
                            break;
                        }
                    }
                }
                else
                {
                    Debug.Log("Move left");
                    transform.Translate(-1, 0, 0);
                    map.maze[positionY, positionX - 1] = 'P';
                    map.maze[positionY, positionX] = ' ';
                    positionX -= 1;
                    SpriteSettings.sprite = Left;
                }
            }
            else if (map.maze[positionY, positionX - 1] == 'M')
            {
                //attack boi
                Debug.Log("ATTACK left");
                foreach(GameObject unit in unitcont.Enemies)
                {
                    if (unit.GetComponent<Monster>().positionX == positionX - 1 && unit.GetComponent<Monster>().positionY == positionY)
                    {
                        Attack(unit);
                        playerfeedback.text = "Dealt an attack";
                        FeedbackActive = true;
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("Can't move left ");
            }
        }

        // Up equals value 3
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (CheckMove(3))
            {
                Debug.Log("Pass");
                if (map.maze[positionY + 1, positionX] == 'E')
                {
                    Debug.Log("Exit Up");
                    map.CallMapChange = true;
                    Debug.Log(unitcont.Enemies.Count);
                }
                else if (map.maze[positionY + 1, positionX] == 'I')
                {
                    //Repeat with other directions
                    foreach (GameObject item in unitcont.Items)
                    {
                        if (item.GetComponent<Item>().positionX == positionX && item.GetComponent<Item>().positionY == positionY + 1)
                        {
                            item.GetComponent<Item>().ItemPickedUp();
                            FeedbackActive = true;

                            map.maze[positionY + 1, positionX] = ' ';
                            break;
                        }
                    }
                }
                else
                {
                    Debug.Log("move up");
                    transform.Translate(0, 1, 0);
                    map.maze[positionY + 1, positionX] = 'P';
                    map.maze[positionY, positionX] = ' ';
                    positionY += 1;
                    SpriteSettings.sprite = Up;
                }
            }
            else if (map.maze[positionY + 1, positionX] == 'M')
            {
                //attack boi
                Debug.Log("Attack up");
                foreach (GameObject unit in unitcont.Enemies)
                {
                    if (unit.GetComponent<Monster>().positionX == positionX && unit.GetComponent<Monster>().positionY == positionY + 1)
                    {
                        Attack(unit);
                        playerfeedback.text = "Dealt an attack";
                        FeedbackActive = true;
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("Can't move up");
            }
        }

        // Down equals value 4
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (CheckMove(4))
            {
                Debug.Log("Pass");
                if (map.maze[positionY - 1, positionX] == 'E')
                {
                    Debug.Log("Exit down");
                    map.CallMapChange = true;
                    Debug.Log(unitcont.Enemies.Count);
                }
                else if (map.maze[positionY - 1, positionX] == 'I')
                {
                    //Repeat with other directions
                    foreach (GameObject item in unitcont.Items)
                    {
                        if (item.GetComponent<Item>().positionX == positionX && item.GetComponent<Item>().positionY == positionY - 1)
                        {
                            item.GetComponent<Item>().ItemPickedUp();
                            FeedbackActive = true;
                            map.maze[positionY - 1, positionX] = ' ';
                            break;
                        }
                    }
                }
                else
                {
                    Debug.Log("Move Down");
                    transform.Translate(0, -1, 0);
                    map.maze[positionY - 1, positionX] = 'P';
                    map.maze[positionY, positionX] = ' ';
                    positionY -= 1;
                    SpriteSettings.sprite = Down;
                }
            }
            else if (map.maze[positionY - 1, positionX] == 'M')
            {
                //attack boi
                Debug.Log("ATTACK down");
                foreach (GameObject unit in unitcont.Enemies)
                {
                    if (unit.GetComponent<Monster>().positionX == positionX && unit.GetComponent<Monster>().positionY == positionY - 1)
                    {
                        Attack(unit);
                        playerfeedback.text = "Dealt an attack";
                        FeedbackActive = true;
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("Can't move down");
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
                Debug.Log("Can move right");
                return false;
            }
            else
            {
                //Movement code
                return true;
            }
        }
        else if (value == 4)
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
        else
        {
            Debug.Log("error");
            return false;
        }
        
    }

    void Attack(GameObject unit)
    {
        unitcont.AttackIndicator = 1;
        unit.GetComponent<Monster>().health -= attack;
        if (unit.GetComponent<Monster>().health == 0)
        {
            Color currColor = unit.GetComponent<SpriteRenderer>().color;
            currColor.a = 0;
            unit.GetComponent<SpriteRenderer>().color = currColor;
        }
    }

	// Update is called once per frame
	public void UpdatePlayer () {
        Movement();
        if (playerfeedback.color.a > 0 && FeedbackActive == false)
        {
            Color currColor = playerfeedback.color;
            currColor.a -= 0.5f * Time.deltaTime;
            playerfeedback.color = currColor;
        }
        if (FeedbackActive == true)
        {
            Debug.Log("Change alpha");
            Color currColor = playerfeedback.color;
            currColor.a = 1.0f;
            FeedbackActive = false;
            playerfeedback.color = currColor;
        }
    }
}
