using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    public Vector3 offset;

    void Start()
    {
        transform.position = target.position + offset;
    }

    void LateUpdate()
    {
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        transform.LookAt(target);
    }
}