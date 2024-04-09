using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int health = 100;
    private bool hitable = true;
    private float cooldown = 2.0f;
    public bool gotKey = false;

    // Update is called once per frame
    void Update()
    {
        HitCooldown();
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void HitCooldown()
    {
        if (!hitable)
        {
            cooldown -= Time.deltaTime;
            if (cooldown < 0)
            {
                hitable = true;
                cooldown = 1.5f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Zombie" && hitable)
        {
            health -= 20;
            hitable = false;
        }
    }
}
