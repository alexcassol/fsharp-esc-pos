namespace CommonLibrary
open Microsoft.FSharp.Reflection
open System

module Conversions =
    (*
    let bytesToHex : byte array -> string =
        fun bytes -> bytes |> Array.fold (fun a x -> a + (byteToHex x)) ""
        *)

    let ToByteArray arr = 
        seq { for x in [0..(Array.length arr) - 1] do yield  byte arr.[x] }
        |> Seq.toArray 

    let ToByte : string -> byte array = 
        fun s -> System.Text.Encoding.ASCII.GetBytes(s) 

    let TryCast<'a> o =
        match box o with
        | :? 'a as output -> Some output
        | _ -> None
  

    let TupleToByteArray tup =
        tup
        |> List.map ( fun m -> Byte.Parse(m))
        |> List.toArray
  