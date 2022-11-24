# Project Menthol
Unity 학습을 위한 Vampire Survivors 컨셉의 간단한 쿼터뷰 게임 만들기 프로젝트

이후에 다시 점검해야 할 사항

 - [ ] 오브젝트 풀링 방식으로 변경
 - [ ] 컴패니언이 따라갈 PC의 궤적 생성 방식 보완
 - [ ] 씬 관리, 점수 관리, 컴패니언 추가 등의 기능이 전부 GameManager에 모여있는 문제 해결

### 22.11.21
> 1. 프로젝트  생성
> 2. Scene의 구조 정함
> 3. Character 클래스 작성
> 4. Actor 클래스 작성
> 5. FireActor 클래스에 필요한 TargetFinder 클래스 작성

 - 대략적으로 씬들의 구조를 정했다. 시작부터 너무 크게 목표를 정하면 착수에 힘이 많이 들기 때문에 처음 목표는 최대한 간단하게 정하되, 이후에 어떤 방식으로 확장할 수 있을지 정도만 생각해두자.
 - 시작하면 플레이어 캐릭터(이하 PC)는 계속 보는 방향으로 전진한다. 플레이어는 PC를 'A'키와 'D'를 통해 이동방향을 꺾는 식으로만 조종할 수 있다.
 - 일정 시간마다 플레이어를 향해 접근하는 적 캐릭터가 화면 밖에서 생성된다. 일반적인 적 캐릭터와 다르게 체력이 많은 '강한 적 캐릭터'가 존재하며, 해당 캐릭터는 죽을 때 '컴패니언 캡슐' 아이템을 죽은 자리에 떨어뜨린다. 플레이 타임이 길어질 수록 난이도가 상승하며, 적 캐릭터들의 다음 생성까지 걸리는 시간, 적 캐릭터들의 기초 체력 등이 영향을 받는다.
- 플레이어가 '컴패니언 캡슐'를 얻으면 어떤 컴패니언 캐릭터(이하 컴패니언)를 추가할 것인지 선택할 수 있는 선택지 버튼 3개가 화면에 등장한다. 해당 선택지는 4가지 컴패니언 중 임의로 3가지가 등장하며, 선택하면 '컴패니언 캡슐'을 하나 소모하여 선택한 컴패니언을 씬에 추가한다.
- 모든 Character는 자신이 가진 Actor에 따라 Act()한다. (PC는 Player Actor, MachinegunCompanionCharacter는 MachinegunActor 등)
- 공격을 위해 투사체 객체를 생성하고 특정 위치를 향해 발사하는 캐릭터들을 위한 FireActor를 만들고, 해당 Actor가 공격할 위치를 찾기 위해 사용할 TargetFinder 클래스와 해당 클래스를 상속한 ClosestTargetFinder 클래스를 작성했다.

TargetFinder는 적의 위치를 찾기 위한 기능만을 제공할 예정이므로, Monobehaviour 객체로 만들지 않고 인터페이스처럼 작성하였다.
다만, Monobehaviour 객체가 아닐 경우, transform, OnTriggerEnter() 등이 제공되지 않으므로 충돌체를 사용하여 사거리 내 적 캐릭터를 찾는 방식을 사용하지 않고, 해당 기능을 사용할 객체의 position과 range를 받아 씬 내의 EnemyPool 객체 아래 존재하는 적 캐릭터들을 대상으로 찾는 방식을 사용하였다.

