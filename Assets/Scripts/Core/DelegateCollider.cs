using UnityEngine;

// Unity doesn't support having multiple colliders on one gameObject, so if our
// script logic requires 2 or more colliders/triggers, we can create a child
// object, attach collider and this script to it. Then the main script can
// use DelegateCollider to operate on multiple colliders/triggers
public class DelegateCollider : BaseBehaviour {
  public delegate void TriggerDidEnterHandler(Collider collider);
  public event TriggerDidEnterHandler TriggerDidEnterEvent;
  public delegate void TriggerHandler(Collider collider);
  public event TriggerHandler TriggerEvent;
  public delegate void TriggerDidExitHandler(Collider collider);
  public event TriggerDidExitHandler TriggerDidExitEvent;
  public delegate void CollisionDidEnterHandler(Collision collision);
  public event CollisionDidEnterHandler CollisionDidEnterEvent;
  public delegate void CollisionHandler(Collision collision);
  public event CollisionHandler CollisionEvent;
  public delegate void CollisionDidExitHandler(Collision collision);
  public event CollisionDidExitHandler CollisionDidExitEvent;
  public delegate void ParticlesCollisionHandler(GameObject go);
  public event ParticlesCollisionHandler ParticlesCollisionEvent;

  protected override void OnTriggerDidEnter(Collider collider) {
    base.OnTriggerDidEnter(collider);
    if (TriggerDidEnterEvent != null) {
      TriggerDidEnterEvent(collider);
    }
  }
  protected override void OnTrigger(Collider collider) {
    base.OnTrigger(collider);
    if (TriggerEvent != null) {
      TriggerEvent(collider);
    }
  }
  protected override void OnTriggerDidExit(Collider collider) {
    base.OnTriggerDidExit(collider);
    if (TriggerDidExitEvent != null) {
      TriggerDidExitEvent(collider);
    }
  }
  protected override void OnCollisionDidEnter(Collision collision) {
    base.OnCollisionDidEnter(collision);
    if (CollisionDidEnterEvent != null) {
      CollisionDidEnterEvent(collision);
    }
  }
  protected override void OnCollision(Collision collision) {
    base.OnCollision(collision);
    if (CollisionEvent != null) {
      CollisionEvent(collision);
    }

  }
  protected override void OnCollisionDidExit(Collision collision) {
    base.OnCollisionDidExit(collision);
    if (CollisionDidExitEvent != null) {
      CollisionDidExitEvent(collision);
    }
  }

  protected override void OnParticlesCollision(GameObject other) {
    base.OnParticlesCollision(other);
    if (ParticlesCollisionEvent != null) {
      ParticlesCollisionEvent(other);
    }
  }
}
