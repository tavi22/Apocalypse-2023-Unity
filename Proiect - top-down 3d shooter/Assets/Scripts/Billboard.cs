using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Billboard : MonoBehaviour
{
    Transform CameraTransform;

    private void Start()
    {
        CameraTransform = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
    }
    void LateUpdate()
    {
        transform.LookAt(transform.position + CameraTransform.forward);
    }
}