![image](https://user-images.githubusercontent.com/21697638/203860604-5eff85d5-6a72-4a14-9a3a-24e662df0926.png)
![image](https://user-images.githubusercontent.com/21697638/203860555-7f1e5a1d-9e4a-4eeb-97d2-0097c9a37865.png)

### 22.11.22

> 1. TargetFinder 클래스 작성
> 2. FireActor 클래스 작성
> 3. EnemySpawner 클래스 작성
> 4. Layer 구분을 통한 충돌 검사 분리

 - ClosestTargetFinder 클래스를 마저 완성하고 FireActor에 추가했다.
 - 직접사격을 하는 StraightFireActor와 간접사격을 하는 ArtilleryFireActor를 추가하고 StraightFireActor를 먼저 작성했다.
 - FireActor 내에서 다음 발포까지의 Delay 코루틴을 사용하기 위해 Monobehaviour 객체로 작성했다. 또한, TargetFinder를 사용하려면 transform에 접근하여 position 값을 얻어오는 것도 필요하다.
 - C#은 인자 전달 시에 사용할 const 한정자가 없었다.
 - FireActor는 RPM 값을 갖는다.
 - Bullet 클래스를 작성하고 해당 클래스를 사용하는 프리팹을 만들었다.
 - Layer를 통해 PlayerBullet과 Enemy, Boundary만 충돌 검사 하도록 작성했다.
 - 모든 Actor와 TargetFinder, Bullet 등은 전부 플레이어가 사용한다고 가정하고 작성했다. 이후에 적 캐릭터가 원거리 공격을 하게 된다면 해당 사양을 추가해야 한다.
 - EnemySpawner 클래스를 작성하고 일정 시간마다 적을 생성하게 했다.
 - Bullet이나 적 캐릭터, 컴패니언 등의 모든 객체는 Instantiate() 로 생성하고 있다. 객체 풀링을 그동안 직접 구현해왔었는데 지금 사용하는 버전의 유니티에서 객체 풀링 기능을 지원한다고 알고 있다. 추후에 해당 기능을 사용하도록 변경해야 한다.

![image](https://user-images.githubusercontent.com/21697638/203860713-0df34591-facb-42cc-9a82-0c8cc5fe118c.png)
![image](https://user-images.githubusercontent.com/21697638/203861064-ac11dd49-87cb-4ded-b145-cfa699cc5264.png)

### 22.11.23

> 1. 컴패니언이 따라올 수 있도록 Trail를 만들 수 있는 TrailGenerator 클래스 작성

- 이전에 PC의 이동 궤적을 따라 다른 객체가 따라가는 기능을 구현할 때는 TrailRenderer를 사용했었다. 해당 기능을 제공하는 게 본 목적이 아닌 컴포넌트를 사용하는 것이 마음에 들지 않아 다른 방식으로 구현했다.
- 간단하게 구현할 수 있을 줄 알았는데 예상 외로 시간이 오래 걸렸다.
- TrailGenerator 클래스는 일정 시간마다 기준점이 될 위치를 저장하고, 해당 기준점들의 위치를 시간에 따라 보간하여 얻어낸 위치를 함수로 제공한다.
- 처음엔 유니티에서 제공하는 Lerp()를 이용하여 선형 보간 했는데, 기준점마다 딱딱 끊어지듯이 꺾이는 모습이 마음에 들지 않아 DirectX 프로젝트에서 사용했었던 Catmullrom() 함수를 가져와서 사용하였다.
- 다만, 완벽하게 원하는 대로 부드럽게 따라오는 것이 아닌, 텐션이 있게 따라오고 있다보니 마음에 들지 않는다. 기준점의 개수가 적어서 생기는 현상인지 확인해보진 않았지만 계속 매달려 있기엔 시간을 너무 많이 써서 다음 작업으로 넘어갔다.

![image](https://user-images.githubusercontent.com/21697638/203861072-3d12132f-4b10-4e04-beb6-168b2eb56d6f.png)

### 22.11.24

> 1. Scene 전환이나 Scene 간 값 전달을 위한 GameManager 작성
> 2. Button과 Text 등의 UI 틀 작성

- 씬 변경, 버튼 입력, 킬 카운터 등의 기능이 필요하다.
- GameScene에서 ResultScene으로 전환되면 ResultScene에서 띄울 킬 카운터(또는 점수)와 생존한 시간, 플레이 도중 모은 컴패니언 종류 등의 값도 전달할 수 있어야 하므로 이런 정보를 GameManager에 담고 해당 클래스를 씬 전환 시에도 파괴되지 않도록 작성하였다.
- 다만, 이런 방식이 깔끔한 방식인지는 의문이다. GameManager가 책임지는 기능이 너무 많다. Scene의 전환 조건이나, 점수 관리, 컴패니언 캡슐 획득과 선택으로 인한 컴패니언 추가 등의 자잘한 기능들이 전부 GameManager라는 이름 아래 모여있다보니 상당히 마음에 들지 않는다. 하지만 오랜 시간을 들여봐도 진도가 나가지 않아 일단 임시로 작성했다.

유니티에서 Text UI를 사용하려 했더니 새로운 패키지가 추가됐다. TextMeshPro 컴포넌트를 스크립트 상에서 접근하기 위해선 다음과 같은 방식을 사용해야 한다.
자료형이 TextMeshProUGUI 인 점만 알면 된다.
```C#
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timeText;
    [SerializeField]
    private TextMeshProUGUI scoreText;

    private void Start()
    {
        timeText = GameObject.Find("Time").GetComponent<TextMeshProUGUI>();
        scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        timeText.text = "Time: " + GameManager.Instance.CurrTime.ToString("F0");
        scoreText.text = "Score: " + GameManager.Instance.scoreMng.GetScore();
    }
}
```
![image](https://user-images.githubusercontent.com/21697638/203861076-842d4753-3890-4bd5-a811-b6e66c55b6f6.png)
![화면 캡처 2022-11-25 053626](https://user-images.githubusercontent.com/21697638/203867523-a06ca0ed-d2ef-46fd-95d2-79cbe512cddb.png)


> Written with [StackEdit](https://stackedit.io/).