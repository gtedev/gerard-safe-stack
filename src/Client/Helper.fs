[<AutoOpen>]
module Helper

open Types

let getWorkoutName w =
    match w with
    | Exercise exo -> exo.Name
    | Program prgm -> prgm.Name

let cordeASatuer () =
    [ { Item =
            Program
                { Name = "3x1 1x2 3x5"
                  Exercises =
                      [ { Name = "1 min" }
                        { Name = "1 min" }
                        { Name = "1 min" }
                        { Name = "2 min" }
                        { Name = "5 min" }
                        { Name = "5 min" }
                        { Name = "5 min" } ] }
        isSelected = false }
      { Item = Exercise { Name = "5 min" }
        isSelected = false }
      { Item = Exercise { Name = "2 min" }
        isSelected = false }
      { Item = Exercise { Name = "1 min" }
        isSelected = false } ]

let pompes () =
    [ { Item = Exercise { Name = "10 pompes" }
        isSelected = false }
      { Item = Exercise { Name = "20 pompes" }
        isSelected = false }
      { Item =
            Program
                { Name = "10 x 20 pompes lestées 10kgs"
                  Exercises =
                      [ { Name = "série 1" }
                        { Name = "série 2" }
                        { Name = "série 3" }
                        { Name = "série 4" }
                        { Name = "série 5" }
                        { Name = "série 6" }
                        { Name = "série 7" }
                        { Name = "série 8" }
                        { Name = "série 9" }
                        { Name = "série 10" } ] }
        isSelected = false }
      { Item = Exercise { Name = "20 pompes jambes suspension" }
        isSelected = false } ]

let dips () =
    [ { Item = Exercise { Name = "10 dips" }
        isSelected = false }
      { Item = Exercise { Name = "20 dips" }
        isSelected = false }
      { Item =
            Program
                { Name = "10 x (10 dips lestées 10kgs)"
                  Exercises =
                      [ { Name = "série 1" }
                        { Name = "série 2" }
                        { Name = "série 3" }
                        { Name = "série 4" }
                        { Name = "série 5" }
                        { Name = "série 6" }
                        { Name = "série 7" }
                        { Name = "série 8" }
                        { Name = "série 9" }
                        { Name = "série 10" } ] }
        isSelected = false } ]

let tractions () =
    [ { Item = Exercise { Name = "6 tractions" }
        isSelected = false }
      { Item =
            Program
                { Name = "9 x (3 lestées 10kgs - 3)"
                  Exercises =
                      [ { Name = "série 1" }
                        { Name = "série 2" }
                        { Name = "série 3" }
                        { Name = "série 4" }
                        { Name = "série 5" }
                        { Name = "série 6" }
                        { Name = "série 7" }
                        { Name = "série 8" }
                        { Name = "série 9" } ] }
        isSelected = false } ]

let abs () =
    [ { Item =
            Program
                { Name = "3 x (5 stomach vaccumms - 5 abwheels)"
                  Exercises =
                      [ { Name = "5 stomach vaccumms - 5 abwheels" }
                        { Name = "5 stomach vaccumms - 5 abwheels" }
                        { Name = "5 stomach vaccumms - 5 abwheels" } ] }
        isSelected = false } ]

let courses () =
    [ { Item = Exercise { Name = "5 km" }
        isSelected = false }
      { Item = Exercise { Name = "10 km" }
        isSelected = false } ]
