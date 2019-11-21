namespace Printer 

open CommonLibrary.SysOp
open CommonLibrary.Conversions

type Printer(printerName:string) =

    let printLinux (doc:Document) =
        let b = 
            doc.Get.GetAll()
            |> Seq.concat
            |> Array.ofSeq

        PrinterCupsHelper.Printer.SendBytesToPrinter (printerName, b)
    (*
    let sendToPrinter (b:byte array) =
        match getOS with
        | Linux -> printLinux b
        //| Windows -> printWindows b
        | _ -> failwith "not supported"

 
    let rec printWindows (doc:Document)= 
        match doc.Get.TryDequeue() with
        | Some x -> 
            sendToPrinter x |> ignore

            printWindows doc
        | _ -> ()
        *)
    member val PrinterName = printerName   
        
    member this.Print (doc:Document) : unit=
        match getOS with
        | Linux -> printLinux doc 
        //| Windows -> printWindows doc  
        | _ -> failwith "not supported"