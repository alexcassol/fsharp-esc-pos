namespace Printer

open CommonLibrary.Lists
open CommonLibrary.Conversions 

type Document(?commands) =  
    
    let doc = 
        let tmp = SimpleQueue<byte array>()
 
        try 
            if commands.IsSome then
                match box commands.Value with
                | :? Interfaces.ICommandEscPos as x -> tmp.Enqueue x.InitializePrinter
                | _ -> ()
        with
        | _ -> () 
        tmp
    
    let appendBytes b = 
        doc.Enqueue b    
     
    let appendString (s:string, newLine:string) =         
        ToByte (s + "\n")
        |> appendBytes

    member this.AppendWithouLf (s:string) =
        ToByte s
        |> appendBytes
 
    member this.Append o = 
        match box o with
        | :? System.String as o -> appendString (o, "\n")
        | :? System.Array as o -> appendBytes (TryCast<byte array>(o)).Value 
        | _ -> ()

    member this.NewLine() = 
        this.NewLines 1

    member this.NewLines count = 
        String.replicate count "\r"
        |> this.AppendWithouLf
    
    member this.Clear() = 
        doc.Clear()
    
    member this.Get with get() = doc

 