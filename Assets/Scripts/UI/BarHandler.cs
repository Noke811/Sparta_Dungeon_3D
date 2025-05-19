using UnityEngine;
using UnityEngine.UI;

public class BarHandler : MonoBehaviour
{
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void UpdateBar(float curAmount, float baseAmount)
    {
        image.fillAmount = curAmount / baseAmount;
    }
}
