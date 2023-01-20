using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{

    public static InventoryManager Instance;
    [SerializeField] List<Item> Items = new List<Item>();
    [SerializeField] Transform ItemContent;
    [SerializeField] GameObject InventoryItem;
    #region Exposed

    

    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    #endregion

    #region Methods

    public void AddItem(Item item)
    {
        Items.Add(item);
    }

    public void RemoveItem(Item item) 
    { 
        Items.Remove(item);
    }

    public void ListItems()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }


        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            itemName.text = item.ItemName;
            itemIcon.sprite = item.Icon;
        }
    }

    #endregion

    #region Private & Protected

    

    #endregion
}
