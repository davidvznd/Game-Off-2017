using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public string ItemName;
    public GameObject PlayerRef;
    public int Value;
    public int positionX;
    public int positionY;
    public bool isActive;
    public GameMap Map;
    Color currColor;

    void Start() {
        Map = GameObject.Find("Map").GetComponent<GameMap>();
        PlayerRef = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(PlayerRef);
        Value = Random.Range(1, 4);
        isActive = true;
    }

    public void ItemPickedUp() {
        Value = Map.levelvalue;
        switch (Value)
        {
            case 1:
                Debug.Log("Health Up");
                PlayerRef.GetComponent<Player>().playerfeedback.text = "You heal for " + Value + " health";
                PlayerRef.GetComponent<Player>().health += Value;

                currColor = this.GetComponent<SpriteRenderer>().color;
                currColor.a = 0;
                this.GetComponent<SpriteRenderer>().color = currColor;

                break;
            case 2:
                Debug.Log("Attack Up");
                PlayerRef.GetComponent<Player>().playerfeedback.text = "You gain " + Value + " attack";
                PlayerRef.GetComponent<Player>().attack += Value;

                currColor = this.GetComponent<SpriteRenderer>().color;
                currColor.a = 0;
                this.GetComponent<SpriteRenderer>().color = currColor;

                break;
            case 3:
                Debug.Log("There was nothing inside");
                PlayerRef.GetComponent<Player>().playerfeedback.text = "There's nothing inside";

                currColor = this.GetComponent<SpriteRenderer>().color;
                currColor.a = 0;
                this.GetComponent<SpriteRenderer>().color = currColor;

                break;
            case 4:
                Debug.Log("There was nothing inside");
                PlayerRef.GetComponent<Player>().playerfeedback.text = "There's nothing inside";

                currColor = this.GetComponent<SpriteRenderer>().color;
                currColor.a = 0;
                this.GetComponent<SpriteRenderer>().color = currColor;

                break;
        }
            
     }       

}

