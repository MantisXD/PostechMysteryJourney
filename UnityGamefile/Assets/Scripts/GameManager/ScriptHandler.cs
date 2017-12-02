using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;

//Script를 불러오는 Script입니다.

public class ScriptHandler : MonoBehaviour
{

    //현재 Scene 번호와 Phase 번호입니다. 
    //지금은 기본값인 1,1로 지정됩니다.
    public int Scene, Phase;
    public bool Loaded;

    //Parsing이 안된 RawScript를 저장합니다.(Narration만 저장합니다)
    public List<String> RawScript = new List<String>();
    public NPCHandler GMNPCHandler;

    GameObject Printer;

    string ScriptCache;
    //모든 Script를 분류 후 저장합니다.
    //Key는 "(NPC ID)_(순서)"로 구성되며, Script[]는 그 Cluster의 모든 Script를 나타냅니다.
    Dictionary<string, List<String> > ScriptDataBase = new Dictionary<string, List<String> >();


    // Use this for initialization
    void Start()
    {
        //Printer를 찾습니다.
        Printer = GameObject.Find("ScriptPrinter");
        Printer.SetActive(false);
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
            //로드가 완료되었음을 나타냅니다.
            Loaded = true;
            //나래이션을 호출합니다.
            Narration();
        }
    }
    //나레이션 스크립트를 실행합니다.
    private void Narration()
    {
        Printer.SetActive(true);
        //대사를 넘겨줍니다.
        Printer.GetComponent<ScriptPrinter>().Get_Script(RawScript);
    }

    public List<String> Get_Script(int ID,int Sequence)
    {
        return ScriptDataBase[ID.ToString() + "_" + Sequence.ToString()];
    }

    public void Deactivate_Printer()
    {
        Printer.SetActive(false);
    }

    //Script를 로드합니다.
    void ScriptLoader()
    {
        StreamReader Reader;
        //현재 씬/Phase에 맞는 Script 파일의 이름을 생성합니다.
        string ScriptTextName = "Script_" + Scene.ToString() + "_" + Phase.ToString() + ".txt";
        string ScriptLocation = Application.dataPath + "/Resources/Script/" + ScriptTextName;//Script가 저장된 위치입니다.
        int c = 0, count;
        //이전 데이터를 말소시킵니다.

        ScriptDataBase.Clear();
        RawScript.Clear();

        try
        {
            //Streamreader를 호출합니다.
            Reader = new StreamReader(ScriptLocation, Encoding.UTF8);
        }
        catch (FileNotFoundException)//파일이 있는가?
        {
            Debug.Log("Script File doesn't exist!");
            return;
        }
        //파일이 있구나 한줄씩 읽어보자(다 읽을 때 까지
        while ((ScriptCache = Reader.ReadLine()) != null)
        {
            try
            {
                if(ScriptCache.Length < 2 || ScriptCache.Substring(0,2) == "//")//주석이니?
                {
                    continue;
                }
            }
            catch(ArgumentOutOfRangeException e)//에러 잡기
            {
                Debug.Log(e);
            }
            //일단 키워드 중 하나라는 것을 알았으니 Parsing을 수행합니다.
            //일단은 띄어쓰기를 기준으로 Parsing합니다. (대사임이 밝혀지면 후에 다시 Parsing 합니다)
            string[] ParsedScriptCache = ScriptCache.Split(new char[] { ' ' });

            //키워드에 따른 다양한 행동을 수행합니다.

            //NPC의 수를 저장한 명령어라면, NPCHandler를 비롯해서 NPC의 수가 필요한 Object들에게 정보를 넘겨줍니다.
            if(ScriptCache.Length >= 8 && ParsedScriptCache[0] == "NPCCount")
            {
                int.TryParse(ParsedScriptCache[1], out count);
                int ID, x, y;
                while (c < count)
                {
                    //읽고
                    ScriptCache = Reader.ReadLine();

                    if (ScriptCache.Length < 2)//"\n"이니?
                        continue;
                    try
                    {
                        if (ScriptCache.Substring(0, 2) == "//")//주석이니?
                        {
                            continue;
                        }
                    }
                    catch (ArgumentOutOfRangeException e)//에러 잡기
                    {
                        Debug.Log(e);
                    }
                    //나눈다.
                    ParsedScriptCache = ScriptCache.Split(new char[] { ' ' });

                    int.TryParse(ParsedScriptCache[1], out ID);
                    int.TryParse(ParsedScriptCache[2], out x);
                    int.TryParse(ParsedScriptCache[3], out y);
                    
                    //새로운 NPC를 만든다.
                    GMNPCHandler.CreateNPC(ID, x, -y, Printer);
                    c++;
                }
                int temp;
                int.TryParse(ParsedScriptCache[1], out temp);
                //GMNPCHandler.SetNPCCount(temp);
                continue;
            }
            //NarrStart 키워드라면 NarrEnd를 만날 때 까지 계속 Read합니다.
            if(ScriptCache.Length >= 9 && ParsedScriptCache[0].Substring(0,9) == "NarrStart")
            {
                //나레이션을 저장할 Script를 깔끔히 비워줍니다.
                RawScript.Clear();
                //Parsing은 할 필요 없습니다. 주석과 \n을 제외한 모든 Script를 담아 주세요.
                while ((ScriptCache = Reader.ReadLine()) != null)
                {
                    if (ScriptCache.Length >= 7 && ScriptCache.Substring(0,7) == "NarrEnd")
                        break;
                    if (ScriptCache.Length < 2)//"\n"이니?
                        continue;
                    try
                    {
                        if (ScriptCache.Substring(0, 2) == "//")//주석이니?
                        {
                            continue;
                        }
                    }
                    catch (ArgumentOutOfRangeException e)//에러 잡기
                    {
                        Debug.Log(e);
                    }
                    //둘 다 아니면 걍 담아.
                    RawScript.Add(ScriptCache);
                }

            }
            //ScriptList Start 키워드라면 Dictonary에 저장합니다.
            if((ScriptCache.Length >= 10 && ParsedScriptCache[0].Substring(0,10) == "ScriptList") && ParsedScriptCache[3] == "Start")
            {
                //ScriptDB의 Key를 생성합니다.
                string name = ParsedScriptCache[1] + "_" + ParsedScriptCache[2];
                //리스트를 비워줍니다.
                if (ScriptDataBase.ContainsKey(name))
                    ScriptDataBase[name].Clear();
                else
                    ScriptDataBase[name] = new List<String>();
                //Parsing은 할 필요 없습니다. 주석과 \n을 제외한 모든 Script를 담아 주세요.
                while ((ScriptCache = Reader.ReadLine()) != null)
                {
                    if (ScriptCache.Length >= 14 && ScriptCache.Substring(0, 14) == "ScriptList End")
                        break;

                    if (ScriptCache.Length < 2)//"\n"이니?
                        continue;
                    try
                    {
                        if (ScriptCache.Substring(0, 2) == "//")//주석이니?
                        {
                            continue;
                        }
                    }
                    catch (ArgumentOutOfRangeException e)//에러 잡기
                    {
                        Debug.Log(e);
                    }
                    //둘 다 아니면 걍 담아.
                    ScriptDataBase[name].Add(ScriptCache);
                }
            }
        }
            //기존의 NPC를 전부 삭제한 뒤, 새로운 NPC를 생성합니다.
            //생성한 NPC의 Sprite를 Load합니다.(NPCScriptHandler가 수행합니다)
            //나래이션 스크립트를 분리해서 맵핑합니다. (Key는 "Narration"입니다)

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
