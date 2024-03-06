using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankDisplay : MonoBehaviour
{
    public TextMeshProUGUI[] nameTexts; // 인스펙터에서 할당 (랭킹 이름 텍스트 배열)
    public TextMeshProUGUI[] timeTexts; // 인스펙터에서 할당 (랭킹 시간 텍스트 배열)
    RankingManager rankingManager; // 인스펙터에서 할당

    private void Awake()
    {
        nameTexts = new TextMeshProUGUI[5];
        timeTexts = new TextMeshProUGUI[5];

        rankingManager = FindObjectOfType<RankingManager>();
        Canvas canvas = FindObjectOfType<Canvas>();
        Transform transform = canvas.transform;
        Transform rank = transform.GetChild(1);

        for (int i = 0; i < 5; i++)
        {
            Transform num = rank.GetChild(i);
            Transform pick = num.GetChild(1);
            nameTexts[i] = pick.transform.GetComponent<TextMeshProUGUI>();
            pick = num.GetChild(2);
            timeTexts[i] = pick.transform.GetComponent<TextMeshProUGUI>();
        }
    }

    public void DisplayRanking()
    {
        // 랭킹을 최대 순위까지 표시합니다.
        for (int i = 0; i < rankingManager.highScores.Count && i < nameTexts.Length && i < timeTexts.Length; i++)
        {
            nameTexts[i].text = rankingManager.highScores[i].playerName;
            timeTexts[i].text = rankingManager.highScores[i].survivalTime.ToString("N2") + "s";
        }

        // 나머지 랭킹 텍스트를 초기화합니다.
        for (int i = rankingManager.highScores.Count; i < nameTexts.Length && i < timeTexts.Length; i++)
        {
            nameTexts[i].text = "";
            timeTexts[i].text = "";
        }
    }
}