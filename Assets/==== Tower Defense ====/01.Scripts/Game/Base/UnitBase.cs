using Sirenix.Utilities.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
public abstract class UnitBase : MonoBehaviour
{
    private int _currentHP;
    [ShowInInspector]
    protected int CurrentHP
    {
        get => _currentHP;
        set
        {
            if (value <= 0)
            {
                _currentHP = 0;
                OnDie();
            }
            else
            {
                if (value != _currentHP)
                    try
                    {
                        if (value >= currentConfig.MaxHp)
                        {
                            _currentHP = currentConfig.MaxHp;
                        }
                        else if (value > _currentHP)
                        {
                            OnHeal();
                            _currentHP = value;
                            Debug.Log($"Heal {value}");
                        }
                        else
                        {
                            OnHurt();
                            _currentHP = value;
                            Debug.Log($"Minus {value}");
                        }
                    }
                    catch
                    {
                        Debug.LogError("No Config or may not init config. Check again !!!");
                    }
            }
        }
    }
    protected int CurrentLevel;
    public UnitConfig[] Config;
    public virtual Type_Unit TypeUnit { get; }
    public bool IsAlive { get; protected set; }

    protected UnitConfig currentConfig;
    protected Action onHurt = null, onHeal = null;
    Action<GameObject> onDie = null;
    [Button("Init")]
    public virtual void InitUnit(Action onHurt = null, Action onHeal = null, Action<GameObject> onDie = null)
    {
        CurrentLevel = 0;
        ApplyConfig(CurrentLevel);
        this.onHeal = onHeal;
        this.onHurt = onHurt;
        this.onDie = onDie;
    }
    public virtual void ApplyConfig(int id)
    {
        currentConfig = Config[id];
        CurrentHP = currentConfig.MaxHp;
    }

    public virtual void ChangeHp(int hp)
    {
        CurrentHP += hp;
    }
    public virtual void UpLevel(int level = 1)
    {
        CurrentLevel += level;
        if (CurrentLevel >= Config.Length)
            CurrentLevel = Config.Length - 1;
        ApplyConfig(CurrentLevel);
    }
    public virtual void OnHurt()
    {
        onHurt?.Invoke();
    }
    public virtual void OnHeal()
    {
        onHeal?.Invoke();
    }
    [Button]
    public virtual void OnDie()
    {
        onDie?.Invoke(this.gameObject);
        gameObject.SetActive(false);
    }
}
