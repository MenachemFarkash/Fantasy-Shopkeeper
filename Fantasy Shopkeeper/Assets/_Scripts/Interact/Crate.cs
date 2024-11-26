public class Crate : Interactable {

    public bool canBePickedUp = true;
    public override void Interact() {
        base.Interact();

        if (canBePickedUp) {
            PlayerManager.instance.Player.GetComponent<PlayerCarrySystem>().AddItemToHand(gameObject);
        }
    }
}
