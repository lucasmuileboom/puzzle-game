using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercollider : MonoBehaviour
{
    [SerializeField] private Collectable collectable;
    private Health health;
    private movement movement;
    private TimerClass timer;

    private void Start()
    {
        health = GetComponent<Health>();
        movement = GetComponent<movement>();
        timer = gameObject.AddComponent<TimerClass>();
        timer.TimerCompleted += collectableDelay;
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "bullet")
        {
            health.TakeDamage(1);
            Destroy(col.gameObject);
        }
        else if (col.gameObject.tag == "bounce")
        {
            movement.BounceOffWall(col.contacts[0].normal);
            if (col.gameObject.name == "BigEnemy")
            {
                col.gameObject.GetComponent<EnemyTarget>().stun();
            }
        }
        else if(col.gameObject.tag == "Collectable")
        {
            Destroy(col.gameObject);
            timer.StartTimer(0.0001f);
        }
        else if (col.gameObject.tag == "SmallEnemy")
        {
            Destroy(col.gameObject);
        }
        else if (col.gameObject.tag == "death")
        {
            health.TakeDamage(100);
        }
        else if (col.gameObject.tag == "door")
        {
            print("your win");
        }
    }
    private void collectableDelay()
    {
        collectable.checkCollectables();
    }
}
