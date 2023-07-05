using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceInventorySelection : MonoBehaviour
{
    private static GameObject selectedPiece = null;
    private bool followMouse = false;
    private GameObject highlightGraphics;
    private Vector3 originalPosition;
    private bool mouseOverThisPiece = false;
    private static bool mouseOverAnyPiece = false;

    // Start is called before the first frame update
    void Start()
    {
        highlightGraphics = transform.Find("GraphicsHighlight").gameObject;
        highlightGraphics.SetActive(false);
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (selectedPiece != null)
    //        Debug.Log("selected piece: " + selectedPiece.gameObject.name);
    //    else
    //        Debug.Log("selected piece is null");
    //
    //    if (followMouse)
    //    {
    //        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        mousePos.z = 0.0f;
    //        transform.position = mousePos;
    //
    //        if (Input.GetKeyUp(KeyCode.Mouse0))
    //        {
    //            // replacing piece in spinner
    //            if (mouseOverThisPiece && selectedPiece != null && selectedPiece != this.gameObject)
    //            {
    //                Debug.Log("replacing piece");
    //                // align new piece in spinner
    //                selectedPiece.transform.parent = transform.parent;
    //                selectedPiece.transform.localScale = transform.localScale;
    //                selectedPiece.transform.position = transform.position;
    //                selectedPiece.transform.rotation = transform.rotation;
    //                selectedPiece = null;
    //                // delete this old piece
    //                Destroy(this.gameObject);
    //            }
    //            // unselecting if there's not a piece underneath to replace
    //            else if (!mouseOverAnyPiece)
    //            {
    //                Debug.Log("unselecting piece");
    //                GetComponent<CircleCollider2D>().enabled = true;
    //                transform.position = originalPosition;
    //                selectedPiece = null;
    //                followMouse = false;
    //            }
    //        }
    //    }
    //}

    private void OnMouseOver()
    {
        SliceInventoryManager.Instance().AssignHighlightedPiece(this);
        //mouseOverThisPiece = true; // set by this piece
        //mouseOverAnyPiece = true; // set by any piece
        //highlightGraphics.SetActive(true);
    }
    
    private void OnMouseExit()
    {
        SliceInventoryManager.Instance().ClearHighlightedPiece(this);

        //mouseOverThisPiece = false; // set by this piece
        //mouseOverAnyPiece = false; // set by any piece
        //highlightGraphics.SetActive(false);
    }

    //private void OnMouseDown()
    //{
    //    GetComponent<CircleCollider2D>().enabled = false;
    //    originalPosition = transform.position;
    //    selectedPiece = this.gameObject;
    //    followMouse = true;
    //    highlightGraphics.SetActive(false);
    //}


    // new stuff


    public void Highlight()
    {
        highlightGraphics.SetActive(true);

    }

    public void UnHighlight()
    {
        highlightGraphics.SetActive(false);

    }

    public void PickUp()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().sortingOrder = 100;
        originalPosition = transform.position;
        UnHighlight();
    }

    public void Reset(Vector3 originalPosition)
    {
        GetComponent<CircleCollider2D>().enabled = true;
        GetComponent<SpriteRenderer>().sortingOrder = 1;
        transform.position = originalPosition;
    }
}