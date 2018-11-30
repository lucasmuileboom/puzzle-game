using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    private BoxCollider col;

	void Start ()
    {
        col = GetComponent<BoxCollider>();
	}
    public void open()
    {
        col.enabled = true;
    }
}
