using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    private RaycastHit hit;
    private Vector3 mouseposition;
    private Vector3 movedirection;
    private Vector3 lastFrameVelocity;
    private Rigidbody rigidbody;
    [SerializeField] private float maxforce;
    [SerializeField] private float minforce;
    [SerializeField] private GameObject arrow;
    private float force;
    private bool ismoving;
    

    //movedirection is inverted

    void Start ()
    {
        rigidbody = GetComponent<Rigidbody>();
	}
	void Update ()
    {
        lastFrameVelocity = rigidbody.velocity;
        if (ismoving && rigidbody.velocity.magnitude <= 0.1)
        {
            ismoving = false;
        }
        if (Input.GetMouseButton(0) && !ismoving)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                mouseposition = hit.point;
                movedirection = transform.position - mouseposition;
                movedirection.y = 0;
                SetForce(movedirection.x, movedirection.z);
                setArrow(mouseposition.x, mouseposition.z);
            }
        }
        if (Input.GetMouseButtonUp(0) && !ismoving)
        {
            RemoveArrow();        
            move(movedirection);
        }
    }
    void move(Vector3 direction)
    {
        ismoving = true;
        rigidbody.AddForce(direction.normalized * force, ForceMode.Impulse);
    }
    private void setArrow(float x, float y)
    {
        arrow.transform.LookAt(mouseposition);
        Vector3 arrowrotation = arrow.transform.rotation.eulerAngles;
        arrowrotation.x = 0;
        arrow.transform.rotation = Quaternion.Euler(arrowrotation);
        arrow.transform.localScale = new Vector3(0.2f, 0.2f,force /80);
        arrow.SetActive(true);
    }
    private void RemoveArrow()
    {
        arrow.SetActive(false);
    }
    private void SetForce(float x,float y)
    {
        force = (Mathf.Abs(x) + Mathf.Abs(y)*4);
        if (force < minforce)
        {
            force = minforce;
        }
        else if (force > maxforce)
        {
            force = maxforce;
        }
    }
    public void BounceOffWall(Vector3 normal)//zorgen dat de ball bounced 
    {
        rigidbody.velocity =  Vector3.Reflect(lastFrameVelocity, normal);
    }
}
