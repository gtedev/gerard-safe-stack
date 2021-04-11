namespace Shared

open System

type Workout = { Name: string }

module Route =
    let builder (typeName:string) methodName =

        let apiName = typeName.Replace("Api","")

        sprintf "/api/%s/%s" apiName methodName

type WorkoutApi =
    { getWorkouts: unit -> Async<Workout list> }