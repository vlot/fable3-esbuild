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

open Feliz
open Feliz.Bulma


Fable.Core.JsInterop.importSideEffects "../node_modules/toastr/toastr.scss"
Fable.Core.JsInterop.importSideEffects "../node_modules/bulma/bulma.sass"


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
    let mydiv (clazz:string) (kids:seq<ReactElement>) = 
        Html.div [ 
            prop.className clazz
            prop.children kids
        ]

    let mydiv' classes (kids:seq<ReactElement>) = 
        Html.div [ 
            prop.classes classes
            prop.children kids
        ]


    div [] [
        Fable.React.Standard.button [ OnClick(fun _ -> dispatch Increment) ] [
            str "+"
        ]
        div [] [ str (string model) ]
        Fable.React.Standard.button [ OnClick(fun _ -> dispatch Decrement) ] [
            str "-"
        ]
        
        hr [] 
        Bulma.button.button [
           Bulma.color.isPrimary
           prop.text "Primary"
        ]

        hr [] 
        
        
        Bulma.hero [ 
          hero.isFullHeight 
          color.isLight
          prop.children [
            Bulma.heroBody [
                Bulma.container [
                    Bulma.columns [ 
                        columns.isCentered
                        prop.children [
                            Bulma.column [ 
                                column.is5Tablet
                                column.is4Desktop 
                                column.is3Widescreen
                            // mydiv' ["column"; "is-5-tablet"; "is-4-desktop"; "is-3-widescreen"] 
                            
                                prop.children [
                            Html.form [
                                Bulma.field.div [
                                    Bulma.label "Username"
                                    Bulma.control.div [
                                        Bulma.input.text [
                                            prop.placeholder "nickname"
                                        ]
                                    ]
                                ]
                                Bulma.field.div [
                                    Bulma.label "Password"
                                    Bulma.control.div [
                                        Bulma.input.password [
                                            prop.placeholder "*****"
                                        ]
                                    ]
                                ]
                                Bulma.field.div [
                                    Bulma.field.isGrouped
                                    Bulma.field.isGroupedCentered
                                    prop.children [
                                        Bulma.control.div [
                                            Bulma.button.button [
                                                Bulma.color.isLink
                                                prop.text "Submit"
                                            ]
                                        ]
                                    ]
                                ]
                            ]
                            ]
                        ]
                    ]
                    ]
                ]
            ]
            
              

          ]
        ]


    ]

// App
Program.mkProgram init update view
|> Program.withReactSynchronous "elmish-app"
|> Program.withConsoleTrace
|> Program.withDebugger
|> Program.run
