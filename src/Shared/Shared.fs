namespace Shared

open System

type Todo =
    { Id : Guid
      Description : string }

type Workout = { Name: string }

module Todo =
    let isValid (description: string) =
        String.IsNullOrWhiteSpace description |> not

    let create (description: string) =
        { Id = Guid.NewGuid()
          Description = description }

module Route =
    let builder (typeName:string) methodName =

        let apiName = typeName.Replace("Api","")

        sprintf "/api/%s/%s" apiName methodName

type ITodosApi =
    { getTodos : unit -> Async<Todo list>
      addTodo : Todo -> Async<Todo> }

type WorkoutApi =
    { getWorkouts: unit -> Async<Workout list> }