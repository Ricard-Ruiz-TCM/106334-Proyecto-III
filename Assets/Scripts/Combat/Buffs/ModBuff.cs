using UnityEngine;

public abstract class ModBuff : Buff {

    [Header("Effect on:")]
    public modType type;
    [Header("Value:")]
    public int value;
    [Header("How affect:")]
    public modOperation operation;

    public int applyMod(int inputValue) {
        switch (operation) {
            case modOperation.add:
                return (inputValue + value);
            case modOperation.sub:
                return (inputValue - value);
            case modOperation.mult:
                return (inputValue * value);
            case modOperation.div:
                return (inputValue * value);
        }
        return inputValue;
    }

}
