using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField] PlayerManager player;
    private bool open = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player" && player.gotKey && !open)
        {
            GetComponent<Animation>().Play("openDoor");
            open = true;
            gameObject.GetComponent<BoxCollider>().isTrigger = false;
        }
    }
}
