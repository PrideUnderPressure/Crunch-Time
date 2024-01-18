using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
    public GameObject cardFront;
    public GameObject cardImage;
    public List<CardFlavour> cardFlavours;
    public CardScriptableObject cardScriptable;
    
    public TMP_Text cardName;
    public TMP_Text cardValue;
    public TMP_Text cardText;
    public TMP_Text cardType;

    //STATS
    public string cardID;
    public int cardValueInt;
    public int cardTimeInt;
    public Image timerCircle;
    void Start()
    {

    }
    void Update()
    {
        
    }
    
    public void InitiateCard(CardScriptableObject scriptableObject, CardStorage cardStorage)
    {
        cardScriptable = scriptableObject;
        cardImage.GetComponent<Image>().sprite = scriptableObject.cardSprite;
        cardValueInt = scriptableObject.cardValue;
        cardTimeInt = scriptableObject.cookingTime;
        cardID = scriptableObject.cardID;
        
        int frontNumber = (int)scriptableObject.cardType;
        cardFront.GetComponent<Image>().sprite = cardStorage.cardFronts[frontNumber];
        
        cardName.text = scriptableObject.cardName;
        cardValue.text = scriptableObject.cardValue.ToString();
        cardText.text = scriptableObject.cardText;
        cardType.text = scriptableObject.cardType.ToString();
        
        cardFlavours = scriptableObject.cardFlavours;
        var howManyFlavours = cardFlavours.Count;
        for (int i = 0; i < howManyFlavours; i++)
        {
            var myString = cardFlavours[i].ToString();
            if (cardFlavours[i] != 0)
            {
                AddCardText(myString);
            }
            else 
                break;
        }
        //TIMER
        switch (scriptableObject.cookingTime)
        {
            case 0:
                break;
            case 1:
                timerCircle.fillAmount = 0.25f;
                timerCircle.color = new Color(107f / 255f, 252f / 255f, 0f, 1f);
                break;
            case 2:
                timerCircle.fillAmount = 0.50f;
                timerCircle.color = new Color(255f / 255f, 252f / 255f, 0f, 1f);
                break;
            case 3:
                timerCircle.fillAmount = 0.75f;
                timerCircle.color = new Color(255f / 255f, 131f / 255f, 0f, 1f);
                break;
            case 4:
                timerCircle.fillAmount = 1f;
                timerCircle.color = new Color(255f / 255f, 0f, 20f / 255f, 1f);
                break;
        }

        cardTimeInt = scriptableObject.cookingTime;
    }

    public void AddCardText(string textToAdd)
    {
        cardText.text = cardText.text + ("\n") + textToAdd;
    }

    public void TakeDamage(int damageTaken)
    {
        cardValueInt -= damageTaken;
        if (cardValueInt <= 0)
        {
            Death();
        }
        else
            cardValue.text = cardValueInt.ToString();
    }

    public void Death()
    {
        
    }

}
