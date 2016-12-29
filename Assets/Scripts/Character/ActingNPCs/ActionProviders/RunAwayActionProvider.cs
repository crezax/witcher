using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class RunAwayActionProvider : ActionProvider {

  private MovementController movementController;

  protected override void OnAwake() {
    base.OnAwake();

    movementController = GetComponent<MovementController>();
  }

  public override void OnTargetSet(GameObject target) {
    if (target == null) {
      movementController.Stop();
    } else {
      movementController.MoveInDirection(
        transform.position - target.transform.position
      );
    }
  }

  public override void PerformAction(GameObject target) {
    return;
  }
}
