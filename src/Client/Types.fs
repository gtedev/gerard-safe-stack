module Types

type Serie =
    | Serie of string
    | Program of string list

type Exercise = { Name: string; isSelected: bool }

type Tab =
    { Name: string
      isSelected: bool
      Exercises: Exercise list }

type Model =
    { Tabs: Tab list
      WorkoutOfDay: Exercise list }

type Msg =
    | TabClicked of Tab
    | ExoClicked of Tab * Exercise
    | AddExosClicked

