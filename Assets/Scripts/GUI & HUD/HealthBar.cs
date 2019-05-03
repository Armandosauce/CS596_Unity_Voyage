using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject player;
    public float totalHealth;
    public float playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        totalHealth = player.GetComponent<Player>().health;
        SetHealth(1);

    }

    // Update is called once per frame
    void Update()
    {
        playerHealth = player.GetComponent<Player>().currentHealth;
        if (playerHealth > 0)
        {
            SetHealth(playerHealth / totalHealth);
        }
        else
        {
            SetHealth(0);
        }
    }

    public void SetHealth(float healthNormalized)
    {
        gameObject.transform.localScale = new Vector3(healthNormalized, 1f);
    }
}
