
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool PlayerInRange;
    public string ItemName;
    public Player player;
    public int itemPoints;


    private void Start()
    {
        if (player == null)
        {
            player = FindFirstObjectByType(typeof(Player)).GetComponent<Player>();
        }
    }
    public string GetItemName()
    {
        return ItemName;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && PlayerInRange)
        {
            Debug.Log(ItemName + " Collected");
            Destroy(gameObject.transform.parent.gameObject);
            player.take_item(itemPoints);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInRange = false;
        }
    }
}