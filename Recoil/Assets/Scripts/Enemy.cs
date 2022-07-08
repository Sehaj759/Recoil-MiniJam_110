using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    static Player player = null;
    void Start()
    {
        if(player == null)
        {
            player = GameObject.Find("Player").GetComponent<Player>();
        }
    }

    void Update()
    {
        
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}
