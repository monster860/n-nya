using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject followTarget;
    GameObject defaultFollowTarget;
    public float followStrength = 1;
    // Start is called before the first frame update
    void Start()
    {
        defaultFollowTarget = PlayerController.instance.gameObject;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 theirs = followTarget ? followTarget.transform.position : defaultFollowTarget.transform.position + (Vector3.up*3);
        Vector3 ours = transform.position;
        Vector3 offset = theirs - ours;
        offset.z = 0;
        offset *= Mathf.Min(1, Mathf.Exp(Time.deltaTime + followStrength));
        transform.position = transform.position + offset;
    }
}
