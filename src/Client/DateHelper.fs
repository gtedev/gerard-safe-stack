[<AutoOpen>]
module DateHelper


open System


let today() = 
  DateTime.Now

let toDateTime dateString = 
  DateTime.Parse(dateString)