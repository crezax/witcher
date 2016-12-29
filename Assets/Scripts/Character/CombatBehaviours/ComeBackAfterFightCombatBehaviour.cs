using UnityEngine;

public class ComeBackAfterFightCombatBehaviour : CombatBehaviour {
  private Vector3 initialPosition;

  protected override void OnAwake() {
    base.OnAwake();

    initialPosition = transform.position;
  }

  public override void OnCombatLeave(Character character) {
    base.OnCombatLeave(character);

    character.GetComponent<MovementController>().MoveTo(initialPosition);
  }
}
