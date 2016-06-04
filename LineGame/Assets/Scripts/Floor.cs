using UnityEngine;
using System.Collections;

public class Floor : Tile
{
    //public Node originNode;         // Make private
    public Tile parentTile;         // Make private
    //public Tile childTile;          // Make private

    #region Getters and setters
    //public Node ParentNode
    //{
    //    get { return originNode; }
    //    set { originNode = value; }
    //}

    public Tile ParentTile
    {
        get { return parentTile; }
        set
        {
            parentTile = value;
            UpdateFloor();
            //if (value)
            //{
            //    SelectTexture(value.BoardIndex);
            //    hasLine = true;
            //}
            //else
            //{
            //    lineTexSelected = null;
            //    hasLine = false;
            //}
        }
    }

    public override Tile ChildTile
    {
        get { return childTile; }
        set
        {
            base.ChildTile = value;
            UpdateFloor();

            //if (value)
            //{
            //    SelectTexture(value.BoardIndex);
            //    hasLine = true;
            //}
            //else
            //{
            //    lineTexSelected = null;
            //    hasLine = false;
            //}
        }
    }
    #endregion

    // Line textures
    public Texture2D lineTexHalf;
    public Texture2D lineTexFull;
    public Texture2D lineTexTurn;

    public bool hasLine;

    public Texture2D lineTexSelected;
    private Vector2 linePosition;
    private float lineRotation;

	// Use this for initialization
	void Start () {
        linePosition = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x - 0.5f, -transform.position.y - 0.5f));
    }

    // Select correct texture and set variables.
    void UpdateFloor()
    {
        if (parentTile && !childTile)
        {
            hasLine = true;
            lineTexSelected = lineTexHalf;

            Vector2 direction = parentTile.BoardIndex - BoardIndex;
            if (direction.y >= 0)
                lineRotation = Vector2.Angle(direction, new Vector2(1.0f, 0.0f));
            else
                lineRotation = Vector2.Angle(direction, new Vector2(1.0f, 0.0f)) + 180;
        }
        else if (parentTile && childTile)
        {
            hasLine = true;
            lineTexSelected = lineTexFull;
        }
        else
        {
            hasLine = false;
            lineTexSelected = null;
        }
    }

    void OnGUI()
    {
        if (!lineTexSelected)
            return;
        GUIUtility.RotateAroundPivot(lineRotation, new Vector2(linePosition.x + 20, linePosition.y + 20));
        GUI.DrawTexture(new Rect(linePosition.x, linePosition.y, 40, 40), lineTexSelected);
    }
}