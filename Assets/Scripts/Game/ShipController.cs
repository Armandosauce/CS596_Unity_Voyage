using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipController : MonoBehaviour
{
    public int missingParts;
    public bool isComplete;
    public GameObject[] missingPartsArray;
    public GameObject[] shipComponentsArray;
    public Transform[] spawnPoints;
    private List<GameObject> activeParts = new List<GameObject>();
    private int completionCount;
    AudioSource audioSource;
    public AudioClip itemDeliveredSound;

    public GameObject shipCompletionBar;

    private void Awake()
    {
        isComplete = false;
        completionCount = 0;
        missingParts = missingPartsArray.Length;
        SpawnShipParts();
        audioSource = GameObject.Find("AudioSource").GetComponent<AudioSource>();
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
            if (completionCount != deliveredParts)
            {
                audioSource.PlayOneShot(itemDeliveredSound, 1f);
                completionCount = deliveredParts;
            }

            if (missingParts == 0)
            {
                isComplete = true;
                Debug.Log("Ship is complete!");
                GameObject gameManager = GameObject.Find("GameManager");
                gameManager.GetComponent<CrossHair>().enabled = false;
                Cursor.visible = true;
                SceneManager.LoadScene("CreditsScreen");
            }
            else
            {
                Debug.Log("Number of parts missing: " + missingParts);
                completionCount = deliveredParts;
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
