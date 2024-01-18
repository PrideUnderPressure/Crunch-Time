using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DishCombination
{
    public Sprite dishImage;
    public string dishName;
    public string cardID1;
    public string cardID2;

    public DishCombination(Sprite image, string name, string id1, string id2)
    {
        dishImage = image;
        dishName = name;
        cardID1 = id1;
        cardID2 = id2;
    }
}
public class CardCombinationsList : MonoBehaviour
{
    
    public List<DishCombination> cardCombinations = new List<DishCombination>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
