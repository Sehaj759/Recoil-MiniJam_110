using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // one player refernce shared by all Enemies
    static Player player = null;

    [SerializeField] Rigidbody2D rb;
    float movementSpeed = 20.0f;

    [SerializeField] GameObject bulletPackPrefab;
    float bulletPackDropChance = 0.55f;

    void Start()
    {
        if(player == null)
        {
            player = GameObject.Find("Player").GetComponent<Player>();
        }
    }

    void FixedUpdate()
    {
        FollowPlayer(Time.fixedDeltaTime);
    }

    public void Hit()
    {
        if (Random.Range(0.0f, 1.0f) <= bulletPackDropChance)
        {
            Instantiate(bulletPackPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    void FollowPlayer(float deltaTime)
    {
        Vector2 playerDir = (Vector2)(player.transform.position) - rb.position;
        rb.velocity = Vector2.zero;
        rb.AddForce(movementSpeed * playerDir);
    }
}
