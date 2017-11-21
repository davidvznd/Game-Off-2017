using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Monster : MonoBehaviour {
    //Sprites
    public Sprite Left;
    public Sprite Right;
    public SpriteRenderer SpriteSettings;

    public string MonsterName;
    public int health;
    public int positionX;
    public int positionY;
    public GameMap map;
    public Player PlayerRef;

    void Start()
    {
        map = GameObject.Find("Map").GetComponent<GameMap>();
        health = 1;
    }

    public void UpdateMonster()
    {
        List<int> directionArr = new List<int> { 1, 2, 3, 4 };
        //Return 1-4, chooses which place to search first
        for (int i = 0; i < directionArr.Count; i++)
        {
            int temp = directionArr[i];
            int randomIndex = Random.Range(i, directionArr.Count);
            directionArr[i] = directionArr[randomIndex];
            directionArr[randomIndex] = temp;
        }

        Debug.Log(positionX + ',' + positionY);
        //Check if Up,Down,Left or Right is player
        if (map.maze[positionY, positionX + 1] == 'P' || map.maze[positionY, positionX - 1] == 'P' || map.maze[positionY + 1, positionX] == 'P' || map.maze[positionY - 1, positionX] == 'P')
        {
            //Attack(PlayerRef);
        }
        else
        {
            //Search for movable position
            for (int i = 0; i < directionArr.Count; i++)
            {
                //Right
                if (directionArr[i] == 1)
                {
                    if (map.maze[positionY, positionX + 1] != '#' && map.maze[positionY, positionX + 1] != 'M')
                    {
                        //Movement code
                        transform.Translate(1, 0, 0);
                        map.maze[positionY, positionX + 1] = 'M';
                        map.maze[positionY, positionX] = ' ';
                        positionX += 1;
                        SpriteSettings.sprite = Right;
                        Debug.Log("RIGHT");
                        break;
                    }
                }
                if (directionArr[i] == 2)
                {
                    if (map.maze[positionY, positionX - 1] != '#' && map.maze[positionY, positionX - 1] != 'M')
                    {
                        //Movement code
                        transform.Translate(-1, 0, 0);
                        map.maze[positionY, positionX - 1] = 'M';
                        map.maze[positionY, positionX] = ' ';
                        positionX -= 1;
                        SpriteSettings.sprite = Left;
                        Debug.Log("LEFT");
                        break;
                    }
                }
                if (directionArr[i] == 3)
                {
                    if (map.maze[positionY + 1, positionX] != '#' && map.maze[positionY + 1, positionX] != 'M')
                    {
                        //Movement code
                        transform.Translate(0, 1, 0);
                        map.maze[positionY + 1, positionX] = 'M';
                        map.maze[positionY, positionX] = ' ';
                        positionY += 1;
                        Debug.Log("UP");
                        break;
                    }
                }
                if (directionArr[i] == 4)
                {
                    if (map.maze[positionY - 1, positionX] != '#' && map.maze[positionY - 1, positionX] != 'M')
                    {
                        //Movement code
                        transform.Translate(0, -1, 0);
                        map.maze[positionY - 1, positionX] = 'M';
                        map.maze[positionY, positionX] = ' ';
                        positionY -= 1;
                        Debug.Log("DOWN");
                        break;
                    }
                }
            }
            Debug.Log("Break Succ");
        }
    }

    void Attack(Player unit) 
    { 
        //Unit takes damage
    }
}
