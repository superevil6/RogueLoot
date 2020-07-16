using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickItemPanel : MonoBehaviour
{
    public Item Item;
    public Text ItemName;
    public Text ItemDescription;
    public Image ItemImage;
    // Start is called before the first frame update
    void Start()
    {


    }
    private void OnEnable()
    {
        Item = GetComponentInParent<Pickup>().Item;
        ItemName.text = Item.Name;
        ItemDescription.text = Item.Description;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
