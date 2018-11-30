using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTarget : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject turret;
    [SerializeField] private int enemyrange;
    [SerializeField] private float firerate;
    [SerializeField] private int recoil;
    [SerializeField] private float projectilespeed;
    private RaycastHit hit;
    private Vector3 direction;
    private float nextFire;
    private TimerClass timer;
    private bool stuned;

    private void Start()
    {
        timer = gameObject.AddComponent<TimerClass>();
        timer.TimerCompleted += removestun;
    }
    void Update ()
    {
        if (!stuned)
        {
            lineOfSight();
        }
    }
    private void lineOfSight()
    {
        direction = player.transform.position - transform.position;

        if (Physics.Raycast(transform.position, direction, out hit, enemyrange))
        {
            if (hit.collider.gameObject.name == "Player")
            {
                shoot();
            }
        }
    }
    private void shoot()
    {
        turret.transform.LookAt(new Vector3(player.transform.position.x, turret.transform.position.y, player.transform.position.z));
        if (Time.time >= nextFire)
        {
            nextFire = Time.time + firerate;
            GameObject bullet = Instantiate(projectile, turret.transform.position + (direction.normalized/2), turret.transform.rotation);
            bullet.GetComponent<Rigidbody>().velocity = direction.normalized * projectilespeed;
        }
    }
    public void stun()
    {
        timer.StartTimer(10);
        stuned = true;
    }
    private void removestun()
    {
        stuned = false;
    }
}
