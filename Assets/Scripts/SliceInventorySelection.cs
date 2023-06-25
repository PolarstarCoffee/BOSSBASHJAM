using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceInventorySelection : MonoBehaviour
{
    private static GameObject selectedPiece = null;
    private bool followMouse = false;
    private GameObject highlightGraphics;
    private Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        highlightGraphics = transform.Find("GraphicsHighlight").gameObject;
        highlightGraphics.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (followMouse)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0.0f;
            transform.position = mousePos;

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                GetComponent<BoxCollider2D>().enabled = true;
                transform.position = originalPosition;
                selectedPiece = null;
                followMouse = false;
            }
        }
    }

    private void OnMouseOver()
    {
        if (selectedPiece != null)
        {
            highlightGraphics.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {

        }
    }

    private void OnMouseExit()
    {
        highlightGraphics.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (!followMouse && selectedPiece != null) // going to replace this piece with the selected one
        {

        }
        else // select this piece
        {
            GetComponent<BoxCollider2D>().enabled = false;
            originalPosition = transform.position;
            selectedPiece = this.gameObject;
            followMouse = true;
            highlightGraphics.SetActive(false);
        }
    }
}