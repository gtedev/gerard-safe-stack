module Workout

open Types
open AppCss
open FSharp.Core.Extensions
open Fable.React
open Fable.React.Props
open Fulma
open Fable.Core 

let header () =
    Heading.p [ Heading.Modifiers [ Modifier.TextAlignment(Screen.All, TextAlignment.Centered) ] ] [
        str "Gerard Workouts"
    ]

let allExercises (model: Model) (dispatch: Msg -> unit) =
    let selectedTab =
        List.find (fun t -> t.isSelected) model.Tabs

    let workouts =
        selectedTab.WorkoutItems
        |> List.map
            (fun exo ->
                Panel.checkbox [ Panel.Block.Option.Props [ OnClick
                                                                (fun _ -> WorkoutClicked(selectedTab, exo) |> dispatch) ] ] [
                    input [ Type "checkbox"
                            Checked exo.isSelected
                            ClassName AppCss.ButtonHover ]
                    str (exo.Item |> getWorkoutName)
                ])

    Field.div [ Field.Option.CustomClass AppCss.ExercisesPanel ] workouts


let seanceDuJour (model: Model) (dispatch: Msg -> unit) =
    let myList =
        model.WorkoutOfDay
        |> List.mapi
            (fun index workOutExo ->

                match workOutExo.Item with

                | Exercise exo ->

                    div [ Class "panel-block workout-exo button-hover" ] [
                        div [] [
                            input [ Type "checkbox"
                                    ClassName "button-hover" ]
                            str (workOutExo.Item |> getWorkoutName)
                        ]
                        div [ OnClick(fun _ -> RemoveWorkoutOfDay(index, workOutExo) |> dispatch) ] [
                            Icon.icon [ Icon.Size IsSmall ] [
                                i [ ClassName "fas fa-trash-alt red button-hover" ] []
                            ]
                        ]
                    ]

                | Program prgm ->

                    div [] [
                        div [ Class "panel-block workout-exo button-hover" ] [
                            div [] [
                                str (workOutExo.Item |> getWorkoutName)
                            ]
                            div [ OnClick(fun _ -> RemoveWorkoutOfDay(index, workOutExo) |> dispatch) ] [
                                Icon.icon [ Icon.Size IsSmall ] [
                                    i [ ClassName "fas fa-trash-alt red button-hover" ] []
                                ]
                            ]
                        ]
                        for exo in prgm.Exercises do
                            div [ Class "panel-block" ] [
                                input [ Type "checkbox"
                                        ClassName "button-hover"
                                        Style [ MarginLeft 30 ] ]
                                str (exo.Name)
                            ]
                    ]

                )

    Field.div [] myList


let exercisesPanel model dispatch =
    div [] [
        Panel.heading [] [ str "Exercises" ]
        Panel.tabs [] [
            for tab in model.Tabs do
                Panel.tab [ Panel.Tab.IsActive tab.isSelected
                            Panel.Tab.Props [ OnClick(fun _ -> TabClicked tab |> dispatch) ] ] [
                    str tab.Name
                ]
        ]

        allExercises model dispatch

        Panel.Block.div [] [
            Button.button [ Button.Color IsPrimary
                            Button.IsOutlined
                            Button.IsFullWidth
                            Button.OnClick(fun _ -> AddWorkoutItemsClicked |> dispatch) ] [
                str "Ajouter"
            ]
        ]
    ]


let workOutOfDayPanel model dispatch =
    let now = today().ToString("yyyy-MM-dd")
    let workoutDate = model.WorkoutDate.ToString("yyyy-MM-dd")

    JS.console.log now

    Panel.panel [ Panel.Option.CustomClass AppCss.WorkoutPanel ] [
        Panel.heading [] [
            str "Séance du jour"
            input [
                ClassName "workout-date input is-primary is-small"
                Type "date"; Max now ; Value workoutDate
                OnChange (fun onChangeDateEvent -> OnWorkoutDateChanged (onChangeDateEvent.Value |> toDateTime )|> dispatch)]
        ]

        seanceDuJour model dispatch
    ]


let mainPanel (model: Model) (dispatch: Msg -> unit) =
    Box.box' [] [
        Panel.panel [] [
            exercisesPanel model dispatch
            workOutOfDayPanel model dispatch
        ]
    ]
