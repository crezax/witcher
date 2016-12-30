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

  protected override bool CanPerformImplementation(GameObject target) {
    return Energy.CurrentValue >= EnergyCost;
  }

  protected override float CastTime {
    get {
      return AnimationConstants.CASTING_DURATION;
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
