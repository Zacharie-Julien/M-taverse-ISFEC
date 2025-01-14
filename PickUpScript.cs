using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    public GameObject player;
    public Transform holdPos;
    public float throwForce = 500f;
    public float pickUpRange = 50f;
    private GameObject heldObj;
    private Rigidbody heldObjRb;
    private bool canDrop = true;
    private int LayerNumber;

    private GameObject highlightedObj;
    private Material originalMaterial;
    public Material highlightMaterial;

    void Start()
    {
    }

    void Update()
    {
        HighlightObject();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObj == null)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
                {
                    if (hit.transform.gameObject.tag == "canPickUp")
                    {
                        PickUpObject(hit.transform.gameObject);
                    }
                }
            }
            else
            {
                if (canDrop == true)
                {
                    StopClipping();
                    DropObject();
                }
            }
        }

        if (heldObj != null)
        {
            MoveObject();
            if (Input.GetKeyDown(KeyCode.Mouse0) && canDrop == true)
            {
                StopClipping();
                ThrowObject();
            }
        }
    }

    void HighlightObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
        {
            GameObject obj = hit.transform.gameObject;

            if (obj.tag == "canPickUp")
            {
                if (highlightedObj != obj)
                {
                    ClearHighlight();
                    highlightedObj = obj;

                    Renderer objRenderer = highlightedObj.GetComponent<Renderer>();
                    if (objRenderer != null)
                    {
                        originalMaterial = objRenderer.material;
                        objRenderer.material = highlightMaterial;
                    }
                }
                return;
            }
        }
        ClearHighlight();
    }

    void ClearHighlight()
    {
        if (highlightedObj != null)
        {
            Renderer objRenderer = highlightedObj.GetComponent<Renderer>();
            if (objRenderer != null && originalMaterial != null)
            {
                objRenderer.material = originalMaterial;
            }
            highlightedObj = null;
        }
    }

    void PickUpObject(GameObject pickUpObj)
    {
        ClearHighlight();
        if (pickUpObj.GetComponent<Rigidbody>())
        {
            heldObj = pickUpObj;
            heldObjRb = pickUpObj.GetComponent<Rigidbody>();
            heldObjRb.isKinematic = true;
            heldObjRb.transform.parent = holdPos.transform;
            heldObj.layer = LayerNumber;
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        }
    }

    void DropObject()
    {
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0;
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null;
        heldObj = null;
    }

    void MoveObject()
    {
        heldObj.transform.position = holdPos.transform.position;
    }

    void ThrowObject()
    {
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0;
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null;
        heldObjRb.AddForce(transform.forward * throwForce);
        heldObj = null;
    }

    void StopClipping()
    {
        var clipRange = Vector3.Distance(heldObj.transform.position, transform.position);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), clipRange);
        if (hits.Length > 1)
        {
            heldObj.transform.position = transform.position + new Vector3(0f, -0.5f, 0f);
        }
    }
}
