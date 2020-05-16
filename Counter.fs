namespace NewApp

module Counter =
    open Avalonia.Controls
    open Avalonia.FuncUI.DSL
    open Avalonia.Layout
    
    type State = {
        realPart : int
        imaginaryPart: int
    }
    let init = {
        realPart = 0
        imaginaryPart = 0
    }

    type Msg = IncrementReal | DecrementReal | IncrementImaginary | DecrementImaginary | Reset

    let update (msg: Msg) (state: State) : State =
        match msg with
        | IncrementReal -> { state with realPart = state.realPart + 1 }
        | DecrementReal -> { state with realPart = state.realPart - 1 }
        | IncrementImaginary -> { state with imaginaryPart = state.imaginaryPart + 1 }
        | DecrementImaginary -> { state with imaginaryPart = state.imaginaryPart - 1 }
        | Reset -> init
    
    let view (state: State) (dispatch) =
        DockPanel.create [
            DockPanel.children [
                Button.create [
                    Button.dock Dock.Bottom
                    Button.onClick (fun _ -> dispatch Reset)
                    Button.content "reset"
                ]
                DockPanel.create [
                    DockPanel.dock Dock.Bottom
                    DockPanel.children [
                        Button.create [
                            Button.dock Dock.Left
                            Button.width 200.0
                            Button.horizontalAlignment HorizontalAlignment.Stretch
                            Button.onClick (fun _ -> dispatch DecrementImaginary)
                            Button.content "-i"
                        ]
                        Button.create [
                            Button.dock Dock.Right
                            Button.horizontalAlignment HorizontalAlignment.Stretch
                            Button.onClick (fun _ -> dispatch IncrementImaginary)
                            Button.content "+i"
                        ]
                    ]
                ]
                DockPanel.create [
                    DockPanel.dock Dock.Bottom
                    DockPanel.children [
                        Button.create [
                            Button.dock Dock.Left
                            Button.width 200.0
                            Button.horizontalAlignment HorizontalAlignment.Stretch
                            Button.onClick (fun _ -> dispatch DecrementReal)
                            Button.content "-"
                        ]
                        Button.create [
                            Button.dock Dock.Right
                            Button.horizontalAlignment HorizontalAlignment.Stretch
                            Button.onClick (fun _ -> dispatch IncrementReal)
                            Button.content "+"
                        ]
                    ]
                ]
                TextBlock.create [
                    TextBlock.dock Dock.Top
                    TextBlock.fontSize 48.0
                    TextBlock.verticalAlignment VerticalAlignment.Center
                    TextBlock.horizontalAlignment HorizontalAlignment.Center
                    TextBlock.text (string state.realPart + " + " + string state.imaginaryPart + "i")
                ]
            ]
        ]
        
     