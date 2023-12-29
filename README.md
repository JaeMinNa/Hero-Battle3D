# 🖥️ Hero Battle 3D (나재민)

+ 쉬운 조작으로 적과 1:1 생동감있는 배틀에서 승리하세요!
+ 당신은 얼마나 빨리 승리할 수 있나요?
+ 단순한 조작 : W/A/S/D로 이동, 마우스 좌 클릭으로 공격, 마우스 우 클릭으로 스킬!

## 📆 Develop Schedule
* 23.12.26 ~ 23.12.28
<br/>

## ⚙️ Environment
- `Unity 2022.3.2f1`
- **IDE** : Visual Studio 2019
- **Envrionment** : PC `only`
- **Resolution** :	16:9 `Aspect`
<br/>

## ▶️ 게임 스크린샷
<p align="center">
  <img src="https://github.com/JaeMinNa/Hero-Battle3D/assets/149379194/f6851ad6-5c59-4793-8ff1-fec945690540" width="49%"/>
  <img src="https://github.com/JaeMinNa/Hero-Battle3D/assets/149379194/5e0c84ea-ba9a-4fe3-b709-62fae1548ba4" width="49%"/>
</p>
<p align="center">
  <img src="https://github.com/JaeMinNa/Hero-Battle3D/assets/149379194/0984f76f-d1e8-4c41-b3fb-bb1c4a581370" width="49%"/>
  <img src="https://github.com/JaeMinNa/Hero-Battle3D/assets/149379194/85510102-ab2a-45dc-9c96-987dabc6c0a4" width="49%"/>
</p>
<p align="center">
  <img src="https://github.com/JaeMinNa/Hero-Battle3D/assets/149379194/5b9a0d72-c012-4436-85a2-642a9976d4e8" width="49%"/>
  <img src="https://github.com/JaeMinNa/Hero-Battle3D/assets/149379194/5091c433-39b7-47b7-9179-47f1a6dab768" width="49%"/>
</p>

## 🎮 구현기능
* 필수요구사항
   * 인트로 씬 구성
   * 자유 게임 만들기  
* 선택요구사항
   * Instantiate 로 오브젝트 생성
   * InputAction 사용
   * 스크립트로 버튼에 이벤트 추가
   * FSM
* 추가구현사항
   * 시네머신 카메라
   * 콤보 공격
   * 스킬
   * 스킬 쿨타임 UI
   * 체력 UI
   * 클리어 시간
   * 사운드 추가
<br/>

## 🔍 세부내용
### FSM
* Player : Idle, Walk, Run, Attack, Skill State 구현
* Enemy : Idle, Chasing, Attack State 구현
<br/>

### 시네머신 카메라
* 시네머신 카메라로 생동감 있는 3인칭 구현
<br/>

### 콤보 공격
* 일정 시간 내 추가공격 시, 콤보 공격 가능
* Player 최대 3단 콤보공격 가능
<br/>

## ❓ 트러블 슈팅
### Enemy -> Player 피격 문제
<p align="center">
<img src="https://github.com/JaeMinNa/Hero-Battle3D/assets/149379194/f0795ce6-932f-45c6-aee7-e64311fd73f1" width="1000">
</p>
Player가 공격 시, Enemy 체력은 떨어지지만, Enemy가 아무리 공격을 해도 Player의 체력은 변함이 없었다. 
처음에는 코드에 문제가 있는줄 알고 거의 처음부터 모든 스크립트를 훑어본 것 같다. 하지만 해결이 되지 않았고, 에셋이 문제인건가.. 아니면 사용하지않던 터레인을 사용해서 그런가.. 100가지 정도 경우의 수를 생각해봤지만 답이 나오지 않았다. 
디버깅을 통해 콜라이더와 Player가 접촉하지 않는다는 것을 알게되었다. 하지만 콜라이더를 막상 또 확인을 하니 잘 만들어져 있었다. 
또 다시 원점으로 돌아가는 듯 했으나, 위 사진 처럼 콜라이더가 검을 휘두를 때 순간적으로 잠깐 나타났다가 검이 Player와 접촉할 때 쯤은 콜라이더가 없는 것을 확인했다. 정말 순식간에 나타났다가 없어졌기 때문에 눈으로 확인하기 어려워서 찾을 수가 없었던 것이다.
<br/>


```
float normalizedTime = GetNormalizedTime(stateMachine.Enemy.Animator, "Attack");
if (normalizedTime < 1f)
{
    if (normalizedTime >= stateMachine.Enemy.Data.ForceTransitionTime)
        TryApplyForce();

    if (!alreadyAppliedDealing && normalizedTime >= stateMachine.Enemy.Data.Dealing_Start_TransitionTime)
    {
        stateMachine.Enemy.Weapon.SetAttack(stateMachine.Enemy.Data.Damage, stateMachine.Enemy.Data.Force);
        stateMachine.Enemy.Weapon.gameObject.SetActive(true);
        alreadyAppliedDealing = true;
    }

    if (alreadyAppliedDealing && normalizedTime >= stateMachine.Enemy.Data.Dealing_End_TransitionTime)
    {
        stateMachine.Enemy.Weapon.gameObject.SetActive(false);
    }
}
```

<br/>

EnemyAttackState 스크립트 부분인데, stateMachine.Enemy.Data.Dealing_Start_TransitionTime에서 콜라이더가 켜지고 stateMachine.Enemy.Data.Dealing_End_TransitionTime에서 콜라이더가 꺼지도록 구현했다.
<p align="center">
<img src="https://github.com/JaeMinNa/Hero-Battle3D/assets/149379194/c37859e4-8f11-4c4d-bef9-2564a31f3834" width="1000">
</p>
EnemySO가 위 처럼 설정되어있어서 0.1초만에 콜라이더가 나타났다가 사라진 것이다. Player가 Enemy를 공격할 때는 Player는 콤보공격이 있기때문에 이 부분이 조금 다르게 설정되어 있어서 Player는 콜라이더가 Enemy와 충분히 충돌할 수 있도록 설정되어있었다. 
<p align="center">
<img src="https://github.com/JaeMinNa/Hero-Battle3D/assets/149379194/17bf9422-dba4-48d9-9ca0-aed9c38210e0" width="1000">
</p>
버그를 수정하고 피격이 잘 들어가는 모습이다!
<br/>

## 📒 프로젝트 소감
이번 개인 프로젝트를 하면서 가장 크게 느낀 것은 게임 제작은 기초 공사가 중요하다라는 것이다.
조금 귀찮고 어렵다고 초반부터 잘못된 방향으로 작업을 하게되면 이러한 부분이 스노우볼처럼 굴러와 나중에는 수정이 어려운 상황이 올 수도 있다라는 것을 느꼈다.
이번 프로젝트를 하면서 아쉬웠던 점은, 인벤토리가 항상 처음부터 정렬된다는 것이다. 큰 게임을 만들기 위해서는 슬롯 각각 독립적으로 Item을 관리하는 인벤토리가 필요하기 때문에 보완이 필요한 것 같다.
그리고 부모 클래스인 Item에 접근할 수가 없어서 Item 프리팹을 Tag로 나눈것은 효율적인 방법이 아니라고 생각한다. 정작 Item Tag가 필요한 상황에서 사용할 수 없다. 이 부분은 해결방법을 따로 공부하도록 하겠다.
지난번 프로젝트보다 많이 성장했다고 생각하지만, 아직 부족한 부분도 많았다. 다음 프로젝트에서는 더 좋은 프로젝트를 완성하겠다.


