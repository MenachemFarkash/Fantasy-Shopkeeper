using TMPro;
using UnityEngine;

public class GoldManager : MonoBehaviour {



    public int amount;
    public TextMeshProUGUI amountText;

    private void Start() {
        amountText.text = amount.ToString();
    }

    public bool CheckIfCanAfford(int price) {
        if (amount >= price) {
            return true;
        } else {
            print("Can't Afford");
            return false;
        }
    }
    public void AddGold(int amountToAdd) {
        amount += amountToAdd;
        amountText.text = amount.ToString();
    }

    public void RemoveGold(int amountToRemove) {
        amount -= amountToRemove;
        amountText.text = amount.ToString();
    }
}
