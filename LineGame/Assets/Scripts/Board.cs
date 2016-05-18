using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Board : MonoBehaviour
{
    public List<string> levelToBuild;

    private GameObject[,] board;
    public int boardWidth;
    public int boardHeight;
	
    // Use this for initialization
	void Start ()
    {
        GetLevelData();
        CreateBoardFromString();
	}
    void GetLevelData()
    {
        // find GameMode object in scene and then find its component
        GameMode gameMode = (GameMode)GameObject.Find("GameMode").GetComponent("GameMode");
        // set variables from the GameMode
        levelToBuild = gameMode.levelToBuild;
    }
    // Create the map from the string
    void CreateBoardFromString()
    {
        // Get number of Tiles in the game
        string boardSize = levelToBuild[0];
        boardWidth = int.Parse(boardSize.Split(',')[0]);
        boardHeight = int.Parse(boardSize.Split(',')[1]);
        Vector2 offset = new Vector2(-(boardWidth - 1) * 0.5f, -(boardHeight - 1) * 0.5f);

        // create the board with prefabs
        board = new GameObject[boardHeight, boardHeight];
        for(int i = 0; i < boardHeight; ++i)
        {
            for(int j = 0; j < boardWidth; ++j)
            {
                char tileType = levelToBuild[i + 1][j];
                switch(tileType)
                {
                    case '+':
                        GameObject tempFloor = (GameObject)Instantiate(Resources.Load("Prefabs/Floor"));
                        tempFloor.transform.SetParent(this.transform);
                        tempFloor.transform.position = new Vector3(offset.x + j, offset.y + i, 0.0f);
                        Node floorComponent = (Node)tempFloor.GetComponent("Node");
                        board[i, j] = tempFloor;
                        break;
                    case '#':
                        GameObject tempWall = (GameObject)Instantiate(Resources.Load("Prefabs/Wall"));
                        tempWall.transform.SetParent(this.transform);
                        tempWall.transform.position = new Vector3(offset.x + j, offset.y + i, 0.0f);
                        Node wallComponent = (Node)tempWall.GetComponent("Node");
                        board[i, j] = tempWall;
                        break;
                    default:
                        GameObject tempNode = (GameObject)Instantiate(Resources.Load("Prefabs/Node"));
                        tempNode.transform.SetParent(this.transform);
                        tempNode.transform.position = new Vector3(offset.x + j, offset.y + i, 0.0f);
                        Node nodeComponent = (Node)tempNode.GetComponent("Node");
                        nodeComponent.nnrOfTiles = CalculateNumberOfTiles(tileType);
                        board[i, j] = tempNode;
                        break;
                }
            }
        }
    }
    int CalculateNumberOfTiles(char number)
    {
        string parseString = "" + number;
        int returnNumber;
        
        // test if the char is a character or number and uses the number or else it calculates the value of the charater
        if (!(int.TryParse(parseString, out returnNumber)))
        {
            int charValue = (int)number - 96;
            returnNumber = 9 + charValue;
        }

        return returnNumber;
    }
	// Update is called once per frame
	void Update ()
    {

	}
}
