using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//EventManager Object의 Component입니다.

public class NPCSpriteHandler : MonoBehaviour {

    //Sprite 배열을 저장할 변수를 선언합니다. C++와는 문법이 다르니 주의!
    Sprite[] NPCSpriteCache;
    //NPC의 수입니다.(그만큼의 Sprite가 있습니다) GameManager의 다른 Script에게 받아오게 할 예정입니다.
    int NPCCount;

	// Use this for initialization
	void Start () {

        //NPC의 수를 임시로 2개로 정합니다.
        SetNPCCount(2);
        //Sprite 배열을 생성합니다 C++와는 문법이 다르니 주의! (한번 할당하면 크기는 고정됩니다. 새로운 원소 추가 불가능)
        NPCSpriteCache = new Sprite[NPCCount+1];
        //생성한 Sprite 배열에 Sprite를 때려박습니다.
        SetNPCSpriteCache();
    }
	//NPC의 머릿수를 설정합니다. 
    public void SetNPCCount(int value)
    {
        NPCCount = value;
    }

	// Update is called once per frame
	void Update () {
		
	}
    //Resources 폴더에 저장된 이미지를 임시로 Texture2D로 변환하고, Texture2D를 다시 Sprite로 변환합니다.
    //리소스 폴더에 저장된 NPC의 이름은 NPC_XXX로 해주세요(XXX는 번호를 의미하며 1~999까지 가능합니다. 001이 아니라 1입니다!)
    void SetNPCSpriteCache()
    {
        Texture2D temp;
        for (int i = 0; i < NPCCount; i++)
        {
            //0번 원소에는 더미 데이터가 들어갑니다. 일반적으로는 절대 로드되지 않습니다.

            //Texture2D로 저장된 이미지를 로드합니다.
            temp = Resources.Load<Texture2D>("Images/NPC/NPC_" + i.ToString());
            //로드한 Texture2D를 Sprite로 변환합니다.
            //로드한 이미지를 전부 다 Sprite로 사용하며, 이미지의 중심(0,0에서 전체의 0.5배, 0.5배만큼 떨어져 있는 지점)을 기준으로 좌표를 지정합니다.
            NPCSpriteCache[i] = Sprite.Create(temp, new Rect(0.0f, 0.0f, temp.width, temp.height), new Vector2(0.5f, 0.5f));

        }
    }

    //NPC의 아이디를 받으면 적절한 Sprite를 제공하는 함수
    //안전한 구조를 위해서 포인터가 아니라 Sprite 자체를 복사해서 리턴합니다.
    public Sprite GetNPCSprite(int ID)
    {
        return NPCSpriteCache[ID];
    }
}
