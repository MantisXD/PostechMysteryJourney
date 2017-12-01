using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        TextPrint("ScrollRect Test입니다 이게 정해진 구역을 넘어가면 어떻게 되는지 궁금해요");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //ScriptPrinter가 호출하면 대사를 출력하고 크기를 적절히 바꿔주는 Script
    public void TextPrint(string text)
    {
        //Text를 바꿔준다.
        ScriptTextText.text = text;
        //Text창의 Recttransform의 크기를 Text에 적절하게 바꿔준다.
        ScriptTextRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, ScriptTextText.preferredHeight);
        //ScriptTextRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, ScriptTextText.preferredWidth);
    }
}
