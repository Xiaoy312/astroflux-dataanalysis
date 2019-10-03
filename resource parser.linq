<Query Kind="Program">
  <NuGetReference>Humanizer</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Humanizer</Namespace>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
</Query>

void Main()
{
    var json = File.ReadAllText(@"D:\Code\LinqPad Queries\Gaming\AF\res\23_cache_json$f0170aad834e758c8de1f3455e70ad5c919569007.bin");
    var data = JObject.Parse(json);
    data.ToObject().Dump("RawData", 0);
    
//    JsonHelper.GetCommonClassDefinition(data["MissionTypes"])
//        .Dump(0);
//    data["MissionTypes"].ToDictionary()
//        .Values
//        //.SelectMany(x => x["eliteTechs"] as JArray)
//        .Cast<JObject>().SelectMany(x => x.Properties())
//        .Select(x => new { x.Name, Value = JsonConvert.SerializeObject(x.Value) })
//        .Distinct()
//        .ToLookup(x => x.Name, x => (string)x.Value)
//        .ToDictionary(x => x.Key, x => x.AsEnumerable())
//        .Dump(0);
        
    var resources = JsonConvert.DeserializeObject<Resources>(json).Dump("Parsed Object", 0);
    ResourcesHelper.DebugTimedMissions(resources);
}

// Define other methods and classes here
public class Resources
{
    public Dictionary<string, ImageData> Images { get; set; }
    //public Dictionary<string, EffectData> Effects { get; set; }
    public Dictionary<string, DropData> Drops { get; set; }
    public Dictionary<string, EngineData> Engines { get; set; }
    public Dictionary<string, WeaponData> Weapons { get; set; }
    public Dictionary<string, ShipData> Ships { get; set; }
    public Dictionary<string, CommodityData> Commodities { get; set; }
    [JsonProperty(PropertyName = "MissionTypes")]
    public Dictionary<string, MissionData> Missions { get; set; }
    public Dictionary<string, DailyMissionData> DailyMissions { get; set; }

    public abstract class BigDBObject
    {
        public string Key { get; set; }
        public string Table { get; set; }
    }
    public class ImageData
    {
        public string Key { get; set; }
        public string Table { get; set; }
        public string FileName { get; set; }
        public string TextureName { get; set; }
        public double Scale { get; set; }
        public bool Mirror { get; set; }
        public ImageType Type { get; set; }
        public bool Animate { get; set; }
        public bool AnimateOnStart { get; set; }
        public int AnimationDelay { get; set; }
        public int AnimationCellHeight { get; set; }
        public int AnimatonStrips { get; set; }
        public int AnimationCells { get; set; }
        public int AnimationCellWidth { get; set; }
        public int Volume { get; set; }
        public int AnimationStrips { get; set; }

        public enum ImageType { Ships, TechIcons, Projectiles, Music, Drops, Commodities, Spawners, Artifacts, Specials, Bodies, Boss, Crew, Effects, SolarSystems, SolarSystem }
    }
    public class DropData
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Table { get; set; } // "Drops"
        public string Type { get; set; }  // {drop, mission, explore}
        public bool Crate { get; set; }
        public double Chance { get; set; }
        public List<DropTableItem> DropItems { get; set; }
        public double ArtifactChance { get; set; }
        public int ArtifactAmount { get; set; }
        public int ArtifactLevel { get; set; }
        public int FluxMin { get; set; }
        public int FluxMax { get; set; }
        public int XpMin { get; set; }
        public int XpMax { get; set; }
        public int Reputation { get; set; }

