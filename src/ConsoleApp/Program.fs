open System
open Printer
open Printer.Interfaces
 
[<EntryPoint>]
let main argv =
    printfn "Printing..."
 
    let docDef = { ColsNormal=42; ColsCondensed=56; ColsExpanded=24 }
 
    //let prn = (PrinterEscPos(docDef) :> ICommandEscPos) 
    let prn = PrinterEscPos(docDef).Commands
     
    let doc = Document(prn)
    doc.NewLine 5
    
    doc.Append "teste"
    doc.Append "taste2" 
    
    doc.Append (prn.ItalicText "teste")
    doc.Append (prn.Italic On)
    doc.Append "teste3"
    doc.Append (prn.Italic Off)

    doc.Append prn.Center
    doc.Append "texto centralizado"
    doc.Append prn.Left 
    doc.Append (prn.Separator 30)
    doc.Append prn.OpenCloseTable
    doc.Append "teste"
    doc.Append prn.LineTable
    doc.Append prn.OpenCloseTable
    

    doc.Append (prn.PaperCut Full)
 
    let p = Printer.Printer("PDF")
    
    p.Print doc
    1