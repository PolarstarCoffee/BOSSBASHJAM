using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFaceCamera : MonoBehaviour
{
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 adjustedPosition = new Vector3(cam.transform.position.x, transform.position.y, cam.transform.position.z);
        transform.LookAt(adjustedPosition, Vector3.up);
    }
}
