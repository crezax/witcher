using UnityEngine;

public abstract class Resource : BaseBehaviour {
  private float baseMaxValue;
  private float currentValue;

  public float baseRegen;
  public float bonusRegen;

  public delegate void ValueChangedDelegate(float oldValue, float newValue);
  public event ValueChangedDelegate OnValueChanged;

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

  protected override void OnUpdate() {
    base.OnUpdate();

    baseRegen = BaseRegenRate;
    bonusRegen = BonusRegenRate;

    if (ShouldRegen) {
      CurrentValue = CurrentValue + RegenRate * Time.deltaTime;
    }
  }
}
