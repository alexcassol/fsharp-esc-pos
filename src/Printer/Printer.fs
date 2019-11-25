namespace Printer 

open System
open CommonLibrary.SysOp
open CommonLibrary.Conversions

type Printer(printerName:string) =

    let printLinux b = 
        PrinterCupsHelper.Printer.SendBytesToPrinter (printerName, b) |> ignore
    
    let printWindows b =        
        RawPrinterHelper.Printer.SendBytesToPrinter (printerName, b) |> ignore
  
    member this.Print (doc:Document) : unit=
        let b =
            doc.Get.GetAll()
            |> Seq.concat
            |> Array.ofSeq
            
        match getOS with
        | Linux -> printLinux b 
        | Windows -> printWindows b  
        | _ -> failwith "not supported"