using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//NPC를 UI(버튼)로 처리할 겁니다.
using UnityEngine.UI;

public class NPC : MonoBehaviour {
    //NPC는 UI의 Button으로 생성합니다.
    //게임 데이터 오브젝트는 NPC의 정보가 저장된 Prefab을 미리 저장해둔 뒤, 지정된 데이터를 읽어서 적절한 NPC를 만들어서 배치합니다.
    //이때, NPC는 Canvas Object의 Child 가 되어야 합니다.


    //NPC의 ID입니다.
    int NPC_ID;
    //NPC 오브젝트의 Image Component입니다. 저 컴포넌트의 image element에 Sprite를 할당하면 됩니다. (Sprite는 게임 데이터 오브젝트에서 불러오면 될거 같습니다)
    Image NPCImage;
    //???
    string location;
    //ScriptText를 저장하는 Object입니다. UnityEditor에서 미리 할당받은채로 시작.
    public GameObject ScriptText;
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
        temp = GameManager.GetComponent<NPCHandler>().GetNPCSprite(NPC_ID);
        //할당해
        this.GetComponent<Image>().sprite = temp;
        //그리고 크기를 원본의 1/4 크기(가로 절반, 세로 절반)로 할당합니다.
        this.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, temp.textureRect.height/2);
        this.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, temp.textureRect.width/2);
    }

}
