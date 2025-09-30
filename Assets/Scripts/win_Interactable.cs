
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class win_Interactable : MonoBehaviour
{
    public bool PlayerInRange;
    public string ItemName;
    public Player player;
    public int itemPoints;
    public float time = 20;
    public float ini_time = 20;


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
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && player.hp > 100)
        {
            player.take_item(player.hp);
        }
    }
}