using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBlackboard : MonoBehaviour
{
    public bool InsideShop { get; set; }
    public bool InCounter { get; set; }
    public bool PickedCoffee { get; set; }
    public bool PurchasedCoffee { get; set; }
    public bool GotCoffee { get; set; }

    public CoffeeType SelectedCoffee { get; set; }
    public Vector3 ShopEnterPosition { get; set; }
    public Vector3 ShopExitPosition { get; set; }
}
