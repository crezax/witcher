using UnityEngine;

public class IncapacitateEffect : Effect {
  public override bool IsValidTarget(GameObject target) {
    return target.GetComponent<Character>() != null;
  }

  public override void OnEffectStart(GameObject target) {
    Character targetCharacter = target.GetComponent<Character>();
    targetCharacter.Health.OnValueChanged += OnCharacterTookDamage;
    targetCharacter.DisableAction();
    targetCharacter.Animator.SetBool(AnimationConstants.IS_STUNNED, true);
  }

  private void OnCharacterTookDamage(float oldValue, float newValue) {
    if (oldValue <= newValue) {
      return;
    } else {
      Destroy(gameObject);
    }
  }

  public override void OnEffectStay(GameObject target) { }

  public override void OnEffectEnd(GameObject target) {
    Character targetCharacter = target.GetComponent<Character>();
    targetCharacter.EnableAction();
    targetCharacter.Health.OnValueChanged -= OnCharacterTookDamage;

    foreach (Effect effect in GetEffectsOnTarget(target)) {
      if (effect is IncapacitateEffect && effect != this) {
        // There are still incapacitating effects on target, don't stop stun
        // animation
        return;
      }
    }
    targetCharacter.Animator.SetBool(AnimationConstants.IS_STUNNED, false);
  }

}
