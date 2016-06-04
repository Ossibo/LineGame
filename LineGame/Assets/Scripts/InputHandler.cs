using UnityEngine;
using System.Collections;

// Input handler handles touch (and mouse debug) input from the player and draws lines accordingly.
public class InputHandler : MonoBehaviour {

    //private bool isDragging;
    public Node originNode;            // Node that line originates from, used to calculate available lines left  MAKE PRIVATE
    private Tile selectedTile;        // Currently selected TILE (node, floor, wall...)

	// Use this for initialization
	void Start ()
    {
        //isDragging = false;
        originNode = null;
        selectedTile = null;
	}
	
	// Update is called once per frame
	void Update ()
    {
        HandleMouseInput();
	}

    void Awake()
    {
        // stop object from automatically destroyed on loading a scene
        DontDestroyOnLoad(this);
    }

    // Get and handle touch input
    void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
                CheckHit(Input.GetTouch(0).position);
        }

        if (Input.touchCount == 0)
        {
            originNode = null;
            selectedTile = null;
        }
    }

    // Get and handle mouse input for debuging, ***************delete before release***************
    void HandleMouseInput()
    {
        if (Input.GetMouseButton(0))
        {
            CheckHit(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            originNode = null;
            selectedTile = null;
        }
    }

    // Check if input hits a node or line
    void CheckHit(Vector3 pos)
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(pos);
        Vector2 hitPos = new Vector2(worldPos.x, worldPos.y);

        Collider2D hit = Physics2D.OverlapPoint(hitPos);

        if (hit)
        {
            // If same tile not already selected
            if (!selectedTile || selectedTile.gameObject != hit.gameObject)
                HandleHit(hit);
        }
    }

    void HandleHit(Collider2D hit)
    {
        if (hit.tag == "Node")
        {
            originNode = hit.GetComponent<Node>();
        }
        else if (hit.tag == "Floor")
        {
            if (selectedTile)
            {
                Floor floor = hit.GetComponent<Floor>();    // The new floor hovered over
                UpdateTile(floor);
            }
        }
        selectedTile = hit.GetComponent<Tile>();
    }

    // Updates tile's parent and child if allowed.
    // Is separate function from HandleHit to allow breaking
    void UpdateTile(Floor floor)
    {
        // Return if there is no origin node
        if (!originNode)
        {
            originNode = GetOriginNode(selectedTile as Floor);
            if (!originNode)
                return;
        }

        // Return if not adjacent
        if ((floor.BoardIndex - selectedTile.BoardIndex).magnitude > 1)
        {
            originNode = null;
            return;
        }

        // If floor has a line, clear recursive (forward) relations
        if (floor.hasLine)
        {
            if (IsSameLine(floor, selectedTile as Floor))
            {
                originNode = null;
                return;
            }
            ClearRelationsFwd(floor);
        }

        floor.ParentTile = selectedTile;

        if (selectedTile.ChildTile) // Clear children, and on, if there are any
            ClearRelationsFwd(selectedTile.ChildTile as Floor);
        selectedTile.ChildTile = floor;
    }

    // Returns true if the two floors are connected.
    bool IsSameLine(Floor floor1, Floor floor2)
    {
        Floor tempFloor = floor1;
        while (tempFloor)
        {
            if (tempFloor == floor2)
                return true;
            tempFloor = tempFloor.ParentTile as Floor;
        }

        tempFloor = floor1;
        while(tempFloor)
        {
            if (tempFloor == floor2)
                return true;
            tempFloor = tempFloor.ChildTile as Floor;
        }
        return false;
    }

    // Recursive function to get the origin node, returns false if there is none.
    Node GetOriginNode(Floor startFloor)
    {
        if (!startFloor.parentTile)
            return null;

        if (startFloor.ParentTile is Node)
            return (startFloor.ParentTile as Node);
        else
            return GetOriginNode(startFloor.ParentTile as Floor);
    }

    // Recursive function to clear start floor's and it's children's relations.
    void ClearRelationsFwd(Floor startFloor)
    {
        startFloor.ParentTile.ChildTile = null;
        startFloor.ParentTile = null;

        if (startFloor.ChildTile)
        {
            ClearRelationsFwd(startFloor.ChildTile as Floor);
            startFloor.ChildTile = null;
        }
    }
}

