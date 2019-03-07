using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGenerator : MonoBehaviour {

    public float planetRadius;
    private float xSize, ySize, zSize;
    private const float pi = 3.14159265f;

	// Use this for initialization
	void Start () {
        MeshFilter meshfilter = GetComponent<MeshFilter>();
        Mesh mesh = meshfilter.mesh;

        xSize = mesh.bounds.size.x;
        ySize = mesh.bounds.size.y;
        zSize = mesh.bounds.size.z;

        Vector3[] vertices = new Vector3[mesh.vertices.Length];
        System.Array.Copy(mesh.vertices, vertices, vertices.Length);

        for(int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = MapToSphere(vertices[i]);
            
        }

        mesh.vertices = vertices;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
        
        meshfilter.mesh = mesh;

        this.GetComponentInChildren<MeshCollider>().sharedMesh = mesh;
        
	}

    private Vector3 MapToSphere(Vector3 coord)
    {
        float thetaDelta = ((2 * pi) / (xSize - 1));
        float phiDelta = ((pi) / (zSize - 0.5f));
        float planetRadius = ((xSize) / pi / 2f);
        float theta = (coord.z * thetaDelta);
        float phi = coord.x * phiDelta;

        //Limit the map to half a sphere
        /*
        if (theta > pi) { theta = theta - (pi); }

        if (theta < 0.0) { theta = theta + (pi); }

        if (phi > 2 * pi) { phi = phi - (2 * pi); }

        if (phi < 0.0) { phi = phi + (2 * pi); }
        */
        Vector3 coords2 = new Vector3();
        coords2.x = (float)(((planetRadius) * Mathf.Sin(theta) * Mathf.Cos(phi)) + xSize / 2f);
        coords2.y = (float)((planetRadius) * Mathf.Sin(theta) * Mathf.Sin(phi));
        coords2.z = (float)(((planetRadius) * Mathf.Cos(theta)) + zSize / 2f);
        return coords2;
    
    }
	
}
