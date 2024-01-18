using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public enum CardType
{
    Meat = 0,
    Vegetable = 1,
    Spice = 2,
}
public enum CardFlavour
{
    NoFlavour = 0,
    Sweet = 1,
    Sour = 2,
    Spicy = 3,
    Salty = 4,
    Bitter = 5,
}
[CreateAssetMenu(fileName = "New Card", menuName = "Card")]

public class CardScriptableObject : ScriptableObject
{
    public string cardID;
    public string cardName;
    public CardType cardType;
    public List<CardFlavour> cardFlavours = new List<CardFlavour>();
    public Sprite cardSprite;
    public string cardText;
    public int cardValue;
    public int cookingTime;

}
