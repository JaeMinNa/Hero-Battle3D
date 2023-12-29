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

<img src="https://github.com/JaeMinNa/SpartanDungeonUnityProject/assets/149379194/87189de8-efd6-4f41-a59d-d693c3630f1a" width="1000">

```
if(item.tag == "WeaponItem")
{
    weapon = item.GetComponent<Weapon>();
    if(weapon.isEquip == true)
    {
        InventorySlotTempImage[count].transform.Find($"EquipButton{count}/Text (TMP)").gameObject.SetActive(true);
    }
}
else
{
    armor = item.GetComponent<Armor>();
    if (armor.isEquip == true)
    {
        InventorySlotTempImage[count].transform.Find($"EquipButton{count}/Text (TMP)").gameObject.SetActive(true);
    }
}
```
<br/>

### 9개의 버튼 함수 생성
인벤토리에서 각각 슬롯을 만들어서 인스펙터 창에서 연결했기 때문에, 9개의 버튼을 생성했다. 이 버튼들은 각각 이 버튼들이 가지고 있는 Text에 접근해서
gameObject를 활성화해서 장착[E] 표시를 해야 한다. 그렇게되면 각각 버튼에 연결할 9개의 함수가 필요해서, 불필요하게 코드가 엄청 길어졌다. 
그래서 EventSystem를 사용해서 마지막에 클릭한 버튼의 이름에 접근해서, 모든 버튼에 적용가능한 하나의 함수를 작성했다.
<br/>

```
using UnityEngine.EventSystems;

public void EquipButton()
{
string str = GetButtonName().Substring(11);
int count = int.Parse(str);

if(inventory.InventorySlot[count].tag == "WeaponItem")
{
  weapon = inventory.InventorySlot[count].GetComponent<Weapon>();
  if (weapon.isEquip == true)
  {
      weapon.isEquip = false;
      inventory.InventorySlotTempImage[count].transform.Find($"EquipButton{count}/Text (TMP)").gameObject.SetActive(false);
      equipment.DisEquip(inventory.InventorySlot[count]);
  }
  else
  {
      weapon.isEquip = true;
      inventory.InventorySlotTempImage[count].transform.Find($"EquipButton{count}/Text (TMP)").gameObject.SetActive(true);
      equipment.Equip(inventory.InventorySlot[count]);
  }
}
else
{
  armor = inventory.InventorySlot[count].GetComponent<Armor>();
  if (armor.isEquip == true)
  {
      armor.isEquip = false;
      inventory.InventorySlotTempImage[count].transform.Find($"EquipButton{count}/Text (TMP)").gameObject.SetActive(false);
      equipment.DisEquip(inventory.InventorySlot[count]);
  }
  else
  {
      armor.isEquip = true;
      inventory.InventorySlotTempImage[count].transform.Find($"EquipButton{count}/Text (TMP)").gameObject.SetActive(true);
      equipment.Equip(inventory.InventorySlot[count]);
  }
}

}

public string GetButtonName()
{
    string EventButtonName = EventSystem.current.currentSelectedGameObject.name;

    return EventButtonName;
}
```
<br/>

## 📒 프로젝트 소감
이번 개인 프로젝트를 하면서 가장 크게 느낀 것은 게임 제작은 기초 공사가 중요하다라는 것이다.
조금 귀찮고 어렵다고 초반부터 잘못된 방향으로 작업을 하게되면 이러한 부분이 스노우볼처럼 굴러와 나중에는 수정이 어려운 상황이 올 수도 있다라는 것을 느꼈다.
이번 프로젝트를 하면서 아쉬웠던 점은, 인벤토리가 항상 처음부터 정렬된다는 것이다. 큰 게임을 만들기 위해서는 슬롯 각각 독립적으로 Item을 관리하는 인벤토리가 필요하기 때문에 보완이 필요한 것 같다.
그리고 부모 클래스인 Item에 접근할 수가 없어서 Item 프리팹을 Tag로 나눈것은 효율적인 방법이 아니라고 생각한다. 정작 Item Tag가 필요한 상황에서 사용할 수 없다. 이 부분은 해결방법을 따로 공부하도록 하겠다.
지난번 프로젝트보다 많이 성장했다고 생각하지만, 아직 부족한 부분도 많았다. 다음 프로젝트에서는 더 좋은 프로젝트를 완성하겠다.


