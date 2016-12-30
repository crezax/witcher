using UnityEngine;
using System.Collections;

public class DamageProjectile : Projectile {
  [SerializeField]
  private GameObject hitParticlesPrefab;

  public float Damage { get; set; }

  protected override void OnHit(Collider collider) {
    collider.GetComponent<Health>().CurrentValue -= Damage;
    Instantiate(hitParticlesPrefab).transform.position = transform.position;
    Destroy(gameObject);
  }
}
