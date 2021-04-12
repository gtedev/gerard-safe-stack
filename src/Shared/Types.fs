namespace Shared

type Exercise = { Name: string; WorkoutFamily: string }

type Program =
    { Name: string
      Exercises: Exercise list
      WorkoutFamily: string }

type WorkoutItem =
    | Exercise of Exercise
    | Program of Program
