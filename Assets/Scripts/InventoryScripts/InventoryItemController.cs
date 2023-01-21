using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    #region Exposed

    [SerializeField] Item item;
    [SerializeField] Button RemoveButton;

    #endregion

    
    #region Methods

    public void RemoveItem()
    {
        InventoryManager.Instance.RemoveItem(item);
        Destroy(gameObject);
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
    }

    #endregion

    #region Private & Protected


    #endregion
}
