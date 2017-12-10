using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;

//GameManager Object의 Component입니다.
//NPC와 관련된 모든 일을 수행합니다.

//필드 NPC(필드에 나와 있는 NPC)의 Sprite를 로드합니다.
//그리고 스탠딩 NPC(텍스트 창과 함께 보여지는 NPC)의 Sprite의 이름과 Sprite를 대응시킵니다.

//필드 NPC를 소환합니다.
//스탠딩 NPC를 타켓팅합니다.

public class NPCHandler : MonoBehaviour {

    //필드Sprite 배열을 저장할 List를 선언합니다. C++와는 문법이 다르니 주의!
    List<Sprite> FieldNPCSpriteCache = new List<Sprite>();
    //스탠딩 Sprite를 저장할 Dictionary를 만듭니다. keyword와 Sprite가 대응됩니다.
    List<Sprite> StandingNPCSpirteCache = new List<Sprite>();
    //NPC의 수입니다.(나중에 할당해주세요)
    public int FieldNPCCount, StandingNPCCount;
    //NPC의 Template입니다.
    public GameObject NPCTemplate;

    //만들어진 NPC를 가리킵니다. Scene이나 Phase가 바뀌면 NPC를 싹 다 갈아엎기 위해서입니다.
    List<GameObject> NPCList = new List<GameObject>();

    //Canvas의 너비와 높이
    float width, height;

    private void Awake()
    { 
        //(수수께끼 Scene에서 Main Scene으로 넘어올 때 자동으로 Script를 로드하게 하기 위해서 입니다.
        DontDestroyOnLoad(this);
    }
    // Use this for initialization
    void Start () {

        SetNPCSpriteCache();
        //생성한 Sprite 배열에 Sprite를 때려박습니다.

    }
    public void RemoveAllNPC()
    {
        if (NPCList.Count != 0)
        {
            for (int i = 0; i < NPCList.Count; i++)
            {
                Destroy(NPCList[i]);
            }
            NPCList.Clear();
        }
    }
	//새로운 NPC를 형성합니다(ScriptHandler에서 호출합니다)
    public void CreateNPC(int ID, int x, int y,GameObject Printer)
    {
        GameObject Canvas = GameObject.Find("NPCHolder");
        Rect CanvasRect = Canvas.GetComponent<RectTransform>().rect;
        GameObject CreatedNPC = Instantiate(NPCTemplate);

        width = CanvasRect.width;// * Canvas.GetComponent<RectTransform>().localScale.x;
        height = CanvasRect.height;// * Canvas.GetComponent<RectTransform>().localScale.y;

        //Sprite 설정 및 부모 설정
        CreatedNPC.transform.SetParent(Canvas.transform);
        CreatedNPC.GetComponent<NPC>().SetSprite(ID);
        CreatedNPC.GetComponent<NPCScriptHandler>().Set_Printer(Printer);

        RectTransform NPCRect = CreatedNPC.GetComponent<RectTransform>();
        NPCRect.anchorMax = new Vector2(0f, 1f);
        NPCRect.anchorMin = new Vector2(0f, 1f);

        NPCRect.anchoredPosition = new Vector2(width*x/100, height*y/100);

        NPCList.Add(CreatedNPC);


    }

    // Update is called once per frame
    void Update()
    {

    }
    void SetNPCSpriteCache()
    {
        FieldNPCSpriteCache.Clear();
        StandingNPCSpirteCache.Clear();
       //string NPCMaplocation = Application.dataPath + "/Resources/Images/StandingNPC/NPCmap.txt";//스탠딩 NPC대응표가 저장된 위치입니다.
        Texture2D temp;



        //Resources 폴더에 저장된 이미지를 임시로 Texture2D로 변환하고, Texture2D를 다시 Sprite로 변환합니다.

        //리소스 폴더에 저장된 NPC(스탠딩, 필드)의 이름은 NPC_XXX로 해주세요(XXX는 번호를 의미하며 1~999까지 가능합니다. 001이 아니라 1입니다!)

        //필드 NPC를 불러옵니다.
        for (int i = 0; i < FieldNPCCount; i++)
        {
            //0번 원소에는 더미 NPC의 Sprite가 들어갑니다. 일반적으로는 절대 사용되지 않습니다.
            //Texture2D로 저장된 이미지를 로드합니다.
            temp = Resources.Load<Texture2D>("Images/FieldNPC/NPC_" + i.ToString());
            //로드한 Texture2D를 Sprite로 변환합니다.
            FieldNPCSpriteCache.Add(Sprite.Create(temp, new Rect(0.0f, 0.0f, temp.width, temp.height), new Vector2(0.5f, 0.5f)));
        }

        //대응표에 맞게 스탠딩 NPC의 그림을 불러옵니다.
        for (int i = 0; i < StandingNPCCount; i++)
        {

            //Texture2D로 저장된 이미지를 로드합니다.
            temp = Resources.Load<Texture2D>("Images/StandingNPC/NPC_" + i.ToString());
            //로드한 이미지를 전부 다 Sprite로 사용하며, 이미지의 중심(0,0에서 전체의 0.5배, 0.5배만큼 떨어져 있는 지점)을 기준으로 좌표를 지정합니다.
            StandingNPCSpirteCache.Add(Sprite.Create(temp, new Rect(0.0f, 0.0f, temp.width, temp.height), new Vector2(0.5f, 0.5f)));
        }
    }
    //NPC의 아이디를 받으면 적절한 Sprite를 제공하는 함수
    //안전한 구조를 위해서 포인터가 아니라 Sprite 자체를 복사해서 리턴합니다.
    public Sprite GetFieldNPCSprite(int ID)
    {
        return FieldNPCSpriteCache[ID];
    }

    //NPC의 키워드를 받으면 적절한 Sprite를 제공하는 함수.(Override)
    //안전한 구조를 위해서 포인터가 아니라 Sprite 자체를 복사해서 리턴합니다.
    //Script에서 제시하는 키워드를 받아 리턴합니다.
    public Sprite GetStandingNPCSprite(int ID)
    { 
        return StandingNPCSpirteCache[ID];
    }
}
