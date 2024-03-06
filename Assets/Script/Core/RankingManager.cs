using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class PlayerScore
{
    public string playerName;
    public float survivalTime;
}

public class RankingManager : MonoBehaviour
{
    public List<PlayerScore> highScores = new List<PlayerScore>();
    private const int MaxRankings = 5;
    TMP_InputField inputField; // �ν����Ϳ��� �Ҵ�
    RankDisplay rankDisplay;
    public static TMP_InputField insName;
    public static List<PlayerScore> playerScores;
    string json;
    int nowRank;

    private void Awake()
    {
        inputField = FindObjectOfType<TMP_InputField>();
        insName = inputField;
        Canvas canvas = FindObjectOfType<Canvas>();
        Transform transform = canvas.transform;
        Transform rank = transform.GetChild(1);
        rankDisplay = FindAnyObjectByType<RankDisplay>();

        LoadRanking();
    }


    public void TextOff()
    {
        insName.gameObject.SetActive(false);
    }

    public void TextOn()
    {
        insName.gameObject.SetActive(true);
    }

    public void CheckForHighScore(float time)
    {
        // ��ŷ�� ������� Ȯ��
        if (highScores.Count < MaxRankings || time > highScores[highScores.Count - 1].survivalTime)
        {
            switch(highScores.Count)
            {
                case 1:
                    nowRank = 1;
                    break;
                case 2:
                    nowRank = 2;
                    break;
                case 3:
                    nowRank = 3;
                    break;
                case 4:
                    nowRank = 4;
                    break;
                case 5:
                    nowRank = 5;
                    break;
            }
            // InputField Ȱ��ȭ
            insName.gameObject.SetActive(true);
            insName.onEndEdit.AddListener(delegate { EnterName(time); });
            insName.Select(); // ����� �Է��� �ޱ� ���� ����
            rankDisplay.DisplayRanking();
        }
    }

    private void EnterName(float time)
    {
        // InputField�κ��� �̸��� �޾ƿ� ����Ʈ�� �߰�
        highScores.Add(new PlayerScore { playerName = insName.text, survivalTime = time });
        SortAndSaveRanking();
        insName.gameObject.SetActive(false); // �̸� �Է��� ������ InputField ��Ȱ��ȭ
        insName.text = "";
        Debug.Log($"1�� {highScores.Count}");
        rankDisplay.DisplayRanking();
    }

    private void SortAndSaveRanking()
    {
        // ������ ������������ ����
        highScores = highScores.OrderByDescending(score => score.survivalTime).ToList();
        // ��ŷ�� MaxRankings�� �ʰ��ϸ� �������� ����
        if (highScores.Count > MaxRankings)
        {
            highScores.RemoveAt(highScores.Count - 1);
        }
        // ��ŷ ����
        SaveRanking();
    }

    public void SaveRanking()
    {
        if(playerScores == null)
        {
            playerScores = highScores;
        }
        // ��ŷ �����͸� JSON���� ��ȯ
        string json = JsonUtility.ToJson(highScores);
        // JSON�� PlayerPrefs�� ����
        PlayerPrefs.SetString("HighScores", json);
        PlayerPrefs.Save();
    }

    public void LoadRanking()
    {
        // PlayerPrefs���� ��ŷ ������ �ҷ�����
        json = PlayerPrefs.GetString("HighScores", "[]");
        highScores = JsonUtility.FromJson<List<PlayerScore>>(json);
    }
}
