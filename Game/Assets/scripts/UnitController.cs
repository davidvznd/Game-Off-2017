using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitController : MonoBehaviour {
    int playerX;
    int playerY;
    int oplayerX;
    int oplayerY;
    public int Alive;
    GameObject CurrentPlayer;
    GameObject EnemyToDelete;
    GameMap map;
    public Text UIAttack;
    public Text UIHealth;
    public Text enemyfeedback;
    public Text Screen;
    int x;
    int y;
    int oldlevelvalue;
    int levelvalue;
    public int AttackIndicator = 0;


    //How you construct the list.
    public List<GameObject> Enemies = new List<GameObject>();
    public List<GameObject> Items = new List<GameObject>();
    Player PlayerController;
    Monster EnemyController;

    // Use this for initialization
    void Start () {
        Alive = 1;
        oldlevelvalue = 1;
        levelvalue = 1;
        enemyfeedback = GameObject.Find("EnemyFeedback").GetComponent<Text>();
        Screen = GameObject.Find("ScreenFeedback").GetComponent<Text>();
        map = GameObject.Find("Map").GetComponent<GameMap>();
        CurrentPlayer = GameObject.FindGameObjectWithTag("Player");
        PlayerController = CurrentPlayer.GetComponent<Player>();
        UIAttack = GameObject.Find("Attack").GetComponent<Text>();
        UIHealth = GameObject.Find("HP").GetComponent<Text>();
        playerX = PlayerController.GetPositionX();
        playerY = PlayerController.GetPositionY();
        foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Enemies.Add(unit);
        }
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Item"))
        {
            Items.Add(item);
        }
    }

    // Update is called once per frame. THIS CONTROLS EVERY MOVABLE OBJECT
    // Does array remove things that no longer exist?
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (PlayerController.health <= 0)
        {
            Alive = 0;
        }
        Debug.Log(Alive);
        if (Alive == 1)
        {
            Color currColorScreen = Screen.color;
            currColorScreen.a -= 0.5f * Time.deltaTime;
            Screen.color = currColorScreen;

            levelvalue = map.levelvalue;
            if (levelvalue != oldlevelvalue)
            {
                oldlevelvalue = levelvalue;
                foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    unit.GetComponent<Monster>().isActive = true;
                    Debug.Log(unit.GetComponent<Monster>().isActive);
                    unit.GetComponent<Monster>().health = levelvalue;
                    Color currColor = unit.GetComponent<SpriteRenderer>().color;
                    currColor.a = 1;
                    unit.GetComponent<SpriteRenderer>().color = currColor;
                }
                foreach (GameObject item in GameObject.FindGameObjectsWithTag("Item"))
                {
                    Color currColor = item.GetComponent<SpriteRenderer>().color;
                    currColor.a = 1;
                    item.GetComponent<SpriteRenderer>().color = currColor;
                }
                currColorScreen = Screen.color;
                currColorScreen.a = 1.0f;
                Screen.color = currColorScreen;
            }
            else
            {
                oplayerX = playerX;
                oplayerY = playerY;
                playerX = PlayerController.GetPositionX();
                playerY = PlayerController.GetPositionY();
                //Note you have to check if player even moved before moving units.
                PlayerController.UpdatePlayer();

                if (map.CallMapChange == false)
                {
                    //Used to to disable dead units.
                    foreach (GameObject unit in Enemies)
                    {
                        if (unit.GetComponent<Monster>().health == 0)
                        {
                            if (unit.GetComponent<Monster>().isActive == false && map.maze[unit.GetComponent<Monster>().positionY, unit.GetComponent<Monster>().positionX] != 'P')
                            {
                                map.maze[unit.GetComponent<Monster>().positionY, unit.GetComponent<Monster>().positionX] = ' ';
                            }
                            else
                            {
                                unit.GetComponent<Monster>().isActive = false;
                            }
                        }
                    }

                    if (oplayerX == playerX && oplayerY == playerY && AttackIndicator == 0)
                    {
                        Color currColor = enemyfeedback.color;
                        currColor.a -= 0.5f * Time.deltaTime;
                        enemyfeedback.color = currColor;
                    }
                    else
                    {
                        AttackIndicator = 0;
                        int TotalDamage = 0;
                        Debug.Log("Start enemy turn");
                        foreach (GameObject unit in Enemies)
                        {
                            if (unit.GetComponent<Monster>().isActive)
                            {
                                EnemyController = unit.GetComponent<Monster>();
                                EnemyController.UpdateMonster();
                                if (EnemyController.AttackIndicator == 1)
                                {
                                    EnemyController.AttackIndicator = 0;
                                    TotalDamage += EnemyController.attack;
                                }
                            }
                            else
                            {
                                continue;
                            }
                        }
                        if (TotalDamage != 0)
                        {
                            enemyfeedback.text = "Total damage taken: " + TotalDamage;
                        }
                    }
                    UIHealth.text = "HP: " + PlayerController.health;
                    UIAttack.text = "ATK: " + PlayerController.attack;
                }
            }
        }
        else
        {
            Color currColorScreen = Screen.color;
            currColorScreen = Screen.color;
            currColorScreen.a = 1.0f;
            Screen.color = currColorScreen;
            Screen.text = "GAME OVER";
        }
    }
}
