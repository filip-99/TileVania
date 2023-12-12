using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goat : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void InstantiateBullet()
    {
        Instantiate(bullet, gun.position, transform.rotation);
    }
}
