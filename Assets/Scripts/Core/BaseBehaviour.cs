using UnityEngine;

// Base class for all scripts, instead of MonoBehaviour
// The reason for this is to override lifecycle methods - VS will autocomplete
// names and add "base.MethodName()" as first line, which will prevent people
// from hiding implementation of parent class by mistake.
public class BaseBehaviour : MonoBehaviour {

  protected void Awake() {
    OnAwake();
  }

  protected void Start() {
    OnStart();
  }

  protected void Update() {
    OnUpdate();
  }

  protected void FixedUpdate() {
    OnPhysicsUpdate();
  }

  protected void LateUpdate() {
    OnLateUpdate();
  }

  protected void OnDestroy() {
    OnWillDestroy();
  }

  protected void OnTriggerEnter(Collider collider) {
    OnTriggerDidEnter(collider);
  }

  protected void OnTriggerStay(Collider collider) {
    OnTrigger(collider);
  }

  protected void OnTriggerExit(Collider collider) {
    OnTriggerDidExit(collider);
  }

  protected void OnCollisionEnter(Collision collision) {
    OnCollisionDidEnter(collision);
  }

  protected void OnCollisionStay(Collision collision) {
    OnCollision(collision);
  }

  protected void OnCollisionExit(Collision collision) {
    OnCollisionDidExit(collision);
  }

  protected void OnParticleCollision(GameObject go) {
    OnParticlesCollision(go);
  }

  protected virtual void OnAwake() { }
  protected virtual void OnStart() { }
  protected virtual void OnUpdate() { }
  protected virtual void OnPhysicsUpdate() { }
  protected virtual void OnLateUpdate() { }
  protected virtual void OnWillDestroy() { }
  protected virtual void OnTriggerDidEnter(Collider collider) { }
  protected virtual void OnTrigger(Collider collider) { }
  protected virtual void OnTriggerDidExit(Collider collider) { }
  protected virtual void OnCollisionDidEnter(Collision collision) { }
  protected virtual void OnCollision(Collision collision) { }
  protected virtual void OnCollisionDidExit(Collision collision) { }
  protected virtual void OnParticlesCollision(GameObject other) { }
}
