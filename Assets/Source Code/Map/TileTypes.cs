using UnityEngine;
using System.Collections.Generic;
using System;

namespace Map
{

    //COMO EU QUERIA QUE C# TIVESSE ENUMS DECENTES!!!!!!!!!!
    //5000 linhas de código pra chegar perto do que o Enum do java faz

    [Serializable]
    public enum TileTypeEnum
    {
        SPAWNPOINT,
        ROAD,
        GROUND,
        TOWER,
        BARRACKS
    }

    public static class TileTypeEnumExtension
    {
        public static TileType Type(this TileTypeEnum ttenum) => TileType.fromEnum(ttenum);
    }
    public class TileType
    {
        public static readonly TileType SPAWNPOINT = new TileType(TileTypeEnum.SPAWNPOINT, "/Tiles/Spawnpoint/", "Spawnpoint", null);
        public static readonly TileType ROAD = new TileType(TileTypeEnum.ROAD, "/Tiles/Road/", "Road", typeof(RoadVarietyEnum));
        public static readonly TileType GROUND = new TileType(TileTypeEnum.GROUND, "/Tiles/Ground/", "Ground", null);
        public static readonly TileType TOWER = new TileType(TileTypeEnum.TOWER, "/Tiles/Tower/", "Tower", typeof(TowerVarietyEnum));
        public static readonly TileType BARRACKS = new TileType(TileTypeEnum.BARRACKS, "/Tiles/Barracks/", "Barracks", null);

        public static Dictionary<TileTypeEnum, TileType> tileTypes = new Dictionary<TileTypeEnum, TileType>()
        {
            { TileTypeEnum.SPAWNPOINT, SPAWNPOINT },
            { TileTypeEnum.ROAD, ROAD },
            { TileTypeEnum.GROUND, GROUND },
            { TileTypeEnum.TOWER, TOWER },
            { TileTypeEnum.BARRACKS, BARRACKS }
        };

        public TileTypeEnum TTEnum { get; private set; }
        public string ResourceFolder { get; private set; }

        public string BaseResource { get; private set; }
        public Type VarietyEnumAssociated { get; private set; }

        private TileType(TileTypeEnum TTEnum, string ResourceFolder, string BaseResource, Type VarietyEnumAssociated)
        {
            (this.TTEnum, this.ResourceFolder, this.BaseResource, this.VarietyEnumAssociated) 
                = (TTEnum, ResourceFolder, BaseResource, VarietyEnumAssociated);
        }
        public static TileType fromEnum (TileTypeEnum Enum) => tileTypes[Enum]; 

    }
    public abstract class TileVariety<T> where T : Enum
    {
        public T EnumAssociated;
        public string ResourceName;

        protected internal TileVariety (T EnumAssociated, string ResourceName)
        {
            (this.EnumAssociated, this.ResourceName) = (EnumAssociated, ResourceName);
        }
    }

    [Flags]
    [Serializable]
    public enum RoadVarietyEnum
    {
        TOP_CONNECTED = 0b0001,
        BOTTOM_CONNECTED = 0b0010,
        LEFT_CONNECTED = 0b0100,
        RIGHT_CONNECTED = 0b1000,

        STRAIGHT_VERTICAL = TOP_CONNECTED | BOTTOM_CONNECTED,
        STRAIGHT_HORIZONTAL = LEFT_CONNECTED | RIGHT_CONNECTED,

        CORNER_TOP_LEFT = TOP_CONNECTED | LEFT_CONNECTED,
        CORNER_TOP_RIGHT = TOP_CONNECTED | RIGHT_CONNECTED,
        CORNER_BOTTOM_LEFT = BOTTOM_CONNECTED | LEFT_CONNECTED,
        CORNER_BOTTOM_RIGHT = BOTTOM_CONNECTED | RIGHT_CONNECTED,

        TSHAPED_TOP = TOP_CONNECTED | STRAIGHT_HORIZONTAL,
        TSHAPED_BOTTOM = BOTTOM_CONNECTED | STRAIGHT_HORIZONTAL,
        TSHAPED_LEFT = LEFT_CONNECTED | STRAIGHT_VERTICAL,
        TSHAPED_RIGHT = RIGHT_CONNECTED | STRAIGHT_VERTICAL,

        FOURCORNERS = CORNER_TOP_LEFT | CORNER_BOTTOM_RIGHT

    }

    public static class RoadVarietyEnumExtension
    {
        public static RoadVariety Type(this RoadVarietyEnum ttenum) => RoadVariety.fromEnum(ttenum);
    }
    public class RoadVariety : TileVariety<RoadVarietyEnum>
    {
        public static readonly RoadVariety STRAIGHT_VERTICAL = new RoadVariety(RoadVarietyEnum.STRAIGHT_VERTICAL, "STRAIGHT_VERTICAL");
        public static readonly RoadVariety STRAIGHT_HORIZONTAL = new RoadVariety(RoadVarietyEnum.STRAIGHT_HORIZONTAL, "STRAIGHT_HORIZONTAL");

