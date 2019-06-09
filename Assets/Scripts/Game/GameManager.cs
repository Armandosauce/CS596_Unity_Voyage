using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public Text objectiveText;
    public GameObject pauseScreen;

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

    public void Pause(bool isPaused)
    {
        if (isPaused == true)
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
            
        }
        else
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
        }
    }


        private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
            Invoke("DisableObjectiveText", 4f);
        }
        else
        {
            Destroy(this.gameObject);
        }

        Cursor.visible = false;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "world1")
        {
            Cursor.lockState = CursorLockMode.Locked;
            GetComponent<CrossHair>().enabled = true;
            GenerateScene.instance.generate();
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
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
