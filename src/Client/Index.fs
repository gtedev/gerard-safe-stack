module Index

open Elmish
open Fable.Remoting.Client
open Shared
open Types
open FSharp.Core.Extensions
open Fable.React
open Fable.React.Props
open Fulma
open Fable.Core

let workoutApi =
    Remoting.createApi ()
    |> Remoting.withRouteBuilder Route.builder
    |> Remoting.buildProxy<WorkoutApi>

let init (): Model * Cmd<Msg> =

    let model =
        { WorkoutDate = today ()
          WorkoutOfDay = []
          Tabs = [{ isSelected = true ; Name= "Loading workouts..." ; WorkoutItems = [] }]  }

    let cmd =
        Cmd.OfAsync.perform workoutApi.getWorkouts () GotWorkouts

    model, cmd


let update (msg: Msg) (model: Model): Model * Cmd<Msg> =
    match msg with
    | GotWorkouts workouts ->
        let tabs =
            workouts
            |> List.groupBy (fun w -> w |> getWorkoutNameFamily)
            |> List.mapi (fun index grp ->
                { Name = fst grp
                  isSelected = index = 0
                  WorkoutItems = grp |> toSelectableWorkoutItems  })

        let newModel = { model with Tabs = tabs}

        (newModel, Cmd.none)

    | TabClicked tab ->

        let clickedTab = { tab with isSelected = true }

        let newTabs =
            model.Tabs
            |> List.map (fun tab -> { tab with isSelected = false })
            |> List.map (fun tab -> if tab.Name = clickedTab.Name then clickedTab else tab)

        ({ model with Tabs = newTabs }, Cmd.none)


    | WorkoutClicked (tab, workout) ->

        let newWorkout =
            { workout with
                  isSelected = not workout.isSelected }

        let newWorkoutItems =
            tab.WorkoutItems
            |> List.map
                (fun wkout ->
                    if (wkout.Item |> getWorkoutName) = (newWorkout.Item |> getWorkoutName)
                    then newWorkout
                    else wkout)

        let clickedTab =
            { tab with
                  WorkoutItems = newWorkoutItems }

        let newTabs =
            model.Tabs
            |> List.map (fun tab -> if tab.Name = clickedTab.Name then clickedTab else tab)

        ({ model with Tabs = newTabs }, Cmd.none)


    | AddWorkoutItemsClicked ->

        let selectedWorkoutItems =
            model.Tabs
            |> List.collect (fun tab -> tab.WorkoutItems)
            |> List.filter (fun wkout -> wkout.isSelected)

        let newTabs =
            model.Tabs
            |> List.map
                (fun tab ->

                    let newWorkouts =
                        tab.WorkoutItems
                        |> List.map (fun exo -> { exo with isSelected = false })

                    { tab with WorkoutItems = newWorkouts })

        ({ model with
               WorkoutOfDay = model.WorkoutOfDay @ selectedWorkoutItems
               Tabs = newTabs },
         Cmd.none)

    | RemoveWorkoutOfDay (index, _) ->

        let newWorkoutOfDays =
            model.WorkoutOfDay |> List.tryRemoveAt index

        ({ model with
               WorkoutOfDay = newWorkoutOfDays },
         Cmd.none)

    | OnWorkoutDateChanged selectedDate ->

        ({ model with
               WorkoutDate = selectedDate },
         Cmd.none)



let view (model: Model) (dispatch: Msg -> unit) =
    Hero.hero [ Hero.Color IsPrimary
                Hero.IsFullHeight
                Hero.Props [ Style [ Background
                                         """linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)), url("https://unsplash.it/1200/900?random") no-repeat center center fixed"""
                                     BackgroundSize "cover" ] ] ] [
        Hero.head [] [
            Navbar.navbar [] [
                Container.container [] [
                    Navbar.Brand.div [] [
                        Navbar.Item.a [ Navbar.Item.Props [ Href "https://safe-stack.github.io/" ]
                                        Navbar.Item.IsActive true ] [
                            img [ Src "/favicon.png"; Alt "Logo" ]
                        ]
                    ]
                ]
            ]
        ]

        Hero.body [] [
            Container.container [] [
                Column.column [ Column.Width(Screen.All, Column.Is6)
                                Column.Offset(Screen.All, Column.Is3) ] [
                    Workout.header ()
                    Workout.mainPanel model dispatch
                ]
            ]
        ]
    ]
