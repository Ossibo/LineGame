using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMode : MonoBehaviour {

    public List<string> levelToBuild;

    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        // stop object from automatically destroyed on loading a scene
        DontDestroyOnLoad(this);
    }
}
