namespace NewApp

open SharpDX.Direct3D11
open Weapons.Weapons

module Counter =
    open Avalonia.Controls
    open Avalonia.FuncUI.DSL
    open Avalonia.Layout
    
    type State = {
        message: string
        baseWeapon: Weapon
        moddedWeapon: Weapon
    }
    
    let baseWeapon = {
            attackSpeed = 100.0
            damage = listToDmg [1.5; 1.7; 69.0]
        }
    
    let init: State = {
        message = "blah"
        baseWeapon = baseWeapon
        moddedWeapon = baseWeapon
    }

    type Msg = IncreaseDamage | Reset
       
    let formatDamageMessage (weapon: Weapon): string =
            "Rate of Fire: " + string weapon.attackSpeed
            + "\nImpact: " + string weapon.damage.impact
            + "\nPuncture: " + string weapon.damage.puncture
            + "\nSlash: " + string weapon.damage.slash
            + "\n"
            
    let stateView (state: State) =
        formatDamageMessage state.moddedWeapon + state.message
        
    let calculate (state: State): State =
        let total = dps state.moddedWeapon
        { state with message = string total }
        
    let applyDmgIncrease (state: State): State =
        { state with moddedWeapon = state.moddedWeapon |> (plusDmgPercent 100.0 state.baseWeapon) }
        
    let update (msg: Msg) (state: State) : State =
        match msg with
        | Reset -> state 
        | IncreaseDamage -> state |> applyDmgIncrease
        |> calculate
        
    let view (state: State) (dispatch) =
        let message = stateView state
        DockPanel.create [
            DockPanel.children [
                Button.create [
                    Button.dock Dock.Bottom
                    Button.onClick (fun _ -> dispatch Reset)
                    Button.content "CALCULATE"
                ]
                Button.create [
                    Button.dock Dock.Bottom
                    Button.onClick (fun _ -> dispatch IncreaseDamage)
                    Button.content "INCREASE DAMAGE 100%"
                ]
                TextBlock.create [
                    TextBlock.dock Dock.Top
                    TextBlock.fontSize 48.0
                    TextBlock.verticalAlignment VerticalAlignment.Center
                    TextBlock.horizontalAlignment HorizontalAlignment.Center
                    TextBlock.text message
                ]
            ]
        ]
        
     