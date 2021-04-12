module Types

open System
open Shared

type ItemPosition = int


type SelectableItem<'a> = { Item: 'a; isSelected: bool }

type Tab =
    { Name: string
      isSelected: bool
      WorkoutItems: SelectableItem<WorkoutItem> list }

type Model =
    { Tabs: Tab list
      WorkoutOfDay: SelectableItem<WorkoutItem> list
      WorkoutDate: DateTime }

type Msg =
    | GotWorkouts of WorkoutItem list
    | TabClicked of Tab
    | WorkoutClicked of Tab * SelectableItem<WorkoutItem>
    | AddWorkoutItemsClicked
    | RemoveWorkoutOfDay of ItemPosition * SelectableItem<WorkoutItem>
    | OnWorkoutDateChanged of DateTime
