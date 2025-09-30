
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool PlayerInRange;

    public string ItemName;

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