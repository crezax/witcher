using UnityEngine;

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

  protected void OnTriggerEnter2D(Collider2D collider) {
    OnTriggerDidEnter2D(collider);
  }

  protected void OnTriggerStay2D(Collider2D collider) {
    OnTrigger2D(collider);
  }

  protected void OnTriggerExit2D(Collider2D collider) {
    OnTriggerDidExit(collider);
  }

  protected void OnCollisionEnter2D(Collision2D collision) {
    OnCollisionDidEnter2D(collision);
  }

  protected void OnCollisionStay2D(Collision2D collision) {
    OnCollision2D(collision);
  }

  protected void OnCollisionExit2D(Collision2D collision) {
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
  protected virtual void OnTriggerDidEnter2D(Collider2D collider) { }
  protected virtual void OnTrigger2D(Collider2D collider) { }
  protected virtual void OnTriggerDidExit(Collider2D collider) { }
  protected virtual void OnCollisionDidEnter2D(Collision2D collision) { }
  protected virtual void OnCollision2D(Collision2D collision) { }
  protected virtual void OnCollisionDidExit(Collision2D collision) { }
  protected virtual void OnParticlesCollision(GameObject other) { }
}
