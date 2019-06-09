using UnityEngine;

public class InventoryUI : MonoBehaviour {

    public Transform itemsParent;
    public GameObject inventoryUI;

    private Inventory inventory;
    private InventorySlot[] slots;
    private PlayerInputController input;

	// Use this for initialization
	void Start () {

        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        input = PlayerManager.instance.player.GetComponent<PlayerInputController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(input.Current.InventoryInput)
        {
            Debug.Log("inventory opening");
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
	}

    void UpdateUI()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if(i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].clearSlot();
            }
        }
    
    }
}
