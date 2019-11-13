open System
open RawPrinterHelper.Printer 
open Printer
open CommonLibrary.Conversions
open Printer.Interfaces
 
[<EntryPoint>]
let main argv =
    printfn "Printing..."
 
    let docDef = { ColsNormal=42; ColsCondensed=32; ColsExpanded=12 }
 
    let cmd = (PrinterEscPos() :> ICommandEscPos) 
     
    let doc = Printer.Document(docDef)
    doc.Append "teste"
    doc.Append "teste2" 
     
    doc.Append (cmd.ItalicText "teste")
    doc.Append (cmd.Italic On)
    doc.Append "teste3"
    doc.Append (cmd.Italic Off)

    doc.Append cmd.Center
    doc.Append "texto centralizado"
    doc.Append cmd.Left

    doc.Append (cmd.PaperCut Partial)
 
    let p = Printer.Printer("PDF")
    
    p.Print doc
   // p.Print doc2

    
    

    //SendStringToPrinter("PDF", "teste de impressao") |> ignore

    //gameLoop
    //|> ignore

    //let proc = 
    //    for x in argv do
    //        SendStringToPrinter("PDF", x) |> ignore

    1