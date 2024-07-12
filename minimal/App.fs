module App

(**
 The famous Increment/Decrement ported from Elm.
 You can find more info about Elmish architecture and samples at https://elmish.github.io/
*)

open Elmish
open Elmish.React
open Elmish.Debug
open Feliz

// MODEL

type Model = int

type Msg =
    | Increment
    | Decrement

let init () : Model = 0

// UPDATE

let update (msg: Msg) (model: Model) =
    match msg with
    | Increment -> model + 1
    | Decrement -> model - 1

// VIEW (rendered with React)
[<ReactComponent>]
let View (model: Model) dispatch =
    React.useEffect(fun () -> Browser.Dom.document.title <- sprintf "Count = %d" model)
    
    Html.div [
        Html.button [
            prop.onClick(fun _ -> dispatch Increment)
            prop.text "+"
        ]
        Html.div [ prop.text (string model) ]
        Html.button [
            prop.onClick(fun _ -> dispatch Decrement)
            prop.text "-"
        ]
    ]

// App
Program.mkSimple init update View
|> Program.withReactSynchronous "elmish-app"
|> Program.withConsoleTrace
|> Program.withDebugger
|> Program.run
