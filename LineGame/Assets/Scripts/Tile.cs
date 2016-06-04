using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

    private Vector2 boardIndex;     // Index of tile on board
    public Tile childTile;          // Make private

    public Vector2 BoardIndex
    {
        get { return boardIndex; }
        set { boardIndex = value; }
    }

    public virtual Tile ChildTile
    {
        get { return childTile; }
        set
        {
            childTile = value;
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
