using UnityEngine;
public class TriggerZone : MonoBehaviour
{

    public void OnTriggerEnter(Collider col)
    {
        col.transform.Translate(new Vector3(0f, 0f, 22f));
    }

}
