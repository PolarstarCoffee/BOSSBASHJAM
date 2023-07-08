using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3[] cameraPoints;
    public AnimationCurve curve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
    private int currentIndex;
    private Camera cam;

    // lerp variables
    private bool lerping;
    private float lerpCounter;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        currentIndex = 0;
        lerping = false;
        lerpCounter = 0.0f;
        cam.transform.position = cameraPoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (lerping)
        {
            lerpCounter += Time.deltaTime;
            cam.transform.position = Vector3.Lerp(cameraPoints[currentIndex], cameraPoints[currentIndex + 1], curve.Evaluate(lerpCounter));
            if (lerpCounter >= 1.0f)
            {
                currentIndex++;
                lerping = false;
            }

        }

        if (Input.GetKey(KeyCode.R))
        {
            MoveToNextCheckpoint();
        }
    }

    public void MoveToNextCheckpoint()
    {
        // early exit if we're at the final spot
        if (currentIndex == cameraPoints.Length)
            return;
        lerpCounter = 0.0f;
        lerping = true;
    }
}
