using System;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Transform[] targets;
    public Transform target;
    public float speed;
    public float targetTime;
    public float coolDownTime;
    public float initialTargetTime = 10;
    public float iniCoolDownTime = 10;
    private bool targetingPlayer = false;
    private bool onCoolDown = false;
    public int damagePoints = 999;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetTime = initialTargetTime;
        random_target();
    }

    // Update is called once per frame
    void Update()
    {
        targetTime -= Time.deltaTime;

        if (targetTime <= 0.0f)
        {
            targetTime = initialTargetTime;
            random_target();
        }

        if (onCoolDown)
        {
            coolDownTime -= Time.deltaTime;
            if (coolDownTime <= 0f)
            {
                 onCoolDown = false;
            }
        }

        if (!targetingPlayer && transform.position == target.position)
        {
            random_target();
        }

        transform.LookAt(target);
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed* Time.deltaTime);
    }

    void random_target()
    {
        if (!targetingPlayer)
        {
            int randomNumber = UnityEngine.Random.Range(0, targets.Length);
            Debug.Log("rand target index" + randomNumber);
            target = targets[randomNumber];
            Debug.Log("new target" + target.transform.position);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !onCoolDown)
        {
            targetingPlayer = true;
            target = other.transform;
            Debug.Log("target is player ?" + target);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetingPlayer = false;
            random_target();

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !onCoolDown)
        {
            onCoolDown = true;
            coolDownTime = iniCoolDownTime;
            targetingPlayer = false;
            Debug.Log("target is player but now change it");
            random_target();
            Player otherScript = collision.gameObject.GetComponent<Player>();
            if (otherScript != null)
            {
                Debug.Log("player cauth dmging player");
                otherScript.take_damage(damagePoints);
            }

        }
    }
}