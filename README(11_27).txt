NPC�� Sprite�� ��û�ϴ� �������� ���ڷ� int(FIeld NPC ID)���� �Ѱ��ִ���, string(Standing NPC keyword)�� �Ѱ��ִ����� ���� �ٸ� �ൿ�� �ϵ��� Override�� �Ǿ��ֽ��ϴ�.

Field NPC�� Ingame������ ũ�Ⱑ 1/4�� �پ ��µǸ�, Standing NPC�� �״�� ��µ˴ϴ�.
(����, FIeld NPC�� Sprite�� 640x640 px ũ����, �ΰ��ӿ����� 320x320 px�� ��µ˴ϴ�)

�ܺ� �ؽ�Ʈ ������ �о���� ���� ������ ����� Web Platform������ �۵����� Ȯ���� �����ϴ�.
(NPCSpritHandler�� ScriptHandler�� ������ ������� �����Ǿ��ֽ��ϴ�...�����)

Script ���Ͽ��� Ű���� �ص� �� �׿� ���� �ൿ�� ScriptHandler.cs ��ũ��Ʈ�� namespaceȭ ���Ѽ� ������ �����Դϴ�.

C#�� Multiple Inheritance�� ���� ���Ѵ�ϴ� �ù�

NPCScriptHandler�� ScriptHandler�� ����մϴ�. ScriptHandler�� ������ �ٸ� Class�� MonoBehaviour�� ��ӹ޽��ϴ�

=========

11�� 27�ϱ��� �����Ѱ�:

NPC�� ���, ���â �¿쿡 ���� �׸� ��Ʈ��, Ư�� ��� ���� �������� ���⸦ ���� Script ���� �۾� �Ϸ�(�ڼ��� ������ ScriptREADME�� �����ϼ���)

����� Script�� GameManager ������Ʈ �� NPC ������Ʈ���� �ڽſ��� �ʿ��� �͸� Load�ϰ� �� Algorithm ���� �Ϸ�(�ڵ�� ��¥�� �ּ����� �˰��� ���� ��)

NPC�� ScriptHandler�� GameManager�� ScriptHandler�� ��ӹ޾Ƽ� �� NPC�� �ʿ��� Script�� Load�ϰ� ����ȭ ���׽��ϴ�.(�ڵ�� ��¥�� �ּ����� �˰��� ���� ��)

========

�ؾ��Ұ�(ASAP):

Script ���Ŀ� ���� ����� �����ϴ� �Լ�(Ex: RIddle Ű���带 ������ ���������� �����)

Load �˰����� �ڵ�� ��ȯ

Script ���Ŀ� ����� �ٿ�ε� �޴°͵� ����� �Ѵ�.(Scene �ϳ��� ��� �ϳ�, Phase�� ��濡 ������ ��ġ�� �ʴ´�)

�޴� ����, �� ���� ��ư �� ���� ��ɼ� ��ư�� �����ؾ� �Ѵ�.-----����ϱ���!

���� �ִ°� ���� ��ư���� ����(Script���� ������ �ִ� ���� ��ư�� ��ġ�� �����ϼ�)
->��ư�� Ŭ���ϸ� GameManager���� �̰� ��İ� ������� �Ѵ�.

========