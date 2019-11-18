namespace Printer

open CommonLibrary.Lists
open CommonLibrary.Conversions 

type Document(?printerLanguage) =  
    
    let doc = 
        let tmp = SimpleQueue<byte array>()
 
        try 
            if printerLanguage.IsSome then
                match box printerLanguage.Value with
                | :? Interfaces.ICommandEscPos as x -> tmp.Enqueue (x.InitializePrinter())
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
        | :? string as output -> appendString (output, "\n")
        | _ -> appendBytes (TryCast<byte array>(o)).Value 

    member this.NewLine() = 
        this.NewLines 1

    member this.NewLines count = 
        String.replicate count "\r"
        |> this.AppendWithouLf
    
    member this.Clear() = 
        doc.Clear()
    
    member this.Get with get() = doc

 