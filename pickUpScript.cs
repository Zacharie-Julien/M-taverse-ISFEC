using UnityEngine;
using Fusion;
using Unity.VisualScripting;

public class pickUpScript : NetworkBehaviour
{
    RaycastHit hit;
    GameObject lastHit;
    public GameObject holdPosition;
    public bool isHit = false;

    public void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 10f, Color.blue);

        if (Physics.Raycast(transform.position, transform.forward, out hit, 10f) && hit.transform.gameObject.CompareTag("isGrabbable"))
        {
            lastHit = hit.transform.gameObject;
            hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;

            if (Input.GetKey(KeyCode.Mouse0))
            {
                isHit = true;
            }else
            {
                isHit = false;
            }

        }else if (lastHit != null)
        {
            lastHit.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    public override void FixedUpdateNetwork()
    {   
        if (isHit && hit.transform.gameObject.CompareTag("isGrabbable"))
        {
            hit.transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            hit.transform.position = holdPosition.transform.position;
            hit.transform.rotation = holdPosition.transform.rotation;    
        } else
        {
            if (lastHit != null)
            {
                lastHit.transform.gameObject.GetComponent<Rigidbody>().isKinematic = false;    
            }
        }
    }
}
