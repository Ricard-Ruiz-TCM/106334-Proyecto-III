﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Attack", menuName = "Combat/Skills/Attack")]
public class Attack : Skill {
    public List<Slash> slashes;
    [SerializeField] GameObject bloodPrefab;
    public override void action(BasicActor from, Node to) {
        BasicActor target = Stage.StageManager.getActor(to);
        
        if (target != null)
        {

            WeaponItem weapon = ((Actor)from).equip.weapon;
            if (target is PilarStatic)
            {
                switch (weapon.ID)
                {
                    case itemID.Bow:
                        FMODManager.instance.PlayOneShot(FMODEvents.instance.InicioLanzarFlecha);
                        FMODManager.instance.PlayOneShot(FMODEvents.instance.FlechaPiedra);
                        break;
                    case itemID.Dolabra:
                        FMODManager.instance.PlayOneShot(FMODEvents.instance.DolabraPiedra);
                        from.StartCoroutine(StartSlash(from));
                        break;
                    case itemID.Gladius:
                        FMODManager.instance.PlayOneShot(FMODEvents.instance.GladiusPiedra);
                        from.StartCoroutine(StartSlash(from));
                        break;
                    case itemID.Hasta:
                        FMODManager.instance.PlayOneShot(FMODEvents.instance.HastaPiedra);
                        from.StartCoroutine(StartSlash(from));
                        break;
                    case itemID.Pugio:
                        FMODManager.instance.PlayOneShot(FMODEvents.instance.PugioPiedra);
                        from.StartCoroutine(StartSlash(from));
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (weapon.ID)
                {
                    case itemID.Bow:
                        FMODManager.instance.PlayOneShot(FMODEvents.instance.InicioLanzarFlecha);
                        FMODManager.instance.PlayOneShot(FMODEvents.instance.FlechaContraCarne);
                        break;
                    case itemID.Dolabra:
                        FMODManager.instance.PlayOneShot(FMODEvents.instance.DolabraContraCarne);
                        break;
                    case itemID.Gladius:
                        FMODManager.instance.PlayOneShot(FMODEvents.instance.GladiusContraCarne);
                        break;
                    case itemID.Hasta:
                        FMODManager.instance.PlayOneShot(FMODEvents.instance.HastaContraCarne);
                        break;
                    case itemID.Pugio:
                        FMODManager.instance.PlayOneShot(FMODEvents.instance.PugioContraCarne);
                        break;
                    default:
                        break;
                }
            }            

            target.takeDamage((Actor)from, from.totalDamage());
            var lookPos = target.transform.position - from.transform.position;

            Vector3 relativePos = from.transform.position - target.transform.position;

            // the second argument, upwards, defaults to Vector3.up
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);

            GameObject blood = Instantiate(bloodPrefab, new Vector3(target.transform.position.x, target.transform.position.y + 0.8f, target.transform.position.z), rotation);
            Destroy(blood, 2f);

            lookPos.y = 0;
            from.transform.rotation = Quaternion.LookRotation(lookPos);
        }
        else
        {
            FMODManager.instance.PlayOneShot(FMODEvents.instance.MissAttack);
        }
        
        ((Actor)from).endAction();
    }
    IEnumerator StartSlash(BasicActor from)
    {
        GameObject go;        
        yield return new WaitForSeconds(0.5f);        
        for (int i = 0; i < slashes.Count; i++)
        {
            go = Instantiate(slashes[i].objSlash, Vector3.zero, Quaternion.identity);
            go.transform.SetParent(from.transform);
            go.transform.localPosition = slashes[i].pos;
            go.transform.localRotation = Quaternion.identity;
            go.SetActive(true);
            yield return new WaitForSeconds(slashes[i].delay);
            Destroy(go);
        }
        DissableSlashes();
    }
    void DissableSlashes()
    {
        for (int i = 0; i < slashes.Count; i++)
        {
            slashes[i].objSlash.SetActive(false);
        }
    }

}

[System.Serializable]
public class Slash
{
    public GameObject objSlash;
    public float delay;
    public Vector3 pos;
}
