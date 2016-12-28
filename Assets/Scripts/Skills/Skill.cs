using System.Collections;
using UnityEngine;

public abstract class Skill : BaseBehaviour {

  protected abstract float CastTime { get; }
  protected abstract void PerformImplementation();
  protected abstract bool CanPerform { get; }
  protected abstract void PaySkillCost();

  private MovementController movementController;
  private Animator animator;

  public bool IsPerforming { get; private set; }

  protected override void OnAwake() {
    base.OnAwake();

    movementController = GetComponent<MovementController>();
    animator = GetComponent<Animator>();
  }

  public void Perform() {
    if (!CanPerform || !movementController.CanMove) {
      return;
    }
    StartCoroutine(PerformCoroutine());
  }

  private IEnumerator PerformCoroutine() {
    animator.SetTrigger("Skill");

    if (movementController != null) {
      movementController.Stop();
      movementController.CanMove = false;
    }
    PaySkillCost();
    IsPerforming = true;
    yield return new WaitForSeconds(CastTime);
    IsPerforming = false;
    PerformImplementation();
    if (movementController != null) {
      movementController.CanMove = true;
    }
  }
}
