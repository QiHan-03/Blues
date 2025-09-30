
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public bool PlayerInRange;
    public string Message;
    public Player player;
    public int BreakLVL;


    private void Start()
    {
        if (player == null)
        {
            player = FindFirstObjectByType(typeof(Player)).GetComponent<Player>();
        }
    }

    public string GetMessage()
    {
        return Message;
    }

    private void Update()
    {
        if (player.lvl >= BreakLVL)
        {
            Debug.Log("Barrier Broken");
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