        public static readonly RoadVariety CORNER_TOP_LEFT = new RoadVariety(RoadVarietyEnum.CORNER_TOP_LEFT, "CORNER_TOP_LEFT");
        public static readonly RoadVariety CORNER_TOP_RIGHT = new RoadVariety(RoadVarietyEnum.CORNER_TOP_RIGHT, "CORNER_TOP_RIGHT");
        public static readonly RoadVariety CORNER_BOTTOM_LEFT = new RoadVariety(RoadVarietyEnum.CORNER_BOTTOM_LEFT, "CORNER_BOTTOM_LEFT");
        public static readonly RoadVariety CORNER_BOTTOM_RIGHT = new RoadVariety(RoadVarietyEnum.CORNER_BOTTOM_RIGHT, "CORNER_BOTTOM_RIGHT");

        public static readonly RoadVariety TSHAPED_TOP = new RoadVariety(RoadVarietyEnum.TSHAPED_TOP, "TSHAPED_TOP");
        public static readonly RoadVariety TSHAPED_BOTTOM = new RoadVariety(RoadVarietyEnum.TSHAPED_BOTTOM, "TSHAPED_BOTTOM");
        public static readonly RoadVariety TSHAPED_LEFT = new RoadVariety(RoadVarietyEnum.TSHAPED_LEFT, "TSHAPED_LEFT");
        public static readonly RoadVariety TSHAPED_RIGHT = new RoadVariety(RoadVarietyEnum.TSHAPED_RIGHT, "TSHAPED_RIGHT");

        public static readonly RoadVariety FOURCORNERS = new RoadVariety(RoadVarietyEnum.FOURCORNERS, "FOURCORNERS");

        public static Dictionary<RoadVarietyEnum, RoadVariety> tileTypes = new Dictionary<RoadVarietyEnum, RoadVariety>()
        {
            { RoadVarietyEnum.STRAIGHT_VERTICAL, STRAIGHT_VERTICAL },
            { RoadVarietyEnum.STRAIGHT_HORIZONTAL, STRAIGHT_HORIZONTAL },

            { RoadVarietyEnum.CORNER_TOP_LEFT, CORNER_TOP_LEFT },
            { RoadVarietyEnum.CORNER_TOP_RIGHT, CORNER_TOP_RIGHT },
            { RoadVarietyEnum.CORNER_BOTTOM_LEFT, CORNER_BOTTOM_LEFT },
            { RoadVarietyEnum.CORNER_BOTTOM_RIGHT, CORNER_BOTTOM_RIGHT },

            { RoadVarietyEnum.TSHAPED_TOP, TSHAPED_TOP },
            { RoadVarietyEnum.TSHAPED_BOTTOM, TSHAPED_BOTTOM },
            { RoadVarietyEnum.TSHAPED_LEFT, TSHAPED_LEFT },
            { RoadVarietyEnum.TSHAPED_RIGHT, TSHAPED_RIGHT },

            { RoadVarietyEnum.FOURCORNERS, FOURCORNERS }
        };

        public static RoadVariety fromEnum (RoadVarietyEnum Enum) => tileTypes[Enum];

        private RoadVariety(RoadVarietyEnum EnumAssociated, string ResourceName) : base(EnumAssociated, ResourceName){}

    }

    [Serializable]
    public enum TowerVarietyEnum
    {
        ARROW_TOWER,
        FLAME_TOWER,
        TROOPS_TOWER
    }

    public static class TowerVarietyEnumExtension
    {
        public static TowerVariety Type(this TowerVarietyEnum ttenum) => TowerVariety.fromEnum(ttenum);
    }
    public class TowerVariety : TileVariety<TowerVarietyEnum>
    {
        public static readonly TowerVariety ARROW_TOWER = new TowerVariety(TowerVarietyEnum.ARROW_TOWER, "ARROW_TOWER");
        public static readonly TowerVariety FLAME_TOWER = new TowerVariety(TowerVarietyEnum.FLAME_TOWER, "FLAME_TOWER");
        public static readonly TowerVariety TROOPS_TOWER = new TowerVariety(TowerVarietyEnum.TROOPS_TOWER, "TROOPS_TOWER");

        public static Dictionary<TowerVarietyEnum, TowerVariety> tileTypes = new Dictionary<TowerVarietyEnum, TowerVariety>()
        {
            { TowerVarietyEnum.ARROW_TOWER, ARROW_TOWER },
            { TowerVarietyEnum.FLAME_TOWER, FLAME_TOWER },
            { TowerVarietyEnum.TROOPS_TOWER, TROOPS_TOWER }
        };

        public static TowerVariety fromEnum (TowerVarietyEnum Enum) => tileTypes[Enum];

        private TowerVariety(TowerVarietyEnum EnumAssociated, string ResourceName) : base(EnumAssociated, ResourceName) { }
        
    }

}