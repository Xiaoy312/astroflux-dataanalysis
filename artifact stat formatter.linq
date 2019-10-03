<Query Kind="Statements" />

var source = @"
public static const TYPE_CORROSIVE_ADD:String = ""corrosiveAdd"";

public static const TYPE_CORROSIVE_ADD_SUPERIOR:String = ""corrosiveAdd2"";

public static const TYPE_CORROSIVE_ADD_EXCEPTIONAL:String = ""corrosiveAdd3"";

public static const TYPE_CORROSIVE_MULTI:String = ""corrosiveMulti"";

public static const TYPE_KINETIC_ADD:String = ""kineticAdd"";

public static const TYPE_KINETIC_ADD_SUPERIOR:String = ""kineticAdd2"";

public static const TYPE_KINETIC_ADD_EXCEPTIONAL:String = ""kineticAdd3"";

public static const TYPE_KINETIC_MULTI:String = ""kineticMulti"";

public static const TYPE_ENERGY_ADD:String = ""energyAdd"";

public static const TYPE_ENERGY_ADD_SUPERIOR:String = ""energyAdd2"";

public static const TYPE_ENERGY_ADD_EXCEPTIONAL:String = ""energyAdd3"";

public static const TYPE_ENERGY_MULTI:String = ""energyMulti"";

public static const TYPE_CORROSIVE_RESIST:String = ""corrosiveResist"";

public static const TYPE_KINETIC_RESIST:String = ""kineticResist"";

public static const TYPE_ENERGY_RESIST:String = ""energyResist"";

public static const TYPE_SHIELD_ADD:String = ""shieldAdd"";

public static const TYPE_SHIELD_ADD_SUPERIOR:String = ""shieldAdd2"";

public static const TYPE_SHIELD_ADD_EXCEPTIONAL:String = ""shieldAdd3"";

public static const TYPE_SHIELD_MULTI:String = ""shieldMulti"";

public static const TYPE_SHIELD_REGEN:String = ""shieldRegen"";

public static const TYPE_HEALTH_ADD:String = ""healthAdd"";

public static const TYPE_HEALTH_ADD_SUPERIOR:String = ""healthAdd2"";

public static const TYPE_HEALTH_ADD_EXCEPTIONAL:String = ""healthAdd3"";

public static const TYPE_HEALTH_MULTI:String = ""healthMulti"";

public static const TYPE_ARMOR_ADD:String = ""armorAdd"";

public static const TYPE_ARMOR_ADD_SUPERIOR:String = ""armorAdd2"";

public static const TYPE_ARMOR_ADD_EXCEPTIONAL:String = ""armorAdd3"";

public static const TYPE_ARMOR_MULTI:String = ""armorMulti"";

public static const TYPE_ALL_ADD:String = ""allAdd"";

public static const TYPE_ALL_ADD_SUPERIOR:String = ""allAdd2"";

public static const TYPE_ALL_ADD_EXCEPTIONAL:String = ""allAdd3"";

public static const TYPE_ALL_MULTI:String = ""allMulti"";

public static const TYPE_ALL_RESIST:String = ""allResist"";

public static const TYPE_SPEED:String = ""speed"";

public static const TYPE_SPEED_SUPERIOR:String = ""speed2"";

public static const TYPE_SPEED_EXCEPTIONAL:String = ""speed3"";

public static const TYPE_REFIRE:String = ""refire"";

public static const TYPE_REFIRE_SUPERIOR:String = ""refire2"";

public static const TYPE_REFIRE_EXCEPTIONAL:String = ""refire3"";

public static const TYPE_CONV_HP:String = ""convHp"";

public static const TYPE_CONV_SHIELD:String = ""convShield"";

public static const TYPE_POWER_REG:String = ""powerReg"";

public static const TYPE_POWER_REG_SUPERIOR:String = ""powerReg2"";

public static const TYPE_POWER_REG_EXCEPTIONAL:String = ""powerReg3"";

public static const TYPE_POWER_MAX:String = ""powerMax"";

public static const TYPE_COOLDOWN:String = ""cooldown"";

public static const TYPE_COOLDOWN_SUPERIOR:String = ""cooldown2"";

public static const TYPE_COOLDOWN_EXCEPTIONAL:String = ""cooldown3"";
";

var affixes = Regex.Matches(source, @"const TYPE_(?<type>\w+?)(_(?<quality>SUPERIOR|EXCEPTIONAL))?:String = ""(?<name>[\w\d]+)""")
    .Cast<Match>()
    .Select(m => m.Groups)
    .Select(grp => new
    {
        Quality = grp["quality"].Value,
        Type = grp["type"].Value,
        Name = grp["name"].Value,
    })
    .Dump();