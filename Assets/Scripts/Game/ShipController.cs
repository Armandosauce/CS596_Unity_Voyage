using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public int missingParts;
    public bool isComplete;
    public GameObject[] missingPartsArray;
    public GameObject[] shipComponentsArray;
    public Transform[] spawnPoints;
    private List<GameObject> activeParts = new List<GameObject>();

    public GameObject shipCompletionBar;

    private void Awake()
    {
        isComplete = false;
        missingParts = missingPartsArray.Length;
        SpawnShipParts();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        // Consider making it a trigger instead of using RigidBody for collision
        if (collision.gameObject.name == "PlayerCharacter")
        {
            missingParts = 0;
            int deliveredParts = 0;

            for (int i = 0; i < activeParts.Count; i++)
            {
                if (activeParts[i].GetComponent<ShipPartsController>().collected == false)
                {
                    missingParts++;
                }
                else
                {
                    shipComponentsArray[i].SetActive(true);
                    deliveredParts++;
                }
            }

            SetCompletionBar(deliveredParts);

            if (missingParts == 0)
            {
                isComplete = true;
                Debug.Log("Ship is complete!");
            }
            else
            {
                Debug.Log("Number of parts missing: " + missingParts);
            }
        }
    }

    public void SpawnShipParts()
    {
        Debug.Log("Spawning ship parts on map.");
        List<Transform> activeSpawnPoints = new List<Transform>(spawnPoints);


        foreach (GameObject part in missingPartsArray)
        {
            int pos = Random.Range(0, activeSpawnPoints.Count);
            GameObject prefab = Instantiate(part, activeSpawnPoints[pos]);
            activeParts.Add(prefab);
            activeSpawnPoints.RemoveAt(pos);
        }
    }

    public void SetCompletionBar(int deliveredItems)
    {
        switch(deliveredItems)
        {
            case 1:
                shipCompletionBar.transform.GetChild(1).gameObject.SetActive(true);
                break;
            case 2:
                shipCompletionBar.transform.GetChild(2).gameObject.SetActive(true);
                break;
            case 3:
                shipCompletionBar.transform.GetChild(3).gameObject.SetActive(true);
                break;
        }
    }
}
