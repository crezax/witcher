using System.Collections.Generic;
using UnityEngine;

public class UIController : BaseBehaviour {
  private static UIController instance;

  public static UIController Instance {
    get {
      return instance;
    }
  }

  [SerializeField]
  private GameObject npcHealthBarPrefab;
  private Dictionary<GameObject, List<GameObject>> npcToResourceBars;

  protected override void OnAwake() {
    base.OnAwake();

    if (instance != null) {
      Destroy(instance.gameObject);
      return;
    }

    instance = this;
    npcToResourceBars = new Dictionary<GameObject, List<GameObject>>();
  }

  public void ShowNpcResourceBars(Character npc) {
    if (npcToResourceBars.ContainsKey(npc.gameObject)) {
      return;
    }
    npcToResourceBars[npc.gameObject] = new List<GameObject>();
    Health npcHealth = npc.GetComponent<Health>();

    GameObject healthBarGO = (GameObject)Instantiate(
      npcHealthBarPrefab,
      transform,
      false
    );
    NPCResourceBar healthBar = healthBarGO.GetComponent<NPCResourceBar>();
    healthBar.resource = npcHealth;
    healthBar.Offset = npc.Height;

    npcToResourceBars[npc.gameObject].Add(healthBarGO);
  }

  public void HideNpcResourceBard(GameObject npc) {
    if (!npcToResourceBars.ContainsKey(npc)) {
      return;
    }

    foreach (GameObject resourceBar in npcToResourceBars[npc]) {
      Destroy(resourceBar);
    }

    npcToResourceBars.Remove(npc);
  }
}
