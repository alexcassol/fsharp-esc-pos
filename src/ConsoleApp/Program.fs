open System
open RawPrinterHelper.Printer 
open Printer
open CommonLibrary.Conversions
open Printer.Interfaces
 
[<EntryPoint>]
let main argv =
    printfn "Printing..."
 
    let docDef = { ColsNormal=42; ColsCondensed=56; ColsExpanded=24 }
 
    //let prn = (PrinterEscPos(docDef) :> ICommandEscPos) 
    let prn = PrinterEscPos(docDef).DocLanguage
     
    let doc = Document(prn)
    doc.Append "teste"
    doc.Append "teste2" 
     
    doc.Append (prn.ItalicText "teste")
    doc.Append (prn.Italic On)
    doc.Append "teste3"
    doc.Append (prn.Italic Off)

    doc.Append prn.Center
    doc.Append "texto centralizado"
    doc.Append prn.Left

    doc.Append (prn.PaperCut Partial)
 
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