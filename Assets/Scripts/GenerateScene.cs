using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateScene : MonoBehaviour
{
    public static GenerateScene instance;

    [SerializeField]
    private GameObject[] env_objects;

    //The step size determines the desnity of objects in the scene
    //a value of 1 will place an object on every vertex in the terrain mesh
    //as such, a higher value will spread out objects in the scene more
    [SerializeField]
    private int step;
    
    [SerializeField]
    private Transform spawnPositions;
    [SerializeField]
    private Transform parent;

    Quaternion spawnRotation;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }

    public void generate()
    {
        foreach (Transform child in spawnPositions)
        {
            Instantiate(env_objects[Random.Range(0, env_objects.Length)], child.position, Quaternion.identity, parent);
        }
    }


    /*
    spawnPosition = vertexWorldPosition(terrain.mesh.vertices[i], terrain.transform);
    Vector3 a = Vector3.Cross(Vector3.up, terrain.mesh.normals[i]);

    spawnRotation.x = a.x;
    spawnRotation.y = a.y;
    spawnRotation.z = a.z;

    spawnRotation.w = Mathf.Sqrt((Mathf.Pow(Vector3.up.magnitude, 2)) * (Mathf.Pow(terrain.mesh.normals[i].magnitude, 2))
                                    + Vector3.Dot(Vector3.up, terrain.mesh.normals[i]));      


    Instantiate(env_objects[Random.Range(0, env_objects.Length)], spawnPosition, spawnRotation);
            
    private Vector3 vertexWorldPosition(Vector3 vertex, Transform owner)
    {
        return owner.localToWorldMatrix.MultiplyPoint3x4(vertex);
    }
    
    */
}
