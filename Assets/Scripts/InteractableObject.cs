
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
        time -= Time.deltaTime;

        if (time <= 0.0f)
        {
            time = ini_time;
            res();
        }
    }

    void res()
    {
        Transform parent = transform.parent;
        if (parent != null)
        {
            // Generate a random position
            float x = 0;
            float z = 0;
            if (player.lvl == 1)
            {
                x = UnityEngine.Random.Range(100f, 390f);
                z = UnityEngine.Random.Range(60f, 160f);
            }
            else if (player.lvl == 2)
            {
                x = UnityEngine.Random.Range(160f, 335f);
                z = UnityEngine.Random.Range(250f, 320f);
            }
            else if (player.lvl == 3)
            {
                x = UnityEngine.Random.Range(185f, 320f);
                z = UnityEngine.Random.Range(390f, 480f);
            }
            Vector3 randomPos = new Vector3(
                    x,  // X 
                    (player.transform.position.y + 20),  // Y 
                    z   // Z 
            );

            // Move the parent to that position
            parent.position = randomPos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.take_item(itemPoints);
            res();
        }
    }
}