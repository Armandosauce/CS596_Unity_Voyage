using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float startHealth;

    public float currentHealth { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0)
        {
            GameObject gameManager = GameObject.Find("GameManager");
            gameManager.GetComponent<CrossHair>().enabled = false;
            Cursor.visible = true;
            SceneManager.LoadScene("GameOverScreen");
        }
    }

    public void takeDamage(float x)
    {
        currentHealth -= x;
    }
}
