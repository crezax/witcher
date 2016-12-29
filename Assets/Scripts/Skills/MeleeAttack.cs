using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeleeAttack : Skill {
  [SerializeField]
  private DelegateCollider attackRange;
  private List<Health> potentialTargets;

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

    potentialTargets = new List<Health>();
    attackRange.TriggerDidEnterEvent += OnAttackRangeEnter;
    attackRange.TriggerDidExitEvent += OnAttackRangeExit;
  }

  private void OnAttackRangeEnter(Collider collider) {
    Health health = collider.GetComponent<Health>();
    if (health == null) {
      return;
    }

    potentialTargets.Add(health);
  }

  private void OnAttackRangeExit(Collider collider) {
    Health health = collider.GetComponent<Health>();
    if (health == null) {
      return;
    }

    potentialTargets.Remove(health);
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
    return potentialTargets.Contains(targetHealth);
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
    // Again, Unity doesn't call OnTriggerExit when object is destroyed, so get
    // rid of nulls...
    potentialTargets = potentialTargets.Where(t => t != null).ToList();
    foreach (Health h in potentialTargets) {
      h.CurrentValue -= Damage;
    }
  }
}
