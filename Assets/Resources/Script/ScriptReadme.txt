//여러 키워드를 통해 명령을 구분합니다.
//각 키워드 사이는 Space로 구분합니다.
//키워드 설명란에 있는 대괄호 속 내용을 실제로 입력할 때는 대괄호를 벗겨주세요
//Readme 파일에 적은 것처럼 // 주석을 사용할 수 있습니다. 단, /* */는 사용 불가능합니다.

//맨 앞에 S가 붙은 것은 씬 넘버를 의미하며, 맨 첫번째 줄에 넘버를 기재해 주세요. 한 텍스트 파일에는 하나의 씬만 들어가야 합니다.

S [씬 넘버]
//맨 앞에 P가 붙은 것은 Phase를 나타내며, 각 씬에서 상황을 구분하기 위해 사용합니다. 한 텍스트 파일에는 하나의 Phase만 들어가야 합니다.
P [페이즈 넘버]

//Scene과 Phase Number를 다 기재했으면 배치할 NPC의 수를 다음과 같이 입력해주세요:

NPCCount [NPC 수]

//NPC의 번호(1부터 시작하거나, 공차가 1인 등차수열일 필요는 없습니다)와 좌표를 다음과 같이 입력해주세요(좌표계는 추후 정할거에요):
//각 NPC가 어떤 Image를 사용하는지는 NPC의 번호로 결정됩니다.

NPC 1 x=[x축 좌표] y=[y축 좌표]

//여담: name과 script 영역은 큰따옴표를 기준으로 parsing 한 뒤 대사 키워드를 제외하곤 띄어쓰기로 parsing합니다.
//나머지는 그냥 띄어쓰기로 parsing합니다

//NPC를 다 적으셨으면 나래이션 대사를 다음과 같이 입력해주세요(Phase가 시작될 때 출력될 대사입니다)
//나레이터 이름은 후술할 NPC 이름과 동일한 규칙을 따릅니다.

NarrStart

Speaker [나레이터 이름]
Script "[나래이션1 \n 나래이션1]"

Speaker [나레이터 이름]
Script "[나래이션2 \n 나래이션2]"

NarrEnd

//대사는 기본적으로 (클릭 가능한) NPC별 스크립트로 구성됩니다. 이때, 클릭하는 NPC와 화자의 NPC가 같을 필요는 없습니다.(화자가 없어도 됩니다)

//한 NPC를 클릭할 때 실행할 일련의 명령어를 시작하는 명령어 입니다.
//한 Phase에서 같은 NPC가 여러번 대사를 호출해야 할 때, 순서를 통해 구분합니다.
//순서는 반드시 1부터 시작하는 공차가 1인 등차수열을 이루어야 합니다. 
//Script가 하나 뿐이라서 순서를 고려할 필요가 없는 경우는 1을 입력해주세요

ScriptList [NPC 번호] [순서] Start

//말하는 NPC의 이름을 정합니다. 키워드로 구성되며 다른 파일에 키워드와 실제 이름의 대응표가 있습니다.
//Ex: NULL이라고 입력하면 말하는 사람의 이름창에 아무것도 띄우지 않습니다. 

Speaker [나레이터 이름]
Script "[대사]"

ScriptList End

//기타 명령어 입니다. 전부 다 ScriptList 영역 안에서만 사용 가능합니다.

Riddle [수수께끼 번호]
-> 정해진 번호의 수수께끼를 호출합니다

RiddleSuccess [Phase 번호]
-> Riddle 키워드가 있는 줄 바로 밑에만 사용 가능합니다.
-> Riddle 키워드로 부른 수수께끼를 풀었을 때 이동할 Phase의 번호를 입력해주세요.

RiddleFail [Phase 번호]
-> RiddleSuccess 키워드 바로 밑에만 사용 가능합니다.
-> Riddle 키워드로 부른 수수께끼를 포기했을 때 이동할 Phase의 번호를 입력해주세요.

Script Rand "대사 1" "대사 2" "대사 3"
-> 여러 대사 중 한가지를 랜덤하게 출력합니다.

PhaseShift [Phase 번호]
-> Phase를 바꿉니다. 

SceneShift [Scene 번호]
-> Scene을 바꿉니다. 이때, 바꾼 Scene의 Phase는 1로 고정됩니다.

LeftSprite/MiddleSprite/RightSprite [Sprite 번호]
-> 말할 때 대화창 왼쪽/중앙/오른쪽에 미연시처럼 NPC의 그림을 세울 때, 몇번째 Sprite를 꺼내 쓸것인지 바꿔줍니다.
-> 번호로 0보다 작거나 같은 수를 넣으면 Sprite가 없는 것을 나타냅니다.

IsLeftSpriteBright/IsMiddleSpriteBright/IsRIghtSpriteBright [true/false]
-> 왼쪽/중앙/오른쪽에 세운 그림을 어둡게 처리할 것인지(역강조), 밝게 처리할 것인지를 정합니다.