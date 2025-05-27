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
                        if (value >= _currentConfig.MaxHp)
                        {
                            OnHeal();
                            _currentHP = _currentConfig.MaxHp;
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
    public int CurrentLevel { get; protected set; }
    public UnitConfig[] Config;
    [ShowInInspector]
    public virtual Type_Unit TypeUnit { get; }
    [ShowInInspector]
    public virtual Type_Unit[] TargetUnitsType { get; }
    public bool IsAlive { get; protected set; }

    protected UnitConfig _currentConfig;
    public UnitConfig CurrentConfig => _currentConfig;
    protected Action onHurt = null, onHeal = null;
    [SerializeField] AttackSystem _attackSystem;
    public AttackSystem AttackSystem =>_attackSystem;

    // TODO: Refactor
    public event Action<UnitBase> onDie;
    [Button("Init")]
    public virtual void InitUnit( bool isReset = false)
    {
        if (isReset)
            CurrentLevel = 0;
        ApplyConfig();
        IsAlive = true;
        if (_attackSystem)
        {
            _attackSystem.Setup(this);
        }
    }
    public UnitBase AddEvent(Action onHurt = null, Action onHeal = null, Action<UnitBase> onDie = null)
    {
        this.onHeal = onHeal;
        this.onHurt = onHurt;
        this.onDie += onDie;
        return this;
    }
    public virtual void ApplyConfig(int id = -1)
    {
        // default
        if (id < 0)
        {
            _currentConfig = Config[CurrentLevel];
            CurrentHP = _currentConfig.MaxHp;
            return;
        }
        _currentConfig = Config[id];
        CurrentHP = _currentConfig.MaxHp;
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
        onDie?.Invoke(this);
        IsAlive = false;
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        onDie = null;
    }
}
