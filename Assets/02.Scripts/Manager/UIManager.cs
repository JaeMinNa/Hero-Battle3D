using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("HP Health Bar")]
    [SerializeField] private Slider _playerSlider;
    [SerializeField] private Slider _enemySlider;
    [SerializeField] private CharacterHealth _playerHealth;
    [SerializeField] private CharacterHealth _enemyHealth;

    [Header("Skill CoolTime Image")]
    [SerializeField] private Image _image;

    [Header("Time Text")]
    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private TMP_Text _victoryTimeText;

    [Header("Panel")]
    [SerializeField] private GameObject _victoryPanel;
    [SerializeField] private GameObject _gameOverPanel;

    [field: SerializeField] public Player Player { get; private set; }
    private float _timer;

    private void Start()
    {
        SetPlayerHealthBar();
        SetEnemyHealthBar();
        _timer = 0;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        _timeText.text = _timer.ToString("N2");
    }

    IEnumerator COVictoryPanel()
    {
        yield return new WaitForSeconds(3f);
        Cursor.lockState = CursorLockMode.Confined;
        SetVictoryTime();
        _victoryPanel.gameObject.SetActive(true);
    }

    IEnumerator COGameOverPanel()
    {
        yield return new WaitForSeconds(3f);
        Cursor.lockState = CursorLockMode.Confined;
        _gameOverPanel.gameObject.SetActive(true);
    }

    public void VictoryPanel()
    {
        StartCoroutine(COVictoryPanel());
    }

    public void GameOverPanel()
    {
        StartCoroutine(COGameOverPanel());
    }

    public void SetVictoryTime()
    {
        float time = _timer;
        _victoryTimeText.text = time.ToString("N2");
        _timer = 0;
    }

    public void RetryButton()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void LobbyButton()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    public void ExitButton()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void CoolTimeCount()
    {
        StartCoroutine(CoolTimeRoutine());
    }

    IEnumerator CoolTimeRoutine()
    {
        float coolTime = Player.Data.SkillData.SkillCoolTime;
        float timer = 0f;
        while (true)
        {
            timer += Time.deltaTime;

            float per = timer / coolTime;

            _image.fillAmount = per;

            if (timer >= coolTime)
            {
                _image.fillAmount = 1f;
                break;
            }
            yield return null;
        }
    }

    public void SetPlayerHealthBar()
    {
        _playerSlider.value = (float)_playerHealth.health / _playerHealth.maxHealth;
    }

    public void SetEnemyHealthBar()
    {
        _enemySlider.value = (float)_enemyHealth.health / _enemyHealth.maxHealth;
    }
}
