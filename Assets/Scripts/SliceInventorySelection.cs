using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceInventorySelection : MonoBehaviour
{
    private static GameObject selectedPiece = null;
    private bool followMouse = false;
    private GameObject highlightGraphics;
    private Vector3 originalPosition;
    private bool mouseOver = false;

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

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                // replacing piece in spinner
                if (mouseOver && selectedPiece != null && selectedPiece != this.gameObject)
                {
                    // align new pice in spinner
                    selectedPiece.transform.parent = transform.parent;
                    selectedPiece.transform.localScale = transform.localScale;
                    selectedPiece.transform.position = transform.position;
                    selectedPiece.transform.rotation = transform.rotation;
                    selectedPiece = null;
                    // delete this old piece
                    Destroy(this.gameObject);
                }
                // unselecting if there's not a piece underneath to replace
                else
                {
                    GetComponent<CircleCollider2D>().enabled = true;
                    transform.position = originalPosition;
                    selectedPiece = null;
                    followMouse = false;
                }
            }
        }
    }

    private void OnMouseOver()
    {
        mouseOver = true;
        highlightGraphics.SetActive(true);
    }

    private void OnMouseExit()
    {
        highlightGraphics.SetActive(false);
        mouseOver = false;
    }

    private void OnMouseDown()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        originalPosition = transform.position;
        selectedPiece = this.gameObject;
        followMouse = true;
        highlightGraphics.SetActive(false);
    }

    public void UnSelect()
    {

    }
}