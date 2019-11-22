namespace Printer

open CommonLibrary.Lists
open CommonLibrary.Conversions 

type Document(?commands) =  
    
    let doc = SimpleQueue<byte array>()
    
    do  
        try 
            if commands.IsSome then
                match box commands.Value with
                | :? Interfaces.ICommandEscPos as x -> doc.Enqueue x.InitializePrinter
                | _ -> ()
        with
        | _ -> failwith "Unknown command" 
    
    let appendBytes b = 
        doc.Enqueue b    
     
    let appendString s =
        ToByte (s + "\n")
        |> appendBytes

    member this.AppendWithoutLf s =
        ToByte s
        |> appendBytes
 
    member this.Append o = 
        match box o with
        | :? System.String as o -> appendString o
        | :? System.Array as o -> appendBytes (TryCast<byte array>(o)).Value 
        | _ -> ()

    member this.NewLine ?count =
        let nTimes = 
            match count with
            | Some i -> i
            | _ -> 1                
        String.replicate nTimes "\r"
        |> this.AppendWithoutLf
    
    member this.Clear() = 
        doc.Clear()
    
    member this.Get with get() = doc

 