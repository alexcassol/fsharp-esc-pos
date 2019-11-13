namespace Printer 


type Printer(printerName:string) =
    
    member val PrinterName = printerName   

    member this.Print (doc:Document) =
        let mutable continueLooping = true

        while continueLooping do
            let item = doc.Get.TryDequeue()
            
            match item with
            | Some x -> printfn "ok %A" x
            | _ -> continueLooping <- false
 