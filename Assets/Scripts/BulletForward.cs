using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletForward : MonoBehaviour
{
    public float mForwardSpeed = 15.0f;
    public float mTimeToLive = 1000.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * mForwardSpeed * Time.deltaTime);

        mTimeToLive -= Time.deltaTime;

        if(mTimeToLive < 0 ) GameObject.Destroy(gameObject);
    }
}
