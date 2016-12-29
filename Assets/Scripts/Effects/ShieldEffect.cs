using UnityEngine;

public class ShieldEffect : Effect {
  [SerializeField]
  private GameObject shieldPrefab;

  public override bool IsValidTarget(GameObject target) {
    return target.GetComponent<Health>() != null;
  }

  public override void OnEffectStart(GameObject target) {
    // Make sure we don't put double shield on a character
    ShieldEffect[] shieldEffects = target.GetComponentsInChildren<ShieldEffect>();
    foreach (ShieldEffect shieldEffect in shieldEffects) {
      if (shieldEffect != this) {
        Destroy(shieldEffect.gameObject);
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

  public override void OnEffectStay(GameObject target) {
    return;
  }

  public override void OnEffectEnd(GameObject target) {
    target.GetComponent<Health>().OnValueWillChange -= OnTargetAttacked;
  }
}
