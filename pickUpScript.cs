using UnityEngine;

public class pickUpScript : MonoBehaviour
{
    RaycastHit hit;
    GameObject lastHit;
    public GameObject holdPosition;

    public void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 10f, Color.blue);

        if (Physics.Raycast(transform.position, transform.forward, out hit, 10f) && hit.transform.gameObject.CompareTag("isGrabbable"))
        {
            lastHit = hit.transform.gameObject;
            hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;

            if (Input.GetKey(KeyCode.Mouse0))
            {
                hit.transform.position = holdPosition.transform.position;
                hit.transform.rotation = holdPosition.transform.rotation;
            }

        }else if (lastHit != null)
        {
            lastHit.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }
}
