using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    Text textScore;
    Text textJudge;
    Text textCombo;


    public int SongScore { private set; get; }

    public int[] JudgeCnt { private set; get; }

    public int Combo { private set; get; }
    public int MaxCombo { private set; get; }

    string strJudge;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        textScore = GameObject.Find("ScoreText").GetComponent<Text>();
        textJudge = GameObject.Find("JudgeText").GetComponent<Text>();
        textCombo = GameObject.Find("ComboText").GetComponent<Text>();

        SongScore = 0;
        Combo = 0;


        JudgeCnt = new int[]{ 0, 0, 0, 0, 0};

        MaxCombo = 0;

        strJudge = "";
    }

    public void ProcessScore(int judge)
    {
        // 0 : Worst, 1 : Bad, 2 : Soso, 3 : Good, 4 : Best


        if(judge.Equals(0))
        {
            strJudge = "Worst";
            Combo = 0;
            textJudge.color = Color.gray;
        }
        else if(judge.Equals(1))
        {
            strJudge = "Bad";
            Combo++;
            textJudge.color = Color.yellow;
        }
        else if(judge.Equals(2))
        {
            strJudge = "Soso";
            Combo++;
            textJudge.color = Color.blue;
        }
        else if (judge.Equals(3))
        {
            strJudge = "Soso";
            Combo++;
            textJudge.color = Color.blue;
        }
        else if (judge.Equals(4))
        {
            strJudge = "Soso";
            Combo++;
            textJudge.color = Color.blue;
        }

        JudgeCnt[judge] += 1;

        SetMaxCombo();
        SetTextScore();
    }

    void SetMaxCombo()
    {
        if (Combo > MaxCombo)
            MaxCombo = Combo;
    }

    void SetTextScore()
    {
        SongScore=(JudgeCnt[0] * 0 + JudgeCnt[1] * 40 + JudgeCnt[2] * 75 + JudgeCnt[3] * 95 + JudgeCnt[4] * 100) / JudgeCnt.Sum();


        textScore.text = SongScore.ToString();
        textJudge.text = strJudge;
        textCombo.color = Color.white;
        textCombo.text = Combo.ToString();
        
    }
}
