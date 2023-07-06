using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceInventoryManager : MonoBehaviour
{
    public SliceInventorySelection highlightedPiece = null;
    public SliceInventorySelection selectedPiece = null;
    private Vector3 selectedPieceOriginalPos;

    // singleton
    private static SliceInventoryManager instance;

    // singleton access
    public static SliceInventoryManager Instance()
    {
        return instance;
    }

    private void Awake()
    {
        // setting up singleton
        if (instance != null && instance != this)
            Destroy(this);
        instance = this;
    }

    void Update()
    {
        // if holding a piece, it follows the mouse
        if (selectedPiece != null)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0.0f;
            selectedPiece.transform.position = mousePos;
        }

        // selecting a piece
        if (Input.GetKeyDown(KeyCode.Mouse0) && highlightedPiece != null && selectedPiece == null)
        {
            AssignSelectedPiece(highlightedPiece);
        }
        // releasing selected piece
        else if (Input.GetKeyUp(KeyCode.Mouse0) && selectedPiece != null)
        {
            ProcessClick();
        }
    }

    // decides what happens when you let go of a selected piece
    private void ProcessClick()
    {
        // replace highlighted piece with selected one
        if (highlightedPiece != null)
        {
            selectedPiece.transform.parent = highlightedPiece.transform.parent;
            selectedPiece.transform.localScale = highlightedPiece.transform.localScale;
            selectedPiece.transform.position = highlightedPiece.transform.position;
            selectedPiece.transform.rotation = highlightedPiece.transform.rotation;
            selectedPiece.GetComponent<SpinnerSlice>().CopySliceInfo(highlightedPiece.GetComponent<SpinnerSlice>());
            selectedPiece = null;
            Destroy(highlightedPiece.gameObject);
        }
        // reset selected piece
        else
        {
            selectedPiece.Reset(selectedPieceOriginalPos);
            selectedPiece = null;
        }
    }

    public void AssignHighlightedPiece(SliceInventorySelection piece)
    {
        highlightedPiece = piece;
        highlightedPiece.Highlight();
    }

    public void ClearHighlightedPiece(SliceInventorySelection piece)
    {
        if (highlightedPiece == piece)
        {
            highlightedPiece.UnHighlight();
            highlightedPiece = null;
        }
    }

    public void AssignSelectedPiece(SliceInventorySelection piece)
    {
        selectedPiece = piece;
        selectedPiece.PickUp();
        selectedPieceOriginalPos = selectedPiece.gameObject.transform.position;
        highlightedPiece = null; // go back and make all this cleanre
    }
}
