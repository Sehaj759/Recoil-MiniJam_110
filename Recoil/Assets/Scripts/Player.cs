using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Camera mainCamera;

    Rigidbody2D rb;

    Vector2 mousePos = Vector2.zero;

    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;
    bool fire = false;
    float bulletForce = 50.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    void Update()
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - rb.position;

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
            rb.AddForce(bulletForce * (rb.position - mousePos));
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Destroy(bullet, 2.0f);
        }
    }
}
