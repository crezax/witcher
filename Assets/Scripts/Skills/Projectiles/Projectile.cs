using UnityEngine;

[RequireComponent(typeof(MovementController))]
public abstract class Projectile : BaseBehaviour {
  protected abstract void OnHit(Collider collider);

  public GameObject Caster { get; set; }

  protected override void OnTriggerDidEnter(Collider collider) {
    base.OnTriggerDidEnter(collider);

    if (collider.gameObject != Caster) {
      OnHit(collider);
    }
  }
}
