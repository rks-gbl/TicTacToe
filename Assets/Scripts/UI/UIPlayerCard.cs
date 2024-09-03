using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UIPlayerCard : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public Image profileImage;

    public void UpdateCard(PlayerData data)
    {
        nameText.text = data.Name;
        levelText.text = ""+data.Level;
        //Access Sprite dictionary
        //profileImage.sprite = data.ProfileImage;
    }
} 