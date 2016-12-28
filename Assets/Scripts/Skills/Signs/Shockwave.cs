using System.Collections.Generic;
using UnityEngine;

public class Shockwave : BaseBehaviour {
  [SerializeField]
  private DelegateCollider[] colliders;
  private HashSet<GameObject> victims;

  public GameObject Caster { get; set; }
  public float Power { get; set; }

  protected override void OnAwake() {
    base.OnAwake();

    foreach (DelegateCollider collider in colliders) {
      collider.TriggerDidEnterEvent += HandleCollision;
    }
    victims = new HashSet<GameObject>();

    Power = 20;
    Destroy(gameObject, 1);
  }

  private void HandleCollision(Collider collider) {
    Rigidbody victimRigidbody = collider.GetComponent<Rigidbody>();
    if (victimRigidbody == null) {
      // We cannot apply forces to non-rigidbodies, so surely this object is
      // not intended to be thrown around
      return;
    }
    if (Caster == collider.gameObject) {
      // We don't want to push ourselves by accident
      return;
    }
    if (victims.Contains(collider.gameObject)) {
      return;
    }
    victims.Add(collider.gameObject);
    victimRigidbody.AddForce(transform.forward * Power, ForceMode.Impulse);
  }
}
