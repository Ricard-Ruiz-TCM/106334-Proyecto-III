
// Enum con las escenas del juego IMPORTANTE "Match" de nombre
public enum gameScenes {
    Intro, Menu, StageSelector, Event, Stage, Credits
}

// Enum "básico" para determinar el estado de algo
public enum state {
    Active, Inactive
}

// Enum para la seguridad de la FStateMachine
public enum fsmSecurity {
    None, Soft, Hard
}

// Enum que determina el tipo de audio que vamos a reproducir
public enum audioType {
    SFX_3D, SFX_2D, SoundTrack
}

// Enum que determina el timpo de Input D: (algo más "homemade")
public enum inputScheme {
    Keyboard, Mouse, GamePad
}

// Enum para indicar los idiomas que tiene le juego en temas de Localización
public enum language {
    EN, ES
}

// Enum para los diferentes efectos disponibles
public enum effects {
    fadeIn, fadeOut, cameraShake
}

// Enum para indicar el progresso de algo
public enum progress {
    ready, doing, done
}

// GAME ENUMS


// Tipos y esatdos del sistema de turnos
public enum rounds {
    waitingRound, positioningRound, combatRound, endRound
}

// Todos los items del juego
public enum items {
    NONE,

    // Weapons
    Gladius, Hasta, Bow, Catapult, Pugio, Dolabra,
    // Armors
    Segmentata, Hamata, Squamata, CatapultCoating,
    // Shields
    Scutum,

    MAX
}

// Todas las skills que existen
public enum skills {
    NONE,

    ArrowRain, Attack, Defense, DemolishingBlow, Disarm, DoubleLunge, MoralizingShout, TurtleShield, Vanish,

    MAX
}

// Todas las perks que existen
public enum perks {
    NONE,

    PerkDisarmSkill, PerkTurtleShieldSkill, PerkEmpireRage, PerkCementSkin,

    MAX
}

// Tipo de modificación que da una perk
public enum perkModification {
    damage, defense, health, skill
}

// Tipo de perk
public enum perkCategory {
    offensive, defensive
}

// Tipo de combate del enemigo
public enum enemyCombatStyle {
    ranged, melee
}

// Tipo de estados
public enum aStatus {
    Stunned, Bleed, Poisoned, Invisible
}

// Dificultad del stage
public enum stageDifficulty {
    diff0, diff1, diff2
}

// Tipo de stage
public enum stageType {
    combat, comrade, blacksmith, campfire
}

// Tipo de terreno del stage
public enum stageTerrain {
    mountain, swamp, meadow
}