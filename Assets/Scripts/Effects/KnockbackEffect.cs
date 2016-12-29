using UnityEngine;

public class KnockbackEffect : Effect {

  public Vector3 Force { get; set; }

  public override bool IsValidTarget(GameObject target) {
    return target.GetComponent<Character>() != null ||
      target.GetComponent<Rigidbody>() != null;
  }

  public override void OnEffectStart(GameObject target) {
    Character character = target.GetComponent<Character>();
    if (character == null) {
      target.GetComponent<Rigidbody>().AddForce(Force, ForceMode.Impulse);
      return;
    } else {
      // Could add more fancy stuff with more animations, like target going into
      // "stunned" state with some other animation for the duration of effect
      DurationLeft = AnimationConstants.KNOCKBACK_DURATION;
      character.Animator.SetTrigger(AnimationConstants.KNOCKBACK);
      character.MovementController.Stop();
      character.MovementController.CanMove = false;
    }
  }

  public override void OnEffectStay(GameObject target) {
    return;
  }

  public override void OnEffectEnd(GameObject target) {
    Character character = target.GetComponent<Character>();
    if (character == null) {
      return;
    }
    character.MovementController.CanMove = true;
  }
}
