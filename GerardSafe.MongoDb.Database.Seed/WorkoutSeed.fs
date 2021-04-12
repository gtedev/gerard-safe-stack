module WorkoutSeed

open GerardSafe.MongoDb.Database.Models


let private Program name = Workout(name, WorkoutType.Program)

let private Exercise name = Workout(name, WorkoutType.Exercise)

let private withExercises exercises (workout: Workout) =
    let exos =
        exercises
        |> List.map (fun exoName -> Exercise exoName)

    workout.Exercises <- exos
    workout

let private serieExercises count =
    // [ 1 .. count ] |> List.map (fun i -> $"série {i}")    donet build cannot handle string interpolation...
    [ 1 .. count ]
    |> List.map (fun i -> sprintf "série %i" i)

let seedWorkout (seedInserter: Workout list -> unit) (getExistingWorkout: unit -> Workout list) =

    let cordeASauter =
        [ Program(string "3x1 1x2 3x5")
          |> withExercises [ "1 min"
                             "1 min"
                             "1 min"
                             "2 min"
                             "5 min"
                             "5 min"
                             "5 min" ]
          Exercise "5 min"
          Exercise "2 min"
          Exercise "1 min" ]

    let pompes =
        [ Program "10 x 20 pompes lestées 10kgs"
          |> withExercises (serieExercises 10)
          Exercise "10 pompes"
          Exercise "20 pompes" ]

    let dips =
        [ Program "10 x (10 dips lestées 10kgs)"
          |> withExercises (serieExercises 10)
          Exercise "10 dips"
          Exercise "20 dips" ]

    let tractions =
        [ Program "9 x (3 lestées 10kgs - 3)"
          |> withExercises (serieExercises 9)
          Exercise "6 tractions" ]

    let abs =
        [ Program "3 x (5 stomach vaccumms - 5 abwheels)"
          |> withExercises (serieExercises 3) ]

    let courses = [ Exercise "5 km"; Exercise "10 km" ]

    let workouts =
        cordeASauter
        @ pompes @ dips @ tractions @ abs @ courses

    let existingWorkout = getExistingWorkout ()

    let workoutToInsert =
        workouts
        |> List.filter (fun w -> not (List.contains w existingWorkout))

    seedInserter workoutToInsert
