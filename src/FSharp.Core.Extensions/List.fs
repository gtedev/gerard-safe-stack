namespace FSharp.Core.Extensions

[<RequireQualifiedAccess>]
module List =

    /// <summary>Removes an element from the List at index. If index is not found, same list is returned.</summary>
    /// <param name="index">index of the item to remove.</param>
    /// <param name="xs">The list to remove the item.</param>
    /// <returns>A list without wihout the item.</returns>
    let tryRemoveAt (index:int) (xs: 'a list) =
           xs
           |> List.mapi (fun index item -> (index, item))
           |> List.filter (fun (i, _) -> index <> i)
           |> List.map (fun (_, item) -> item)
