using TMPro;
using UnityEngine;

public class IncomeCalculator : MonoBehaviour
{
    public Transform parentObject; // Obiectul p?rinte care con?ine copiii cu venit
    public TMP_Text IncomeText;
    public float totalIncome = 0f;
    public string[] childNames; // Numele copiilor care con?in venitul

    private void Start()
    {
        CalculateTotalIncome();
    }

    private void OnEnable()
    {
        // Ad?ug?m un listener pentru evenimentul de modificare a transform?rii obiectului p?rinte
        if (parentObject != null)
        {
            parentObject.gameObject.transform.hasChanged = true;
        }
    }

    private void OnDisable()
    {
        // Elimin?m listenerul la dezactivarea obiectului
        if (parentObject != null)
        {
            parentObject.gameObject.transform.hasChanged = false;
        }
    }

    public void UpdateIncome()
    {
        CalculateTotalIncome();
    }

    private void CalculateTotalIncome()
    {
        totalIncome = 0f; // Reset?m venitul total la fiecare recalculare

        // Verific?m dac? obiectul p?rinte ?i numele copiilor sunt setate
        if (parentObject != null && childNames != null)
        {
            // Iter?m prin fiecare copil al obiectului p?rinte
            for (int i = 0; i < parentObject.childCount; i++)
            {
                Transform child = parentObject.GetChild(i);
                // Verific?m dac? numele copilului se potrive?te cu cele din list?
                if (ArrayContains(child.name, childNames))
                {
                    // Ad?ug?m venitul corespunz?tor
                    totalIncome += GetIncomeFromChild(child.name);
                }
            }
        }

        // Afi??m venitul total
        Debug.Log("Total Income: " + totalIncome);

        // Actualiz?m textul venitului
        IncomeText.text = totalIncome.ToString();
    }

    private bool ArrayContains(string target, string[] array)
    {
        // Verific?m dac? un ?ir de caractere se g?se?te într-un array de ?iruri
        foreach (string item in array)
        {
            if (item == target)
            {
                return true;
            }
        }
        return false;
    }

    private float GetIncomeFromChild(string childName)
    {
        // Func?ie fictiv? care returneaz? venitul în func?ie de numele copilului
        // Aici ar trebui s? ai logica real? pentru a returna venitul
        // în func?ie de numele copilului
        switch (childName)
        {
            case "Dacia":
                return 100f; // Venit pentru copilul 1
            case "Child2":
                return 200f; // Venit pentru copilul 2
            // Adaug? mai multe cazuri pentru fiecare copil dac? este necesar
            default:
                return 0f; // Dac? numele copilului nu se potrive?te, venitul este 0
        }
    }
}
