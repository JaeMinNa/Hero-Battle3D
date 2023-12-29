using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public UIManager UIManager { get; private set; }
    public PlayerManager PlayerManger { get; private set; }
    [field: SerializeField] public SoundManager SoundManager { get; private set; }

    void Awake()
    {
        Instance = this;

        UIManager = GetComponentInChildren<UIManager>();
        PlayerManger = GetComponentInChildren<PlayerManager>();
    }
}
