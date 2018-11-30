using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private int health;
    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }
}
