using System.Linq;
using UnityEngine;

public class MeleeAttack : Skill {
  [SerializeField]
  private CharacterDetector attackRange;

  public float BaseDamage { get; set; }
  public float BonusDamage { get; set; }
  public float Damage {
    get {
      return BaseDamage + BonusDamage;
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

  public override bool CanPerform(GameObject target) {
    if (target == null) {
      // I mean, swing the sword into the air, why not?
      return true;
    }
    Health targetHealth = target.GetComponent<Health>();
    if (targetHealth == null) {
      // I mean, swing the sword into the air, why not?
      return true;
    }
    // is target in range
    return attackRange.PotentialTargets.Contains(target);
  }

  protected override float CastTime {
    get {
      // Only animation we have is 2.767 long, so we don't really have a choice
      return 2.767f;
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

  protected override void PerformImplementation() {
    foreach (GameObject target in attackRange.PotentialTargets) {
      Health targetHealth = target.GetComponent<Health>();
      if (targetHealth != null) {
        // Should always be true, unless someone created character without
        // Health, or there is a non character layer triggering character
        // detection layer... Maybe it would be good to even throw if thats 
        // the case?
        targetHealth.CurrentValue -= Damage;
      }
    }
  }
}
