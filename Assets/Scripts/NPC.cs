using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//NPC를 UI(버튼)로 처리할 겁니다.
using UnityEngine.UI;

public class NPC : MonoBehaviour {
    //NPC는 UI의 Button으로 생성합니다.
    //게임 데이터 오브젝트는 NPC의 정보가 저장된 Prefab을 미리 저장해둔 뒤, 지정된 데이터를 읽어서 적절한 NPC를 만들어서 배치합니다.
    //이때, NPC는 Canvas Object의 Child 가 되어야 합니다.

    //Script를 출력하는 과정은 다음과 같습니다.
    //1. NPC가 스크립트 오브젝트에게 적절한 Script를 요청합니다.
    //2. 스크립트 오브젝트는 NPC에게 적절한 Script(String)을 리턴합니다.
    //3. 리턴받은 Script(문자열)을 ScriptPrinter에게 전달합니다.
    //이거 만들어 주세요

    //NPC의 ID입니다.
    int NPC_ID;
    //NPC가 현재 출력해야 할 대사입니다. 대사 출력창에 넘겨주기 위한 임시 변수 역할도 합니다.
    string script;
    //NPC 오브젝트의 Image Component입니다. 저 컴포넌트의 image element에 Sprite를 할당하면 됩니다. (Sprite는 게임 데이터 오브젝트에서 불러오면 될거 같습니다)
    Image NPCImage;
    //???
    string location;
    //ScriptPrinter를 저장하는 Object입니다. UnityEditor에서 미리 할당받은채로 시작.
    public GameObject ScriptPrinter;
    //GameManager를 저장하는 Object입니다. UnityEditor에서 미리 할당받은채로 시작.
    public GameObject GameManager;
	// Use this for initialization
	void Start () {

        //더미 Sprite를 일단 할당
        SetSprite(0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetSprite(int ID)
    {
        NPC_ID = ID;
        //받아서
        Sprite temp;
        temp = GameManager.GetComponent<NPCSpriteHandler>().GetNPCSprite(NPC_ID);

        //할당해
        this.GetComponent<Image>().sprite = temp;
        //그리고 크기를 원본의 1/4 크기(가로 절반, 세로 절반)로 할당합니다.
        this.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, temp.textureRect.height/2);
        this.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, temp.textureRect.width/2);
    }

    //게임 데이터 오브젝트에 저장된 Script 데이터에서 적절한 Script를 받아오는 Method입니다.
    //일단 Public Method로 만들었습니다. 나중에 필요하면 private로 바꿔도 무방
    public void ScriptDownloader()
    {

    }

    //현재 NPC가 가지고 있는 Script를 ScriptPrinter로 보내주는 Method입니다.
    //일단 Public Method로 만들었습니다. 나중에 필요하면 private로 바꿔도 무방
    public void ScriptSender()
    {

        //ScriptPrinter Object의 ScriptPrinter(코드) Component를 호출한 뒤, 코드 내 Public Method를 호출합니다.
        //어떤 Method를 호출할지는 모르겠다.
        //ScriptPrinter.GetComponent<ScriptPrinter>().(호출하고 싶은 Public Method);
    }

}
