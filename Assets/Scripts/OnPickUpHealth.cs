using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPickUpHealth : MonoBehaviour
{
    int HealthValue = 10;
    public PlayerManager player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerManager>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == player.gameObject.name)
        {
            player.health += HealthValue;
            GameObject.Destroy(gameObject);
        }
    }
}
