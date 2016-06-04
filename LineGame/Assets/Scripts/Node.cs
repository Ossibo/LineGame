using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Node : Tile {

    public int nrOfTiles;
    private Vector2 displayNrPos;       // Position for the nrOfTiles variable label in OnGUI function

	// Use this for initialization
	void Start ()
    {
        displayNrPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y)); 
	}

	// Update is called once per frame
	void Update ()
    {
	}

    // Updates object's GUI "components" once per frame
    void OnGUI()
    {
        GUI.Label(new Rect(displayNrPos.x, displayNrPos.y, 100, 100), nrOfTiles.ToString());
    }
}
