using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameUIUpdate : MonoBehaviour
{
    [SerializeField] Player player;

    [SerializeField] TextMeshProUGUI bulletCountText;

    void Update()
    {
        SetBulletCountText();    
    }

    void SetBulletCountText()
    {
        bulletCountText.SetText(player.BulletCount.ToString());
    }
}
