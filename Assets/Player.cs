using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float health;

    private float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentHealth);   
    }

    public void takeDamage(float x)
    {
        currentHealth -= x;
    }
}
