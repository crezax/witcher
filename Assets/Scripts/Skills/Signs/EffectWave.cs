using System.Collections.Generic;
using UnityEngine;

public abstract class EffectWave : BaseBehaviour {

  protected abstract void InitEffect(GameObject effectGO);

  [SerializeField]
  private DelegateCollider[] colliders;
  private HashSet<GameObject> victims;
  [SerializeField]
  private GameObject effectPrefab;

  public GameObject Caster { get; set; }
  public virtual float Duration {
    get {
      return 1;
    }
  }

  protected override void OnAwake() {
    base.OnAwake();

    foreach (DelegateCollider collider in colliders) {
      collider.TriggerDidEnterEvent += HandleCollision;
    }
    victims = new HashSet<GameObject>();

    Destroy(gameObject, Duration);
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
    InitEffect(Effect.Apply(effectPrefab, victimRigidbody.gameObject));
  }
}
