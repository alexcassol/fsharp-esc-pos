namespace Printer

open CommonLibrary.Lists
open CommonLibrary.Conversions 

type DocumentDefinition = { 
    ColsNormal: int
    ColsCondensed: int
    ColsExpanded: int 
    }    

type Document(docDef:DocumentDefinition) =  
     
    let doc = SimpleQueue<byte array>()
  
    let appendBytes b = doc.Enqueue b
 
    let appendString (s:string) =         
        s + "\n"
        |> ToByte
        |> appendBytes

    member this.AppendWithouLf (s:string) =
        s
        |> ToByte
        |> appendBytes
 
    member this.Append o = 
        match box o with
        | :? string as output -> appendString output
        | _ -> appendBytes (TryCast<byte array>(o)).Value

    member this.NewLine = 
        this.NewLines 1

    member this.NewLines count = 
        String.replicate count "\r"
        |> this.AppendWithouLf
    
    member this.Clear = doc.Clear()

    member this.Get with get() = doc

 