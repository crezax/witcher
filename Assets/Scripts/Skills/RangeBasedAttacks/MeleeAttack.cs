using UnityEngine;

public class MeleeAttack : RangeBasedAttack {
  [SerializeField]
  private GameObject stunEffectPrefab;

  protected override float CastTime {
    get {
      return AnimationConstants.ATTACK_DURATION;
    }
  }

  protected override void PerformImplementation(GameObject target) {
    foreach (GameObject targetInRange in AttackRange.PotentialTargets) {
      Health targetHealth = targetInRange.GetComponent<Health>();
      if (targetHealth != null) {
        // Should always be true, unless someone created character without
        // Health, or there is a non character layer triggering character
        // detection layer... Maybe it would be good to even throw if thats 
        // the case?
        targetHealth.CurrentValue -= Damage;
      }
      if (targetInRange.GetComponent<Character>() != null) {
        Effect.Apply(stunEffectPrefab, targetInRange.gameObject)
          .GetComponent<Effect>()
          .DurationLeft = .5f;
      }
    }
  }
}
