module Index

open Elmish
open Fable.Remoting.Client
open Shared
open Types
open AppCss

let todosApi =
    Remoting.createApi ()
    |> Remoting.withRouteBuilder Route.builder
    |> Remoting.buildProxy<ITodosApi>

let init (): Model * Cmd<Msg> =

    let cordeASatuer =
        [ { Item =
                Program
                    { Name = "3x1 1x2 3x5"
                      Exercises =
                          [ { Name = "1 min" }
                            { Name = "1 min" }
                            { Name = "1 min" }
                            { Name = "2 min" }
                            { Name = "5 min" }
                            { Name = "5 min" }
                            { Name = "5 min" } ] }
            isSelected = false }
          { Item = Exercise { Name = "5 min" }
            isSelected = false }
          { Item = Exercise { Name = "2 min" }
            isSelected = false }
          { Item = Exercise { Name = "1 min" }
            isSelected = false } ]

    let pompes =
        [ { Item = Exercise { Name = "10 pompes" }
            isSelected = false }
          { Item = Exercise { Name = "20 pompes" }
            isSelected = false }
          { Item = Exercise { Name = "20 pompes lestees 10kgs" }
            isSelected = false }
          { Item = Exercise { Name = "20 pompes jambes suspension" }
            isSelected = false } ]

    let dips =
        [ { Item = Exercise { Name = "10 dips" }
            isSelected = false }
          { Item = Exercise { Name = "20 dips lestees 10kgs" }
            isSelected = false } ]

    let tractions =
        [ { Item = Exercise { Name = "6 tractions" }
            isSelected = false }
          { Item = Exercise { Name = "3 lst 10kgs - 3" }
            isSelected = false } ]

    let abs =
        [ { Item = Exercise { Name = "(5 stomach vaccumms - 5 abwheels) x 5" }
            isSelected = false } ]

    let courses =
        [ { Item = Exercise { Name = "5 km" }
            isSelected = false }
          { Item = Exercise { Name = "10 km" }
            isSelected = false } ]

    let model =
        { WorkoutOfDay = []
          Tabs =
              [ { Name = "Corde a sauter"
                  isSelected = true
                  WorkoutItems = cordeASatuer }
                { Name = "Pompes"
                  isSelected = false
                  WorkoutItems = pompes }
                { Name = "Dips"
                  isSelected = false
                  WorkoutItems = dips }
                { Name = "Tractions"
                  isSelected = false
                  WorkoutItems = tractions }
                { Name = "Abdominaux"
                  isSelected = false
                  WorkoutItems = abs }
                { Name = "Course a pied"
                  isSelected = false
                  WorkoutItems = courses } ] }

    model, Cmd.none


let getWorkoutName w =
    match w with
    | Exercise exo -> exo.Name
    | Program prgm -> prgm.Name

let update (msg: Msg) (model: Model): Model * Cmd<Msg> =
    match msg with
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

open Fable.React
open Fable.React.Props
open Fulma

let navBrand =
    Navbar.Brand.div [] [
        Navbar.Item.a [ Navbar.Item.Props [ Href "https://safe-stack.github.io/" ]
                        Navbar.Item.IsActive true ] [
            img [ Src "/favicon.png"; Alt "Logo" ]
        ]
    ]

let tabContent (model: Model) (dispatch: Msg -> unit) =
    let selectedTab =
        List.find (fun t -> t.isSelected) model.Tabs

    let tabContent =
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

    Field.div [ Field.Option.CustomClass AppCss.ExercisesPanel ] tabContent

let containerBox (model: Model) (dispatch: Msg -> unit) =
    Box.box' [] [
        Panel.panel [] [
            Panel.heading [] [ str "Exercises" ]
            Panel.tabs [] [
                for tab in model.Tabs do
                    Panel.tab [ Panel.Tab.IsActive tab.isSelected
                                Panel.Tab.Props [ OnClick(fun _ -> TabClicked tab |> dispatch) ] ] [
                        str tab.Name
                    ]
            ]

            tabContent model dispatch

            Panel.Block.div [] [
                Button.button [ Button.Color IsPrimary
                                Button.IsOutlined
                                Button.IsFullWidth
                                Button.OnClick(fun _ -> AddWorkoutItemsClicked |> dispatch) ] [
                    str "Ajouter"
                ]
            ]
            Panel.panel [ Panel.Option.CustomClass AppCss.WorkoutPanel ] [
                Panel.heading [] [
                    str "Séance du jour"
                ]

                for workOutExo in model.WorkoutOfDay do
                    div [ Class "panel-block workout-exo button-hover" ] [

                        div [] [
                            input [ Type "checkbox"
                                    ClassName "button-hover" ]
                            str (workOutExo.Item |> getWorkoutName)
                        ]
                        div [] [
                            Icon.icon [ Icon.Size IsSmall ] [
                                i [ ClassName "fas fa-trash-alt red button-hover" ] []
                            ]
                        ]
                    ]
            ]
        ]
    ]

let view (model: Model) (dispatch: Msg -> unit) =
    Hero.hero [ Hero.Color IsPrimary
                Hero.IsFullHeight
                Hero.Props [ Style [ Background
                                         """linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)), url("https://unsplash.it/1200/900?random") no-repeat center center fixed"""
                                     BackgroundSize "cover" ] ] ] [
        Hero.head [] [
            Navbar.navbar [] [
                Container.container [] [ navBrand ]
            ]
        ]

        Hero.body [] [
            Container.container [] [
                Column.column [ Column.Width(Screen.All, Column.Is6)
                                Column.Offset(Screen.All, Column.Is3) ] [
                    Heading.p [ Heading.Modifiers [ Modifier.TextAlignment(Screen.All, TextAlignment.Centered) ] ] [
                        str "Gerard Workouts"
                    ]
                    containerBox model dispatch
                ]
            ]
        ]
    ]
