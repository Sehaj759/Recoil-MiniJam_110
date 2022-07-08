using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Camera mainCamera;

    Rigidbody2D rb;

    Vector2 lookDir = Vector2.zero;

    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;
    bool fire = false;
    float bulletForce = 50.0f;
    float maxVelocityMagnitude = 9.0f;
    float maxVelocityMagnitudeSq;

    int maxBullets = 75;
    int curBullets;

    int maxHitPoints = 4;
    int curHitPoints;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
        
        curBullets = maxBullets;
        curHitPoints = maxHitPoints;

        maxVelocityMagnitudeSq = maxVelocityMagnitude * maxVelocityMagnitude;
    }

    void Update()
    {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        lookDir = mousePos - rb.position;

        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg);

        if (Input.GetButtonDown("Fire1"))
        {
            fire = true;
        }
    }

    void FixedUpdate()
    {
        Fire();
    }

    void Fire()
    {
        if (fire)
        {
            fire = false;
            if (curBullets > 0)
            {
                curBullets--;
                rb.AddForce(bulletForce * (-lookDir));
                if (rb.velocity.sqrMagnitude >= maxVelocityMagnitudeSq)
                {
                    rb.velocity = maxVelocityMagnitude * rb.velocity.normalized;
                }
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
                Rigidbody2D bulletRigidBody = bullet.GetComponent<Rigidbody2D>();
                bulletRigidBody.AddForce(bulletForce * lookDir);
                Destroy(bullet, 2.0f);
            }
            Debug.Log("Cur Bullets: " + curBullets.ToString());
        }
    }
}
