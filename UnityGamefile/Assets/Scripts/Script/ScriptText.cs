using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System;

public class ScriptText : MonoBehaviour {

    RectTransform ScriptTextRect;
    Text ScriptTextText;
    public Scrollbar ScriptTextScroll;

	// Use this for initialization
	void Start () {
        //ScriptText의 Recttransform Component을 할당한다.
        ScriptTextRect = this.GetComponent<RectTransform>();
        //ScriptText의 Text Component를 할당한다.
        ScriptTextText = this.GetComponent<Text>();
        //ScritPrinter의 Scroll Component를 할당한다
        TextPrint(" ");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //ScriptPrinter가 호출하면 대사를 출력하고 크기를 적절히 바꿔주는 Script
    public void TextPrint(String text)
    {
        //Text를 바꿔준다.
        ScriptTextText.text = text;
        //Text창의 Recttransform의 크기를 Text에 적절하게 바꿔준다.
        ScriptTextRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, ScriptTextText.preferredHeight);
        ScriptTextRect.anchoredPosition = new Vector2(0, ScriptTextText.preferredHeight);
        //ScriptTextRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, ScriptTextText.preferredWidth);
        ScriptTextScroll.value = 1f;
    }
}
