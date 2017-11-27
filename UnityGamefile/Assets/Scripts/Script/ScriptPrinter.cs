using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptPrinter : MonoBehaviour {

    RectTransform ScriptPrinterRect;
    Text ScriptPrinterText;
    public Scrollbar ScriptPrinterScroll;

	// Use this for initialization
	void Start () {
        //Scriptprinter의 Recttransform Component을 할당한다.
        ScriptPrinterRect = this.GetComponent<RectTransform>();
        //ScriptPrinter의 Text Component를 할당한다.
        ScriptPrinterText = this.GetComponent<Text>();
        //ScritPrinter의 Scroll Component를 할당한다
        TextPrint("ScrollRect Test입니다 이게 정해진 구역을 넘어가면 어떻게 되는지 궁금해요");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //NPC나 다른 오브젝트가 호출하면 대사를 출력하고 크기를 적절히 바꿔주는 Script
    public void TextPrint(string text)
    {
        //Text를 바꿔준다.
        ScriptPrinterText.text = text;
        //Text창의 Recttransform의 크기를 Text에 적절하게 바꿔준다.
        ScriptPrinterRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, ScriptPrinterText.preferredHeight);
        //ScriptPrinterRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, ScriptPrinterText.preferredWidth);
    }
}
