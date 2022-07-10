using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameUIUpdate : MonoBehaviour
{
    [SerializeField] Player player;

    [SerializeField] TextMeshProUGUI bulletCountText;
    [SerializeField] TextMeshProUGUI timerText;

    uint curSecondsPassed = 0;

    void Start()
    {
        StartCoroutine(Timer());    
    }

    void Update()
    {
        SetBulletCountText();    
    }

    void SetBulletCountText()
    {
        bulletCountText.SetText(player.BulletCount.ToString());
    }

    IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            curSecondsPassed++;

            string minutes;
            string seconds;
            CurSecondsToTimeText(out minutes, out seconds);

            timerText.SetText(minutes + ":" + seconds);
        }
    }

    void CurSecondsToTimeText(out string minutes, out string seconds)
    {
        uint _seconds = curSecondsPassed % 60;
        uint _minutes = curSecondsPassed / 60;

        if (_seconds < 10)
            seconds = "0" + _seconds.ToString();
        else
            seconds = _seconds.ToString();

        if (_minutes < 10)
            minutes = "0" + _minutes.ToString();
        else
            minutes = _minutes.ToString();
    }
}
