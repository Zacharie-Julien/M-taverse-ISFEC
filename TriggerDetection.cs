using UnityEngine;

public class TriggerDetection : MonoBehaviour
{
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            print("Joueur trouvé");
            // player.transform.translate
        }
    }
}
