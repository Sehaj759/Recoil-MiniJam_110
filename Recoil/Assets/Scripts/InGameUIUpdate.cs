using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIUpdate : MonoBehaviour
{
    [SerializeField] Player player;

    [SerializeField] Text bulletCountText;

    void Update()
    {
        SetBulletCountText();    
    }

    void SetBulletCountText()
    {
        bulletCountText.text = player.BulletCount.ToString();
    }
}
