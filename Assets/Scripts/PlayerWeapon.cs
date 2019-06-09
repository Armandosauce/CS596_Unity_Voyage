using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInputController))]
public class PlayerWeapon : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletSpawn;

    [SerializeField]
    private float bulletSpeed;
    [SerializeField]
    private float fireRate;

    private PlayerWeapon weapon;
    private PlayerInputController input;
    private Transform cam;

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<PlayerInputController>();
        cam = PlayerManager.instance.mainCamera.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (input.Current.Fire)
        {
            Fire();
        }
        
    }

    public void Fire()
    {
        GameObject projectileInstance = Instantiate(bullet, transform.GetChild(2).position, transform.GetChild(2).rotation);
        Rigidbody rb = projectileInstance.GetComponent<Rigidbody>();
        Vector3 direction = cam.forward.normalized;
        rb.AddForce(direction * bulletSpeed);
    }
}
