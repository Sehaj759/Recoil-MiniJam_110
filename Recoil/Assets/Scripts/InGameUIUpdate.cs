using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InGameUIUpdate : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] EnemySpawnner enemySpawnner;

    [SerializeField] TextMeshProUGUI bulletCountText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject disableOnGameOverUI;
    [SerializeField] TextMeshProUGUI finalTimerText;
    [SerializeField] Image pauseButtonImage;


    [SerializeField] GameObject hitPointsParent;
    [SerializeField] GameObject hitPointPrefab;

    [SerializeField] Sprite pauseImage;
    [SerializeField] Sprite resumeImage;

    GameObject[] hitPoints; 

    uint curSecondsPassed = 0;
    int hitPointsRemaining;

    bool isPaused = false;
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
                string minutes;
                string seconds;
                CurSecondsToTimeText(out minutes, out seconds);

                finalTimerText.SetText(minutes + ":" + seconds);
                disableOnGameOverUI.SetActive(false);
                gameOverUI.SetActive(true);
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
        player.Restart();
        hitPointsRemaining = hitPoints.Length;
        curSecondsPassed = 0;
        timerText.SetText("00:00");
        foreach (GameObject hitPoint in hitPoints)
        {
            hitPoint.SetActive(true);
        }
        disableOnGameOverUI.SetActive(true);
        gameOverUI.SetActive(false);
        StartCoroutine(Timer());
        enemySpawnner.Restart();
    }

    public void Pause()
    {
        if (!player.GameOver)
        {
            isPaused = !isPaused;

            if (isPaused)
            {
                Time.timeScale = 0;
                pauseButtonImage.sprite = resumeImage;
            }
            else
            {
                Time.timeScale = 1;
                pauseButtonImage.sprite = pauseImage;
            }
        }
    }
}
