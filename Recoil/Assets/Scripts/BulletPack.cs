using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPack : MonoBehaviour
{
    // one player refernce shared by all BulletPacks
    static Player player = null;

    int bulletCount = 3;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.Find("Player").GetComponent<Player>();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.AddBullets(bulletCount);
            Destroy(gameObject);
        }
    }
}
