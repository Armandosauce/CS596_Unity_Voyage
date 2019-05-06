using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager manager;
    public Text objectiveText;
    /*
    public KeyCode jump { get; set; }
    public KeyCode forward { get; set; }
    public KeyCode backward { get; set; }
    public KeyCode left { get; set; }
    public KeyCode right { get; set; }
    */
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Exit command received.");
            QuitApp();
        }
    }


    private void Awake()
    {
        if (manager == null)
        {
            DontDestroyOnLoad(this.gameObject);
            manager = this;
            Invoke("DisableObjectiveText", 4f);
        }
        else
        {
            Destroy(this.gameObject);
        }
        Cursor.visible = false;
    }

    void DisableObjectiveText()
    {
        objectiveText.enabled = false;
    }

    public void QuitApp()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }

}
