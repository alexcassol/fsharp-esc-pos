//The MIT License (MIT)
//Copyright (c) 2019 Alex Cassol
//
//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights to
//use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
//the Software, and to permit persons to whom the Software is furnished to do so,
//subject to the following conditions:
//
//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.
//
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
//INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
//PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
//FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
//OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
//DEALINGS IN THE SOFTWARE.
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

 