using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

public class RiddleHandler : MonoBehaviour {

    //수수께끼 정보
    List<RiddleClass> RiddleList = new List<RiddleClass>();
    //수수께기 점수
    int Score;
    //힌트코인 개수
    int CoinCount;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RiddleDataLoad(List<RiddleClass> tempRiddleCache, int Sco,int Coin)
    {
        Score = Sco;
        CoinCount = Coin;

        //받은 RiddleData로부터 데이터를 설정한다.
        for (int i=0;i<tempRiddleCache.Count;i++)
        {
            RiddleList.Add(tempRiddleCache[i]);
        }
    }
}
