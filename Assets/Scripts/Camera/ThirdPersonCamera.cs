using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;
    public Transform camTransform;
    
    [SerializeField]
    private float distance;
    [SerializeField]
    private float sensitivityX;
    [SerializeField]
    private float sensitivityY;
    [SerializeField]
    private float yAngleMin;
    [SerializeField]
    private float yAngleMax;
    [SerializeField]
    private PlayerInputController input;

    private Vector3 direction;
    private Camera cam;
    private float currentX;
    private float currentY;

    // Start is called before the first frame update
    void Start()
    {
        camTransform = this.transform;
        cam = Camera.main;
    }

    void Update()
    {
        currentX += input.Current.MouseInput.x;
        currentY -= input.Current.MouseInput.y;

        currentY = Mathf.Clamp(currentY, yAngleMin, yAngleMax);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        direction = new Vector3(0, 0, -distance);

        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = target.position + rotation * direction;

        camTransform.LookAt(target.position);
        
    }
}
