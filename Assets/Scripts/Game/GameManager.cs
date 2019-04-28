using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager manager;
    /*
    public KeyCode jump { get; set; }
    public KeyCode forward { get; set; }
    public KeyCode backward { get; set; }
    public KeyCode left { get; set; }
    public KeyCode right { get; set; }
    */

    private void Awake()
    {
        if(manager == null)
        {
            DontDestroyOnLoad(this.gameObject);
            manager = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        Cursor.visible = false;
    }
    
}
