using System.Collections;
using UnityEngine;

public abstract class Skill : BaseBehaviour {

  protected abstract float CastTime { get; }
  protected abstract float EffectTime { get; }
  protected abstract void PerformImplementation();
  public abstract bool CanPerform(GameObject target);
  protected abstract void PaySkillCost();

  private MovementController movementController;
  private Animator animator;

  public bool IsPerforming { get; private set; }

  protected override void OnAwake() {
    base.OnAwake();

    movementController = GetComponent<MovementController>();
    animator = GetComponent<Animator>();
  }

  public void Perform(GameObject target) {
    if (!CanPerform(target) || !movementController.CanMove) {
      return;
    }
    StartCoroutine(PerformCoroutine());
  }

  protected virtual string AnimationName {
    get {
      return AnimationConstants.SKILL;
    }
  }

  private IEnumerator PerformCoroutine() {
    animator.SetTrigger(AnimationName);

    if (movementController != null) {
      movementController.Stop();
      movementController.CanMove = false;
    }
    PaySkillCost();
    IsPerforming = true;
    yield return new WaitForSeconds(EffectTime);
    PerformImplementation();
    yield return new WaitForSeconds(CastTime - EffectTime);
    IsPerforming = false;
    if (movementController != null) {
      movementController.CanMove = true;
    }
  }
}
