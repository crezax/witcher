using UnityEngine;

public class Knockback : Effect {

  public Vector3 Force { get; set; }

  public override bool IsValidTarget() {
    return GetComponent<Character>() != null || GetComponent<Rigidbody>() != null;
  }

  public override void OnEffectStart() {
    Character character = GetComponent<Character>();
    if (character == null) {
      GetComponent<Rigidbody>().AddForce(Force, ForceMode.Impulse);
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

  public override void OnEffectStay() {
    return;
  }

  public override void OnEffectEnd() {
    Character character = GetComponent<Character>();
    if (character == null) {
      return;
    }
    character.MovementController.CanMove = true;
  }
}
