using UnityEngine;

// An example of extending RangeBasedTargetProvider, to attack 1st seen target
// and to never give up until it dies
public class FirstSeenTargetProvider : RangeBasedTargetProvider {
  private GameObject cachedTarget;

  public override GameObject ProvideTarget() {
    if (cachedTarget == null) {
      cachedTarget = base.ProvideTarget();
    }
    return cachedTarget;
  }
}
