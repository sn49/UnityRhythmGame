using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judgement : MonoBehaviour
{
    Sheet sheet;
    Score score;
    SongManager songManager;

    float currentTime;      // 현재 음악샘플값

    // 숏
    float[] currentNoteTime;

    // 판정 배율(frequency값에 기반)
    float bestRate = 0.04f* 44100;
    float goodRate = 0.08f* 44100;
    float sosoRate = 0.14f * 44100;
    float badRate = 0.22f * 44100;

    int lanecount;

    List<Queue<float>> judgeLanes = new List<Queue<float>>();



    void Start()
    {
        lanecount = 4;
        print(lanecount);
        currentNoteTime= new float[lanecount];

        for (int i=0; i< lanecount; i++)//나중에 레인 여러개일수도
        {
            judgeLanes.Add(new Queue<float>());

        }

        sheet = GameObject.Find("Sheet").GetComponent<Sheet>();
        
        songManager = GameObject.Find("SongSelect").GetComponent<SongManager>();
        score = GameObject.Find("Score").GetComponent<Score>();
        SetQueue();
    }

    void Update()
    {
        // 입력 외 판정(반복문에서 계속 체킹)
        currentTime = songManager.music.timeSamples;



        // 이하 미스판정
        // 큐에서 받아온 노트타임을 샘플로 변환
        for (int i = 0; i < lanecount; i++)//나중에 레인 여러개일수도
        {

            if (judgeLanes[i].Count > 0)
            {
                currentNoteTime[i] = judgeLanes[i].Peek();
                currentNoteTime[i] = currentNoteTime[i] * 0.001f * songManager.music.clip.frequency;

                if (currentNoteTime[i] + badRate <= currentTime)
                {
                    judgeLanes[i].Dequeue();
                    score.ProcessScore(0);
                }
            }

        }

    }

    // 잘돌아가긴하는데 여러 상황을 만들어야함
    // 예를들면 간접미스 범위 어디까지.

    public void JudgeNote(int laneNum)
    {
        laneNum = laneNum-1;

       

        if (currentNoteTime[laneNum] > currentTime - badRate && currentNoteTime[laneNum] < currentTime + badRate)
        {
            if (currentNoteTime[laneNum] > currentTime - goodRate && currentNoteTime[laneNum] < currentTime + goodRate)
            {
                // soso판정
                if (currentNoteTime[laneNum] > currentTime - sosoRate && currentNoteTime[laneNum] < currentTime + sosoRate)
                {
                    judgeLanes[laneNum].Dequeue();
                    score.ProcessScore(2);
                }
                // bad판정
                else
                {
                    judgeLanes[laneNum].Dequeue();
                    score.ProcessScore(1);
                }
            }
            // 너무 빨리 입력했을때 미스처리
            else
            {
                judgeLanes[laneNum].Dequeue();
                score.ProcessScore(0);
                print("worst");
            }
        }



    }

    // 각 레인의 노트 시간들을 큐에 저장
    void SetQueue()
    {
        for (int i = 0; i < lanecount; i++)//나중에 레인 여러개일수도
        {
            foreach (float noteTime in sheet.noteLists[i])
            {
                judgeLanes[i].Enqueue(noteTime);

            }
        }

           
    }
    
}
