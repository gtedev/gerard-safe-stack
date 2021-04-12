namespace Shared

open System

module Route =
    let builder (typeName: string) methodName =

        let apiName = typeName.Replace("Api", "")

        sprintf "/api/%s/%s" apiName methodName

type WorkoutApi =
    { getWorkouts: unit -> Async<WorkoutItem list> }