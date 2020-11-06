using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualHierarchy
{
    /*                Si on veut changer, ça il faut aller dans OOE_Map.cs et changer l'ordre des ifs dans Overlay()
    Overlay : 
       - Confirmation d'input de direction
       - Bord de map
       - FX (genre traits de vents, dégâts pc, préparation éclair)
       - Nuages
     */

    public static objectType[] objectHierarchy =
    {
        objectType.player,
        objectType.lightning,
        objectType.mountain
    };
    /*- Objet avec altitude moindre

    Background
    */
}
