using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

public class RiddleHandler : MonoBehaviour {

    //몇번 수수께끼를 풀었니
    List<bool> IsRiddleSolved = new List<bool>();
    //수수께기 점수
    int Score;
    //힌트코인 개수
    int HintCoinCount;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RiddleDataLoad(String RD,int S,int HC)
    {
        String TempRiddleSolveData = RD;
        Score = S;
        HintCoinCount = HC;
        //0번 데이터는 언제나 True
        IsRiddleSolved.Add(true);
        //받은 RiddleData로부터 데이터를 설정한다.
        for (int i=0;i<TempRiddleSolveData.Length;i++)
        {
            IsRiddleSolved.Add(TempRiddleSolveData[i] == 'T');
        }
    }
}
