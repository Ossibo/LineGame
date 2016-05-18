using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Node : Tile {

    public int nnrOfTiles;
    public Text displayNumber;

	// Use this for initialization
	void Start ()
    {
        displayNumber = GetComponent<Text>();
        //displayNumber.transform.position = this.transform.position;
        displayNumber.color = Color.white;
        displayNumber.text = "" + nnrOfTiles;
	}


	// Update is called once per frame
	void Update ()
    {
	}
}
