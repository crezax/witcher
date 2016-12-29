using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// The script assumes, that if physics engine is setup to make the object
// trigger the detection/actionRange, it is a valid target. Should we need 
// additional validation, we can extend this class and override the 
// ProvideTarget method to include required validation, like "Player is valid
// target only once they attack us".
public class RangeBasedTargetProvider : TargetProvider {

  [SerializeField]
  protected DelegateCollider detectionRange;
  [SerializeField]
  protected DelegateCollider actionRange;

  protected List<GameObject> potentialActionTargets;
  protected List<GameObject> potentialWatchTargets;

  protected override void OnAwake() {
    base.OnAwake();

    potentialActionTargets = new List<GameObject>();
    potentialWatchTargets = new List<GameObject>();
    detectionRange.TriggerDidEnterEvent += OnDetectionRangeTriggerEnter;
    detectionRange.TriggerDidExitEvent += OnDetectionRangeTriggerExit;
    actionRange.TriggerDidEnterEvent += OnActingRangeTriggerEnter;
    actionRange.TriggerDidEnterEvent += OnActingRangeTriggerExit;
  }

  public override GameObject ProvideTarget() {
    // Unity doesn't call TriggerExit event when game object is destroyed within
    // the trigger, so we need to get rid of nulls sometimes...
    potentialActionTargets = potentialActionTargets.Where(t => t != null).ToList();
    if (potentialActionTargets.Count > 0) {
      return potentialActionTargets.OrderBy(
        t => Vector3.Distance(transform.position, t.transform.position)
      ).First();
    }
    potentialWatchTargets = potentialWatchTargets.Where(t => t != null).ToList();
    if (potentialWatchTargets.Count > 0) {
      return potentialWatchTargets.OrderBy(
        t => Vector3.Distance(transform.position, t.transform.position)
      ).First();
    }
    return null;
  }

  private void OnActingRangeTriggerEnter(Collider collider) {
    potentialActionTargets.Add(collider.gameObject);
  }

  private void OnActingRangeTriggerExit(Collider collider) {
    potentialActionTargets.Remove(collider.gameObject);
  }

  private void OnDetectionRangeTriggerEnter(Collider collider) {
    potentialWatchTargets.Add(collider.gameObject);
  }

  private void OnDetectionRangeTriggerExit(Collider collider) {
    potentialWatchTargets.Remove(collider.gameObject);
  }
}
