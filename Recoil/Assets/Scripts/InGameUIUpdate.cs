using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InGameUIUpdate : MonoBehaviour
{
    [SerializeField] Player player;

    [SerializeField] TextMeshProUGUI bulletCountText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject hitPointsParent;
    [SerializeField] GameObject hitPointPrefab;

    GameObject[] hitPoints; 

    uint curSecondsPassed = 0;
    int hitPointsRemaining;

    void Start()
    {
        hitPoints = new GameObject[player.MaxHitPoints];
        hitPointsRemaining = hitPoints.Length;
        SetHitPointImages();
        StartCoroutine(Timer());    
    }

    void Update()
    {
        SetBulletCountText();
        if (hitPointsRemaining > 0 && player.HitPointsRemaining < hitPointsRemaining)
        {
            hitPoints[hitPointsRemaining - 1].SetActive(false);
            hitPointsRemaining--;

            if(hitPointsRemaining <= 0)
            {
                gameOver.SetActive(true);
                StopCoroutine(Timer());
            }
        }
    }

    void SetBulletCountText()
    {
        bulletCountText.SetText(player.BulletCount.ToString());
    }

    void SetHitPointImages()
    {
        float posX = -115.0f;
        for(int i = 0; i < hitPoints.Length; ++i)
        {
            GameObject hitPoint = Instantiate(hitPointPrefab);
            hitPoint.transform.SetParent(hitPointsParent.transform);
            RectTransform hitPointRect = hitPoint.GetComponent<RectTransform>();
            if (hitPointRect)
            {
                hitPointRect.anchoredPosition = Vector2.left;
                hitPointRect.localPosition = new Vector3(posX, 0, 0);
                posX += (hitPointRect.sizeDelta.x - 45.0f);
            }
            hitPoints[i] = hitPoint;
        }
    }

    IEnumerator Timer()
    {
        while (!player.GameOver)
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

    public void Restart()
    {
        player.Reset();
        hitPointsRemaining = hitPoints.Length;
        curSecondsPassed = 0;
        timerText.SetText("00:00");
        foreach (GameObject hitPoint in hitPoints)
        {
            hitPoint.SetActive(true);
        }
        gameOver.SetActive(false);
        StartCoroutine(Timer());
    }
}
