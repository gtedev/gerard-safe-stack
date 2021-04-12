[<RequireQualifiedAccess>]
module WorkoutSeed

open GerardSafe.MongoDb.Database.Models


let private Program name family =
    Workout(name, WorkoutType.Program, family)

let private Exercise name family =
    Workout(name, WorkoutType.Exercise, family)

let private withExercises exercises (workout: Workout) =
    let exos =
        exercises
        |> List.map (fun exoName -> Exercise exoName workout.WorkoutFamily)

    workout.Exercises <- exos
    workout

let private serieExercises count =
    // [ 1 .. count ] |> List.map (fun i -> $"série {i}")    donet build cannot handle string interpolation...
    [ 1 .. count ]
    |> List.map (fun i -> sprintf "série %i" i)

let seedWorkout (seedInserter: Workout list -> unit) (getExistingWorkout: unit -> Workout list) =

    let cordeASauter =
        [ Program "3x1 1x2 3x5" WorkoutFamily.CordeASauter
          |> withExercises [ "1 min"
                             "1 min"
                             "1 min"
                             "2 min"
                             "5 min"
                             "5 min"
                             "5 min" ]
          Exercise "5 min" WorkoutFamily.CordeASauter
          Exercise "2 min" WorkoutFamily.CordeASauter
          Exercise "1 min" WorkoutFamily.CordeASauter ]

    let pompes =
        [ Program "10 x 20 pompes lestées 10kgs" WorkoutFamily.Pompes
          |> withExercises (serieExercises 10)
          Exercise "10 pompes" WorkoutFamily.Pompes
          Exercise "20 pompes" WorkoutFamily.Pompes ]

    let dips =
        [ Program "10 x (10 dips lestées 10kgs)" WorkoutFamily.Dips
          |> withExercises (serieExercises 10)
          Exercise "10 dips" WorkoutFamily.Dips
          Exercise "20 dips" WorkoutFamily.Dips ]

    let tractions =
        [ Program "9 x (3 lestées 10kgs - 3)" WorkoutFamily.Tractions
          |> withExercises (serieExercises 9)
          Exercise "6 tractions" WorkoutFamily.Tractions ]

    let abs =
        [ Program "3 x (5 stomach vaccumms - 5 abwheels)" WorkoutFamily.Abs
          |> withExercises (serieExercises 3) ]

    let courses = [ Exercise "5 km" WorkoutFamily.CourseAPied; Exercise "10 km" WorkoutFamily.CourseAPied ]

    let workouts =
        cordeASauter
        @ pompes @ dips @ tractions @ abs @ courses

    let existingWorkout = getExistingWorkout ()

    let workoutToInsert =
        workouts
        |> List.filter (fun w -> not (List.contains w existingWorkout))

    seedInserter workoutToInsert
