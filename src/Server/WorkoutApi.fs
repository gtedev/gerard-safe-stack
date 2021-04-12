module WorkoutApi

open Microsoft.AspNetCore.Http
open GerardSafe.MongoDb.Database
open GerardSafe.MongoDb.Database.Models
open Shared

let private mapExercises (exercises: Workout list): Exercise list =
    exercises
    |> Seq.toList
    |> List.map
        (fun exo ->
            { Name = exo.Name
              WorkoutFamily = exo.WorkoutFamily })


let private toWorkoutItem (w: Workout) =
    match w.Type with
    | WorkoutType.Exercise ->

        Exercise
            { Name = w.Name
              WorkoutFamily = w.WorkoutFamily }

    | WorkoutType.Program ->

        Program
            { Name = w.Name
              WorkoutFamily = w.WorkoutFamily
              Exercises = w.Exercises |> Seq.toList |> mapExercises }


let private createWorkoutApi (mongoDbContext: IMongoDBContext): WorkoutApi =
    { getWorkouts =
          fun () ->
              async {

                  let workouts = mongoDbContext.GetWorkouts()

                  let result =
                      workouts |> Seq.map toWorkoutItem |> Seq.toList

                  return result
              } }


let createWorkoutApiFromContext (httpContext: HttpContext): WorkoutApi =
    let mongoContext =
        httpContext.GetService<IMongoDBContext>()

    createWorkoutApi mongoContext