        public class DropTableItem
        {
            public string Table { get; set; }
            public string Item { get; set; }
            public double Chance { get; set; }
            public int Min { get; set; }
            public int Max { get; set; }
        }
    }
    public class CommodityData
    {
        public string Key { get; set; }
        public string Table { get; set; }
        public string Name { get; set; }
        public CommodityType Type { get; set; }
        public int Size { get; set; }
        public int SellValue { get; set; }
        public string Bitmap { get; set; }
        public List<RecycleItemData> RecycleItems { get; set; }

        public enum CommodityType { SpaceJunk, Mineral }
        public class RecycleItemData
        {
            public string Item { get; set; }
            public int Min { get; set; }
            public int Max { get; set; }
            public double Chance { get; set; }
        }
    }
    public class EngineData
    {
        public string Key { get; set; }
        public string Table { get; set; }
        public string Name { get; set; }
        public int Speed { get; set; }
        public double Acceleration { get; set; }
        public double RotationSpeed { get; set; }
        //        public string RibbonTexture { get; set; }
        //        public int IdleThrustFinishColor { get; set; }
        //        public int RibbonThickness { get; set; }
        //        public int IdleThrustStartColor { get; set; }
        //        public bool UseEffects { get; set; }
        //        public bool ChangeIdleThrustColors { get; set; }
        //        public int ThrustStartColor { get; set; }
        //        public bool ChangeThrustColors { get; set; }
        //        public double RibbonAlpha { get; set; }
        //        public bool RibbonTrail { get; set; }
        //        public bool Dual { get; set; }
        //        public string IdleEffect { get; set; }
        //        public int RibbonColor { get; set; }
        //        public int ThrustFinishColor { get; set; }
        //        public string Effect { get; set; }
        //        public double RibbonSpeed { get; set; }
        //        public string Sound { get; set; }
        //        public int DualDistance { get; set; }
    }
    public class WeaponData
    {
        public string Key { get; set; }
        public string Table { get; set; }
        public string Name { get; set; }
        public string TechIcon { get; set; }

        public string Description { get; set; }
        public int DescriptionRefire { get; set; }
        public int DescriptionDifficulty { get; set; }
        public int DescriptionHeat { get; set; }
        public int DescriptionDmg { get; set; }
        public int DescriptionRange { get; set; }

        public string Projectile { get; set; }
        public int Speed { get; set; }
        //public List<object> EliteTechs { get; set; } TODO
        public bool IsMissileWeapon { get; set; }
        public bool MultiSideFire { get; set; }
        public int Dot { get; set; }
        public WeaponDebuffType DebuffType { get; set; }
        public int DotDuration { get; set; }
        public string Sound { get; set; }
        //public ??? TechLevels { get; set; } TODO
        public bool HasTechTree { get; set; }
        public WeaponDamageType DamageType { get; set; }
        public WeaponType Type { get; set; }
        public int MaxProjectiles { get; set; }
        public int MultiOffset { get; set; }
        public int NumberOfHits { get; set; }
        public bool MultiSpreadStart { get; set; }
        public int MultiNrOfP { get; set; }
        public int Radius { get; set; }
        public int HealthVamp { get; set; }
        public bool RandomAngle { get; set; }
        public int PositionXVariance { get; set; }
        public int EnergyDepleteTime { get; set; }
        public bool TriggerMeleeAnimation { get; set; }
        public bool HasSpecialFunction { get; set; }
        public int TwinOffset { get; set; }
        public int PvpMod { get; set; }
        public WeaponDamageType DotDamageType { get; set; }
        public int Dmg { get; set; }
        public double AngleVariance { get; set; }
        public int PositionOffset { get; set; }
        public string DotEffect { get; set; }
        public bool HasChargeUp { get; set; }
        public bool Simultaneous { get; set; }
        public int PvpModDot { get; set; }
        public int BurstDelay { get; set; }
        public double AimArc { get; set; }
        public int Range { get; set; }
        public int Ttl { get; set; }
        public bool UseShipSystem { get; set; }
        public int HeatCost { get; set; }
        public double Acceleration { get; set; }
        public bool Global { get; set; }
        public double Friction { get; set; }
        public double RotationSpeed { get; set; }
        public int ShieldVamp { get; set; }
        public bool NoRandomSpeed { get; set; }
        public int MultiAngleOffset { get; set; }
        public string FireSound { get; set; }
        public int PositionVariance { get; set; }
        public int Burst { get; set; }
        public int ReloadTime { get; set; }
        public bool Twin { get; set; }
        public bool FireBackwards { get; set; }
        public int MaxChargeDuration { get; set; }
        public string FireEffect { get; set; }
        public bool Cluster { get; set; }
        public bool HasSpecialBonus { get; set; }
        public string Enemy { get; set; }
        public int MaxPets { get; set; }
        public int EnemyHpBonus { get; set; }
        public int ChargeUpBonusDmg { get; set; }
        public int PositionOffsetY { get; set; }
        public int ChargeUp { get; set; }
        public int IncInterval { get; set; }
        public bool Homing { get; set; }
        public int Overheat { get; set; }
        public string HitEffect { get; set; }
        public int DmgInterval { get; set; }
        public string HitSound { get; set; }
        public int GlowColor { get; set; }
        public int Beams { get; set; }
        public int BeamColor { get; set; }
        public double BeamThickness { get; set; }
        public double BeamAmplitude { get; set; }
        public double BeamAlpha { get; set; }
        public double BeamNodes { get; set; }
        public int NrTargets { get; set; }
        public int IncOverheat { get; set; }
        public bool Teleport { get; set; }
        public int SpecialBonusPercentage { get; set; }
        public string SpecialCondition { get; set; }

        public enum WeaponType { Projectile, Blaster, SmartGun, PetSpawner, Beam, Teleport, Cloak }
        public enum WeaponDebuffType { None = -1, DOT = 0, StackingDOT = 1, Bomb = 2, ReduceArmor = 3, Burn = 4, DisableRegen = 5, DisableHeal = 6, ReduceDamange = 7, ReduceKineticRes = 8, ReduceEnegyRes = 9, ReduceCorrosiveRes = 10 }
        public enum WeaponDamageType { Kinetic, Energy, Corrosive, KineticEnergy, CorrosiveKinetic, Unknown, Heal, EnergyKinetic, AllThree, None, EnergyCorrosive }
    }
    public class ShipData
    {
        public string ExplosionSound { get; set; }
        public string ExplosionEffect { get; set; }
        public string Hp { get; set; }
        public string CollisionRadius { get; set; }
        public string Armor { get; set; }
        public string Name { get; set; }
        public string WeaponPosY { get; set; }
        public string Key { get; set; }
        public string EnginePosX { get; set; }
        public string ShieldRegen { get; set; }
        public string WeaponPosX { get; set; }
        public string ShieldHp { get; set; }
        public string Table { get; set; }
        public string BlendModeAdd { get; set; }
        public string Type { get; set; }
        public string EnginePosY { get; set; }
        public string Bitmap { get; set; }
        public string UseBitmapTint { get; set; }
        public string AnimationDelay { get; set; }
        public string BitmapTint { get; set; }
        public string Animation { get; set; }
        public string AnimationCellWidth { get; set; }
        public string AnimationCellHeight { get; set; }
        public string AnimationCells { get; set; }
        public string HueRotation { get; set; }
    }
    public class MissionData : BigDBObject
    {
        public string Title { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string Drop { get; set; }
        public int Expires { get; set; }
        public List<string> AddedBodies { get; set; }
        public int MinLvl { get; set; }
        public int MaxLvl { get; set; }
        public MissionMajorType MajorType { get; set; }
        public int Value { get; set; }
        public bool Available { get; set; }
        public MissionType Type { get; set; }
        public string Item { get; set; }
        public string NextMission { get; set; }
        public string CompleteDescription { get; set; }
        public List<string> AddedEnemies { get; set; }
        public MissionSubtype Subtype { get; set; }
        public bool MustBeInorder { get; set; }
        public int NrAreas { get; set; }
        public bool Proofread { get; set; }
        public bool PoliceMission { get; set; }
        public bool PirateMission { get; set; }
        public string Solarsystem { get; set; }
        public string Spawner { get; set; }
        public string Bitmap { get; set; }
        public List<object> AddedSystems { get; set; }
        public int ReqLvl { get; set; }
        public int TargetLvlReq { get; set; }
        public bool TargetsNeutral { get; set; }
        public bool TargetsPolice { get; set; }
        public bool TargetsPirate { get; set; }
        public string RewardKey { get; set; }
        public string NextMissionPolice { get; set; }
        public string NextMissionPirate { get; set; }
        public string Body { get; set; }

        public enum MissionMajorType { Time, Static, PvpChain }
        public enum MissionType { Pickup, Kill, Transport, Recycle, Level, Explore }
        public enum MissionSubtype { Ship, Boss, Frenzy, Reputation, Spawner, Player, PvpStart }
    }
    public class DailyMissionData : BigDBObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [JsonConverter(typeof(SnakeCaseEnumConverter))]
        public DailyMissionType Type { get; set; }
        public int Level { get; set; }
        public string Bitmap { get; set; }
        public List<string> Reward { get; set; }
        public DailyMissionTarget Targets { get; set; }

        public class DailyMissionTarget
        {
            public bool Any { get; set; }
            public string Type { get; set; }
            public string Planet { get; set; }
            public List<string> Enemies { get; set; }
            public string SolarSystem { get; set; }
            public int Count { get; set; }
            public bool Distinct { get; set; }
        }
        public enum DailyMissionType { Kill, UpgradeTech, LandOnPlanet, Pickup, CapturePlanet, Missions, WinPvp, PlaySurvival, HealPlayer, KillDistinct }
    }
}

