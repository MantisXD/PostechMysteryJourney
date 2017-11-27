using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

//Script를 불러오는 Script입니다.

public class ScriptHandler : MonoBehaviour
{
    //현재 Scene 번호와 Phase 번호입니다. 
    //지금은 기본값인 1,1로 지정되지만, 나중에는 Main화면에서 넘어올 때 데이터를 Load한 뒤 
    //Constructor로 지정해 주는 형식으로 바꿀 예정입니다.
    public int Scene, Phase;
    public bool Loaded;
    //Script를 임시로 저장합니다.
    public string[] ScriptCache;
    //Parsing이 안된 RawScript를 저장합니다. Narration 대사만 저장합니다.
    private string RawScript;

    // Use this for initialization
    void Start()
    {
        //임시로 첫번째 Scene의 Phase 1을 호출합니다.
        //나중에는 저장된 Data에서 Load 가능하게 바꿀 예정입니다.
        Shifter(1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Loaded)
        {
            //Load가 안되어 있거나 갱신이 필요하면 얘를 호출한다.
            ScriptLoader();
        }
    }
    //나레이션 스크립트를 실행합니다.
    private void Narration()
    {
        //Parsing이 안된 Narrator 스크립트를 Parsing합니다.
        //Parsing한 Script를 바탕으로 명령을 실행합니다.
    }

    //Script를 로드합니다.(나래이터만 Load합니다)
    void ScriptLoader()
    {
        //현재 씬/Phase에 맞는 Script 파일의 이름을 생성합니다.
        string ScriptTextName = "Script" + Scene.ToString() + "_" + Phase.ToString();
        string Scriptlocation = Application.dataPath + "/Resources/Script/" + ScriptTextName;//Script가 저장된 위치입니다.
        if (File.Exists(Scriptlocation))
        {
            ScriptCache = File.ReadAllLines(Scriptlocation, Encoding.UTF8);

            //기존의 NPC를 전부 Unload한 뒤, 새로운 NPC를 생성합니다.
            //생성한 NPC의 Sprite를 Load합니다.(이때 NPC의 ID를 같이 할당합니다)
            //맵핑은 한줄한줄 읽은 뒤 String에 Append하는 방식으로 이루어집니다. 이때 \n은 무시합니다.
            //나래이션 스크립트를 분리해서 맵핑합니다. (Key는 "Narration"입니다)

            //각 NPC의 스크립트를 분리해서 맵핑합니다. (Key는 "NPC_[NPC 번호]_[순서]" 입니다)
            //나래이션 스크립트가 있다면(Scripts["Narration"] != "NULL" 이라면) 실행합니다.
        }
        else
        {
            Debug.Log("Script File doesn't exists!");
        }
        Loaded = true;
    }
    //Scene이나 Phase를 바꿔줍니다. 주로 NPC가 호출합니다. Main화면에서 넘어올 때 호출되기도 합니다.
    public void Shifter(int S, int P)
    {
        if (Scene != S)
        {
            Loaded = false;
            Scene = S;
        }
        if (Phase != P)
        {
            Loaded = false;
            Phase = P;
        }
    }

}
