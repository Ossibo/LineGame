using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public GameMode gameMode;

    public List<string> lines;
    public string[] files;

	// Use this for initialization
	void Start ()
    {
        // find GameMode object in scene and then find its component
        gameMode = (GameMode)GameObject.Find("GameMode").GetComponent("GameMode");

        // find the path to the levels, then load all the level paths
        string dataPath = Application.dataPath + "/Resources/Levels/free linking/";
        files = System.IO.Directory.GetFiles(dataPath, "*.txt");

        // loads the first level directly to test if it works
        LoadFromResorces(files[1]);

        // loads the new scene to test if it works
        LoadToGameScene();
	}


    void LoadFromResorces(string filePath)
    {
        // splits the string to fit the the load function
        string readPath = filePath.Split('.')[0];
        string[] stringSeparators = new string[] { "Resources/" };
        readPath = readPath.Split(stringSeparators, System.StringSplitOptions.None)[1];
        
        // loads the file to a string
        TextAsset textFile = Resources.Load(readPath) as TextAsset;
        string stringFromFile = textFile.text;

        // splits the string in segments
        lines = new List<string>();
        lines.AddRange(stringFromFile.Split("\n"[0]));

    }
    void LoadToGameScene()
    {
        //information to pass through the scenes
        gameMode.levelToBuild = lines;

        // scene loader
        SceneManager.LoadScene("GameScene");
    }
	// Update is called once per frame
	void Update ()
    {
	
	}
}
