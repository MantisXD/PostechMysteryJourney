using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RiddleHint : MonoBehaviour {

    //0: 잠김 1:열림 2:아예 없음
    int Status;
    string Hint;
    Sprite OpenHintSprite, LockHintSprite, DisableHintSprite;
    // Use this for initialization
    void Start () {
        StartCoroutine(SpriteUpdater());
	}
	
    public void Assign(int S,string HintText, Sprite OpenHint,Sprite LockHint,Sprite DisableHint)
    {
        Status = S;
        HintText = Hint;
        OpenHintSprite = OpenHint;
        LockHintSprite = LockHint;
        DisableHintSprite = DisableHint;
    }

    public void SetHint(int Num)
    {
        GameObject Temp = GameObject.Find("RiddleHint" + Num.ToString());
        //오브젝트 찾고
        string Hint = Temp.GetComponent<RiddleHint>().GetHint();
        //잠겨있거나 비활성화되어있으면 무시한다.
        if (Hint != "NULL")
        {
            GameObject.Find("RiddleHintManager").GetComponent<RiddleHintManagement>().SetHint(Hint,Num);
        }


       
    }

    public string GetHint()
    {
        if (Status != 1)
            return "NULL";
        else
            return Hint;
    }

    // Update is called once per frame
    void Update () {
		
	}
    //퍼포먼스 개선을 위해서 0.5초에 한번씩 이미지를 업데이트
    IEnumerator SpriteUpdater()
    {
        while (true)
        {
            if (Status == 0)
                GetComponent<Image>().sprite = LockHintSprite;
            else if (Status == 1)
                GetComponent<Image>().sprite = OpenHintSprite;
            else if (Status == 2)
                GetComponent<Image>().sprite = DisableHintSprite;
            yield return new WaitForSeconds(.5f);
        }
    }
}
