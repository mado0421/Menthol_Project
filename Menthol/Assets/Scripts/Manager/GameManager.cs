using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private ScoreManager scoreManager;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Test Option
        if (SceneManager.GetActiveScene().name == "GameScene") 
            InitializeGameScene();
    }

    private void ChangeSceneTo(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        InitializeGameScene();
    }

    public void ChangeScene()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "TitleScene": ChangeSceneTo("MenuScene"); break;
            case "MenuScene": ChangeSceneTo("GameScene"); break;
            case "GameScene": ChangeSceneTo("ResultScene"); break;
            case "ResultScene": ChangeSceneTo("MenuScene"); break;
        }
    }

    //=========================================================================
    // GameScene�� ����
    //  - PlayerCharacter.Die() -> ChangeScene("ResultScene");
    //  - EnemyCharacter.Die() -> ScoreManager.AddScore();
    //  - PlayerCharacter�� ���дϾ� ĸ���� ������ GetCompanionCapsule() ȣ��
    //     remainCompanionCapsule++;
    //  - GetCompanionCapsule() ȣ��Ǹ� CompanionSelectButtons�� Ȱ��ȭ
    //  - CompanionSelectButtons�� �� ��ư�� 1 ~ numTypeOfCompanion�� ���Ƿ� ����
    //     ���� ������? -> ó�� ������ ��, ������ �Ϸ����� ��
    //     (�̷��� remainCompanionCapsule ���������� ��� �� �� ����)
    //  - ��ư�� ������ �ش� ���дϾ��� AddCompanion()��.
    //  - AddCompanion()�ϸ� remainCompanionCapsule--;
    //  - remainCompanionCapsule�� 0�� �� ������ ��� ������ �� �ֵ���.
    //  - 0�� �Ǹ� CompanionSelectButtons�� ��Ȱ��ȭ
    //  
    // �ٵ� �̰� �� GameManager���� ���� �ϸ�?
    // �ٸ� Scene�� ��, GetCompanionCapsule()�� �θ� �� ������ �ȵǴ°� �Ƴ�???
    // 
    // SceneContext Ŭ������ �����,
    // GameManager������ SceneContext�� �θ� Ŭ���� �Լ���
    // Ư�� Scene������ GameManager.GameSceneContext.
    // �ƾ� �𸣰ڴ�~ ��� �ϴ��� �� �� �˾ƺ��°� ���� ��.
    // GameManager���� Score ������ �ϴ� ���� ��������?
    //=========================================================================

    private int remainCompanionCapsule;
    private GameObject CompanionSelectButtons;
    private GameObject[] CompanionPrefabs;
    private int[] itemList;
    private int numButton = 3;
    private float currTime;

    private void InitializeGameScene()
    {
        if (itemList == null) itemList = new int[numButton];

        // GameManager�� Scene�� ����� ���� ��� ����ֱ� ������
        // CompanionSelectButtons Object�� ���� ������ �ȵǾ� ���� �� �ִ�.
        // InitializeGameScene()�� ȣ�� Ÿ�̹��� �� ���ؾ� �� ��.
        if (CompanionSelectButtons == null) 
            CompanionSelectButtons = GameObject.Find("CompanionSelectButtons");

        if(CompanionPrefabs == null)
        {
            CompanionPrefabs = new GameObject[4];
            CompanionPrefabs[0] = Resources.Load<GameObject>("Prefabs/Bullets/Bullet");
            CompanionPrefabs[1] = Resources.Load<GameObject>("Prefabs/Bullets/Bullet");
            CompanionPrefabs[2] = Resources.Load<GameObject>("Prefabs/Bullets/Bullet");
            CompanionPrefabs[3] = Resources.Load<GameObject>("Prefabs/Bullets/Bullet");
        }

        if(scoreManager == null) scoreManager = new ScoreManager();

        currTime = 0;

        ShuffleItemList();
        TurnOffCompanionSelectButtons();
    }

    public float CurrTime
    {
        get { return currTime; }
    }

    private void Update()
    {
        currTime += Time.deltaTime;
    }

    public void GetCompanionCapsule()
    {
        TurnOnCompanionSelectButtons();
        remainCompanionCapsule++;
    }
    public void AddCompanion(int index)
    {
        // ���дϾ��� �߰��ϴ� ����
        CreateCompanion(itemList[index]);

        ShuffleItemList();
        remainCompanionCapsule--;
        if (remainCompanionCapsule == 0) TurnOffCompanionSelectButtons();
    }
    private void CreateCompanion(int type)
    {
        Instantiate(CompanionPrefabs[type]);
    }

    private void ShuffleItemList() 
    {
        // ��Ȯ���� �߿����� �ʰ�, 0~3 ������ ������ ������ �������� �ȴ�.
        int count = 0;
        while(count < numButton)
        {
        GetRandNum:
            int value = Random.Range(0, 4);
            for(int i = 0; i < count; i++)
            {
                if (itemList[i] == value) goto GetRandNum;
            }
            itemList[count++] = value;
        }
    }
    private void TurnOnCompanionSelectButtons() { CompanionSelectButtons.SetActive(true); }
    private void TurnOffCompanionSelectButtons() { CompanionSelectButtons.SetActive(false); }

    public ScoreManager scoreMng
    {
        get
        {
            return scoreManager;
        }
    }

}
