using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Color_Butten : MonoBehaviour
{
    public string ColorName;

    public Game_Handler Game_Handler;

    public void SetColor(ColorOption colorOption)
    {
        GetComponent<Image>().color = colorOption.Value;
        ColorName = colorOption.Name;
    }

    public void I_Chooice_This()
    {
        Game_Handler.OnColorButtonClicked(ColorName);
    }


}
