module App

(**
 The famous Increment/Decrement ported from Elm.
 You can find more info about Elmish architecture and samples at https://elmish.github.io/
*)

open Elmish
open Elmish.React
open Elmish.Debug
open Fable.React
open Fable.React.Props
open Elmish
open Elmish.Toastr

Fable.Core.JsInterop.importSideEffects "../node_modules/toastr/toastr.scss"
// MODEL


type Model = int

type Msg =
    | Increment
    | Decrement

let init ()  = 0, Cmd.none

// UPDATE

let update (msg: Msg) (model: Model) =
    let infoToast = 
            Toastr.message "You clicked previous toast"
            |> Toastr.title "Clicked"
            |> Toastr.info

    match msg with
    | Increment -> model + 1, infoToast
    | Decrement -> model - 1, Cmd.none

// VIEW (rendered with React)

let view (model: Model) dispatch =

    div [] [
        button [ OnClick(fun _ -> dispatch Increment) ] [
            str "+"
        ]
        div [] [ str (string model) ]
        button [ OnClick(fun _ -> dispatch Decrement) ] [
            str "-"
        ]
    ]

// App
Program.mkProgram init update view
|> Program.withReactSynchronous "elmish-app"
|> Program.withConsoleTrace
|> Program.withDebugger
|> Program.run
