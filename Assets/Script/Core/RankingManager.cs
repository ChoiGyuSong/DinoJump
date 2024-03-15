using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class RankingData
{
    public string name;
    public float score;
}

public class RankingManager : MonoBehaviour
{
    public TMP_InputField inputField;
    GameObject rankingPanel;
    Text[] rankingTexts;
    public List<RankingData> rankingList = new List<RankingData>();
    public RankDisplay rankingDisplay;
    public int isRank;
    public int sceneLoad = 0;

    private void Awake()
    {
        rankingDisplay = FindObjectOfType<RankDisplay>();
        inputField = FindObjectOfType<TMP_InputField>();
    }

    void Start()
    {
        if (!PlayerPrefs.HasKey("RankingScore0"))
        {
            // PlayerPrefs�� ��ŷ �����Ͱ� ���� ���, ������ ������ ��ŷ�� ä��
            for (int i = 0; i < 5; i++)
            {
                RankingData newRankingData = new RankingData();
                newRankingData.name = "Test Player" + (i + 1); // ������ �̸� ����
                newRankingData.score = (i+1) * 0.01f; // ������ ���� ����
                rankingList.Add(newRankingData);
            }
            // ��ŷ�� PlayerPrefs�� ����
            for (int i = 0; i < rankingList.Count; i++)
            {
                PlayerPrefs.SetString("RankingName" + i, rankingList[i].name);
                PlayerPrefs.SetFloat("RankingScore" + i, rankingList[i].score);
            }
        }
        else
        {
            // PlayerPrefs�� ��ŷ �����Ͱ� �ִ� ���
            LoadRanking();
        }
        rankingDisplay = FindObjectOfType<RankDisplay>();
        rankingDisplay.gameObject.SetActive(false);
    }

    /// <summary>
    /// ��ŷ�� ������ Ȯ��
    /// </summary>
    /// <param name="score"></param>
    public void CheckRanking(float score)
    {
        isRank = 10;
        SceneLoadCheck();

        for (int i = Mathf.Min(rankingList.Count, 5) - 1; i >= 0; i--)
        {
            if (rankingList[i].score < score)
            {
                isRank = i;
            }
            else
            {
                break; // �� �̻� ���� �ʿ� ����
            }
        }

        // ���� Ȯ��
        for (int i = 0; i < rankingList.Count; i++)
        {
            if (score > rankingList[i].score)
            {
                RankingData newRankingData = new RankingData();
                newRankingData.score = score;
                rankingList.Insert(i, newRankingData);

                // �̸� �Է�
                inputField.gameObject.SetActive(true);
                inputField.onEndEdit.AddListener(delegate { EnterName(i); });
                break;
            }
        }

        if (rankingList.Count > 5)
        {
            rankingList.RemoveRange(5, rankingList.Count - 5);
        }

        // ���÷��� ���
        rankingDisplay.DisplayRanking();
    }

    /// <summary>
    /// �Է¹��� �̸� ��ŷ ����
    /// </summary>
    /// <param name="index"></param>
    public void EnterName(int index)
    {
        // �Էµ� �̸� ����
        rankingList[index].name = inputField.text;
        inputField.text = "";

        inputField.gameObject.SetActive(false);

        // ��ŷ ����
        for (int i = 0; i < rankingList.Count; i++)
        {
            PlayerPrefs.SetString("RankingName" + i, rankingList[i].name);
            PlayerPrefs.SetFloat("RankingScore" + i, rankingList[i].score);
        }
        isRank = 10;
        rankingDisplay.nameChange = 2;
        rankingDisplay.DisplayRanking();
    }

    /// <summary>
    /// PlayerPrefs���� ��ŷ �ҷ�����
    /// </summary>
    public void LoadRanking()
    {
        rankingList.Clear();
        for (int i = 0; i < 5; i++)
        {
            if (PlayerPrefs.HasKey("RankingScore" + i))
            {
                RankingData loadedRankingData = new RankingData();
                loadedRankingData.name = PlayerPrefs.GetString("RankingName" + i);
                loadedRankingData.score = PlayerPrefs.GetFloat("RankingScore" + i);
                rankingList.Add(loadedRankingData);
            }
        }
        rankingDisplay.DisplayRanking();
    }

    void SceneLoadCheck()
    {
        rankingDisplay = null;
        inputField = null;
        rankingDisplay = FindObjectOfType<RankDisplay>();
        inputField = FindObjectOfType<TMP_InputField>();
        inputField.gameObject.SetActive(false);
    }
}