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

  protected override bool CanPerform {
    get {
      return Energy.CurrentEnergy >= EnergyCost;
    }
  }

  protected override float CastTime {
    get {
      // 1.167 is the duration of skill animation, so I guess this will have to
      // do for prototype
      return 1f;
    }
  }

  protected override void PaySkillCost() {
    Energy.CurrentEnergy -= EnergyCost;
  }

  protected override void OnAwake() {
    base.OnAwake();

    Energy = GetComponent<Energy>();
  }
}
