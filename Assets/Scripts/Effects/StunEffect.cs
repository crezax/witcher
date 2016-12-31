using UnityEngine;

public class StunEffect : Effect {
  protected override bool IsValidTargetImplementation(GameObject target) {
    return target.GetComponent<Character>() != null;
  }

  public override void OnEffectStart(GameObject target) {
    target.GetComponent<Character>().DisableAction();
    target.GetComponent<Character>().Animator.SetTrigger(AnimationConstants.GET_HIT);
  }

  public override void OnEffectStay(GameObject target) { }

  public override void OnEffectEnd(GameObject target) {
    target.GetComponent<Character>().EnableAction();
  }
}
