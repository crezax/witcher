using UnityEngine;

public class DamageProjectile : Projectile {
  [SerializeField]
  private GameObject hitParticlesPrefab;
  [SerializeField]
  private GameObject stunEffectPrefab;

  public float Damage { get; set; }

  protected override void OnHit(Collider collider) {
    Health targetHealth = collider.GetComponent<Health>();
    if (targetHealth != null) {
      targetHealth.CurrentValue -= Damage;
    }
    if (collider.GetComponent<Character>() != null) {
      Effect.Apply(stunEffectPrefab, collider.gameObject)
        .GetComponent<Effect>()
        .DurationLeft = .5f;
    }
    Instantiate(hitParticlesPrefab).transform.position = transform.position;
    Destroy(gameObject);
  }
}
