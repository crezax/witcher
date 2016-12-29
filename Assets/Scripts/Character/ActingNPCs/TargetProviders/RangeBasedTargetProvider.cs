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
  protected CharacterDetector detectionRange;
  [SerializeField]
  protected CharacterDetector actionRange;

  public override GameObject ProvideTarget() {
    List<GameObject> potentialActionTargets = actionRange.PotentialTargets;
    if (potentialActionTargets.Count > 0) {
      return potentialActionTargets.OrderBy(
        t => Vector3.Distance(transform.position, t.transform.position)
      ).First();
    }

    List<GameObject> potentialWatchTargets = detectionRange.PotentialTargets;
    if (potentialWatchTargets.Count > 0) {
      return potentialWatchTargets.OrderBy(
        t => Vector3.Distance(transform.position, t.transform.position)
      ).First();
    }
    return null;
  }
}