public class ResourcesHelper
{
    public static void DebugTimedMissions(Resources resources)
    {
        var timed = resources.Missions.Values
            .Where(x => x.MajorType == UserQuery.Resources.MissionData.MissionMajorType.Time);

        timed
            .OrderBy(x => x.MinLvl)
            .ThenBy(x => x.Expires)
            .Select(x => new
            {
                x.Key,
                Title = x.Title.Replace("[amount]", x.Value.ToString()),
                Description = x.Description.Replace("[amount]", x.Value.ToString()),
                x.Type,
                x.Subtype,
                x.MinLvl,
                x.Expires
            })
            .Dump();
    }
}

public static class JsonHelper
{
    public static object ToObject(this JToken token)
    {
        switch (token.Type)
        {
            case JTokenType.Object:
                return token
                    .Values<JProperty>()
                    .ToDictionary(x => x.Name, x => x.Value.ToObject());

            case JTokenType.Array:
                return token
                    .Select(Tuple.Create<JToken, int>)
                    .ToDictionary(x => x.Item2.ToString(), x => x.Item1.ToObject());

            default:
                return token.Value<object>();
        }
    }

    public static Dictionary<string, JToken> ToDictionary(this JToken dictionaryRoot)
    {
        return dictionaryRoot
            .Values<JProperty>()
            .ToDictionary(x => x.Name, x => x.Value);
    }
    public static object GetCommonClassDefinition(JToken dictionaryRoot)
    {
        return string.Join("\n", dictionaryRoot
            .Cast<JProperty>()
            .ToDictionary(x => x.Name, x => (JObject)x.Value)
            .Values.SelectMany(x => x.Properties())
            .Select(x => x.Name)
            .Distinct()
            .Select(x => $"public string {x.Pascalize()} {{ get; set; }}")
        );
    }
}
public static class EnumerableExtensions
{
    public static Dictionary<TKey, T> ToDistnctDictionary<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector)
    {
        return source.ToDistnctDictionary(keySelector, x => x);
    }
    public static Dictionary<TKey, TValue> ToDistnctDictionary<T, TKey, TValue>(this IEnumerable<T> source, Func<T, TKey> keySelector, Func<T, TValue> valueSelector)
    {
        return source
            .GroupBy(keySelector)
            .ToDictionary(g => g.Key, g => g.Select(valueSelector).First());
    }
}
public class SnakeCaseEnumConverter : JsonConverter
{
    public override bool CanConvert(Type type)
    {
        type = IsNullableType(type)
            ? Nullable.GetUnderlyingType(type)
            : type;

        return type.IsEnum;
    }
    
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
        {
            if (!IsNullableType(objectType))
                throw new JsonSerializationException($"Cannot convert null value to {objectType}.");
            
            return null;
        }
        
        var type = IsNullableType(objectType)
            ? Nullable.GetUnderlyingType(objectType)
            : objectType;
        var value = reader.Value.ToString().Pascalize();
        var result = Enum.GetValues(type).Cast<Enum>()
            .FirstOrDefault(x => x.ToString() == value);
        if (result == null)
            throw new JsonSerializationException($"Error converting value \"{reader.Value.ToString()}\" to type '{objectType}'.");
        
        return result;
    }

    private bool IsNullableType(Type type)
    {
        return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
    }
}