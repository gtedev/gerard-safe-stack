module Index

open Elmish
open Fable.Remoting.Client
open Shared

type Serie =
    | Serie of string
    | Program of string list

type Exercise = { Name: string; isSelected: bool }

type Tab =
    { Name: string
      isSelected: bool
      Exercises: Exercise list }

type Model =
    { Todos: Todo list
      Input: string
      Exercises: Exercise list
      Tabs: Tab list }

type Msg =
    | GotTodos of Todo list
    | SetInput of string
    | AddTodo
    | AddedTodo of Todo
    | TabClicked of Tab
    | ExoClicked of Tab * Exercise

let todosApi =
    Remoting.createApi ()
    |> Remoting.withRouteBuilder Route.builder
    |> Remoting.buildProxy<ITodosApi>

let init (): Model * Cmd<Msg> =

    let cordeASatuer =
        [ { Name = "3x1 1x2 3x5"
            isSelected = false }
          { Name = "3 x 5 min"
            isSelected = false }
          { Name = "1 min"; isSelected = false }
          { Name = "2 min"; isSelected = false } ]

    let pompes =
        [ { Name = "10 pompes"
            isSelected = false }
          { Name = "20 pompes"
            isSelected = false }
          { Name = "20 pompes lestees 10kgs"
            isSelected = false }
          { Name = "20 pompes jambes suspension"
            isSelected = false } ]

    let dips =
        [ { Name = "10 dips"; isSelected = false }
          { Name = "20 dips lestees 10kgs"
            isSelected = false } ]

    let tractions =
        [ { Name = "6 tractions"
            isSelected = false }
          { Name = "3 lst 10kgs - 3"
            isSelected = false } ]

    let abs =
        [ { Name = "(5 stomach vaccumms - 5 abwheels) x 5"
            isSelected = false } ]

    let model =
        { Todos = []
          Input = ""
          Exercises = []
          Tabs =
              [ { Name = "Corde a sauter"
                  isSelected = true
                  Exercises = cordeASatuer }
                { Name = "Pompes"
                  isSelected = false
                  Exercises = pompes }
                { Name = "Dips"
                  isSelected = false
                  Exercises = dips }
                { Name = "Tractions"
                  isSelected = false
                  Exercises = tractions }
                { Name = "Abdominaux"
                  isSelected = false
                  Exercises = abs } ] }

    let cmd =
        Cmd.OfAsync.perform todosApi.getTodos () GotTodos

    model, cmd

let update (msg: Msg) (model: Model): Model * Cmd<Msg> =
    match msg with
    | GotTodos todos -> { model with Todos = todos }, Cmd.none
    | SetInput value -> { model with Input = value }, Cmd.none
    | AddTodo ->
        let todo = Todo.create model.Input

        let cmd =
            Cmd.OfAsync.perform todosApi.addTodo todo AddedTodo

        { model with Input = "" }, cmd
    | AddedTodo todo ->
        { model with
              Todos = model.Todos @ [ todo ] },
        Cmd.none

    | TabClicked tab ->
        let clickedTab = { tab with isSelected = true }

        let newTabs =
            model.Tabs
            |> List.map (fun tab -> { tab with isSelected = false })
            |> List.map (fun tab -> if tab.Name = clickedTab.Name then clickedTab else tab)

        ({ model with Tabs = newTabs }, Cmd.none)


    | ExoClicked (tab, exo) ->
        let newExo = {exo with isSelected = not exo.isSelected}
        let newExercices = tab.Exercises |> List.map (fun e -> if e.Name = newExo.Name then newExo else e)
        let clickedTab = { tab with Exercises = newExercices }

        let newTabs =
            model.Tabs
            |> List.map (fun tab -> if tab.Name = clickedTab.Name then clickedTab else tab)

        ({ model with Tabs = newTabs }, Cmd.none)


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
        selectedTab.Exercises
        |> List.map
            (fun exo ->
                Panel.checkbox [Panel.Block.Option.Props [ OnClick(fun _ -> ExoClicked (selectedTab, exo)  |> dispatch) ]] [
                    input [ Type "checkbox"; Checked exo.isSelected ]
                    str exo.Name
                ])

    Field.div [ Field.Option.CustomClass "exercises-panel" ] tabContent

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
                                Button.IsFullWidth ] [
                    str "Ajouter"
                ]
            ]
            Panel.panel [] [
                Panel.heading [] [
                    str "Séance du jour"
                ]
                Panel.Block.div [ Panel.Block.IsActive true ] [
                    Panel.icon [] [
                        i [ ClassName "fa fa-book" ] []
                    ]
                    str "Programme 3x1 1x2 3x5"
                ]
                Panel.Block.a [] [
                    Panel.icon [] [
                        i [ ClassName "fas fa-code-branch" ] []
                    ]
                    str "Fable"
                ]
                Panel.Block.label [] [
                    input [ Type "checkbox" ]
                    str "Remember me"
                ]
                Panel.checkbox [] [
                    input [ Type "checkbox" ]
                    str "I am a checkbox"
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
