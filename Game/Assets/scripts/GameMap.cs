using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameMap : MonoBehaviour {
    public GameObject Room;
    public GameObject Wall;
    public Camera GameCamera;
    public Player test;

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
        Debug.Log(directionArr[0] + "," + directionArr[1] + "," + directionArr[2] + "," + directionArr[3]);

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


        for (int i = 0; i < 11; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                maze[i,j] = '#';
            }
        }
        maze[startY,startX] = ' ';

        //Initialize Recrusion
        Recursion(startX, startY);
    }

	// Use this for initialization
	void Start () {
        GenerateMaze();
        int playerX = Random.Range(0, 11);
        int playerY = Random.Range(0, 11);
        while (maze[playerY, playerX] == '#')
        {
            playerY = Random.Range(0, 11);
            playerX = Random.Range(0, 11);
        }
        maze[playerY, playerX] = 'P';
        test.positionY = playerY;
        test.positionX = playerX;

        for (int y = 0; y < 11; y++)
        {
            for (int x = 0; x < 11; x++)
            {
                if (maze[y,x] == ' ')
                {
                    Instantiate(Room, new Vector3(x, y, 0), Quaternion.identity);
                }
                else if (maze[y,x] == 'P')
                {
                    Instantiate(Room, new Vector3(x, y, 0), Quaternion.identity);
                    test.transform.position = new Vector3(x, y, 0);
                }
                else 
                {
                    Instantiate(Wall, new Vector3(x, y, 0), Quaternion.identity);
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {


    }
}