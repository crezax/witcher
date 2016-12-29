using UnityEngine;

public class SnareEffect : Effect {
  public override bool IsValidTarget(GameObject target) {
    return target.GetComponent<MovementController>() != null;
  }

  public override void OnEffectStart(GameObject target) {
    DurationLeft = 5;
    target.GetComponent<MovementController>().CanMove = false;
  }

  public override void OnEffectStay(GameObject target) { }

  public override void OnEffectEnd(GameObject target) {
    if (target.GetComponent<MovementController>() == null) {
      return;
    }
    target.GetComponent<MovementController>().CanMove = true;
  }
}
