module Server

open Fable.Remoting.Server
open Fable.Remoting.Giraffe
open Saturn

open Shared
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.DependencyInjection
open GerardSafe.MongoDb.Database.DependencyInjection
open GerardSafe.MongoDb.Database
open GerardSafe.MongoDb.Database.Models

let mapExercises (exercises: Workout list): Exercise list =
    exercises
    |> Seq.toList
    |> List.map
        (fun exo ->
            { Name = exo.Name
              WorkoutFamily = exo.WorkoutFamily })

let toWorkoutItem (w: Workout) =
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


let createWorkoutApi (mongoDbContext: IMongoDBContext): WorkoutApi =
    { getWorkouts =
          fun () ->
              async {

                  let workouts = mongoDbContext.GetWorkouts()

                  let result =
                      workouts
                      |> Seq.map toWorkoutItem
                      |> Seq.toList

                  return result
              } }

let createWorkoutApiFromContext (httpContext: HttpContext): WorkoutApi =
    let mongoContext =
        httpContext.GetService<IMongoDBContext>()

    createWorkoutApi mongoContext

let webApp =
    Remoting.createApi ()
    |> Remoting.withRouteBuilder Route.builder
    |> Remoting.fromContext createWorkoutApiFromContext
    |> Remoting.buildHttpHandler

let configureServices (services: IServiceCollection) = services.AddMongoDbDatabase()

let app =
    application {
        url "http://0.0.0.0:8085"
        use_router webApp
        memory_cache
        use_static "public"
        use_gzip
        service_config configureServices
    }

run app
