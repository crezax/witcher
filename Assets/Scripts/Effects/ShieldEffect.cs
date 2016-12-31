using UnityEngine;

public class ShieldEffect : Effect {
  [SerializeField]
  private GameObject shieldPrefab;

  public override bool IsPositive {
    get {
      return true;
    }
  }

  protected override bool IsValidTargetImplementation(GameObject target) {
    return target.GetComponent<Health>() != null;
  }

  public override void OnEffectStart(GameObject target) {
    // Make sure we don't put double shield on a character
    foreach (Effect effect in GetEffectsOnTarget(target)) {
      if (effect is ShieldEffect && effect != this) {
        Destroy(effect.gameObject);
      }
    }

    // Quen shield removes all negative effects, should there be spells that
    // only remove negative effects, or shield without removing negatives, we
    // can just move this to separate effect and make Quen apply both
    foreach (Effect effect in GetEffectsOnTarget(target)) {
      if (!effect.IsPositive) {
        Destroy(effect.gameObject);
      }
    }

    target.GetComponent<Health>().OnValueWillChange += OnTargetAttacked;
  }

  private float OnTargetAttacked(float oldValue, float newValue) {
    if (oldValue > newValue) {
      // Block damage
      ShowShield();
      Destroy(gameObject);
      return oldValue;
    }
    return newValue;
  }

  public override void OnEffectStay(GameObject target) { }

  public override void OnEffectEnd(GameObject target) {
    target.GetComponent<Health>().OnValueWillChange -= OnTargetAttacked;
  }

  protected override void OnOtherEffectApplied(Effect effect) {
    base.OnOtherEffectApplied(effect);

    if (!effect.IsPositive) {
      // Block negative effect
      ShowShield();
      Destroy(gameObject);
      Destroy(effect.gameObject);
    }
  }

  private void ShowShield() {
    Destroy(
      Instantiate(shieldPrefab, Target.transform, false),
      .5f
    );
  }
}
