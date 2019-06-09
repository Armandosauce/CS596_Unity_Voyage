using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target, player;
    
    [SerializeField]
    private float sensitivityX;
    [SerializeField]
    private float sensitivityY;
    [SerializeField]
    private float yAngleMin;
    [SerializeField]
    private float yAngleMax;
    [SerializeField]
    private float smoothing;
    [SerializeField]
    private PlayerInputController input;
    
    private Vector3 direction;
    private float currentX;
    private float currentY;

    // Start is called before the first frame update
    void Start()
    {

    }

    void LateUpdate()
    {
        camControl();
    }

    // Update is called once per frame
    void camControl()
    {
        currentX += input.Current.MouseInput.x * sensitivityX;
        currentY -= input.Current.MouseInput.y * sensitivityY;

        currentY = Mathf.Clamp(currentY, yAngleMin, yAngleMax);
        this.transform.LookAt(target);
        
        target.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(currentY, currentX, 0), Time.deltaTime * smoothing);
        player.rotation = Quaternion.Euler(0, currentX, 0);


        /*
        camTransform.position = target.position + rotation * direction;

        camTransform.LookAt(target.position + targetOffset);
        */

    }
}
