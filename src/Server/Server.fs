module Server

open Fable.Remoting.Server
open Fable.Remoting.Giraffe
open Saturn

open Shared
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.DependencyInjection
open GerardSafe.MongoDb.Database.DependencyInjection
open GerardSafe.MongoDb.Database

let createWorkoutApi (mongoDbContext: IMongoDBContext): WorkoutApi =
    { getWorkouts =
          fun () ->
              async {

                  let workouts = mongoDbContext.GetWorkouts()

                  let result =
                      workouts
                      |> Seq.map (fun record -> { Name = record.Name })
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
