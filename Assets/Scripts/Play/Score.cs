using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    Text textScore;
    Text textJudge;
    Text textCombo;


    public int SongScore { private set; get; }

    public Dictionary<string, int> JudgeCnt;
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

        JudgeCnt = new Dictionary<string, int>();
        JudgeCnt.Add("Worst", 0);
        JudgeCnt.Add("Bad", 0);
        JudgeCnt.Add("Soso", 0);
        JudgeCnt.Add("Perfect", 0);
        JudgeCnt.Add("Best", 0);


        SongScore = 0;



        Combo = 0;



        MaxCombo = 0;

        strJudge = "";
    }

    public void ProcessScore(string judge)
    {
        // 0 : 미스, 1 : 굿, 2 : 그레잇
        if(judge.Equals("Worst"))
        {
            
            Combo = 0;
        }
        else
        {
            Combo++;

        }

        JudgeCnt[judge]++;
        strJudge = judge;

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
