using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System;

public class RankDisplay : MonoBehaviour
{
    public TextMeshProUGUI[] nameTexts; // �ν����Ϳ��� �Ҵ� (��ŷ �̸� �ؽ�Ʈ �迭)
    public TextMeshProUGUI[] timeTexts; // �ν����Ϳ��� �Ҵ� (��ŷ �ð� �ؽ�Ʈ �迭)
    RankingManager rankingManager; // �ν����Ϳ��� �Ҵ�
    public TMP_InputField inputField;
    public int nameChange = 0;

    private void Awake()
    {
        nameTexts = new TextMeshProUGUI[6];
        timeTexts = new TextMeshProUGUI[5];

        rankingManager = FindObjectOfType<RankingManager>();
        CanvasSet();
    }

    private void Start()
    {
        nameChange = 1;
        CanvasSet();
    }

    private void CanvasSet()
    {
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

        inputField = FindObjectOfType<TMP_InputField>();
    }

    public void DisplayRanking()
    {
        int rankCount = Mathf.Min(5, rankingManager.rankingList.Count);

        Debug.Log(nameChange);
        // ��ŷ�� �ִ� �������� ǥ���մϴ�.
        for (int i = 0; i < rankCount; i++)
        {
            if (nameChange == 0 || nameChange == 1)
            {
                Color color = new Color(0, 0, 0);
                nameTexts[i].color = color;
            }
            nameTexts[i].text = rankingManager.rankingList[i].name;
            timeTexts[i].text = String.Format("{0:N2}", rankingManager.rankingList[i].score) + "s";
        }
        if (rankingManager.isRank < 5)
        {
            Color color = new Color(255, 0, 255);
            nameTexts[rankingManager.isRank].color = color;
            nameTexts[rankingManager.isRank].text = "!!Your Rank!!";
        }
    }
}