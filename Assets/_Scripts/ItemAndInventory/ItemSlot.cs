using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    [Header("UI References")]
    [SerializeField]public Image image;
    [SerializeField]public TextMeshProUGUI nameText;  
    [SerializeField]public TextMeshProUGUI descriptionText; 
    public static event System.Action<ItemSO> onItemClicked;
    [Header("Item Data")]
    [SerializeField] private ItemSO _itemSO; 

    [SerializeField]public virtual ItemSO itemSO
    {
        get {return _itemSO;}

        set
        {
            _itemSO = value;
            if(_itemSO != null)
            {
                image.sprite = _itemSO.itemIcon;
                nameText.text = _itemSO.itemName;
                descriptionText.text = _itemSO.description;
                image.enabled = true;
            }
            else
            {
                image.sprite = null;
                nameText.text = "";
                descriptionText.text = "";
                image.enabled = false;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(itemSO != null)
        {
            if(itemSO != null)
            {
                Debug.Log($"Invoking onItemClicked with {itemSO.itemName}");
                onItemClicked?.Invoke(itemSO);
            }
        }
    }
}
