using UnityEngine;

public class ShieldEffect : Effect {
  [SerializeField]
  private GameObject shieldPrefab;

  public override bool IsValidTarget(GameObject target) {
    return target.GetComponent<Health>() != null;
  }

  public override void OnEffectStart(GameObject target) {
    // Make sure we don't put double shield on a character
    foreach (Effect effect in GetEffectsOnTarget(target)) {
      if (effect is ShieldEffect && effect != this) {
        Destroy(effect.gameObject);
      }
    }

    target.GetComponent<Health>().OnValueWillChange += OnTargetAttacked;
  }

  private float OnTargetAttacked(float oldValue, float newValue) {
    if (oldValue > newValue) {
      Destroy(
        Instantiate(shieldPrefab, Target.transform, false),
        .5f
      );
      Destroy(gameObject);
      return oldValue;
    }
    return newValue;
  }

  public override void OnEffectStay(GameObject target) { }

  public override void OnEffectEnd(GameObject target) {
    target.GetComponent<Health>().OnValueWillChange -= OnTargetAttacked;
  }
}
