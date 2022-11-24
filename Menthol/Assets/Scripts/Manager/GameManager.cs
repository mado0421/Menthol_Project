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
    // GameScene의 역할
    //  - PlayerCharacter.Die() -> ChangeScene("ResultScene");
    //  - EnemyCharacter.Die() -> ScoreManager.AddScore();
    //  - PlayerCharacter가 컴패니언 캡슐을 얻으면 GetCompanionCapsule() 호출
    //     remainCompanionCapsule++;
    //  - GetCompanionCapsule() 호출되면 CompanionSelectButtons를 활성화
    //  - CompanionSelectButtons는 각 버튼에 1 ~ numTypeOfCompanion를 임의로 배정
    //     배정 시점은? -> 처음 생성될 때, 선택을 완료했을 때
    //     (이래야 remainCompanionCapsule 남아있으면 계속 할 수 있음)
    //  - 버튼을 누르면 해당 컴패니언을 AddCompanion()함.
    //  - AddCompanion()하면 remainCompanionCapsule--;
    //  - remainCompanionCapsule이 0이 될 때까지 계속 선택할 수 있도록.
    //  - 0이 되면 CompanionSelectButtons를 비활성화
    //  
    // 근데 이걸 걍 GameManager에서 전부 하면?
    // 다른 Scene일 때, GetCompanionCapsule()를 부를 수 있으면 안되는거 아냐???
    // 
    // SceneContext 클래스를 만들고,
    // GameManager에서는 SceneContext의 부모 클래스 함수로
    // 특정 Scene에서는 GameManager.GameSceneContext.
    // 아아 모르겠다~ 어떻게 하는지 한 번 알아보는게 좋을 듯.
    // GameManager에서 Score 관리를 하는 것은 괜찮은가?
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

        // GameManager는 Scene이 변경될 때도 계속 살아있기 때문에
        // CompanionSelectButtons Object가 아직 생성이 안되어 있을 수 있다.
        // InitializeGameScene()의 호출 타이밍을 잘 정해야 할 것.
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
        // 컴패니언을 추가하는 내용
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
        // 정확도가 중요하지 않고, 0~3 사이의 임의의 정수만 구해지면 된다.
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
