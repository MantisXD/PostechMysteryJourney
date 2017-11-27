NPC의 Sprite를 요청하는 과정에서 인자로 int(FIeld NPC ID)형을 넘겨주는지, string(Standing NPC keyword)을 넘겨주는지에 따라 다른 행동을 하도록 Override가 되어있습니다.

Field NPC는 Ingame에서는 크기가 1/4로 줄어서 출력되며, Standing NPC는 그대로 출력됩니다.
(가령, FIeld NPC의 Sprite가 640x640 px 크기라면, 인게임에서는 320x320 px로 출력됩니다)

외부 텍스트 파일을 읽어오는 것을 구현한 방법이 Web Platform에서도 작동할지 확신이 없습니다.
(NPCSpritHandler와 ScriptHandler에 동일한 방법으로 구현되어있습니다...상속할까?)

Script 파일에서 키워드 해독 및 그에 따른 행동은 ScriptHandler.cs 스크립트에 namespace화 시켜서 관리할 예정입니다.

C#은 Multiple Inheritance를 지원 안한답니다 시발

NPCScriptHandler는 ScriptHandler를 상속합니다. ScriptHandler를 포함한 다른 Class는 MonoBehaviour를 상속받습니다

=========

11월 27일까지 구현한거:

NPC의 대사, 대사창 좌우에 세울 그림 컨트롤, 특정 대사 이후 수수께끼 띄우기를 위한 Script 서식 작업 완료(자세한 사항은 ScriptREADME를 참고하세요)

저장된 Script를 GameManager 오브젝트 및 NPC 오브젝트에서 자신에게 필요한 것만 Load하게 할 Algorithm 구축 완료(코드는 안짜고 주석으로 알고리즘 설명만 함)

NPC의 SpriteHandler가 GameManager의 ScriptHandler를 상속받아서 각 NPC가 필요한 Script만 Load하게 전문화 시켰습니다.(코드는 안짜고 주석으로 알고리즘 설명만 함)

========

해야할거(ASAP):

Script 서식에 따라 명령을 수행하는 함수(Ex: RIddle 키워드를 받으면 수수께끼를 띄워라)

Load 알고리즘을 코드로 변환
========