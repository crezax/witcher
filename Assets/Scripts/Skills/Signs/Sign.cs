using UnityEngine;

public abstract class Sign : Skill {
  protected abstract float EnergyCost { get; }

  [SerializeField]
  private Sprite icon;
  protected Energy Energy { get; private set; }

  public Sprite Icon {
    get {
      return icon;
    }
  }

  public override bool CanPerform(GameObject target) {
    return Energy.CurrentValue >= EnergyCost;
  }

  protected override float CastTime {
    get {
      // 1.167 is the duration of skill animation, so I guess this will have to
      // do for prototype
      return 1.167f;
    }
  }

  protected override float EffectTime {
    get {
      return 1f;
    }
  }

  protected override void PaySkillCost() {
    Energy.CurrentValue -= EnergyCost;
  }

  protected override void OnAwake() {
    base.OnAwake();

    Energy = GetComponent<Energy>();
  }
}
