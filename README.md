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
<img src="https://github.com/JaeMinNa/Hero-Battle3D/assets/149379194/f0795ce6-932f-45c6-aee7-e64311fd73f1" width="49%">
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
<img src="https://github.com/JaeMinNa/Hero-Battle3D/assets/149379194/c37859e4-8f11-4c4d-bef9-2564a31f3834" width="49%">
</p>
EnemySO가 위 처럼 설정되어있어서 0.1초만에 콜라이더가 나타났다가 사라진 것이다. Player가 Enemy를 공격할 때는 Player는 콤보공격이 있기때문에 이 부분이 조금 다르게 설정되어 있어서 Player는 콜라이더가 Enemy와 충분히 충돌할 수 있도록 설정되어있었다. 
<p align="center">
<img src="https://github.com/JaeMinNa/Hero-Battle3D/assets/149379194/17bf9422-dba4-48d9-9ca0-aed9c38210e0" width="49%">
</p>
버그를 수정하고 피격이 잘 들어가는 모습이다!
<br/>

## 📒 프로젝트 소감
항상 느끼는 것은 머리속에 구현하고 싶은 욕심은 엄청 많은데 정작 1개를 구현하는데 최소 1~2시간이라는 것이다. 
이번 개인과제도 개인적으로 적의 스킬 사용, Player 회피 스킬, 아이템 등 여러가지 구현하고 싶었으나 제출 기한을 맞추기 위해 1:1 배틀까지만 구현하고 마무리했다. 
아직까지 개발 레벨이 많이 높지않아서 막히는 부분이 많아서 그런 것 같다. 그리고 새로 느낀 새로운 교훈은 '애니메이션은 누군가가 만들어 놓은 것을 사용하자...' 이다. 
Player의 스킬을 구현할 애니메이션이 부족해서 다른 캐릭터의 애니메이션을 사용했는데 정말 힘들게 스킬 구현을 마쳤지만 애니메이션 오류인지 스킬을 사용할 때 마다 발에서부터 조금씩 땅 속으로 들어가는 버그를 발견했다. 
애써서 몇시간동안 구현했는데 제일 마지막이 해결할 수 없는 버그라서 맥이 너무 빠졌다. 결국 기존 사용했던 애니메이션을 사용해서 스킬구현을 했지만 정말 큰 또 다른 교훈을 얻었다.
다음 프로젝트에서는 조금더 넉넉하게 시간적 여유를 가지고 작업을 하고, 리팩토리까지 해보겠다!


