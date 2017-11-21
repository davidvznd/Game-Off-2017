using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public string ItemName;
    public int Value = 1;
    public int positionX;
    public int positionY;

    void ItemPickedUp() {
        switch (Value)
        {
            case 1:
                Debug.Log("Health Up");
                break;
            case 2:
                Debug.Log("Attack Up");
                break;
            case 3:
                Debug.Log("Defence up");
                break;
            case 4:
                Debug.Log("Speed Up");
                break;
            case 5:
                Debug.Log("Accuracy Up");
                break;
        }
            
     }       

}
