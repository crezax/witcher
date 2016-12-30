using UnityEngine;

public class PlayerMeleeAttack : MeleeAttack {
  public override bool CanPerform(GameObject target) {
    return true;
  }

  protected override void PerformImplementation(GameObject target) {
    if (AttackRange.PotentialTargets.Contains(target)) {
      base.PerformImplementation(target);
    } else {
      // Should be a jump towards target with proper animation
      SkillUser.MovementController.Follow(target);
    }
  }
}
