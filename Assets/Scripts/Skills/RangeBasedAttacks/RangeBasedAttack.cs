using UnityEngine;

public abstract class RangeBasedAttack : Skill {
  [SerializeField]
  private CharacterDetector attackRange;

  public float BaseDamage { get; set; }
  public float BonusDamage { get; set; }
  public float Damage {
    get {
      return BaseDamage + BonusDamage;
    }
  }

  protected CharacterDetector AttackRange {
    get {
      return attackRange;
    }
  }

  protected override void OnAwake() {
    base.OnAwake();

    // Some defaults
    BaseDamage = 10;
  }

  protected override string AnimationName {
    get {
      return AnimationConstants.ATTACK;
    }
  }

  protected override float EffectTime {
    get {
      // Sword swings at more less that time
      return 1;
    }
  }

  protected override void PaySkillCost() {
    // No cost!
    return;
  }

  protected override bool CanPerformImplementation(GameObject target) {
    if (target == null) {
      // I mean, swing the sword into the air, why not?
      return true;
    }
    Health targetHealth = target.GetComponent<Health>();
    if (targetHealth == null) {
      // I mean, swing the sword into the air, why not?
      return true;
    }
    if (targetHealth.CurrentValue == 0) {
      // Let's not attack a dead one, ok?
      return false;
    }
    // is target in range
    return AttackRange.PotentialTargets.Contains(target);
  }
}
