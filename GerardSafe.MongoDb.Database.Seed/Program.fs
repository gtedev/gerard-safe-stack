// Learn more about F# at http://fsharp.org

open GerardSafe.MongoDb.Database
open System

[<EntryPoint>]
let main argv =
    printfn "Gerard Safe Database Seed"

    let dbContext = MongoDBContextFactory.CreateContext()
    printfn "1 - Seed Mongo Db Database"
    printfn "2 - Quit"

    let line = Console.ReadLine()

    if line = "1"
    then WorkoutSeed.seedWorkout (dbContext.InsertWorkouts) (fun () -> List.ofSeq (dbContext.GetWorkouts()))

    0 // return an integer exit code
