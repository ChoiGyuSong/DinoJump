using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankDisplay : MonoBehaviour
{
    public TextMeshProUGUI[] nameTexts; // �ν����Ϳ��� �Ҵ� (��ŷ �̸� �ؽ�Ʈ �迭)
    public TextMeshProUGUI[] timeTexts; // �ν����Ϳ��� �Ҵ� (��ŷ �ð� �ؽ�Ʈ �迭)
    RankingManager rankingManager; // �ν����Ϳ��� �Ҵ�

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
        // ��ŷ�� �ִ� �������� ǥ���մϴ�.
        for (int i = 0; i < rankingManager.highScores.Count && i < nameTexts.Length && i < timeTexts.Length; i++)
        {
            nameTexts[i].text = rankingManager.highScores[i].playerName;
            timeTexts[i].text = rankingManager.highScores[i].survivalTime.ToString("N2") + "s";
        }

        // ������ ��ŷ �ؽ�Ʈ�� �ʱ�ȭ�մϴ�.
        for (int i = rankingManager.highScores.Count; i < nameTexts.Length && i < timeTexts.Length; i++)
        {
            nameTexts[i].text = "";
            timeTexts[i].text = "";
        }
    }
}