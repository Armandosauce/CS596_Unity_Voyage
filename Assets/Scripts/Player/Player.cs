using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float startHealth;
    public float currentHealth { get; set; }

    //the amount of time a player is invulnerable after taking damage
    [SerializeField]
    private float inv_time;
    private bool isVulnerable;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startHealth;
        isVulnerable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0)
        {
            GameManager.instance.GetComponent<CrossHair>().enabled = false;
            Cursor.visible = true;
            SceneManager.LoadScene("GameOverScreen");
        }

        if(!isVulnerable)
        {

        }
        
    }

    public void takeDamage(float x)
    {
        if (isVulnerable)
        {
            currentHealth -= x;
            StartCoroutine(invulnerable());
        }
    }

    private IEnumerator invulnerable()
    {
        isVulnerable = false;
        yield return new WaitForSeconds(inv_time);
        isVulnerable = true;
    }
}
