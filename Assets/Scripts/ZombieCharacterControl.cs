using System;
using UnityEditor;
using UnityEngine;

public class ZombieCharacterControl : MonoBehaviour
{

    [SerializeField] private float m_moveSpeed = 2;
    [SerializeField] private float m_turnSpeed = 1;

    [SerializeField] private Animator m_animator = null;
    [SerializeField] private Rigidbody m_rigidBody = null;

    private bool shouldMove = false;
    private double distanceToFocus = 15.0;

    public int health = 50;

    Transform target; 

    private Vector3 m_currentDirection = Vector3.zero;

    private void Awake()
    {
        if (!m_animator) { gameObject.GetComponent<Animator>(); }
        if (!m_rigidBody) { gameObject.GetComponent<Animator>(); }
    }

    private void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        UpdateShouldMove();
        Move();

        if(health <= 0) GameObject.Destroy(gameObject);
    }

    private void UpdateShouldMove()
    {
        if (!shouldMove && distance3D(target.position, transform.position) < distanceToFocus)
        {
            shouldMove = true;
        }
        else if( shouldMove && distance3D(target.position, transform.position)  > 2*distanceToFocus)
        {
            shouldMove = false;
        }
    }

    private void Move()
    {
        if (shouldMove)
        {
            Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, m_turnSpeed * Time.deltaTime);
            transform.position += transform.forward * m_moveSpeed * Time.deltaTime;
        }
    }


    private double distance3D(Vector3 posA, Vector3 posB)
    {
        return Math.Sqrt( Math.Pow(posB[0] - posA[0], 2) + Math.Pow(posB[1] - posA[1], 2) + Math.Pow(posB[2] - posA[2], 2) );
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Bullet(Clone)")
        {
            health -= 10;
            Destroy(other.gameObject);
        }
    }
}
