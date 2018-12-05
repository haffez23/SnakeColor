using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorList : MonoBehaviour
{


  //  public static Color32 black = new Color32(255,0,0,0);
    public static Color red = Color.red;
    public static Color yellow = Color.yellow;
    public static Color cyan = Color.cyan ;
    public static Color green = Color.green;
    public static Color blue =Color.blue;
    public static Color white = Color.white;
    public static Color magenta = Color.magenta;
   


    public static Color32 getColor(string color){

        switch(color){
            case "RED":
                return red;
            case "YELLOW":
                return yellow;
            case "MAGENTA":
                return magenta;
            case "BLUE":
                return blue;
            case "WHITE":
                return white;
            case "CYAN":
                return cyan;
            case "GREEN":
                return green;
            default:
                return red;
        }
    }

    public static string getColorName(Color c)
    {

        if (c == red)
            return "RED";
        else if (c == yellow)
            return "YELLOW";
        else if (c == magenta)
            return "MAGENTA";
        else if (c == blue)
            return "BLUE";
        else if (c == white)
            return "WHITE";
        else if (c == cyan)
            return "CYAN";
        else if (c == green)
            return "GREEN";
        else
            return "RED";
    }
}
