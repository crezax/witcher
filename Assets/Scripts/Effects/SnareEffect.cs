using UnityEngine;

public class SnareEffect : Effect {
  public override bool IsPositive {
    get {
      return false;
    }
  }

  protected override bool IsValidTargetImplementation(GameObject target) {
    return target.GetComponent<Character>() != null;
  }

  public override void OnEffectStart(GameObject target) {
    DurationLeft = 5;
    target.GetComponent<Character>().DisableMovement();
  }

  public override void OnEffectStay(GameObject target) { }

  public override void OnEffectEnd(GameObject target) {
    if (target.GetComponent<MovementController>() == null) {
      return;
    }
    target.GetComponent<Character>().EnableMovement();
  }
}
