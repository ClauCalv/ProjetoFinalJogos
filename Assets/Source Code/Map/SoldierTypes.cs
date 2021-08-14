using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SoldierTypeEnum
{
    BASIC = 1
}

public static class SoldierTypeEnumExtension
{
    public static SoldierType Type(this SoldierTypeEnum ttenum) => SoldierType.fromEnum(ttenum);
}

public class SoldierType
{
    public static readonly SoldierType BASIC = new SoldierType(SoldierTypeEnum.BASIC, "Troops/", "BasicSoldier");

    public static Dictionary<SoldierTypeEnum, SoldierType> tileTypes = new Dictionary<SoldierTypeEnum, SoldierType>()
    {
        { SoldierTypeEnum.BASIC, BASIC }
    };

    public SoldierTypeEnum TTEnum { get; private set; }
    public string ResourceFolder { get; private set; }

    public string BaseResource { get; private set; }

    private SoldierType(SoldierTypeEnum TTEnum, string ResourceFolder, string BaseResource)
    {
        (this.TTEnum, this.ResourceFolder, this.BaseResource)
            = (TTEnum, ResourceFolder, BaseResource);
    }
    public static SoldierType fromEnum(SoldierTypeEnum Enum) => tileTypes[Enum];

}