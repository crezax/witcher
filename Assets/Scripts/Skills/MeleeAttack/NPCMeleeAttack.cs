using UnityEngine;

public class NPCMeleeAttack : MeleeAttack {
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
    return AttackRange.PotentialTargets.Contains(target);
  }
}
