
// Tipo de rondas
public enum roundType {
    thinking, positioning, combat, completed
}

// Estado del turno 
public enum turnState {
    thinking, acting, moving, completed
}

// Objetos del juego
public enum itemID {
    NONE,

    // Armas
    Gladius, Hasta, Bow, Catapult, Pugio, Dolabra,
    // Armaduras
    Segmentata, Hamata, Squamata, CatapultCoating,
    // Escudos
    Scutum,

    MAX
}

// Habilidades del juego
public enum skillID {
    NONE,

    // Offensive
    Attack, ArrowRain, DoubleLunge, Cleave, Bloodlust,
    // Defensive
    Defense, Vanish, ImperialCry, AchillesHeel, Disarm, TortoiseFormation, TrojanHorse,


    MAX
}

// Talentos del juego
public enum perkID {
    NONE,

    // Offensive
    EmpireRage, Bloodlust, WeaponExpert, GladiusExpert, HastaExpert, BowExpert, PoisonUse, DeepThrusts,
    // Defensive
    IronSkin, Disarm, TortoiseFormation, AchillesHeel, TrojanHorse, ArmorExpert, SegmentataExpert, HamataExpert, SquamataExpert,

    MAX
}


// Modifiación aplicable
public enum modType {
    damage, defense, health, skill, movement, special
}

// Tipo de operador de modificación
public enum modOperation {
    add, sub, mult, div
}

// Tipo de IA de enemigos
public enum enemyCombatStyle {
    ranged, melee, flee
}

// Buffos del juego
public enum buffsID {
    NONE,

    // Daño
    Bleeding, Poisoned, Burned,
    // Buffos "Planos"
    LowDefense, MidDefense, MidDamage, MidMovement,
    // Efecto
    Disarmed, Stunned, Invisible, Motivated, Invencible, ArrowProof, 

    MAX
}

// Dificultad del stage
public enum stageDifficulty {
    diff0, diff1, diff2
}

// Tipo de stage
public enum stageType {
    combat, comrade, blacksmith, campfire
}

// Dirección de entrada
public enum stageEntrance {
    north, south, east, west
}

// Resolución del stage
public enum stageResolution {
    victory, defeat
}

// Objetivo del stage
public enum stageObjetive {
    killAll, protectNPC, clearPath
}

// Tipo de terreno del stage
public enum stageTerrain {
    mountain, swamp, road
}

// Tipo de enemigos
public enum enemyClass {
    eBandid = 1, ePrivate = 2, eCenturion = 3, eCommander = 5
}

// Tipo de dificultad del terreno, Enum especial para el package de Array2DEditor
namespace Array2DEditor {
    public enum nodeType {
        __ = 0,
        P = 1,
        X = 255
    }
}