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

    public Transform itemsParent;

    private List<Vector3> itemPositions;
    private List<GameObject> activeParts = new List<GameObject>();
    private int completionCount;
    AudioSource audioSource;
    public AudioClip itemDeliveredSound;

    public GameObject shipCompletionBar;

    private void Awake()
    {
        activeParts = new List<GameObject>();
        itemPositions = new List<Vector3>();
        isComplete = false;
        completionCount = 0;
        missingParts = missingPartsArray.Length;
        audioSource = GameObject.Find("AudioSource").GetComponent<AudioSource>();
        SpawnShipParts();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
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
                //gameManager.GetComponent<CrossHair>().enabled = false;
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

        foreach (Transform child in itemsParent)
        {

            itemPositions.Add(child.position);
        }

        foreach (GameObject part in missingPartsArray)
        {
            int pos = Random.Range(0, itemPositions.Count);
            GameObject prefab = Instantiate(part, itemPositions[pos], Quaternion.identity);
            activeParts.Add(prefab);
            itemPositions.RemoveAt(pos);
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
