namespace Weapons

open System.Collections.Generic

module public Weapons =
    
    type PhysicalDamage =
        {
            impact: float
            puncture: float
            slash: float
        }
        
    type Weapon =
        {
            attackSpeed: float
            damage: PhysicalDamage
        }
        
    type Mod = Weapon -> Weapon
    
    let dmgToList (damage: PhysicalDamage): list<float> =
        [
            damage.impact
            damage.puncture
            damage.slash
        ]
    
    let listToDmg (dmgList: list<float>): PhysicalDamage =
        {
            impact = dmgList.Item(0)
            puncture = dmgList.Item(1)
            slash = dmgList.Item(2)
        }
        
    let addList (damageA: list<float>) (damageB: list<float>): list<float> =
        [for i in 0..(damageA.Length - 1) -> damageA.Item(i) + damageB.Item(i)]
        
    let multiByScalar (scalar: float) (damage: PhysicalDamage): PhysicalDamage =
        damage
        |> dmgToList
        |> List.map(fun x -> x * scalar)
        |> listToDmg
    
    let addDamage (damageA: PhysicalDamage) (damageB: PhysicalDamage): PhysicalDamage =
        let dmgBList = damageB|>dmgToList
        damageA
        |> dmgToList
        |> (addList dmgBList)
        |> listToDmg
        
    let plusDmgPercent (percent: float) (baseWeapon: Weapon) (moddedWeapon: Weapon): Weapon =
        let additionalDamage = multiByScalar (percent/100.0) baseWeapon.damage
        { moddedWeapon with damage = addDamage moddedWeapon.damage additionalDamage }
    
    let perHitTotal (damage: PhysicalDamage): float =
        damage
        |> dmgToList
        |> List.reduce(+)

    let dps (weapon: Weapon) : float =
        weapon.damage
        |> perHitTotal
        |> (fun x -> x * weapon.attackSpeed)
        
    