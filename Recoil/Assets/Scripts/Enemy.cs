using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // one player refernce shared by all Enemies
    static Player player = null;
    static float movementSpeed = 350.0f;
    static float hitRecoilForce = 200.0f;

    [SerializeField] Rigidbody2D rb;

    [SerializeField] GameObject bulletPackPrefab;
    float bulletPackDropChance = 0.55f;

    Vector2 playerDir;

    void Start()
    {
        if(player == null)
        {
            player = GameObject.Find("Player").GetComponent<Player>();
        }
    }

    void Update()
    {
        if (player.GameOver)
        {
            Destroy(gameObject);
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
        playerDir = (Vector2)(player.transform.position) - rb.position;
        playerDir.Normalize();
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(playerDir.y, playerDir.x) * Mathf.Rad2Deg);
        rb.velocity = Vector2.zero;
        rb.AddForce(movementSpeed * playerDir);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.Hit(playerDir, hitRecoilForce);
            rb.velocity = Vector2.zero;
            rb.AddForce(hitRecoilForce * (-playerDir));
        }
    }
}
