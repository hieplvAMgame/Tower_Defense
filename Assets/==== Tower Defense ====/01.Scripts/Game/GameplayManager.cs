using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : Singleton<GameplayManager>
{
    [SerializeField] EntryManager entryManager;

    private UserLocalData localData;
    public UserLocalData LocalData => localData;
    private int currentWave = 0;
    public void GamePrepare()
    {
        localData = new UserLocalData(30, currentWave == 0 ? 500 : null, GameLose); // se khong set coin cua user
    }

    public void GameLose()
    {

    }
    protected override void Awake()
    {
        base.Awake();
        GamePrepare();
    }
}
public class UserLocalData
{
    private int _currentHp;
    private Action onLose;
    private int coin;
    public int CurrentHP
    {
        get => _currentHp;
        set
        {
            if (value < 0)
            {
                _currentHp = 0;
                onLose?.Invoke();
            }
            else
                _currentHp = value;
        }
    }
    public UserLocalData(int hp, int? coin, Action onLose = null)
    {
        this.onLose = onLose;
        _currentHp = hp;
        if (coin is not null)
            this.coin = coin.Value;
    }
    public void EarnCoin(int value)
    {
        if (value > 0)
            coin += value;
    }
    public void BurnCoin(int value, Action onFail = null)
    {
        if (value > 0 && value <= coin)
        {
            coin -= value;
        }
        else
        {
            onFail?.Invoke();
        }
    }
}