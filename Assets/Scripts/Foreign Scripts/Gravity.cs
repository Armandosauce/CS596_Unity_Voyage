using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour {

    public Transform planet;
	
	// Update is called once per frame
	void Update () {

        Vector3 dir = (transform.position - planet.position).normalized;

        //GetComponent<PlayerMachine>().RotateGravity(dir);

        this.transform.rotation = Quaternion.FromToRotation(this.transform.up, dir) * this.transform.rotation;


	}
}
