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
    private bool targetingPlater = false;
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
        }
        if (coolDownTime <= 0.0f)
        {
            onCoolDown = false;
        }

        if (transform.position == target.position)
        {
            random_target();
        }

        transform.LookAt(target);
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed* Time.deltaTime);
    }

    void random_target()
    {
        if (!targetingPlater)
        {
            int randomNumber = UnityEngine.Random.Range(0, targets.Length);
            target = targets[randomNumber];
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !onCoolDown)
        {
            targetingPlater = true;
            target = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetingPlater = false;
            random_target();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player otherScript = collision.gameObject.GetComponent<Player>();
            if (otherScript != null)
            {
                otherScript.take_damage(damagePoints);
                onCoolDown = true;
                targetingPlater = false;
                random_target();
            }
        }
    }
}


