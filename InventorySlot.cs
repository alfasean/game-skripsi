// InventorySlot.cs
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public string itemName; // Nama item
    public Sprite itemIcon; // Icon item
    public int quantity; // Jumlah item

    // Fungsi untuk mengatur item slot
    // Fungsi untuk mengatur item slot
public void SetItem(string name, Sprite icon, int qty)
{
    itemName = name;
    itemIcon = icon;
    quantity = qty;

    // Atur tampilan item di slot
    // GetComponent<Image>().sprite = itemIcon;
    // Atur teks item name dan quantity
    transform.Find("Icon").GetComponent<Image>().sprite = itemIcon; // Set icon sprite
    transform.Find("Name").GetComponent<Text>().text = itemName;
    transform.Find("Quantity").GetComponent<Text>().text = quantity.ToString();
}

    // Fungsi untuk menambah jumlah item
    public void AddQuantity(int qty)
    {
         quantity += qty;
        // Perbarui tampilan jumlah item di slot
        transform.Find("Quantity").GetComponent<Text>().text = quantity.ToString();
    }
}
