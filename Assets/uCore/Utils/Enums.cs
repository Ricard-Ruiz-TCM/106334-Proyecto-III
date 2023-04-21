﻿
// Enum con las escenas del juego IMPORTANTE "Match" de nombre
public enum gameScenes {
    Intro, Menu, StageSelector, Event, Stage, Credits
}

// Enum "básico" para determinar el estado de algo
public enum status {
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

// Todos los items del juego
public enum items {
    NONE,
    // Weapons
    Gladius, Hasta, Bow, Dolabra, Pugi,
    // Armors
    LeatherArmor,
    // Resoruces and Quests/Books
    Leather, Book0, Quest0,

    MAX
}