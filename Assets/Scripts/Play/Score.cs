using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    Text textScore;
    Text textJudge;
    Text textCombo;


    public double SongScore { private set; get; }

    public Dictionary<string,int> JudgeCnt { private set; get; }


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

        JudgeCnt = new Dictionary<string, int>();

        JudgeCnt.Add("Worst", 0);
        JudgeCnt.Add("Bad", 0);
        JudgeCnt.Add("Soso", 0);
        JudgeCnt.Add("Perfect", 0);
        JudgeCnt.Add("Best", 0);


        MaxCombo = 0;

        strJudge = "";
    }

    public void ProcessScore(string judge)
    {
        // 0 : worst, 1 : bad, 2 : soso, 3:perfect 4:best
        if(judge.Equals("Worst"))
        {
            Combo = 0;
            textJudge.color = Color.gray;
        }
        else if(judge.Equals("Bad"))
        {
            Combo++;
            textJudge.color = Color.yellow;
        }
        else if(judge.Equals("Soso"))
        {
            Combo++;
            textJudge.color = Color.blue;
        }

        strJudge = judge;
        JudgeCnt[judge]++;

        int sum = 0;



        foreach (int v in JudgeCnt.Values)
        {
            sum += v;
        }


        SongScore = (JudgeCnt["Worst"] * 0 + JudgeCnt["Bad"] * 40 + JudgeCnt["Soso"] * 75 + JudgeCnt["Perfect"] * 95 + JudgeCnt["Best"] * 100) / sum;


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
        textScore.text = SongScore.ToString();
        textJudge.text = strJudge;
        textCombo.color = Color.white;
        textCombo.text = Combo.ToString();
        
    }
}
