using UnityEngine;
using UnityEngine.AI;

public class CustomerAINavigation : MonoBehaviour {

    public Shelf targetShelf;
    public NavMeshAgent agent;
    public Vector3 destination;

    public ShelfsContainer targetShelfScript;
    public ShelvsManager shelvsManager;

    public Animator animator;
    public Rigidbody rigidBody;

    public int customerBudget = 10;
    public int customerProductsAmount = 2;
    public string customerItemDemand;
    public bool didBuyFromShelf = false;

    public bool isInByingPos = false;
    void Start() {
        shelvsManager = FindAnyObjectByType<ShelvsManager>();
        SetCustomerDemandMode();
        PickShelf();
        agent.updateRotation = true;
    }

    private void Update() {
        animator.SetFloat("Velocity", agent.velocity.magnitude);
        agent.SetDestination(destination);

        if (targetShelf && Vector3.Distance(transform.position, targetShelf.interactionTransform.position) < 1f && !didBuyFromShelf) {
            FaceTarget();
            Invoke(nameof(BuyItemFromShelf), 3);
            didBuyFromShelf = true;


        }
    }



    public void PickShelf() {
        if (shelvsManager.shelfs.Count != 0) {
            targetShelf = shelvsManager.shelfs[Random.Range(0, shelvsManager.shelfs.Count)];
            destination = targetShelf.interactionTransform.position;
        } else {
            destination = new Vector3(Random.Range(5, 15), 0, Random.Range(5, 15));
            Destroy(gameObject, 10);
        }
    }




    public void SetCustomerDemandMode() {
        int temp = 0;
        temp = Random.Range(1, 4);
        switch (temp) {
            case 1:
                customerProductsAmount = SetProductsAmountTheCustomerWillBuy();
                break;
            case 2:
                customerProductsAmount = SetProductsAmountTheCustomerWillBuy();
                customerBudget = SetAmountOfMoneyTheCustomerWillingToPay();
                break;
            case 3:
                customerProductsAmount = SetProductsAmountTheCustomerWillBuy();
                customerItemDemand = SetSpecificItemTheCustomerWantsToBuy();
                break;
            default:
                customerProductsAmount = SetProductsAmountTheCustomerWillBuy();
                break;
        }
    }

    public bool CheckShelfBeforeBuying() {
        if (!shelvsManager.shelfs.Contains(targetShelf)) {
            destination = new Vector3(Random.Range(5, 15), 0, Random.Range(5, 15));
            Destroy(gameObject, 10);
            return false;
        } else return true;
    }

    public void BuyItemFromShelf() {
        if (CheckShelfBeforeBuying()) {
            targetShelf.BuyItemFromShelf(1);
            destination = new Vector3(Random.Range(5, 15), 0, Random.Range(5, 15));
            Destroy(gameObject, 10);
        }
    }





    //CustomersDemandModes:
    public int SetProductsAmountTheCustomerWillBuy() {
        int temp = 1;
        temp = Random.Range(1, 5);
        return temp;
    }

    public int SetAmountOfMoneyTheCustomerWillingToPay() {
        int temp = 1;
        temp = Random.Range(1, 45);
        return temp;
    }
    public string SetSpecificItemTheCustomerWantsToBuy() {
        return "mode3";
    }

    public void FaceTarget() {
        print("Rotating Customer");
        Vector3 direction = (targetShelf.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }















}
