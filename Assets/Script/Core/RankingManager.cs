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
            // PlayerPrefs에 랭킹 데이터가 없는 경우, 임의의 값으로 랭킹을 채움
            for (int i = 0; i < 5; i++)
            {
                RankingData newRankingData = new RankingData();
                newRankingData.name = "Test Player" + (i + 1); // 임의의 이름 설정
                newRankingData.score = (i+1) * 0.01f; // 임의의 점수 설정
                rankingList.Add(newRankingData);
            }
            // 랭킹을 PlayerPrefs에 저장
            for (int i = 0; i < rankingList.Count; i++)
            {
                PlayerPrefs.SetString("RankingName" + i, rankingList[i].name);
                PlayerPrefs.SetFloat("RankingScore" + i, rankingList[i].score);
            }
        }
        else
        {
            // PlayerPrefs에 랭킹 데이터가 있는 경우
            LoadRanking();
        }
        rankingDisplay = FindObjectOfType<RankDisplay>();
        rankingDisplay.gameObject.SetActive(false);
    }

    /// <summary>
    /// 랭킹에 들어가는지 확인
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
                break; // 더 이상 비교할 필요 없음
            }
        }

        // 순위 확인
        for (int i = 0; i < rankingList.Count; i++)
        {
            if (score > rankingList[i].score)
            {
                RankingData newRankingData = new RankingData();
                newRankingData.score = score;
                rankingList.Insert(i, newRankingData);

                // 이름 입력
                inputField.gameObject.SetActive(true);
                inputField.onEndEdit.AddListener(delegate { EnterName(i); });
                break;
            }
        }

        if (rankingList.Count > 5)
        {
            rankingList.RemoveRange(5, rankingList.Count - 5);
        }

        // 디스플레이 출력
        rankingDisplay.DisplayRanking();
    }

    /// <summary>
    /// 입력받은 이름 랭킹 저장
    /// </summary>
    /// <param name="index"></param>
    public void EnterName(int index)
    {
        // 입력된 이름 저장
        rankingList[index].name = inputField.text;
        inputField.text = "";

        inputField.gameObject.SetActive(false);

        // 랭킹 저장
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
    /// PlayerPrefs에서 랭킹 불러오기
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