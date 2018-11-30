using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private GameObject[] collectables;
    private int collectablesLeft;
    [SerializeField] private string collectableTag;
    [SerializeField] private door door;

    void Start ()
    {
        checkCollectables();
    }
    public void checkCollectables()
    {
        collectables = GameObject.FindGameObjectsWithTag(collectableTag);
        collectablesLeft = collectables.Length;
        if (collectablesLeft <= 0)
        {
            door.open();
        }
    }
}
