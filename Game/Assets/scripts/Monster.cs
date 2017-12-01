using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour {
    //Sprites
    public Sprite Left;
    public Sprite Right;
    public SpriteRenderer SpriteSettings;

    public string MonsterName;
    public int health;
    public int attack;
    public int positionX;
    public int positionY;
    public GameMap map;
    Player PlayerRef;
    public bool isActive;
    public Text enemyfeedback;
    public bool FeedbackActive;
    public int AttackIndicator = 0;

    void Start()
    {
        PlayerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        map = GameObject.Find("Map").GetComponent<GameMap>();
        enemyfeedback = GameObject.Find("EnemyFeedback").GetComponent<Text>();
        health = map.levelvalue;
        attack = map.levelvalue;
        isActive = true;
    }

    public void UpdateMonster()
    {
        if (isActive)
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


            //Check if Up,Down,Left or Right is player
            if (map.maze[positionY, positionX + 1] == 'P' || map.maze[positionY, positionX - 1] == 'P' || map.maze[positionY + 1, positionX] == 'P' || map.maze[positionY - 1, positionX] == 'P')
            {
                Debug.Log("ENEMY Attack");
                Attack(PlayerRef);
            }
            else
            {
                //Search for movable position
                for (int i = 0; i < directionArr.Count; i++)
                {
                    //Right
                    if (directionArr[i] == 1)
                    {
                        if (map.maze[positionY, positionX + 1] == ' ')
                        {
                            //Movement code
                            transform.Translate(1, 0, 0);
                            map.maze[positionY, positionX + 1] = 'M';
                            map.maze[positionY, positionX] = ' ';
                            positionX += 1;
                            SpriteSettings.sprite = Right;
                            break;
                        }
                    }
                    if (directionArr[i] == 2)
                    {
                        if (map.maze[positionY, positionX - 1] == ' ')
                        {
                            //Movement code
                            transform.Translate(-1, 0, 0);
                            map.maze[positionY, positionX - 1] = 'M';
                            map.maze[positionY, positionX] = ' ';
                            positionX -= 1;
                            SpriteSettings.sprite = Left;
                            break;
                        }
                    }
                    if (directionArr[i] == 3)
                    {
                        if (map.maze[positionY + 1, positionX] == ' ')
                        {
                            //Movement code
                            transform.Translate(0, 1, 0);
                            map.maze[positionY + 1, positionX] = 'M';
                            map.maze[positionY, positionX] = ' ';
                            positionY += 1;
                            break;
                        }
                    }
                    if (directionArr[i] == 4)
                    {
                        if (map.maze[positionY - 1, positionX] == ' ')
                        {
                            //Movement code
                            transform.Translate(0, -1, 0);
                            map.maze[positionY - 1, positionX] = 'M';
                            map.maze[positionY, positionX] = ' ';
                            positionY -= 1;
                            break;
                        }
                    }
                }
            }
        }
    }

    void Attack(Player unit) 
    {
        AttackIndicator = 1;
        attack = map.levelvalue;
        //Monsters attack is equal
        unit.health -= attack;
        enemyfeedback.text = "Enemy has attacked you!";

        Debug.Log("Change alpha of text");
        Color currColor = enemyfeedback.color;
        currColor.a = 1.0f;
        FeedbackActive = false;
        enemyfeedback.color = currColor;
    }
}
;