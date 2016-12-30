using UnityEngine;

public class Yrden : Sign {
  [SerializeField]
  private GameObject yrdenTrapPrefab;

  protected override float EnergyCost {
    get {
      return 100;
    }
  }

  public float Duration { get; set; }

  protected override void OnAwake() {
    base.OnAwake();

    Duration = 5;
  }

  protected override void PerformImplementation(GameObject target) {
    Vector3 direction = transform.forward;
    float radius = 5;
    int trapCount = 6;

    for (int i = 0; i < trapCount; i++) {
      RaycastHit raycastInfo;
      // Raycasting from a meter above the ground may still get clunky in 
      // situation when we use Yrden near really steep hill, or in a place
      // where there is not much place between floor and ceiling.
      // It should definetely be more polished than that, but for initial version
      // it seems good enough, especially as it likely wouldn't trap anyone
      // in the edge cases anyway.
      bool hit = Physics.Raycast(
        transform.position + direction * radius + Vector3.up * 1,
        -Vector3.up,
        out raycastInfo
      );

      if (hit) {
        GameObject trapGO = Instantiate(yrdenTrapPrefab);
        trapGO.transform.position = raycastInfo.point;
        trapGO.GetComponent<YrdenTrap>().Duration = Duration;
      }

      direction = Quaternion.Euler(0, 360 / trapCount, 0) * direction;
    }
  }
}
