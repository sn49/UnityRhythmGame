using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheet : MonoBehaviour
{
    // [SheetInfo]
    public string AudioFileName { set; get; }
    public string AudioViewTime { set; get; }
    public string ImageFileName { set; get; }
    public float Bpm { set; get; }
    public float Offset { set; get; }
    public int Beat { set; get; }
    public int Bit { set; get; }
    public int BarCnt { set; get; }

    // [ContentInfo]
    public string Title { set; get; }
    public string Artist { set; get; }
    public string Source { set; get; }
    public string SheetBy { set; get; }
    public string Difficult { set; get; }

    int lanecount;



    // [NoteInfo]


    public List<List<float>> noteLists = new List<List<float>>();

    void Awake()
    {
        int lanecount = 4;

        for (int i = 0; i < lanecount; i++)//나중에 레인 여러개일수도
        {
            noteLists.Add(new List<float>());
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {

    }

    public void SetNote(int laneNumber, float noteTime)
    {
        noteLists[laneNumber-1].Add(noteTime);
    }

    void showInfo()
    {
        Debug.Log(AudioFileName);
        Debug.Log(Title);
        Debug.Log(Artist);
        Debug.Log(Difficult);
        Debug.Log(Bpm);
    }
}
