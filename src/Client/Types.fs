module Types

open System
open Shared

type ItemPosition = int

type BodyAreaType =
    | CordeASauter = 0
    | Pompes = 1
    | Dips = 2
    | Tractions = 3
    | Abdominaux = 4
    | CourseAPied = 5

type Exercise = { Name: string }

type Program =
    { Name: string
      Exercises: Exercise list }

type WorkoutItem =
    | Exercise of Exercise
    | Program of Program

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
    | GotWorkouts of Workout list
    | TabClicked of Tab
    | WorkoutClicked of Tab * SelectableItem<WorkoutItem>
    | AddWorkoutItemsClicked
    | RemoveWorkoutOfDay of ItemPosition * SelectableItem<WorkoutItem>
    | OnWorkoutDateChanged of DateTime
