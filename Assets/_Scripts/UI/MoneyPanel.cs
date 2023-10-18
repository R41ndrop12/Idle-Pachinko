using TMPro;
using UnityEngine;

public class MoneyPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI coreText;

    public void SetScore(double score)
    {
        coreText.text = $"{score.ToString("F2")} $";
    }
}

