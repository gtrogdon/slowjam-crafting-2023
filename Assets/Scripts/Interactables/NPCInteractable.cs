using UnityEngine;

public class NPCInteractable : Interactable
{
    private SpriteRenderer onTriggerGraphic;
    // Start is called before the first frame update
    void Start()
    {
       onTriggerGraphic = GetComponentInChildren<SpriteRenderer>(); 
       onTriggerGraphic.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
                
    }

    protected override void OnTriggerEnter(Collider other) {
        base.OnTriggerEnter(other);
        onTriggerGraphic.enabled = true;
    }

    protected override void OnTriggerExit(Collider other) {
        base.OnTriggerEnter(other);
        onTriggerGraphic.enabled = false;
    }

    public override void OnInteract() {
        transform.parent.GetComponentInChildren<DialogueTrigger>().TriggerDialogue();
    }
}
