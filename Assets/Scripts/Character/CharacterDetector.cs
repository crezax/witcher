using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class CharacterDetector : BaseBehaviour {
  private List<GameObject> potentialTargets;

  public List<GameObject> PotentialTargets {
    get {
      // Unity doesn't call TriggerExit event when game object is destroyed 
      // within the trigger, so we need to get rid of nulls sometimes...
      return potentialTargets.Where(pt => pt != null).ToList();
    }
  }

  protected override void OnAwake() {
    base.OnAwake();

    potentialTargets = new List<GameObject>();
  }

  protected override void OnTriggerDidEnter(Collider collider) {
    potentialTargets.Add(collider.gameObject);
  }

  protected override void OnTriggerDidExit(Collider collider) {
    potentialTargets.Remove(collider.gameObject);
  }
}
