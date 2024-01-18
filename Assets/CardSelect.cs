using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class CardSelect : MonoBehaviour
{
    public bool isCard;
    private bool isSelected = false;
    private Vector3 originalScale;
    private Vector3 offset;
    private bool isDragged = false;
    public bool canBePlaced = false;
    public Vector3 originalPosition;
    public DeckScript deckScript;
    public CardScript cardScript;
    public bool canBeInteracted = true;

    public CookerScript cookerScript;

    private bool wasPlayed = false;

    public SelectedCardScript selectedCardScript;
    public GameObject currentSurface;


    public Vector3 previousPosition;
    
    private void Start()
    {
        originalScale = transform.localScale;
        deckScript = GameObject.FindWithTag("DeckManager").GetComponent<DeckScript>();
        selectedCardScript = GameObject.FindWithTag("CardManager").GetComponent<SelectedCardScript>();
        cardScript = gameObject.GetComponent<CardScript>();
    }

    private void OnMouseEnter()
    {
        // Highlight the card when the mouse hovers over it
        if (!isSelected)
        {
            transform.localScale = originalScale * 1.2f;
        }
    }

    private void OnMouseExit()
    {
        if (!isDragged)
        {
            isSelected = false;
            transform.localScale = originalScale;
        }
    }

    private void OnMouseDown()
    {
        if (canBeInteracted)
        {
            // Toggle card selection when clicked
            previousPosition = transform.position;
            isSelected = !isSelected;
            // Store the offset between the mouse click position and the card's position
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (isCard)
                selectedCardScript.SetSelectedCard(this.gameObject);
            else
                selectedCardScript.SetSelectedDish(this.gameObject);
        }
    }

    private void OnMouseUp()
    {
        if (isDragged)
        {
            if (currentSurface != null)
            {
                selectedCardScript.selectedCard = null;
                selectedCardScript.selectedDish = null;
                selectedCardScript.selectedCardScriptable = null;
                isDragged = false;
                isSelected = false;

                if (currentSurface.CompareTag("CookerSlot") && isCard)
                {
                    originalScale = new Vector3(0.0017f, 0.0017f, transform.localScale.z);
                    transform.localScale = originalScale;
                    transform.position = currentSurface.transform.position;
                    currentSurface.GetComponent<CookerSlotScript>().cardInSlot = this.gameObject;
                    canBeInteracted = false;
                    if (!wasPlayed && isCard)
                    {
                        deckScript.CardPlayed(this.gameObject, cardScript.cardScriptable);
                        wasPlayed = true;
                    }
                }
                else if (currentSurface.CompareTag("TableSlot") && !isCard)
                {
                    transform.localScale = originalScale;
                    transform.position = currentSurface.transform.position;
                    currentSurface.GetComponent<TableSlotScript>().cardInSlot = this.gameObject;
                    canBeInteracted = false;
                    cookerScript.UnlockSlots();
                }
                else
                {
                    transform.position = previousPosition;
                    transform.localScale = originalScale;
                }
            }
            else
            {
                selectedCardScript.selectedCard = null;
                selectedCardScript.selectedDish = null;
                selectedCardScript.selectedCardScriptable = null;
                transform.position = previousPosition;
                transform.localScale = originalScale;
                isDragged = false;
                isSelected = false;
            }
        }
    }

    public void SetSurface(GameObject surface)
    {
        currentSurface = surface;
    }
    
    private void OnMouseDrag()
    {
        if (isSelected)
        {
            isDragged = true;
            // Calculate the new card position based on the mouse position and offset
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;

            // Set the card's position to the new position while preserving the z-axis
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
        }
    }

    private void FixedUpdate()
    {
        if (isDragged && isCard)
        {
            CardResizer();
        }
    }

    private void CardResizer()
    {
        float minY = -3f;   // Minimum Y position for resizing
        float maxY = -2.2f; // Maximum Y position for resizing
        float minScale = 0.0022f;
        float maxScale = 0.0017f;

        // Get the current mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Clamp the Y position within the desired range
        float clampedY = Mathf.Clamp(mousePosition.y, minY, maxY);

        // Calculate the scale factor based on the clamped Y position
        float scale = Mathf.Lerp(minScale, maxScale, (clampedY - minY) / (maxY - minY));

        // Apply the scale to the card while preserving the Z-axis
        transform.localScale = new Vector3(scale, scale, transform.localScale.z);
    }
}
