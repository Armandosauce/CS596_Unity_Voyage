using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour
{
    public Transform target;
    public float smoothing;

    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private PlayerInputController mouseInput;
    private Quaternion xturnAngle;
    private Quaternion yturnAngle;


    public bool lookAtPlayer = false;
    public bool rotateAroundPlayer = false;

    void Start()
    {

    }

    void Update()
    {
        if (rotateAroundPlayer)
        {
            xturnAngle =
                Quaternion.AngleAxis(mouseInput.Current.MouseInput.x *
                rotationSpeed, Vector3.up);

            yturnAngle =
                Quaternion.AngleAxis(-mouseInput.Current.MouseInput.y *
                rotationSpeed, Vector3.right);
        }


        offset = xturnAngle * offset;
        Vector3 newxPos = target.position + offset;
        transform.position = Vector3.Slerp(transform.position, newxPos, smoothing);


        offset = yturnAngle * offset;
        Vector3 newyPos = target.position + offset;
        transform.position = Vector3.Slerp(transform.position, newyPos, smoothing);

        if (lookAtPlayer || rotateAroundPlayer)
        {
            transform.LookAt(target);
        }
    }
}