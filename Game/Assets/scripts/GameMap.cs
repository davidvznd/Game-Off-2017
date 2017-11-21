using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameMap : MonoBehaviour {
    public GameObject RoomObj;
    public GameObject WallObj;
    public GameObject ItemObj;
    public GameObject MonsterObj;
    public GameObject ExitObj;
    public Camera GameCamera;
    public Player PlayerObj;
    List<Item> ItemList;
    public int playerY;
    public int playerX;
    public bool CallMapChange; 

    public char[,] maze = new char[15, 15];
    void Recursion(int X, int Y)
    {
        List<int> directionArr = new List<int> { 1, 2, 3, 4};
        //System.Random rnd = new System.Random();
        //int[] directionArr = Enumerable.Range(1, 4).OrderBy(r => rnd.Next()).ToArray();
        for (int i = 0; i < directionArr.Count; i++)
        {
            int temp = directionArr[i];
            int randomIndex = Random.Range(i, directionArr.Count);
            directionArr[i] = directionArr[randomIndex];
            directionArr[randomIndex] = temp;
        }
        //Debug.Log(directionArr[0] + "," + directionArr[1] + "," + directionArr[2] + "," + directionArr[3]);

        for (int i = 0; i < directionArr.Count; i++)
        {
            //If not out of mazes range, we call back the Recursion function with a new coordinate.
            switch (directionArr[i])
            {
                case 1:
                    if (Y - 2 <= 0)
                    {
                        continue;
                    }
                    if (maze[Y-2,X] != ' ')
                    {
                        maze[Y-2,X] = ' ';
                        maze[Y-1,X] = ' ';
                        Recursion(X, Y - 2);
                    }
                    break;
                case 2:
                    if (X + 2 >= 11)
                    {
                        continue;
                    }
                    if (maze[Y, X + 2] != ' ')
                    {
                        maze[Y, X+2] = ' ';
                        maze[Y, X+1] = ' ';
                        Recursion(X + 2, Y);
                    }
                    break;
                case 3:
                    if (Y + 2 >= 11)
                    {
                        continue;
                    }
                    if (maze[Y+2,X] != ' ')
                    {
                        maze[Y+2,X] = ' ';
                        maze[Y+1,X] = ' ';
                        Recursion(X, Y + 2);
                    }
                    break;
                case 4:
                    if (X - 2 <= 0)
                    {
                        continue;
                    }
                    if (maze[Y, X-2] != ' ')
                    {
                        maze[Y,X-2] = ' ';
                        maze[Y,X-1] = ' ';
                        Recursion(X - 2, Y);
                    }
                    break;
            }
        }
    }
    
    void GenerateMaze() {
        //If initial coordinate is even, causes maze to look unfinished.
        int startX = 0;
        int startY = 0;

        //0 to 14 X and Y for start maze position
        while (startX % 2 == 0)
        {
            startX = Random.Range(0, 11);
        }
        while (startY % 2 == 0)
        {
            startY = Random.Range(0, 11);
        }

        //ALL WALLS
        for (int i = 0; i < 11; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                maze[i,j] = '#';
            }
        }
        maze[startY,startX] = ' ';

        //Initialize Recrusion to build the maze
        Recursion(startX, startY);
        
        //Start putting items and monsters in, we have about 70-80 to work with
    }

    void GenerateMonIt()
    {
        bool ExitCreated = false;
        while (ExitCreated == false)
        {
            int i = Random.Range(1, 10);
            int j = Random.Range(1, 10);
            if (maze[i, j] == ' ')
            {
                if ((maze[i, j + 1] == '#' && maze[i, j - 1] == '#' && maze[i + 1, j] == '#') ||
                    (maze[i, j + 1] == '#' && maze[i, j - 1] == '#' && maze[i - 1, j] == '#') ||
                    (maze[i + 1, j] == '#' && maze[i - 1, j] == '#' && maze[i, j - 1] == '#') ||
                    (maze[i + 1, j] == '#' && maze[i - 1, j] == '#' && maze[i, j + 1] == '#'))
                {
                    maze[i, j] = 'E';
                    ExitCreated = true;
                }
            }
        }

        //Slightly inefficient but it works. 
        int ItemsLeftToSpawn = 3;
        int Attempts = 0;
        while (ItemsLeftToSpawn > 0 && Attempts < 200)
        {
            int i = Random.Range(1, 10);
            int j = Random.Range(1, 10);
            if (maze[i, j] == ' ')
            {
                if ((maze[i, j + 1] == '#' && maze[i, j - 1] == '#' && maze[i + 1, j] == '#') ||
                    (maze[i, j + 1] == '#' && maze[i, j - 1] == '#' && maze[i - 1, j] == '#') ||
                    (maze[i + 1, j] == '#' && maze[i - 1, j] == '#' && maze[i, j - 1] == '#') ||
                    (maze[i + 1, j] == '#' && maze[i - 1, j] == '#' && maze[i, j + 1] == '#'))
                {
                    maze[i, j] = 'I';
                    ItemsLeftToSpawn -= 1;
                }
                Attempts += 1;
            }
        }

        //Monsters next!
        int MonstersLeftToSpawn = 4;
        while (MonstersLeftToSpawn > 0)
        {
            int monsterY = Random.Range(1, 10);
            int monsterX = Random.Range(1, 10);
            if (maze[monsterY,monsterX] == ' ')
            {
                maze[monsterY, monsterX] = 'M';
                MonstersLeftToSpawn -= 1;
            }
        }
    }

    // Use this for initialization
    void Start () {
        GenerateMaze();

        //Create player last
        playerX = Random.Range(1, 10);
        playerY = Random.Range(1, 10);
        while (maze[playerY, playerX] == '#')
        {
            playerY = Random.Range(1, 10);
            playerX = Random.Range(1, 10);
        }
        maze[playerY, playerX] = 'P';
        //Debug.Log(playerX);
        //Debug.Log(playerY);
        GenerateMonIt();

        //Visual side of maze
        for (int y = 0; y < 11; y++)
        {
            for (int x = 0; x < 11; x++)
            {
                if (maze[y,x] == ' ')
                {
                    //Empty Room
                    Instantiate(RoomObj, new Vector3(x, y, 0), Quaternion.identity);

                }
                else if (maze[y,x] == 'P')
                {
                    //Player
                    Instantiate(RoomObj, new Vector3(x, y, 0), Quaternion.identity);
                    Instantiate(PlayerObj, new Vector3(x, y, 0), Quaternion.identity);
                }
                else if (maze[y,x] == 'I')
                {
                    GameObject Test;
                    Item script;
                    //Items
                    Instantiate(RoomObj, new Vector3(x, y, 0), Quaternion.identity);
                    Test = Instantiate(ItemObj, new Vector3(x, y, 0), Quaternion.identity);
                    script = Test.GetComponent<Item>();
                    script.positionX = x;
                    script.positionY = y;
                }
                else if (maze[y, x] == 'M')
                {
                    GameObject Test;
                    Monster script;

                    //Monsters
                    Instantiate(RoomObj, new Vector3(x, y, 0), Quaternion.identity);
                    Test = Instantiate(MonsterObj, new Vector3(x, y, 0), Quaternion.identity);
                    script = Test.GetComponent<Monster>();
                    script.positionX = x;
                    script.positionY = y;
                }
                else if (maze[y, x] == 'E')
                {
                    Instantiate(ExitObj, new Vector3(x, y, 0), Quaternion.identity);
                }
                else 
                {
                    //Wall
                    Instantiate(WallObj, new Vector3(x, y, 0), Quaternion.identity);
                }
            }
        }
    }
	
    void UpdateTheMap(bool call) {
        if (call)
        {
            //Visual side of maze
            for (int y = 0; y < 11; y++)
            {
                for (int x = 0; x < 11; x++)
                {
                    if (maze[y, x] == ' ')
                    {
                        //Empty Room
                        Instantiate(RoomObj, new Vector3(x, y, 0), Quaternion.identity);
                    }
                    else if (maze[y, x] == 'P')
                    {
                        //Player
                        Instantiate(RoomObj, new Vector3(x, y, 0), Quaternion.identity);
                        Instantiate(PlayerObj, new Vector3(x, y, 0), Quaternion.identity);
                    }
                    else if (maze[y, x] == 'I')
                    {
                        //Items
                        Instantiate(RoomObj, new Vector3(x, y, 0), Quaternion.identity);
                        Instantiate(ItemObj, new Vector3(x, y, 0), Quaternion.identity);
                    }
                    else if (maze[y, x] == 'M')
                    {
                        //Monsters
                        Instantiate(RoomObj, new Vector3(x, y, 0), Quaternion.identity);
                        Instantiate(MonsterObj, new Vector3(x, y, 0), Quaternion.identity);
                    }
                    else if (maze[y, x] == 'E')
                    {
                        Instantiate(ExitObj, new Vector3(x, y, 0), Quaternion.identity);
                    }
                    else
                    {
                        //Wall
                        Instantiate(WallObj, new Vector3(x, y, 0), Quaternion.identity);
                    }
                }
            }
            call = false;
        }
        else
        {
            //Debug.Log("Nothing to change - MAP");
        }
    }

	// Update is called once per frame
	void Update () {
    }
}