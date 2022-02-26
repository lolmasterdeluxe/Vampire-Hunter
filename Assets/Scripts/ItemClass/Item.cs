using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Author: Ho Junliang 
 * Created: 15/2/2022
 */

// This is the base class for all items
public abstract class Item : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite itemImage;

    public GameObject model3D;
}
