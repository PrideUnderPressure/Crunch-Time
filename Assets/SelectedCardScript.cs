using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCardScript : MonoBehaviour
{
    public GameObject selectedCard;
    public GameObject selectedDish;
    public CardScriptableObject selectedCardScriptable;

    public void SetSelectedCard(GameObject card)
    {
        selectedCard = card;
        selectedCardScriptable = selectedCard.GetComponent<CardScript>().cardScriptable;
    }

    public void SetSelectedDish(GameObject dish)
    {
        selectedDish = dish;
    }
    
}
