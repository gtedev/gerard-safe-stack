[<AutoOpen>]
module Helper

open Types
open Shared

let getWorkoutName w =
    match w with
    | Exercise exo -> exo.Name
    | Program prgm -> prgm.Name

let getWorkoutNameFamily w =
    match w with
    | Exercise exo -> exo.WorkoutFamily
    | Program prgm -> prgm.WorkoutFamily

let toSelectableWorkoutItems (grp:string * WorkoutItem list) =
    snd grp
    |> List.map (fun w -> { isSelected = false; Item = w } )
