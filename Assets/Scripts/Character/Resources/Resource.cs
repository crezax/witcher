using UnityEngine;

public abstract class Resource : BaseBehaviour {
  [SerializeField]
  private float baseMaxValue;
  private float currentValue;

  [SerializeField]
  private float baseRegen;
  private float bonusRegen;

  public delegate void ValueChangedDelegate(float oldValue, float newValue);
  public event ValueChangedDelegate OnValueChanged;

  public delegate float ValueWillChangeDelegate(float oldValue, float newValue);
  public event ValueWillChangeDelegate OnValueWillChange;

  protected virtual bool ShouldRegen {
    get {
      return true;
    }
  }

  public float BaseMaxValue {
    get {
      return baseMaxValue;
    }
    set {
      baseMaxValue = value;
      currentValue = value;
    }
  }

  public float BonusMaxValue { get; set; }

  public float BaseRegenRate {
    get {
      return baseRegen;
    }
    set {
      baseRegen = value;
    }
  }

  public float BonusRegenRate {
    get {
      return bonusRegen;
    }
    set {
      bonusRegen = value;
    }
  }

  public float MaxValue {
    get {
      return BaseMaxValue + BonusMaxValue;
    }
  }

  public float CurrentValue {
    get {
      return currentValue;
    }
    set {
      float oldValue = currentValue;
      if (OnValueWillChange != null) {
        value = OnValueWillChange(oldValue, value);
      }
      currentValue = Mathf.Min(MaxValue, Mathf.Max(value, 0));
      if (OnValueChanged != null) {
        OnValueChanged(oldValue, currentValue);
      }
    }
  }

  public float RegenRate {
    get {
      return BaseRegenRate + BonusRegenRate;
    }
  }

  protected override void OnAwake() {
    base.OnAwake();

    CurrentValue = MaxValue;
  }

  protected override void OnUpdate() {
    base.OnUpdate();

    baseRegen = BaseRegenRate;
    bonusRegen = BonusRegenRate;

    if (ShouldRegen) {
      CurrentValue = CurrentValue + RegenRate * Time.deltaTime;
    }
  }
}
