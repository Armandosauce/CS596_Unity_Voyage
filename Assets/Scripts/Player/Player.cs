using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float health;

    public float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentHealth);

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
