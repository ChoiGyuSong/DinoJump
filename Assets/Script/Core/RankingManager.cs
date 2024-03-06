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
    TMP_InputField inputField; // 인스펙터에서 할당
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
        // 랭킹에 들었는지 확인
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
            // InputField 활성화
            insName.gameObject.SetActive(true);
            insName.onEndEdit.AddListener(delegate { EnterName(time); });
            insName.Select(); // 사용자 입력을 받기 위해 선택
            rankDisplay.DisplayRanking();
        }
    }

    private void EnterName(float time)
    {
        // InputField로부터 이름을 받아와 리스트에 추가
        highScores.Add(new PlayerScore { playerName = insName.text, survivalTime = time });
        SortAndSaveRanking();
        insName.gameObject.SetActive(false); // 이름 입력이 끝나면 InputField 비활성화
        insName.text = "";
        Debug.Log($"1번 {highScores.Count}");
        rankDisplay.DisplayRanking();
    }

    private void SortAndSaveRanking()
    {
        // 점수를 내림차순으로 정렬
        highScores = highScores.OrderByDescending(score => score.survivalTime).ToList();
        // 랭킹이 MaxRankings를 초과하면 마지막을 제거
        if (highScores.Count > MaxRankings)
        {
            highScores.RemoveAt(highScores.Count - 1);
        }
        // 랭킹 저장
        SaveRanking();
    }

    public void SaveRanking()
    {
        if(playerScores == null)
        {
            playerScores = highScores;
        }
        // 랭킹 데이터를 JSON으로 변환
        string json = JsonUtility.ToJson(highScores);
        // JSON을 PlayerPrefs에 저장
        PlayerPrefs.SetString("HighScores", json);
        PlayerPrefs.Save();
    }

    public void LoadRanking()
    {
        // PlayerPrefs에서 랭킹 데이터 불러오기
        json = PlayerPrefs.GetString("HighScores", "[]");
        highScores = JsonUtility.FromJson<List<PlayerScore>>(json);
    }
}
